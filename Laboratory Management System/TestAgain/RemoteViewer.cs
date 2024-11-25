using System;
using System.Windows.Forms;
using VncSharp;

namespace Laboratory_Management_System
{
    public partial class RemoteViewer : Form
    {
        private RemoteDesktop remoteDesktop;

        public RemoteViewer()
        {
            InitializeComponent();

            // Initialize the RemoteDesktop control
            remoteDesktop = new RemoteDesktop();
            this.Controls.Add(remoteDesktop);
            remoteDesktop.Dock = DockStyle.Fill; // Make it fill the entire form
        }

        private void RemoteViewer_Load(object sender, EventArgs e)
        {
            // Connect to the remote VNC server when the form loads
            ConnectToRemoteDevice("192.168.1.12", 5900); // Replace with your IP and port
        }

        private void ConnectToRemoteDevice(string ipAddress, int port)
        {
            try
            {
                // Configure the remote desktop control
                remoteDesktop.VncPort = port;
                remoteDesktop.GetPassword = GetVncPassword;

                // Connect to the remote device
                remoteDesktop.Connect(ipAddress);

                MessageBox.Show("Connected to remote device!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private string GetVncPassword()
        {
            // Return the VNC password for authentication
            return "your_vnc_password";  // Replace with the actual VNC password
        }
    }
}
