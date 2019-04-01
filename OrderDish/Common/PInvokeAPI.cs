using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace OrderDish.Common
{
    public class PInvokeAPI
    {
        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        ///// <summary>
        ///// gets a pointer to the windows searched by name
        ///// </summary>
        ///// <param name="lpClassName">Name of the Window class.</param>
        ///// <param name="lpWindowName">Name of the specific window to find.</param>
        ///// <returns>A handle to the window that has the specified class name and window name or IntPtr.Zero</returns>
        //[DllImport("coredll.dll")]
        //public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("iphlpapi.dll")]
        internal static extern IntPtr IcmpCreateFile();

        //[DllImport("iphlpapi.dll")]
        //internal static extern uint IcmpSendEcho2(IntPtr icmpHandle, IntPtr Event, IntPtr apcRoutine, IntPtr apcContext, uint ipAddress, IntPtr data, ushort dataSize, ref IPOptions options, IntPtr replyBuffer, uint replySize, uint timeout);

        [DllImport("iphlpapi")]
        internal static extern bool IcmpCloseHandle(IntPtr handle);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern IntPtr FindWindow(string caption, string className);

        [DllImport("coredll.dll", SetLastError = true)]
        internal static extern bool ShowWindow(IntPtr hwnd, int state);

        [DllImport("coredll.dll")]
        internal static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

    }
}
