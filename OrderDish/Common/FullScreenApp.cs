using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace OrderDish.Common
{
    public class FullScreenApp
    {
        [DllImport("aygshell.dll")]
        public static extern bool SHFullScreen(IntPtr hWnd, uint dwState);

        //http://msdn.microsoft.com/en-us/library/aa930139.aspx
        const uint SHFS_SHOWTASKBAR = 0x1;//任务栏
        const uint SHFS_HIDETASKBAR = 0x2;
        const uint SHFS_SHOWSIPBUTTON = 0x4;//输入法
        const uint SHFS_HIDESIPBUTTON = 0x8;
        const uint SHFS_SHOWSTARTICON = 0x10;//开始菜单栏
        const uint SHFS_HIDESTARTICON = 0x20;

        /// <summary>
        /// 全屏显示，即隐藏WM和win CE的任务栏（和开始菜单栏）
        /// </summary>
        /// <param name="hWnd">要显示的窗体句柄</param>
        /// <param name="isFull">true全屏，false取消全屏</param>
        /// <returns></returns>
        public static bool FullScreen(IntPtr hWnd, bool isFull)
        {
            uint dwState = 0;
            if (isFull)
            {
                dwState = SHFS_HIDETASKBAR | SHFS_HIDESIPBUTTON | SHFS_HIDESTARTICON;
            }
            else
            {
                dwState = SHFS_SHOWTASKBAR | SHFS_SHOWSIPBUTTON | SHFS_SHOWSTARTICON;
            }
            return SHFullScreen(hWnd, dwState);
        }

    }
}
