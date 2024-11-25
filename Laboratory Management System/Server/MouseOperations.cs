using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class MouseOperations
    {
        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        public const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        public const uint MOUSEEVENTF_LEFTUP = 0x0004;
        public const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        public const uint MOUSEEVENTF_RIGHTUP = 0x0010;

        public static void MouseEvent(MouseEventFlags flags)
        {
            mouse_event((uint)flags, 0, 0, 0, 0);
        }

        public enum MouseEventFlags : uint
        {
            LeftDown = MOUSEEVENTF_LEFTDOWN,
            LeftUp = MOUSEEVENTF_LEFTUP,
            RightDown = MOUSEEVENTF_RIGHTDOWN,
            RightUp = MOUSEEVENTF_RIGHTUP
        }
    }
}
