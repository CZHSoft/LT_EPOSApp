using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace AlphaControls
{
    class Theme
    {
        public static Color AlphaBlend(Color value1, Color value2, int alpha)
        {
            int ialpha = 256 - alpha; //inverse alpha
            return Color.FromArgb((value1.R * alpha) + (value2.R * ialpha) >> 8,
                                  (value1.G * alpha) + (value2.G * ialpha) >> 8,
                                  (value1.B * alpha) + (value2.B * ialpha) >> 8);
        }

        public static Color GradientLight
        {
            get
            {
                var color = AlphaBlend(SystemColors.Highlight, Color.White, 100);
                return AlphaBlend(Color.White, color, 50); ;
            }
        }

        public static Color GradientDark
        {
            get
            {
                var color = AlphaBlend(SystemColors.Highlight, Color.Black, 256);
                return AlphaBlend(Color.White, color, 50); ;
            }
        }
    }

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


    static class GraphicsExtensions
    {
        [DllImport("coredll.dll")]
        static extern IntPtr CreatePen(int fnPenStyle, int nWidth, uint crColor);

        [DllImport("coredll.dll")]
        static extern int SetBrushOrgEx(IntPtr hdc, int nXOrg, int nYOrg, ref Point lppt);

        [DllImport("coredll.dll")]
        static extern IntPtr CreateSolidBrush(uint color);

        [DllImport("coredll.dll")]
        static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobject);

        [DllImport("coredll.dll")]
        static extern bool DeleteObject(IntPtr hgdiobject);

        [DllImport("coredll.dll")]
        static extern IntPtr CreatePatternBrush(IntPtr HBITMAP);

        [DllImport("coredll.dll")]
        static extern bool GradientFill(IntPtr hdc, TRIVERTEX[] pVertex, int dwNumVertex, GRADIENT_RECT[] pMesh, int dwNumMesh, int dwMode);

        [DllImport("coredll.dll")]
        static extern bool RoundRect(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidth, int nHeight);

        static uint GetColorRef(Color value)
        {
            return 0x00000000 | ((uint)value.B << 16) | ((uint)value.G << 8) | (uint)value.R;
        }

        const int PS_SOLID = 0;
        const int PS_DASH = 1;

        static IntPtr CreateGdiPen(Pen pen)
        {
            var style = pen.DashStyle == DashStyle.Solid ? PS_SOLID : PS_DASH;
            return CreatePen(style, (int)pen.Width, GetColorRef(pen.Color));
        }

        const int GRADIENT_FILL_RECT_V = 0x00000001;

        public static void GradientFill(
            this Graphics graphics,
            Rectangle rect,
            Color startColor,
            Color endColor)
        {
            var tva = new TRIVERTEX[2];
            tva[0] = new TRIVERTEX(rect.X, rect.Y, startColor);
            tva[1] = new TRIVERTEX(rect.Right, rect.Bottom, endColor);
            var gra = new GRADIENT_RECT[] { new GRADIENT_RECT(0, 1) };

            var hdc = graphics.GetHdc();
            GradientFill(hdc, tva, tva.Length, gra, gra.Length, GRADIENT_FILL_RECT_V);
            graphics.ReleaseHdc(hdc);
        }

        public static void DrawThemedGradientRectangle(
            this Graphics graphics,
            Pen border,
            Rectangle area,
            Size ellipseSize)
        {
            using (var texture = new Bitmap(area.Right, area.Bottom))
            {
                using (var g = Graphics.FromImage(texture))
                    GradientFill(g, area, Theme.GradientLight, Theme.GradientDark);

                FillRoundedTexturedRectangle(graphics, border, texture, area, ellipseSize);
            }
        }

        public static void FillRoundedTexturedRectangle(
            this Graphics graphics,
            Pen border,
            Bitmap texture,
            Rectangle rect,
            Size ellipseSize)
        {
            Point old = new Point();

            var hdc = graphics.GetHdc();
            var hpen = CreateGdiPen(border);
            var hbitmap = texture.GetHbitmap();
            var hbrush = CreatePatternBrush(hbitmap);

            SetBrushOrgEx(hdc, rect.Left, rect.Top, ref old);
            SelectObject(hdc, hpen);
            SelectObject(hdc, hbrush);

            RoundRect(hdc, rect.Left, rect.Top, rect.Right, rect.Bottom, ellipseSize.Width, ellipseSize.Height);

            SetBrushOrgEx(hdc, old.Y, old.X, ref old);
            DeleteObject(hpen);
            DeleteObject(hbrush);

            graphics.ReleaseHdc(hdc);
        }
    }

    public class AlphaImageButton : Control, IDisposable
    {
        bool pushed = false;

        private Bitmap image;

        private Bitmap offScreen;

        public Bitmap Image
        {
            get { return image; }
            set
            {
                image = value;
                Invalidate();
            }
        }

         private bool _IsDisposed = false;

         ~AlphaImageButton() 
        { 
            Dispose(false); 
        }
        public void Dispose() 
        { 
            Dispose(true);
            // Tell the garbage collector not to call the finalizer 
            // since all the cleanup will already be done.
            GC.SuppressFinalize(true); 
        } 
        protected override void Dispose(bool IsDisposing) 
        { 
            if (_IsDisposed) return; 

            if (IsDisposing) 
            { 
                // Free any managed resources in this section 
                image = null;
                offScreen = null;

            } 
            // Free any unmanaged resources in this section 

            _IsDisposed = true;
        } 

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (offScreen != null)
            {
                offScreen.Dispose();
                offScreen = null;
            }
            offScreen = new Bitmap(ClientSize.Width, ClientSize.Height);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (offScreen == null)
                offScreen = new Bitmap(ClientSize.Width, ClientSize.Height);

            using (var attributes = new ImageAttributes())
            using (var g = Graphics.FromImage(offScreen))
            {
                if (pushed)
                {
                    using (var pen = new Pen(Color.Gold))//SystemColors.Highlight
                    //待改变
                        g.DrawThemedGradientRectangle(pen, ClientRectangle, new Size(50, 50));
                }
                else
                    g.Clear(Parent.BackColor);

                var textSize = g.MeasureString("你好", Font);

                var textArea = new RectangleF(
                    (ClientSize.Width - textSize.Width) / 2,
                    (ClientSize.Height - textSize.Height),
                    textSize.Width,
                    textSize.Height);

                if (Image != null)
                {                    
                    var imageArea = new Rectangle(
                        (ClientSize.Width - Image.Width) / 2,
                        (ClientSize.Height - Image.Height) / 2,
                        Image.Width,
                        Image.Height);

                    var key = Image.GetPixel(0, 0);
                    attributes.SetColorKey(key, key);

                    g.DrawImage(
                        Image,
                        imageArea,
                        0, 0, Image.Width, Image.Height,
                        GraphicsUnit.Pixel,
                        attributes);
                }

                using (var brush = new SolidBrush(ForeColor))
                    g.DrawString(Text, Font, brush, textArea);

                if (pushed)
                {
                    var key = offScreen.GetPixel(0, 0);
                    attributes.SetColorKey(key, key);
                }
                else
                    attributes.ClearColorKey();


                e.Graphics.DrawImage(
                    offScreen,
                    ClientRectangle,
                    0, 0, offScreen.Width, offScreen.Height,
                    GraphicsUnit.Pixel,
                    attributes);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            pushed = true;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            pushed = false;
            Invalidate();
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            Invalidate();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }
    }
}
