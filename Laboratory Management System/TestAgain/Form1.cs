using System;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace TestAgain
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private const string serverIp = "192.168.1.12";
        private const int port = 5900;
        private PictureBox pictureBox1;
        private bool isRunning = true;

        public Form1()
        {
            InitializeComponent();
            InitializePictureBox();
        }

        private void InitializePictureBox()
        {
            pictureBox1 = new PictureBox();
            pictureBox1.Location = new Point(10, 10);
            pictureBox1.Size = new Size(800, 600); // Adjust size as needed
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(pictureBox1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                client = new TcpClient(serverIp, port);
                stream = client.GetStream();

                // Perform VNC handshake and protocol implementation
                PerformVncHandshake();

                // Start receiving and displaying screen updates
                ThreadPool.QueueUserWorkItem(new WaitCallback(ReceiveAndDisplayScreen));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to VNC server: " + ex.Message);
                CloseConnection();
            }
        }

        private void PerformVncHandshake()
        {
            // Send client initialization message
            byte[] clientInit = { 0x52, 0x46, 0x42, 0x20, 0x30, 0x30, 0x30, 0x31 }; // "RFB 003.003\n"
            stream.Write(clientInit, 0, clientInit.Length);

            // Receive and validate server's response (optional for basic implementation)
            byte[] serverInit = new byte[12];
            int bytesRead = stream.Read(serverInit, 0, serverInit.Length);
            if (bytesRead < serverInit.Length)
            {
                throw new Exception("Invalid server response.");
            }
            // Optionally validate serverInit here if needed
        }

        private void ReceiveAndDisplayScreen(object state)
        {
            try
            {
                while (isRunning)
                {
                    byte[] header = new byte[4];
                    int bytesRead = stream.Read(header, 0, 4);

                    if (bytesRead < 4)
                    {
                        throw new Exception("Invalid header received.");
                    }

                    int messageType = header[1];
                    int payloadLength = (header[2] << 8) | header[3]; // Corrected payload length parsing

                    byte[] buffer = new byte[payloadLength];
                    bytesRead = 0;

                    while (bytesRead < payloadLength)
                    {
                        int read = stream.Read(buffer, bytesRead, payloadLength - bytesRead);
                        if (read <= 0)
                        {
                            throw new Exception("Failed to read payload data.");
                        }
                        bytesRead += read;
                    }

                    using (MemoryStream ms = new MemoryStream(buffer))
                    {
                        ms.Position = 0; // Reset stream position
                        Bitmap bitmap = new Bitmap(ms);

                        // Update PictureBox on UI thread
                        pictureBox1.Invoke((MethodInvoker)delegate
                        {
                            pictureBox1.Image = bitmap;
                            pictureBox1.Invalidate(); // Ensure redraw
                        });
                    }
                }
            }
            catch (IOException ex)
            {
                // Handle IO exceptions (e.g., connection closed)
                Console.WriteLine("IOException: " + ex.Message);
                MessageBox.Show("Connection closed by server.");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine("Exception: " + ex.Message);
                MessageBox.Show("Error receiving screen updates: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }



        private void CloseConnection()
        {
            isRunning = false;

            if (stream != null)
            {
                stream.Close();
            }

            if (client != null)
            {
                client.Close();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            CloseConnection();
            base.OnFormClosing(e);
        }
    }
}
