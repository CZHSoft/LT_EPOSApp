using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace AlphaControls
{
    public partial class AlphaPanel:Control
    {

        private Image backgroundImage;

        public Image BackgroundImage
        {
            set{backgroundImage = value;}
            get { return backgroundImage; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (var backdrop = new Bitmap(Width, Height))
            {
                using (var gxOff = Graphics.FromImage(backdrop))
                {
                    using (var border = new Pen(Color.White))
                        gxOff.DrawRectangle(border, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);

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


        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }


        private bool _IsDisposed = false;

        ~AlphaPanel() 
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
    }
}
