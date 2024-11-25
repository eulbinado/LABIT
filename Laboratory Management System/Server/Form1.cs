using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        private TcpListener _listener;
        private const int Port = 12345;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartServer();
        }

        private void StartServer()
        {
            _listener = new TcpListener(IPAddress.Any, Port);
            _listener.Start();
            MessageBox.Show("Server started on port " + Port);

            var serverThread = new Thread(() =>
            {
                while (true)
                {
                    var client = _listener.AcceptTcpClient();
                    var clientThread = new Thread(() => HandleClient(client));
                    clientThread.Start();
                }
            });
            serverThread.Start();
        }

        private void HandleClient(TcpClient client)
        {
            using (var stream = client.GetStream())
            {
                while (true)
                {
                    try
                    {
                        using (var bitmap = CaptureScreen())
                        using (var ms = new MemoryStream())
                        {
                            bitmap.Save(ms, ImageFormat.Jpeg);
                            var imageBytes = ms.ToArray();

                            // Send image length
                            var lengthBytes = BitConverter.GetBytes(imageBytes.Length);
                            stream.Write(lengthBytes, 0, lengthBytes.Length);

                            // Send image data
                            stream.Write(imageBytes, 0, imageBytes.Length);
                        }

                        // Adjust capture rate (e.g., every 100 ms)
                        Thread.Sleep(100);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                        break;
                    }
                }
            }
        }

        private Bitmap CaptureScreen()
        {
            var bounds = Screen.PrimaryScreen.Bounds;
            var bitmap = new Bitmap(bounds.Width, bounds.Height);

            using (var g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size);

                // Draw mouse cursor
                var cursorPosition = Cursor.Position;
                var cursorIcon = Cursors.Default; // You can customize this if needed
                var cursorBounds = cursorIcon.HotSpot;

                // Adjust cursor position to screen coordinate
                var cursorScreenPosition = new Point(cursorPosition.X - cursorBounds.X, cursorPosition.Y - cursorBounds.Y);

                // Draw the cursor
                cursorIcon.Draw(g, new Rectangle(cursorScreenPosition, cursorIcon.Size));
            }

            return bitmap;
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _listener?.Stop();
        }
    }
}
