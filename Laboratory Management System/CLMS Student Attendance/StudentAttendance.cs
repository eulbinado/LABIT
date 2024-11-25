using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CLMS_Student_Attendance
{
    public partial class StudentAttendance : Form
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private LowLevelKeyboardProc _proc;
        private IntPtr _hookID = IntPtr.Zero;
        public StudentAttendance()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;

            HideAndDisableTaskbar();
            _proc = HookCallback;
            _hookID = SetHook(_proc);

            this.Resize += StudentAttendance_Resize;
            CenterLoginPanel();
        }
        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            if (m.Msg == WM_HOTKEY)
            {
                return; 
            }
            base.WndProc(ref m);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            ShowAndEnableTaskbar();
            UnhookWindowsHookEx(_hookID);
            base.OnFormClosing(e);
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (var curProcess = System.Diagnostics.Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(13, proc, IntPtr.Zero, 0);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            const int WM_KEYDOWN = 0x0100;
            const int WM_SYSKEYDOWN = 0x0104; 
            const int VK_TAB = 0x09;
            const int VK_LWIN = 0x5B;
            const int VK_RWIN = 0x5C;
            const int VK_ESCAPE = 0x1B;
            const int VK_F4 = 0x73;
            const int VK_ALT = 0x12;

            if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN))
            {
                int vkCode = Marshal.ReadInt32(lParam);

                if ((vkCode == VK_TAB && ModifierKeys.HasFlag(Keys.Alt)) || 
                    (vkCode == VK_ESCAPE && ModifierKeys.HasFlag(Keys.Alt)) || 
                    (vkCode == VK_F4 && ModifierKeys.HasFlag(Keys.Alt)) || 
                    vkCode == VK_LWIN ||
                    vkCode == VK_RWIN ||
                    (vkCode == VK_TAB) || 
                    vkCode == VK_ALT) 
                {
                    return (IntPtr)1; 
                }
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }


        private void HideAndDisableTaskbar()
        {
            Taskbar.Hide();
            Taskbar.Disable();
        }

        private void ShowAndEnableTaskbar()
        {
            Taskbar.Enable();
            Taskbar.Show();
        }

        private void HideTaskbar()
        {
            Taskbar.Hide();
        }

        private void ShowTaskbar()
        {
            Taskbar.Show();
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
                DateTime currentTime = DateTime.Now;

                // Step 1: Check the temporary_schedules table for an "Approved" schedule
                string tempScheduleQuery = @"
            SELECT schedule_id, instructor, subject, class_start, class_end 
            FROM temporary_schedules 
            WHERE year = @year AND block = @block 
                AND status = 'Approved'
                AND @currentTime BETWEEN class_start AND class_end";

                MySqlParameter[] tempScheduleParams = {
            new MySqlParameter("@year", year),
            new MySqlParameter("@block", block),
            new MySqlParameter("@currentTime", currentTime)
        };

                DataTable tempScheduleData = dbHelper.GetData(tempScheduleQuery, tempScheduleParams);

                if (tempScheduleData.Rows.Count > 0)
                {
                    ProcessScheduleData(studentID, tempScheduleData);
                    return;
                }

                // Step 2: If no approved schedule in temporary_schedules, check the schedules table
                string scheduleQuery = @"
            SELECT schedule_id, instructor, subject, class_start, class_end 
            FROM schedules 
            WHERE year = @year AND block = @block 
                AND @currentTime BETWEEN class_start AND class_end";

                MySqlParameter[] scheduleParams = {
            new MySqlParameter("@year", year),
            new MySqlParameter("@block", block),
            new MySqlParameter("@currentTime", currentTime)
        };

                DataTable scheduleData = dbHelper.GetData(scheduleQuery, scheduleParams);

                if (scheduleData.Rows.Count > 0)
                {
                    ProcessScheduleData(studentID, scheduleData);
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

        private void ProcessScheduleData(string studentID, DataTable scheduleData)
        {
            int scheduleID = Convert.ToInt32(scheduleData.Rows[0]["schedule_id"]);
            string subject = scheduleData.Rows[0]["subject"].ToString();
            string instructor = scheduleData.Rows[0]["instructor"].ToString();

            if (HasAlreadyMarkedAttendance(studentID, scheduleID))
            {
                MessageBox.Show("You have already marked attendance for this class today.");
                return;
            }

            MarkAttendance(studentID, scheduleID);
            ShowAndEnableTaskbar();
            OpenStudentSession(studentID);
        }


        private bool HasAlreadyMarkedAttendance(string studentID, int scheduleID)
        {
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            string checkAttendanceQuery = @"
        SELECT COUNT(*) 
        FROM attendance 
        WHERE student_id = @studentID 
        AND schedule_id = @scheduleID 
        AND attendance_date = @currentDate"; 

            MySqlParameter[] attendanceParams = {
        new MySqlParameter("@studentID", studentID),
        new MySqlParameter("@scheduleID", scheduleID),
        new MySqlParameter("@currentDate", currentDate)
    };

            DataTable attendanceData = dbHelper.GetData(checkAttendanceQuery, attendanceParams);
            int count = Convert.ToInt32(attendanceData.Rows[0][0]);
            return count > 0;
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
            sessionForm.ShowDialog();
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

        private void StudentAttendance_Resize(object sender, EventArgs e)
        {
            CenterLoginPanel();
        }

        private void CenterLoginPanel()
        {
            int panelX = (this.ClientSize.Width - loginPanel.Width) / 2;
            int panelY = (this.ClientSize.Height - loginPanel.Height) / 2;

            loginPanel.Location = new System.Drawing.Point(panelX, panelY);
        }

        private void passcode_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT password FROM users";
                DataTable passwordsTable = dbHelper.GetData(query);

                List<string> correctPasscodes = new List<string>();
                foreach (DataRow row in passwordsTable.Rows)
                {
                    correctPasscodes.Add(row["password"].ToString());
                }

                this.TopMost = false;

                using (Passcode passcodeForm = new Passcode())
                {
                    if (passcodeForm.ShowDialog() == DialogResult.OK)
                    {
                        string inputPasscode = passcodeForm.EnteredPasscode;

                        if (correctPasscodes.Contains(inputPasscode))
                        {
                            MessageBox.Show("Passcode accepted. Closing attendance.");

                            BlankForm blankForm = new BlankForm();
                            blankForm.Show();
                            ShowAndEnableTaskbar();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Incorrect passcode. Please try again.");
                        }
                    }
                }

                this.TopMost = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }


    public static class Taskbar
    {
        [DllImport("user32.dll")]
        private static extern int FindWindow(string className, string windowText);

        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hwnd, int command);

        [DllImport("user32.dll")]
        private static extern bool EnableWindow(int hwnd, bool enable);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;

        public static void Hide()
        {
            int hwnd = FindWindow("Shell_TrayWnd", "");
            ShowWindow(hwnd, SW_HIDE);
        }

        public static void Show()
        {
            int hwnd = FindWindow("Shell_TrayWnd", "");
            ShowWindow(hwnd, SW_SHOW);
        }

        public static void Disable()
        {
            int hwnd = FindWindow("Shell_TrayWnd", "");
            EnableWindow(hwnd, false);
        }

        public static void Enable()
        {
            int hwnd = FindWindow("Shell_TrayWnd", "");
            EnableWindow(hwnd, true);
        }
    }
}
