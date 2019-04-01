using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace AlphaControls
{
    public static class GraphicsExtensionOwn
    {
        //public static void AlphaBlendOwn(this Graphics graphics, Image image, 
        //    byte opacity)
        //{
        //    AlphaBlendOwn(graphics, image, opacity, Point.Empty);
        //}
        //透明
        public static void AlphaBlendOwn(this Graphics graphics, Image image, 
            byte opacity, Point location)
        {
            using (var imageSurface = Graphics.FromImage(image))
            {
                var hdcDst = graphics.GetHdc();
                var hdcSrc = imageSurface.GetHdc();

                try
                {
                    var blendFunction = new BLENDFUNCTION
                    {
                        BlendOp = ((byte)BlendOperation.AC_SRC_OVER),
                        BlendFlags = ((byte)BlendFlags.Zero),
                        SourceConstantAlpha = opacity,
                        AlphaFormat = 0
                    };
                    AlphaBlend(
                        hdcDst,
                        location.X == 0 ? 0 : -location.X,
                        location.Y == 0 ? 0 : -location.Y,
                        image.Width,
                        image.Height,
                        hdcSrc,
                        0,
                        0,
                        image.Width,
                        image.Height,
                        blendFunction);
                }
                finally
                {
                    graphics.ReleaseHdc(hdcDst);
                    imageSurface.ReleaseHdc(hdcSrc);
                }
            }
        }

        //字符串
        //public static void DrawStringOwn(string str, int strSize, Color strColor, 
        //    Graphics gx, int x, int y)
        //{
        //    Font strFont = new Font("Tahoma", strSize, FontStyle.Regular);
        //    SizeF size = gx.MeasureString(str, strFont);
        //    //int x = this.Width / 2 - (int)size.Width / 2;
        //    gx.DrawString(str, strFont, new SolidBrush(strColor), x, y);

        //}


        //参数///////////////////////////////////////////////////////////////////
        public static void GradientFillOwn(this Graphics graphics, Rectangle rectangle,
            Color startColor, Color endColor,GradientFillDirection direction)
        {
            var tva = new TRIVERTEX[2];
            tva[0] = new TRIVERTEX(rectangle.Right, rectangle.Bottom, endColor);
            tva[1] = new TRIVERTEX(rectangle.X, rectangle.Y, startColor);
            var gra = new[] { new GRADIENT_RECT(0, 1) };

            var hdc = graphics.GetHdc();
            try
            {
                GradientFill(
                    hdc,
                    tva,
                    (uint)tva.Length,
                    gra,
                    (uint)gra.Length,
                    (uint)direction);
            }
            finally
            {
                graphics.ReleaseHdc(hdc);
            }
        }

        public enum GradientFillDirection
        {
            Horizontal = 0x00000000,
            Vertical = 0x00000001
        }

        [DllImport("coredll.dll")]
        static extern bool AlphaBlend(
            IntPtr hdcDest,
            int xDest,
            int yDest,
            int cxDest,
            int cyDest,
            IntPtr hdcSrc,
            int xSrc,
            int ySrc,
            int cxSrc,
            int cySrc,
            BLENDFUNCTION blendfunction);

        struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        enum BlendOperation : byte
        {
            AC_SRC_OVER = 0x00
        }

        enum BlendFlags : byte
        {
            Zero = 0x00
        }

        enum SourceConstantAlpha : byte
        {
            Transparent = 0x00,
            Opaque = 0xFF
        }

        enum AlphaFormat : byte
        {
            AC_SRC_ALPHA = 0x01
        }

        [DllImport("coredll.dll")]
        static extern bool GradientFill(
            IntPtr hdc,
            TRIVERTEX[] pVertex,
            uint dwNumVertex,
            GRADIENT_RECT[] pMesh,
            uint dwNumMesh,
            uint dwMode);

        struct TRIVERTEX
        {
            private int x;
            private int y;
            private ushort Red;
            private ushort Green;
            private ushort Blue;
            private ushort Alpha;

            public TRIVERTEX(int x, int y, Color color)
                : this(x, y, color.R, color.G, color.B, color.A)
            {
            }

            public TRIVERTEX(
                int x, int y,
                ushort red, ushort green, ushort blue,
                ushort alpha)
            {
                this.x = x;
                this.y = y;
                Red = (ushort)(red << 8);
                Green = (ushort)(green << 8);
                Blue = (ushort)(blue << 8);
                Alpha = (ushort)(alpha << 8);
            }
        }

        struct GRADIENT_RECT
        {
            private uint UpperLeft;
            private uint LowerRight;

            public GRADIENT_RECT(uint ul, uint lr)
            {
                UpperLeft = ul;
                LowerRight = lr;
            }
        }

    }
}
