using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace AlphaControls
{
    public class OwnLogoPanel : Control
    {

        private Image logoImage;

        public Image LogoImage
        {
            get { return logoImage; }
            set { logoImage = value; }
        }

        private Color colorStart;

        public Color ColorStart
        {
            get 
            {
                if (colorStart == null)
                {
                    colorStart = Color.FromArgb(255,255,255);
                }
                Invalidate();
                return colorStart; 
            }
            set { colorStart = value; }
        }

        private Color colorEnd;

        public Color ColorEnd
        {
            get 
            {
                if (colorEnd == null)
                {
                    colorEnd = Color.FromArgb(0, 8, 88);
                }
                Invalidate();
                return colorEnd; 
            }
            set { colorEnd = value; }
        }

        

        public OwnLogoPanel()
        {
            
        }

        //protected override void OnMouseDown(MouseEventArgs e)
        //{
        //    base.OnMouseDown(e);
        //    pushed = true;
        //    Invalidate();
        //}

        //protected override void OnMouseUp(MouseEventArgs e)
        //{
        //    base.OnMouseUp(e);
        //    pushed = false;
        //    Invalidate();
        //}

        protected override void OnPaint(PaintEventArgs e)
        {
            using (var backdrop = new Bitmap(Width, Height))
            {
                using (var gxOff = Graphics.FromImage(backdrop))
                {
                    
                    var Rect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);

                    gxOff.GradientFillOwn(Rect, ColorStart,ColorEnd,
                            GraphicsExtensionOwn.GradientFillDirection.Vertical);
                    
                    using (var border = new Pen(Color.White))
                        gxOff.DrawRectangle(border, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
                    
                    if(LogoImage!=null)
                    {
                        //gxOff.
                        gxOff.DrawImage(LogoImage,(ClientSize.Width-LogoImage.Width)/2,
                            LogoImage.Height);
                    
                    }
                    //if (!string.IsNullOrEmpty(Text))
                    //{
                    //    var size = gxOff.MeasureString(Text, Font);
                    //    using (var text = new SolidBrush(Color.White))
                    //        gxOff.DrawString(Text,Font,text,
                    //            (ClientSize.Width - size.Width) / 2,
                    //            (ClientSize.Height - size.Height) / 2);
                    //}

                    //try
                    //{
                    //    //var bgOwner = backgroundImage;
                    //    if (BackgroundImage != null)
                    //        gxOff.AlphaBlendOwn(BackgroundImage, 70, Location);
                    //}
                    //catch (MissingMethodException ex)
                    //{
                    //    throw new PlatformNotSupportedException(
                    //        "AlphaBlend is not a supported GDI feature on this device",
                    //        ex);
                    //}
                }

                e.Graphics.DrawImage(backdrop, 0, 0);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }
    }
}
