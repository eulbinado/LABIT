namespace Test
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            RemoteViewing.Vnc.VncClient vncClient2 = new RemoteViewing.Vnc.VncClient();
            this.button1 = new System.Windows.Forms.Button();
            this.remoteDesktop1 = new VncSharp.RemoteDesktop();

            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(242, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;

            // 
            // vncControl1
            // 

            // remoteDesktop1
            // 
            this.remoteDesktop1.AutoScroll = true;
            this.remoteDesktop1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.remoteDesktop1.Location = new System.Drawing.Point(0, 0);
            this.remoteDesktop1.Name = "remoteDesktop1";
            this.remoteDesktop1.Size = new System.Drawing.Size(800, 450);
            this.remoteDesktop1.TabIndex = 4;
            this.remoteDesktop1.AutoScrollMinSize = new System.Drawing.Size(800, 450); // Set the minimum size to the resolution of the remote desktop

            // 
            // Form1
            // 


        }

        #endregion
        private System.Windows.Forms.Button button1;
        private VncSharp.RemoteDesktop remoteDesktop1;
    }
}

