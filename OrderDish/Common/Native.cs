using System;
using System.Runtime.InteropServices;

namespace OrderDish.Common
{
    public class Native
    {
        public const int WM_USER = 0x0400;
        public const int WM_STATUSUPDATE = WM_USER;
        public const int WM_DONE = WM_USER + 1;

        public enum StatusMsg
        {
            InitializingStage1 = 1,
            InitializingStage2 = 2,
            InitializingStage3 = 3,
            InitializingStage4 = 4,
            InitializingStage5 = 5,

            InitializingStage11 = 11,
            InitializingStage12 = 12,
            InitializingStage13 = 13,
            InitializingStage14 = 14,
            InitializingStage15 = 15,
        }

        /// <summary>
        /// used for interprocess communication. sends message to the target window
        /// </summary>
        /// <param name="window">Handle to the window that must receive the message.</param>
        /// <param name="message">The message to be send.</param>
        /// <param name="wparam">WParam of the message.</param>
        /// <param name="lparam">LParam of the message.</param>
        /// <returns></returns>
        [DllImport("coredll.dll")]
        public static extern int SendMessage(IntPtr window, int message, int wparam, int lparam);

        /// <summary>
        /// gets a pointer to the windows searched by name
        /// </summary>
        /// <param name="lpClassName">Name of the Window class.</param>
        /// <param name="lpWindowName">Name of the specific window to find.</param>
        /// <returns>A handle to the window that has the specified class name and window name or IntPtr.Zero</returns>
        [DllImport("coredll.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    }
}
