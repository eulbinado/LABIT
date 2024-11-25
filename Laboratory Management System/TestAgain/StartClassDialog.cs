using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Laboratory_Management_System
{
    public partial class StartClassDialog : Form
    {
        private List<string> connectedAddresses;

        public StartClassDialog(List<string> addresses)
        {
            InitializeComponent();
            connectedAddresses = addresses;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void SendCommandToServer(string command, string subject, string room)
        {
            if (connectedAddresses == null || connectedAddresses.Count == 0)
            {
                MessageBox.Show("No devices are connected. Please connect at least one device before opening attendance.", "No Connected Devices", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                foreach (string address in connectedAddresses)
                {
                    Int32 port = 8888;
                    TcpClient client = new TcpClient(address, port);
                    NetworkStream stream = client.GetStream();

                    string message = $"{command}|{subject}|{room}";
                    byte[] data = Encoding.ASCII.GetBytes(message);
                    stream.Write(data, 0, data.Length);

                    stream.Close();
                    client.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string subject = textBox1.Text;
            string room = textBox2.Text;
            SendCommandToServer("SHOW_FORM", subject, room);
            textBox1.Clear();
            textBox2.Clear();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SendCommandToServer("CLOSE_FORM", string.Empty, string.Empty);
        }
    }
}
