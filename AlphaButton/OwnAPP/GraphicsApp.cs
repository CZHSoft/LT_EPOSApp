using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AlphaControls
{
    public static class GraphicsApp
    {
        const int SRCCOPY = 0x00CC0020;

        /// <summary>
        /// 拷贝图片的某一个区域，生成一个新图片
        /// </summary>
        /// <param name="bitSrc"></param>
        /// <param name="rectDest"></param>
        /// <returns></returns>
        public static Bitmap CopyBitmap(Bitmap bitSrc, Rectangle rectDest)
        {
            Bitmap result = new Bitmap(rectDest.Width, rectDest.Height);
            Graphics g1 = Graphics.FromImage(bitSrc);
            Graphics g2 = Graphics.FromImage(result);
            IntPtr hdc1 = g1.GetHdc();
            IntPtr hdc2 = g2.GetHdc();
            BitBlt(hdc2, 0, 0, rectDest.Width, rectDest.Height, hdc1, rectDest.X, rectDest.Y, SRCCOPY);
            g1.ReleaseHdc(hdc1);
            g2.ReleaseHdc(hdc2);
            g1.Dispose();
            g2.Dispose();
            return result;
        }

        /// <summary>
        /// 使用选定的刷子、源位图和ROP3码绘制选定的矩形
        /// 获得屏幕图形并将它写入内存中的一个位图中(截屏)
        /// </summary>
        /// <param name="hdcDest">目的上下文设备的句柄 </param>
        /// <param name="nXDest">目的图形的左上角的x坐标 </param>
        /// <param name="nYDest">目的图形的左上角的y坐标 </param>
        /// <param name="nWidth">目的图形的矩形宽度 </param>
        /// <param name="nHeight">目的图形的矩形高度 </param>
        /// <param name="hdcSrc">源上下文设备的句柄</param>
        /// <param name="nXSrc">源图形的左上角的x坐标</param>
        /// <param name="nYSrc">源图形的左上角的x坐标</param>
        /// <param name="dwRop">光栅操作代码 </param>
        /// <returns></returns>
        [DllImport("CoreDLL.dll")]
        public static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);
    }
}
