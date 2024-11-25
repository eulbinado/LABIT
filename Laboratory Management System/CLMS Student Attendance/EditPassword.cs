using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CLMS_Student_Attendance
{
    public partial class EditPassword : Form
    {
        private string studentID;
        private DatabaseHelper dbHelper = new DatabaseHelper();

        public EditPassword(string studentID)
        {
            InitializeComponent();
            this.studentID = studentID;
        }

        private void update_Click(object sender, EventArgs e)
        {
            string oldPassword = oldPasswordText.Text;
            string newPassword = newPasswordText.Text;
            string confirmPassword = confirmPasswordText.Text;

            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill out all fields.");
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirm password do not match.");
                return;
            }

            // Step 1: Verify old password
            string query = "SELECT password FROM students WHERE student_id = @studentID";
            MySqlParameter[] parameters = {
                new MySqlParameter("@studentID", studentID)
            };
            DataTable result = dbHelper.GetData(query, parameters);

            if (result.Rows.Count > 0)
            {
                string currentPassword = result.Rows[0]["password"].ToString();

                if (currentPassword != oldPassword)
                {
                    MessageBox.Show("Old password is incorrect.");
                    return;
                }

                // Step 2: Update password
                string updateQuery = "UPDATE students SET password = @newPassword WHERE student_id = @studentID";
                MySqlParameter[] updateParameters = {
                    new MySqlParameter("@newPassword", newPassword),
                    new MySqlParameter("@studentID", studentID)
                };

                dbHelper.ExecuteQuery(updateQuery, updateParameters);

                MessageBox.Show("Password updated successfully.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Student not found.");
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showPassword_CheckedChanged(object sender, EventArgs e)
        {
                if (showPassword.Checked)
                {
                    oldPasswordText.PasswordChar = '\0';
                    newPasswordText.PasswordChar = '\0';
                    confirmPasswordText.PasswordChar = '\0';
            }
                else
                {
                    oldPasswordText.PasswordChar = '●';
                    newPasswordText.PasswordChar = '●';
                    confirmPasswordText.PasswordChar = '●';
            }
            
        }
    }
}
