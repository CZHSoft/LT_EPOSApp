using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace AlphaControls
{
    public class AlphaVerticalButton : Control, IDisposable
    {
        private bool pushed;

        private Image backgroundImage;

        public Image BackgroundImage
        {
            set { backgroundImage = value; }
            get { return backgroundImage; }
        }

        private Color[] colorUnpushed;

        public Color[] ColorUnpushed
        {
            get 
            {
                if ((colorUnpushed == null)||(colorUnpushed.Length!=4))
                {
                    colorUnpushed=new Color[4]{
                        Color.FromArgb(176, 176, 176),
                        Color.FromArgb(32, 32, 32),                   
                        Color.FromArgb(0, 0, 11),                    
                        Color.FromArgb(32, 32, 32)};
                }
                return colorUnpushed; 
            }
            set { colorUnpushed = value; }
        }

        private Color[] colorPushed;

        public Color[] ColorPushed
        {
            get
            {
                if ((colorPushed == null) || (colorPushed.Length != 4))
                {
                    colorPushed = new Color[4]{
                        Color.FromArgb(32, 32, 32),
                        Color.FromArgb(32, 32, 32),                
                        Color.FromArgb(32, 32, 32),
                        Color.FromArgb(32, 32, 32)};
                }
                return colorPushed; 
            }
            set { colorPushed = value; }
        }

        public AlphaVerticalButton()
        {
            
        }

         private bool _IsDisposed = false;

         ~AlphaVerticalButton() 
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
                colorPushed = null;
                colorUnpushed = null;
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
            using (var backdrop = new Bitmap(Width, Height))
            {
                using (var gxOff = Graphics.FromImage(backdrop))
                {
                    var topRect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height / 2);
                    var bottomRect = new Rectangle(0, topRect.Height, ClientSize.Width, ClientSize.Height / 2);

                    if (!pushed)
                    {
                        
                        gxOff.GradientFillOwn(topRect, ColorUnpushed[0], ColorUnpushed[1],
                            GraphicsExtensionOwn.GradientFillDirection.Vertical);

                        gxOff.GradientFillOwn(bottomRect, ColorUnpushed[2], ColorUnpushed[3],
                            GraphicsExtensionOwn.GradientFillDirection.Vertical);                        
                    }
                    else
                    {
                        gxOff.GradientFillOwn(topRect, ColorPushed[0], ColorPushed[1],
                            GraphicsExtensionOwn.GradientFillDirection.Vertical);
                        gxOff.GradientFillOwn(bottomRect, ColorPushed[2], ColorPushed[3],
                            GraphicsExtensionOwn.GradientFillDirection.Vertical);
                    }

                    using (var border = new Pen(Color.White))
                        gxOff.DrawRectangle(border, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);

                    //verticalText
                    if (!string.IsNullOrEmpty(Text))
                    {
                        //var size = gxOff.MeasureString(Text.Substring(0, 1), Font);

                        //for (int txtLength = 0; txtLength < Text.Length; txtLength++)
                        //{
                            
                        //    using (var txtBrush = new SolidBrush(Color.GhostWhite))
                        //        gxOff.DrawString(Text.Substring(txtLength, 1), Font, txtBrush,
                        //            (ClientSize.Width - size.Width) / 2, txtLength * size.Height);
                        //}
                        //this.ClientSize = new Size(this.ClientSize.Width, (int)(Text.Length * size.Height));

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
