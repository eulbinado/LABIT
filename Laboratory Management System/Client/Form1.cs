using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private const string ServerIp = "DESKTOP-4G0KIIP"; 
        private const int Port = 12345;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConnectToServer();
        }

        private void ConnectToServer()
        {
            try
            {
                client = new TcpClient(ServerIp, Port);
                stream = client.GetStream();

                var receiveThread = new Thread(ReceiveImages);
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection error: " + ex.Message);
            }
        }

        private void ReceiveImages()
        {
            while (true)
            {
                try
                {
                    // Read image length
                    var lengthBytes = new byte[4];
                    stream.Read(lengthBytes, 0, lengthBytes.Length);
                    var imageLength = BitConverter.ToInt32(lengthBytes, 0);

                    // Read image data
                    var imageBytes = new byte[imageLength];
                    stream.Read(imageBytes, 0, imageBytes.Length);

                    using (var ms = new MemoryStream(imageBytes))
                    {
                        var bitmap = new Bitmap(ms);
                        if (InvokeRequired)
                        {
                            Invoke(new Action(() => DisplayImage(bitmap)));
                        }
                        else
                        {
                            DisplayImage(bitmap);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error receiving image: " + ex.Message);
                    break;
                }
            }
        }


        private void DisplayImage(Bitmap bitmap)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
            }

            // Resize the image to fit the PictureBox
            var resizedBitmap = ResizeImage(bitmap, pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = resizedBitmap;
        }

        private Bitmap ResizeImage(Bitmap originalImage, int width, int height)
        {
            var ratioX = (double)width / originalImage.Width;
            var ratioY = (double)height / originalImage.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(originalImage.Width * ratio);
            var newHeight = (int)(originalImage.Height * ratio);

            var newBitmap = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(newBitmap))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(originalImage, 0, 0, newWidth, newHeight);
            }

            return newBitmap;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            client?.Close();
        }
    }
}
