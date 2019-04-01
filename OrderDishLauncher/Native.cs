using System;
using System.Runtime.InteropServices;

namespace OrderDishLauncher
{
    class Native
    {
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
