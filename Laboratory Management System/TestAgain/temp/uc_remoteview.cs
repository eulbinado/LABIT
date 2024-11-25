using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using VncSharp;

namespace Laboratory_Management_System.UserControls.InstructorsControls
{
    public partial class uc_remoteview : UserControl
    {
        private List<string> connectedAddresses = new List<string>();
        private TcpClient serverClient;
        private NetworkStream serverStream;
        private readonly object serverLock = new object();

        public uc_remoteview()
        {
            InitializeComponent();
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(addressInput.Text))
            {
                MessageBox.Show("Please enter at least one IP address.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string[] addresses = addressInput.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string address in addresses)
            {
                string trimmedAddress = address.Trim();
                if (connectedAddresses.Contains(trimmedAddress))
                {
                    MessageBox.Show($"The IP address {trimmedAddress} is already connected.", "Duplicate IP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    continue;
                }

                AddRemoteDesktopControl(trimmedAddress);
            }

            addressInput.Clear();
        }

        private void AddRemoteDesktopControl(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("Invalid IP address detected.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsServerAvailable(address, 8888, 1000)) // 1000ms timeout
            {
                MessageBox.Show($"Unable to connect to {address}. Please ensure the IP address is correct and the device is accessible.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Create a panel to contain each RemoteDesktop control
                Panel remoteDesktopPanel = new Panel();
                remoteDesktopPanel.Width = 290;   // Adjust as needed
                remoteDesktopPanel.Height = 200;  // Adjust as needed

                // Create and configure RemoteDesktop control
                RemoteDesktop remoteDesktop = new RemoteDesktop();
                remoteDesktop.Location = new Point(0, 0);  // Position at the top-left corner
                remoteDesktop.Size = new Size(290, 160);   // Adjust size as needed
                remoteDesktop.Connect(address);  // Connect to the remote desktop
                remoteDesktop.Enabled = false;
                remoteDesktop.AutoScroll = false;

                Label nameLabel = new Label();
                //nameLabel.Text = computerName;
                nameLabel.AutoSize = false; // Set AutoSize to false initially
                nameLabel.Font = new Font("Montserrat", 10, FontStyle.Regular); // Adjust font size and style as needed
                nameLabel.TextAlign = ContentAlignment.MiddleCenter; // Center text horizontally

                // Measure the text size to set the label's width
                using (Graphics g = CreateGraphics())
                {
                    SizeF textSize = g.MeasureString(nameLabel.Text, nameLabel.Font);
                    nameLabel.Size = Size.Round(textSize); // Set label size based on measured text size
                }

                //Calculate horizontal position to center the label within the panel
                int labelWidth = nameLabel.Width;
                int panelWidth = remoteDesktopPanel.Width;
                int xPos = (panelWidth - labelWidth) / 2;
                nameLabel.Location = new Point(xPos, 165);


                // Handle click event of the panel to open fullscreen view
                remoteDesktopPanel.Click += (s, e) =>
                {
                    // Open the Fullscreen form and pass the IP address
                    ShowFullScreenRemoteDesktop(address);
                };

                // Add the RemoteDesktop control to the panel
                remoteDesktopPanel.Controls.Add(remoteDesktop);
                //remoteDesktopPanel.Controls.Add(nameLabel);

                // Add the panel to the FlowLayoutPanel (invoke if needed)
                if (flowLayoutPanel1.InvokeRequired)
                {
                    flowLayoutPanel1.Invoke(new MethodInvoker(delegate
                    {
                        flowLayoutPanel1.Controls.Add(remoteDesktopPanel);
                    }));
                }
                else
                {
                    flowLayoutPanel1.Controls.Add(remoteDesktopPanel);
                }

                // Add the address to the list of connected addresses
                connectedAddresses.Add(address);

            }
            catch (VncSharp.VncProtocolException vncEx)
            {
                MessageBox.Show($"Unable to connect to {address}. Error: {vncEx.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SocketException sockEx)
            {
                MessageBox.Show($"Unable to connect to {address}. Please ensure the IP address is correct and the device is accessible. Socket error: {sockEx.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsServerAvailable(string address, int port, int timeout)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    var result = client.BeginConnect(address, port, null, null);
                    var success = result.AsyncWaitHandle.WaitOne(timeout);
                    if (!success)
                    {
                        return false;
                    }
                    client.EndConnect(result);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void ShowFullScreenRemoteDesktop(string ipAddress)
        {
            // Create an instance of the Fullscreen form and pass the IP address
            Fullscreen fullscreenForm = new Fullscreen(ipAddress);

            // Show the fullscreen form
            fullscreenForm.Show();
        }

        private void SendCommandToRemote(string command)
        {
            try
            {
                foreach (string address in connectedAddresses)
                {
                    using (TcpClient client = new TcpClient(address, 8888))
                    {
                        using (NetworkStream stream = client.GetStream())
                        {
                            byte[] data = Encoding.ASCII.GetBytes(command);
                            stream.Write(data, 0, data.Length);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while sending the command: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void startClassButton_Click(object sender, EventArgs e)
        {
            StartClassDialog startClass = new StartClassDialog(connectedAddresses);
            startClass.ShowDialog();
        }

        private void closeClassButton_Click(object sender, EventArgs e)
        {
            SendCommandToRemote("CLOSE_FORM");
        }

        private void uc_remoteview_Load(object sender, EventArgs e)
        {

        }
    }
}
