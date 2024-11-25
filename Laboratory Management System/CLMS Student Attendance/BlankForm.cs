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
    public partial class BlankForm : Form
    {
        public BlankForm()
        {
            InitializeComponent();
        }

        private void BlankForm_Load(object sender, EventArgs e)
        {
            this.Opacity = 0;      
            this.ShowInTaskbar = false; 
            this.WindowState = FormWindowState.Minimized;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        protected override void SetVisibleCore(bool value)
        {
            if (!this.IsHandleCreated)
            {
                CreateHandle();
                value = false;
            }
            base.SetVisibleCore(value);
        }
    }

}
