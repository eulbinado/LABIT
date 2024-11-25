using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CLMS_Student_Attendance
{
    public partial class StudentAttendance : Form
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();

        public StudentAttendance()
        {
            InitializeComponent();
            this.TopMost = true; // Ensures this form stays on top
        }

        private bool AuthenticateStudent(string studentID, string password)
        {
            string query = "SELECT COUNT(*) FROM students WHERE student_id = @studentID AND password = @password";
            MySqlParameter[] parameters = {
                new MySqlParameter("@studentID", studentID),
                new MySqlParameter("@password", password)
            };

            DataTable resultTable = dbHelper.GetData(query, parameters);
            int result = Convert.ToInt32(resultTable.Rows[0][0]);
            return result > 0;
        }

        private void CheckSchedule(string studentID)
        {
            string studentQuery = "SELECT year, block FROM students WHERE student_id = @studentID";
            MySqlParameter[] studentParams = {
                new MySqlParameter("@studentID", studentID)
            };

            DataTable studentData = dbHelper.GetData(studentQuery, studentParams);

            if (studentData.Rows.Count > 0)
            {
                string year = studentData.Rows[0]["year"].ToString();
                string block = studentData.Rows[0]["block"].ToString();

                string scheduleQuery = @"
                    SELECT schedule_id, instructor, subject, class_start, class_end 
                    FROM schedules 
                    WHERE year = @year AND block = @block 
                    AND @currentTime BETWEEN class_start AND class_end";

                MySqlParameter[] scheduleParams = {
                    new MySqlParameter("@year", year),
                    new MySqlParameter("@block", block),
                    new MySqlParameter("@currentTime", DateTime.Now)
                };

                DataTable scheduleData = dbHelper.GetData(scheduleQuery, scheduleParams);

                if (scheduleData.Rows.Count > 0)
                {
                    string subject = scheduleData.Rows[0]["subject"].ToString();
                    string instructor = scheduleData.Rows[0]["instructor"].ToString();
                    MessageBox.Show($"Your current class: {subject} with {instructor}");

                    // Mark attendance and proceed to the student session
                    MarkAttendance(studentID, Convert.ToInt32(scheduleData.Rows[0]["schedule_id"]));
                    OpenStudentSession(studentID);
                }
                else
                {
                    MessageBox.Show("No class is scheduled for this time.");
                }
            }
            else
            {
                MessageBox.Show("Student record not found.");
            }
        }

        private void MarkAttendance(string studentID, int scheduleID)
        {
            string hostname = Environment.MachineName;
            string attendanceQuery = "INSERT INTO attendance (student_id, schedule_id, time_in, workstation) VALUES (@studentID, @scheduleID, @timeIn, @workstation)";

            MySqlParameter[] attendanceParams = {
                new MySqlParameter("@studentID", studentID),
                new MySqlParameter("@scheduleID", scheduleID),
                new MySqlParameter("@timeIn", DateTime.Now),
                new MySqlParameter("@workstation", hostname)
            };

            dbHelper.ExecuteQuery(attendanceQuery, attendanceParams);
            MessageBox.Show("Attendance marked successfully.");
        }

        private void OpenStudentSession(string studentID)
        {
            this.Hide();
            StudentSession sessionForm = new StudentSession(studentID);
            sessionForm.ShowDialog(); // Show the session form as a dialog
            this.Close();
        }

        private void showPassword_CheckedChanged_1(object sender, EventArgs e)
        {
            if (showPassword.Checked)
            {
                studentPassword.PasswordChar = '\0';
            }
            else
            {
                studentPassword.PasswordChar = '●';
            }
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            string studentID = studentId.Text;
            string password = studentPassword.Text;

            if (AuthenticateStudent(studentID, password))
            {
                CheckSchedule(studentID);
            }
            else
            {
                MessageBox.Show("Invalid student ID or password.");
            }
        }
    }
}
