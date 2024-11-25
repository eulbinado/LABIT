using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Drawing;

namespace CLMS_Student_Attendance
{
    public partial class StudentSession : Form
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();
        private string studentID;

        public StudentSession(string studentID)
        {
            InitializeComponent();
            this.studentID = studentID;

            this.FormBorderStyle = FormBorderStyle.None;  
            this.Opacity = 0.85;  
            this.BackColor = Color.LightGray;  
            this.TransparencyKey = Color.LightGray; 
            this.StartPosition = FormStartPosition.Manual;  
            this.Location = new Point(0, 0);  
            this.TopMost = true; 
            this.ShowInTaskbar = false;
        }

        private void LogOutSession()
        {
            string query = "SELECT attendance_id, time_in FROM attendance WHERE student_id = @studentID ORDER BY time_in DESC LIMIT 1";
            MySqlParameter[] parameters = {
        new MySqlParameter("@studentID", studentID)
    };

            DataTable attendanceData = dbHelper.GetData(query, parameters);

            if (attendanceData.Rows.Count > 0)
            {
                TimeSpan timeIn = (TimeSpan)attendanceData.Rows[0]["time_in"];
                TimeSpan timeOut = DateTime.Now.TimeOfDay;
                TimeSpan duration = timeOut - timeIn;

                string updateQuery = "UPDATE attendance SET time_out = @timeOut, duration = @duration WHERE attendance_id = @id";
                MySqlParameter[] updateParams = {
            new MySqlParameter("@timeOut", timeOut),
            new MySqlParameter("@duration", duration),
            new MySqlParameter("@id", attendanceData.Rows[0]["attendance_id"])
        };
                dbHelper.ExecuteQuery(updateQuery, updateParams);

                string hostname = Environment.MachineName; 
                string workstationQuery = "SELECT workstation FROM system_units WHERE hostname = @hostname";
                MySqlParameter[] workstationParams = {
            new MySqlParameter("@hostname", hostname)
        };

                DataTable systemUnitData = dbHelper.GetData(workstationQuery, workstationParams);

                if (systemUnitData.Rows.Count > 0)
                {
                    string workstationID = systemUnitData.Rows[0]["workstation"].ToString();

                    string updateTimeUsageQuery = "UPDATE workstations SET time_usage = time_usage + @duration WHERE workstation_id = @workstationID";
                    MySqlParameter[] timeUsageParams = {
                new MySqlParameter("@duration", (long)duration.TotalSeconds),
                new MySqlParameter("@workstationID", workstationID)
            };
                    dbHelper.ExecuteQuery(updateTimeUsageQuery, timeUsageParams);
                }
                else
                {
                    MessageBox.Show("System unit with the specified hostname was not found.");
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("No active session found.");
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                LogOutSession();

                this.Hide();

                StudentAttendance loginForm = new StudentAttendance();
                loginForm.ShowDialog();  

                this.Close();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Size = new Size(300, 200); 
        }

        private void changePassword_Click(object sender, EventArgs e)
        {
            using (EditPassword editPasswordForm = new EditPassword(studentID))
            {              
                if (editPasswordForm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Password changed successfully.");
                }
            }
        }
    }
}
