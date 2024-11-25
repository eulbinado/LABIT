using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RemoteFormClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // Replace with the IP address of your server running the RemoteFormServer
            string serverIp = "192.168.1.12";
            int serverPort = 8888;

            try
            {
                // Connect to the server
                TcpClient client = new TcpClient(serverIp, serverPort);
                NetworkStream stream = client.GetStream();

                // Loop to allow the client to enter commands
                while (true)
                {
                    Console.Write("Enter command (SHOW_FORM or CLOSE_FORM): ");
                    string command = Console.ReadLine().Trim().ToUpper();

                    // Validate the command
                    if (command == "SHOW_FORM" || command == "CLOSE_FORM")
                    {
                        // Send the command to the server
                        SendCommand(stream, command);
                    }
                    else
                    {
                        Console.WriteLine("Invalid command. Please enter SHOW_FORM or CLOSE_FORM.");
                    }

                    // Option to exit the loop
                    Console.Write("Do you want to send another command? (Y/N): ");
                    if (Console.ReadLine().Trim().ToUpper() != "Y")
                        break;
                }

                // Close connection
                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send command: {ex.Message}");
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private static void SendCommand(NetworkStream stream, string command)
        {
            byte[] data = Encoding.ASCII.GetBytes(command);
            stream.Write(data, 0, data.Length);
            Console.WriteLine($"Command '{command}' sent to server.");
        }
    }
}
