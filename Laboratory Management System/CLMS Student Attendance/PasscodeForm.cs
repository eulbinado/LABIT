using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLMS_Student_Attendance
{
    public partial class Passcode : Form
    {
        public string EnteredPasscode { get; private set; }

        public Passcode()
        {
            InitializeComponent();
            EnteredPasscode = string.Empty;
        } 
        private void ok_Click(object sender, EventArgs e)
        {
            EnteredPasscode = passcodeTextbox.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; 
            this.Close();
        }
    }

}
