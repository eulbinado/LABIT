using System;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratory_Management_System
{
    public partial class Fullscreen : Form
    {
        private string hostname;
        private const int Port = 12345;
        private TcpClient client;
        private NetworkStream stream;
        private Timer statusCheckTimer;

        public Fullscreen()
        {
            InitializeComponent();
            //this.WindowState = FormWindowState.Maximized;
            this.KeyDown += Fullscreen_KeyDown;
            InitializeStatusCheckTimer();
        }

        public void SetHostname(string hostname)
        {
            this.hostname = hostname;
            this.Text = "Workkstation - " + hostname;
            DisplayContent();
        }

        private async void DisplayContent()
        {
            try
            {
                client = new TcpClient();
                await client.ConnectAsync(hostname, Port);
                stream = client.GetStream();
                statusCheckTimer.Start(); 
                await ReceiveImagesAsync();
            }
            catch (Exception)
            {
                Invoke(new Action(() => DisplayOfflineImage(pictureBox1)));
            }
        }

        private async Task ReceiveImagesAsync()
        {
            try
            {
                while (true)
                {
                    var lengthBytes = new byte[4];
                    int bytesRead = await stream.ReadAsync(lengthBytes, 0, lengthBytes.Length);
                    if (bytesRead < lengthBytes.Length) break;

                    var imageLength = BitConverter.ToInt32(lengthBytes, 0);
                    var imageBytes = new byte[imageLength];

                    bytesRead = 0;
                    while (bytesRead < imageLength)
                    {
                        int read = await stream.ReadAsync(imageBytes, bytesRead, imageLength - bytesRead);
                        if (read == 0) break;
                        bytesRead += read;
                    }

                    using (var ms = new MemoryStream(imageBytes))
                    {
                        if (ms.Length > 0)
                        {
                            var bitmap = new Bitmap(ms);
                            if (this.IsHandleCreated)
                            {
                                Invoke(new Action(() => DisplayImage(bitmap)));
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                if (this.IsHandleCreated)
                {
                    Invoke(new Action(() => DisplayOfflineImage(pictureBox1)));
                }
            }
        }

        private void DisplayImage(Bitmap bitmap)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
            }

            var resizedBitmap = new Bitmap(bitmap, new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));
            pictureBox1.Image = resizedBitmap;
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void DisplayOfflineImage(PictureBox pictureBox)
        {
            Bitmap offlineImage = new Bitmap(pictureBox.Width, pictureBox.Height);
            using (Graphics g = Graphics.FromImage(offlineImage))
            {
                g.Clear(Color.Gray);

                string text = "Offline";
                Font font = new Font("Montserrat", 25, FontStyle.Regular);
                SizeF textSize = g.MeasureString(text, font);

                PointF textPosition = new PointF(
                    (pictureBox.Width - textSize.Width) / 2,
                    (pictureBox.Height - textSize.Height) / 2
                );

                g.DrawString(text, font, Brushes.White, textPosition);
            }
            pictureBox.Image = offlineImage;
        }

        private void InitializeStatusCheckTimer()
        {
            statusCheckTimer = new Timer { Interval = 5000 };
            statusCheckTimer.Tick += async (sender, args) => await CheckConnectionStatus();
        }

        private async Task CheckConnectionStatus()
        {
            if (client == null || !client.Connected)
            {
                Invoke(new Action(() => DisplayOfflineImage(pictureBox1)));
                statusCheckTimer.Stop();
                return;
            }

            try
            {
                var pingMessage = new byte[1] { 1 }; 
                await stream.WriteAsync(pingMessage, 0, pingMessage.Length);
            }
            catch (Exception)
            {
                Invoke(new Action(() => DisplayOfflineImage(pictureBox1)));
                statusCheckTimer.Stop();
            }
        }

        private void Fullscreen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void Fullscreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            statusCheckTimer?.Stop();

            if (stream != null)
            {
                stream.Close();
            }

            if (client != null)
            {
                client.Close();
            }
        }
    }
}
