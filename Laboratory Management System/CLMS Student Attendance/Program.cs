using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace CLMS_Student_Attendance
{
    internal static class Program
    {
        private static TcpListener listener;
        private static TcpListener fileListener;
        private const int Port = 12345;
        private const int FilePort = 12346;
        private static CancellationTokenSource cancellationTokenSource;
        private static ScreenLock lockForm;
        private const string SaveFolder = @"C:\ReceivedFiles\";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            StartServer();
            StartFileReceiver();

            Application.Run(new StudentAttendance());

            StopServer();
            StopFileReceiver();
        }

        private static void StartServer()
        {
            listener = new TcpListener(IPAddress.Any, Port);
            listener.Start();
            cancellationTokenSource = new CancellationTokenSource();

            var serverThread = new Thread(() =>
            {
                while (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    try
                    {
                        var client = listener.AcceptTcpClient();
                        var clientThread = new Thread(() => HandleClient(client));
                        clientThread.Start();
                    }
                    catch (SocketException)
                    {
                        // Handle socket exception if needed
                    }
                }
            });
            serverThread.Start();
        }

        private static void StartFileReceiver()
        {
            fileListener = new TcpListener(IPAddress.Any, FilePort);
            fileListener.Start();

            Directory.CreateDirectory(SaveFolder);

            var fileThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        var client = fileListener.AcceptTcpClient();
                        var fileClientThread = new Thread(() => HandleFileClient(client));
                        fileClientThread.Start();
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
            });
            fileThread.Start();
        }


        private static void StopFileReceiver()
        {
            fileListener?.Stop();
        }

        private static void HandleFileClient(TcpClient client)
        {
            Console.WriteLine("Client connected for file transfer.");
            try
            {
                using (var networkStream = client.GetStream())
                {
                    // Step 1: Read the file name length
                    byte[] fileNameLengthBytes = new byte[4];
                    int bytesRead = networkStream.Read(fileNameLengthBytes, 0, fileNameLengthBytes.Length);
                    if (bytesRead != 4)
                    {
                        Console.WriteLine("Error: Unable to read the file name length.");
                        return;
                    }

                    int fileNameLength = BitConverter.ToInt32(fileNameLengthBytes, 0);
                    Console.WriteLine($"File name length: {fileNameLength} bytes");

                    // Step 2: Read the file name based on the length received
                    byte[] fileNameBytes = new byte[fileNameLength];
                    bytesRead = networkStream.Read(fileNameBytes, 0, fileNameBytes.Length);
                    if (bytesRead != fileNameLength)
                    {
                        Console.WriteLine("Error: File name length mismatch.");
                        return;
                    }

                    string fileName = Encoding.UTF8.GetString(fileNameBytes);
                    Console.WriteLine($"File name received: {fileName}");

                    // Step 3: Create the full path and prepare to receive file content
                    string filePath = Path.Combine(SaveFolder, fileName);
                    Console.WriteLine($"Saving file to: {filePath}");

                    // Step 4: Write the file content to the file
                    using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        byte[] buffer = new byte[1024]; // Buffer for reading data in chunks
                        int chunkSize;
                        int totalBytes = 0;

                        // Read until end of stream
                        while ((chunkSize = networkStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            fileStream.Write(buffer, 0, chunkSize);
                            totalBytes += chunkSize;
                            Console.WriteLine($"Received {chunkSize} bytes, total {totalBytes} bytes written.");
                        }
                    }

                    Console.WriteLine("File successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while handling file client: " + ex.Message);
            }
            finally
            {
                client.Close();
            }
        }


        private static void StopServer()
        {
            cancellationTokenSource.Cancel();
            listener?.Stop();
        }

        private static void HandleClient(TcpClient client)
        {
            try
            {
                using (var stream = client.GetStream())
                {
                    var commandThread = new Thread(() => ListenForCommands(stream));
                    commandThread.Start();

                    while (true)
                    {
                        try
                        {
                            SendScreenCapture(stream);
                        }
                        catch (IOException)
                        {
                            break;
                        }
                        catch (ObjectDisposedException)
                        {
                            break;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                            break;
                        }
                    }
                }
            }
            finally
            {
                client.Close();
            }
        }

        private static void ListenForCommands(NetworkStream stream)
        {
            while (true)
            {
                try
                {
                    int messageType = stream.ReadByte();
                    if (messageType == -1) break;

                    byte[] lengthBytes = new byte[4];
                    if (stream.Read(lengthBytes, 0, lengthBytes.Length) != lengthBytes.Length) break;

                    int dataLength = BitConverter.ToInt32(lengthBytes, 0);
                    if (dataLength <= 0) break;

                    byte[] dataBytes = new byte[dataLength];
                    if (stream.Read(dataBytes, 0, dataLength) != dataLength) break;

                    string data = Encoding.UTF8.GetString(dataBytes);

                    if (messageType == 1) // COMMAND type
                    {
                        ProcessCommand(data);
                    }
                    else if (messageType == 0) // MESSAGE type
                    {
                        ShowMessage(data);
                    }
                    else
                    {
                        Console.WriteLine("Unknown message type received.");
                    }
                }
                catch (IOException)
                {
                    break;
                }
                catch (ObjectDisposedException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    break;
                }
            }
        }

        private static void ProcessCommand(string command)
        {
            if (command == "LOCK")
            {
                ShowScreenLockForm();
            }
            else if (command == "UNLOCK")
            {
                CloseScreenLockForm();
            }
            else if (command == "SHUTDOWN")
            {
                ShutdownComputer();
            }
            else if (command == "RESTART")
            {
                RestartComputer();
            }
            else
            {
                Console.WriteLine("Received command: " + command);
            }
        }

        private static void ShowScreenLockForm()
        {
            if (Application.OpenForms.Count > 0)
            {
                Form mainForm = Application.OpenForms[0];
                mainForm.Invoke((MethodInvoker)delegate
                {
                    lockForm = new ScreenLock(); 
                    lockForm.ShowDialog();
                });
            }
        }

        private static void CloseScreenLockForm()
        {
            if (lockForm != null && lockForm.IsHandleCreated)
            {
                lockForm.Invoke((MethodInvoker)delegate
                {
                    lockForm.Close(); 
                    lockForm = null; 
                });
            }
        }

        private static void ShowMessage(string message)
        {
            if (Application.OpenForms.Count > 0)
            {
                Form mainForm = Application.OpenForms[0];
                mainForm.Invoke((MethodInvoker)delegate
                {
                    MessageBox.Show(mainForm, message, "Message from Instructor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                });
            }
        }

        private static void SendScreenCapture(NetworkStream stream)
        {
            using (var bitmap = CaptureScreen())
            using (var ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Jpeg);
                var imageBytes = ms.ToArray();

                var lengthBytes = BitConverter.GetBytes(imageBytes.Length);
                stream.Write(lengthBytes, 0, lengthBytes.Length);
                stream.Write(imageBytes, 0, imageBytes.Length);
            }
        }

        private static Bitmap CaptureScreen()
        {
            var bounds = Screen.PrimaryScreen.Bounds;
            var bitmap = new Bitmap(bounds.Width, bounds.Height);

            using (var g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size);

                var cursorPosition = Cursor.Position;
                var cursorIcon = Cursors.Default;
                var cursorBounds = cursorIcon.HotSpot;

                var cursorScreenPosition = new Point(cursorPosition.X - cursorBounds.X, cursorPosition.Y - cursorBounds.Y);
                cursorIcon.Draw(g, new Rectangle(cursorScreenPosition, cursorIcon.Size));
            }

            return bitmap;
        }

        private static void ShutdownComputer()
        {
            try
            {
                Process.Start(new ProcessStartInfo("shutdown", "/s /f /t 0")
                {
                    CreateNoWindow = true,
                    UseShellExecute = false
                });
                Console.WriteLine("Shutdown command executed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during shutdown: " + ex.Message);
            }
        }

        private static void RestartComputer()
        {
            try
            {
                Process.Start(new ProcessStartInfo("shutdown", "/r /f /t 0")
                {
                    CreateNoWindow = true,
                    UseShellExecute = false
                });
                Console.WriteLine("Restart command executed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during restart: " + ex.Message);
            }
        }
    }
}