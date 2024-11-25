namespace Laboratory_Management_System
{
    partial class RemoteViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxRemoteScreen = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRemoteScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxRemoteScreen
            // 
            this.pictureBoxRemoteScreen.Location = new System.Drawing.Point(32, 38);
            this.pictureBoxRemoteScreen.Name = "pictureBoxRemoteScreen";
            this.pictureBoxRemoteScreen.Size = new System.Drawing.Size(155, 108);
            this.pictureBoxRemoteScreen.TabIndex = 0;
            this.pictureBoxRemoteScreen.TabStop = false;
            // 
            // RemoteViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBoxRemoteScreen);
            this.Name = "RemoteViewer";
            this.Text = "RemoteViewer";
            this.Load += new System.EventHandler(this.RemoteViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRemoteScreen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxRemoteScreen;
    }
}