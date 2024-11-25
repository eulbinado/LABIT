using System;
using System.Net.Sockets;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

public class VncClientForm : Form
{
    private const string ServerIp = "192.168.1.12";
    private const int Port = 5900;

    private TcpClient client;
    private NetworkStream stream;
    private Thread receiveThread;

    private PictureBox pictureBox;

    public VncClientForm()
    {
        InitializeComponents();
        ConnectToServer();
    }

    private void InitializeComponents()
    {
        this.Text = "VNC Client";
        this.Size = new Size(260, 160); // Set form size
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.pictureBox = new PictureBox();
        this.pictureBox.Dock = DockStyle.Fill;
        this.Controls.Add(this.pictureBox);
    }

    private void ConnectToServer()
    {
        try
        {
            client = new TcpClient(ServerIp, Port);
            stream = client.GetStream();
            receiveThread = new Thread(new ThreadStart(ReceiveFramebufferUpdates));
            receiveThread.Start();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error connecting to VNC server: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Close();
        }
    }

    private void ReceiveFramebufferUpdates()
    {
        try
        {
            while (client.Connected)
            {
                byte[] header = new byte[12];
                stream.Read(header, 0, 12);

                int messageType = header[1]; // Message type: 0 - FramebufferUpdate

                int x = (header[4] << 8) | header[5];
                int y = (header[6] << 8) | header[7];
                int width = (header[8] << 8) | header[9];
                int height = (header[10] << 8) | header[11];

                if (width > 0 && height > 0)
                {
                    byte[] pixelData = new byte[width * height * 4];
                    stream.Read(pixelData, 0, pixelData.Length);

                    Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppRgb);
                    Rectangle rect = new Rectangle(0, 0, width, height);
                    BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, bitmap.PixelFormat);
                    System.Runtime.InteropServices.Marshal.Copy(pixelData, 0, bmpData.Scan0, pixelData.Length);
                    bitmap.UnlockBits(bmpData);

                    SetPictureBoxImage(bitmap);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error receiving framebuffer updates: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Close();
        }
    }

    private void SetPictureBoxImage(Bitmap bitmap)
    {
        if (pictureBox.InvokeRequired)
        {
            pictureBox.Invoke((MethodInvoker)delegate {
                SetPictureBoxImage(bitmap);
            });
        }
        else
        {
            pictureBox.Image = bitmap;
        }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        try
        {
            if (receiveThread != null && receiveThread.IsAlive)
                receiveThread.Abort();

            if (client != null && client.Connected)
                client.Close();

            base.OnFormClosing(e);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error closing VNC client: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new VncClientForm());
    }
}
