using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLMS_Student_Attendance
{
    public partial class ScreenLock : Form
    {
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
        public ScreenLock()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;

            HideAndDisableTaskbar();
            _proc = HookCallback;
            _hookID = SetHook(_proc);

            this.Resize += ScreenLock_Resize;
            CenterLockPanel();
        }

        private void ScreenLock_Resize(object sender, EventArgs e)
        {
            CenterLockPanel();
        }
        private void CenterLockPanel()
        {
            int panelX = (this.ClientSize.Width - lockPanel.Width) / 2;
            int panelY = (this.ClientSize.Height - lockPanel.Height) / 2;

            lockPanel.Location = new System.Drawing.Point(panelX, panelY);
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
