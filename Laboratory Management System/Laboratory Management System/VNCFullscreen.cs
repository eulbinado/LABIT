using System;
using System.Drawing;
using System.Windows.Forms;

namespace Laboratory_Management_System
{
    public partial class VNCFullscreen : Form
    {
        private string hostname;

        public VNCFullscreen(string hostname)
        {
            InitializeComponent();
            this.hostname = hostname;
            this.Text = $"Workstation: {hostname}";

            this.WindowState = FormWindowState.Maximized;

            InitializeRemoteDesktop();
        }

        private void InitializeRemoteDesktop()
        {
            try
            {
                remoteDesktop1.Dock = DockStyle.Fill;

                if (remoteDesktop1 == null)
                {
                    throw new InvalidOperationException("Remote desktop control is not initialized.");
                }

                remoteDesktop1.Connect(hostname);

                AdjustRemoteDesktopSize();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to the remote desktop: " + ex.Message);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (remoteDesktop1 != null && remoteDesktop1.IsConnected)
            {
                AdjustRemoteDesktopSize();
            }
        }

        private void AdjustRemoteDesktopSize()
        {
            try
            {
                if (remoteDesktop1 != null && remoteDesktop1.IsConnected)
                {
                    remoteDesktop1.Width = this.ClientSize.Width;
                    remoteDesktop1.Height = this.ClientSize.Height;
                }
                else
                {
                    MessageBox.Show("Remote desktop is not connected. Please try again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adjusting remote desktop size: " + ex.Message);
            }
        }

        private void VNCFullscreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (remoteDesktop1 != null && remoteDesktop1.IsConnected)
                {
                    remoteDesktop1.Disconnect();
                }
            }
            catch
            {
             
            }
        }

        private void VNCFullscreen_Click(object sender, EventArgs e)
        {
            if (remoteDesktop1 == null || !remoteDesktop1.IsConnected)
            {
                MessageBox.Show("Remote desktop is not connected. Click to retry.");
                return;
            }
        }
    }
}
