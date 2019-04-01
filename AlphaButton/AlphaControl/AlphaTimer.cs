using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace AlphaControls
{
    public class AlphaTimer : Control, IDisposable
    {
        private Image backgroundImage;

        public Image BackgroundImage
        {
            set { backgroundImage = value; }
            get { return backgroundImage; }
        }

        private Color[] colorFill;

        public Color[] ColorFill
        {
            get 
            {
                if ((colorFill == null)||(colorFill.Length!=4))
                {
                    colorFill=new Color[4]{
                        Color.FromArgb(176, 176, 176),
                        Color.FromArgb(32, 32, 32),                   
                        Color.FromArgb(0, 0, 11),                    
                        Color.FromArgb(32, 32, 32)};
                }
                return colorFill; 
            }
            set { colorFill = value; }
        }

        private string timeText;

        public string TimeText
        {
            get { return timeText; }
            set 
            { 
                timeText = value;
                this.Invalidate();
            }
        }

        public AlphaTimer()
        {
            
        }

         private bool _IsDisposed = false;

         ~AlphaTimer() 
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
                colorFill = null;
                backgroundImage = null;

            } 
            // Free any unmanaged resources in this section 

            _IsDisposed = true;
        } 

        protected override void OnPaint(PaintEventArgs e)
        {
            using (var backdrop = new Bitmap(Width, Height))
            {
                using (var gxOff = Graphics.FromImage(backdrop))
                {
                    var topRect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height / 2);
                    var bottomRect = new Rectangle(0, topRect.Height, ClientSize.Width, ClientSize.Height / 2);

                    gxOff.GradientFillOwn(topRect, ColorFill[0], ColorFill[1],
                        GraphicsExtensionOwn.GradientFillDirection.Vertical);

                    gxOff.GradientFillOwn(bottomRect, ColorFill[2], ColorFill[3],
                        GraphicsExtensionOwn.GradientFillDirection.Vertical);                        


                    //using (var border = new Pen(Color.White))
                    //    gxOff.DrawRectangle(border, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);

                    if (!string.IsNullOrEmpty(TimeText))
                    {
                        var size = gxOff.MeasureString(TimeText, Font);
                        using (var textBrush = new SolidBrush(Color.White))
                            gxOff.DrawString(TimeText, Font, textBrush,
                                (ClientSize.Width - size.Width) / 2,
                                (ClientSize.Height - size.Height) / 2);
                    }

                    try
                    {
                        //var bgOwner = backgroundImage;
                        if (BackgroundImage != null)
                            gxOff.AlphaBlendOwn(BackgroundImage, 70, Location);
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
    }
}
