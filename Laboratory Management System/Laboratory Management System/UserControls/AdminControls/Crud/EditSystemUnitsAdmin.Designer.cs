﻿namespace Laboratory_Management_System.UserControls.AdminControls.Crud
{
    partial class EditSystemUnitsAdmin
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.update = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.hostname = new System.Windows.Forms.TextBox();
            this.motherboard = new System.Windows.Forms.TextBox();
            this.cpu = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ram = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.gpu = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.psu = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.storage = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.operatingSystem = new System.Windows.Forms.ComboBox();
            this.system_unit_case = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.roomDropdown = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Montserrat", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 29);
            this.label1.TabIndex = 41;
            this.label1.Text = "Edit System Unit";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 18);
            this.label2.TabIndex = 42;
            this.label2.Text = "Operating System:";
            // 
            // update
            // 
            this.update.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(74)))), ((int)(((byte)(173)))));
            this.update.FlatAppearance.BorderSize = 0;
            this.update.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.update.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.update.ForeColor = System.Drawing.Color.White;
            this.update.Location = new System.Drawing.Point(193, 534);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(75, 23);
            this.update.TabIndex = 54;
            this.update.Text = "Update";
            this.update.UseVisualStyleBackColor = false;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.White;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.Black;
            this.cancelButton.Location = new System.Drawing.Point(277, 534);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 55;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(74)))), ((int)(((byte)(173)))));
            this.panel1.Location = new System.Drawing.Point(26, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(3, 30);
            this.panel1.TabIndex = 56;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 66;
            this.label3.Text = "Hostname:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(23, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 18);
            this.label5.TabIndex = 48;
            this.label5.Text = "Motherboard:";
            // 
            // hostname
            // 
            this.hostname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hostname.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hostname.Location = new System.Drawing.Point(181, 128);
            this.hostname.Multiline = true;
            this.hostname.Name = "hostname";
            this.hostname.Size = new System.Drawing.Size(171, 29);
            this.hostname.TabIndex = 68;
            // 
            // motherboard
            // 
            this.motherboard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.motherboard.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.motherboard.Location = new System.Drawing.Point(181, 172);
            this.motherboard.Multiline = true;
            this.motherboard.Name = "motherboard";
            this.motherboard.Size = new System.Drawing.Size(171, 29);
            this.motherboard.TabIndex = 69;
            // 
            // cpu
            // 
            this.cpu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cpu.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cpu.Location = new System.Drawing.Point(181, 216);
            this.cpu.Multiline = true;
            this.cpu.Name = "cpu";
            this.cpu.Size = new System.Drawing.Size(171, 29);
            this.cpu.TabIndex = 71;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(23, 221);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 18);
            this.label4.TabIndex = 70;
            this.label4.Text = "CPU:";
            // 
            // ram
            // 
            this.ram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ram.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ram.Location = new System.Drawing.Point(181, 261);
            this.ram.Multiline = true;
            this.ram.Name = "ram";
            this.ram.Size = new System.Drawing.Size(171, 29);
            this.ram.TabIndex = 73;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(23, 266);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 18);
            this.label6.TabIndex = 72;
            this.label6.Text = "RAM:";
            // 
            // gpu
            // 
            this.gpu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gpu.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpu.Location = new System.Drawing.Point(181, 306);
            this.gpu.Multiline = true;
            this.gpu.Name = "gpu";
            this.gpu.Size = new System.Drawing.Size(171, 29);
            this.gpu.TabIndex = 75;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(23, 311);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 18);
            this.label7.TabIndex = 74;
            this.label7.Text = "GPU:";
            // 
            // psu
            // 
            this.psu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.psu.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.psu.Location = new System.Drawing.Point(181, 351);
            this.psu.Multiline = true;
            this.psu.Name = "psu";
            this.psu.Size = new System.Drawing.Size(171, 29);
            this.psu.TabIndex = 77;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(23, 356);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 18);
            this.label8.TabIndex = 76;
            this.label8.Text = "PSU:";
            // 
            // storage
            // 
            this.storage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.storage.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.storage.Location = new System.Drawing.Point(181, 395);
            this.storage.Multiline = true;
            this.storage.Name = "storage";
            this.storage.Size = new System.Drawing.Size(171, 29);
            this.storage.TabIndex = 79;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(23, 400);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 18);
            this.label9.TabIndex = 78;
            this.label9.Text = "Storage:";
            // 
            // operatingSystem
            // 
            this.operatingSystem.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.operatingSystem.FormattingEnabled = true;
            this.operatingSystem.Location = new System.Drawing.Point(181, 83);
            this.operatingSystem.Name = "operatingSystem";
            this.operatingSystem.Size = new System.Drawing.Size(171, 29);
            this.operatingSystem.TabIndex = 80;
            // 
            // system_unit_case
            // 
            this.system_unit_case.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.system_unit_case.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.system_unit_case.Location = new System.Drawing.Point(181, 438);
            this.system_unit_case.Multiline = true;
            this.system_unit_case.Name = "system_unit_case";
            this.system_unit_case.Size = new System.Drawing.Size(171, 29);
            this.system_unit_case.TabIndex = 82;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(23, 443);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(124, 18);
            this.label10.TabIndex = 81;
            this.label10.Text = "System Unit Case:";
            // 
            // roomDropdown
            // 
            this.roomDropdown.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roomDropdown.FormattingEnabled = true;
            this.roomDropdown.Location = new System.Drawing.Point(181, 482);
            this.roomDropdown.Name = "roomDropdown";
            this.roomDropdown.Size = new System.Drawing.Size(171, 29);
            this.roomDropdown.TabIndex = 84;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Montserrat", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(23, 488);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(50, 18);
            this.label11.TabIndex = 83;
            this.label11.Text = "Room:";
            // 
            // EditSystemUnitsIncharge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 581);
            this.ControlBox = false;
            this.Controls.Add(this.roomDropdown);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.system_unit_case);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.operatingSystem);
            this.Controls.Add(this.storage);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.psu);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.gpu);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ram);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cpu);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.motherboard);
            this.Controls.Add(this.hostname);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.update);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(390, 620);
            this.MinimumSize = new System.Drawing.Size(390, 620);
            this.Name = "EditSystemUnitsIncharge";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit System Unit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox hostname;
        private System.Windows.Forms.TextBox motherboard;
        private System.Windows.Forms.TextBox cpu;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ram;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox gpu;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox psu;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox storage;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox operatingSystem;
        private System.Windows.Forms.TextBox system_unit_case;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox roomDropdown;
        private System.Windows.Forms.Label label11;
    }
}