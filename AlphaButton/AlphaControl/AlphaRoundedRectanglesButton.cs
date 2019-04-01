using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Drawing;

namespace AlphaControls
{
    public partial class AlphaRoundedRectanglesButton : Control, IDisposable
    {
        private bool pushed;

        private Image backgroundImage;

        public Image BackgroundImage
        {
            set { backgroundImage = value; }
            get 
            {
                if (backgroundImage == null)
                {

                    return null;
                }
                else
                {
                    return backgroundImage;
                }
            }
        }

       

        public AlphaRoundedRectanglesButton()
        {
            
        }
         private bool _IsDisposed = false;

         ~AlphaRoundedRectanglesButton() 
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

                backgroundImage = null;

            } 
            // Free any unmanaged resources in this section 

            _IsDisposed = true;
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

        protected override void OnPaint(PaintEventArgs e)
        {
            if (BackgroundImage!=null)
            {
                using (var backdrop = GraphicsApp.CopyBitmap((Bitmap)BackgroundImage,
                     new Rectangle(this.Location.X, this.Location.Y,
                         this.Width, this.Height)))
                //using (var backdrop = new Bitmap(Width,Height))
                {
                    using (var gxOff = Graphics.FromImage(backdrop))
                    {
                        var Rect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);


                        if (!pushed)
                        {
                            //gxOff.DrawRoundedRectangle(Color.WhiteSmoke,
                            //    Color.FromArgb(176, 176, 176), Rect, new Size(50, 50));
                        }
                        else
                        {
                            
                            //gxOff.DrawRoundedRectangle(Color.WhiteSmoke,
                            //    Color.FromArgb(176, 176, 176), Rect, new Size(50, 50));
                        }

                        //using (var border = new Pen(Color.White))
                        //    gxOff.DrawRectangle(border, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);

                        if (!string.IsNullOrEmpty(Text))
                        {
                            var size = gxOff.MeasureString(Text, Font);
                            using (var text = new SolidBrush(Color.White))
                                gxOff.DrawString(Text, Font, text,
                                    (ClientSize.Width - size.Width) / 2,
                                    (ClientSize.Height - size.Height) / 2);
                        }

                        try
                        {
                            //var bgOwner = backgroundImage;
                            if (BackgroundImage != null)
                                gxOff.AlphaBlendOwn(BackgroundImage, 150, Location);
                        }
                        catch (MissingMethodException ex)
                        {
                            throw new PlatformNotSupportedException(
                                "AlphaBlend is not a supported GDI feature on this device",
                                ex);
                        }
                    }

                    e.Graphics.DrawImage(backdrop, 0, 0);
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }
    }
}
