using System;
using System.Runtime.InteropServices;

namespace LokiRAT
{
    class NativeMethodsHelper
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        public static void ScrollToBottom(IntPtr handle)
        {
            SendMessage(handle, 277, new IntPtr(7), IntPtr.Zero);
        }
    }
}