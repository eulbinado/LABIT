using System;
using System.Windows.Forms;
using AxMSTSCLib;  // Add this for the RDP control

namespace ForTesting
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeRemoteDesktopClient();
        }

        private void InitializeRemoteDesktopClient()
        {
            try
            {
                ((System.ComponentModel.ISupportInitialize)(axMsRdpClient81)).BeginInit();
                axMsRdpClient81.Dock = DockStyle.Fill; // Fill the form
                this.Controls.Add(axMsRdpClient81);
                ((System.ComponentModel.ISupportInitialize)(axMsRdpClient81)).EndInit();

                // Set the remote desktop connection settings
                axMsRdpClient81.Server = "192.168.1.12";  // The IP address of the remote machine
                //axMsRdpClient81.UserName = "User"; // Your remote desktop username
                //axMsRdpClient81.AdvancedSettings8.ClearTextPassword = ""; // Your remote desktop password

                // Enable optional settings
                axMsRdpClient81.AdvancedSettings8.SmartSizing = true; // Optional: Automatically resize to fit

                // Connect to the remote desktop
                axMsRdpClient81.Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to Remote Desktop: " + ex.Message);
            }
        }

    }
}
