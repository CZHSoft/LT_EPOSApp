using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace AlphaControls
{
    public class AlphaTimerButton : Control, IDisposable
    {
        private bool pushed;

        private System.Windows.Forms.Timer timer = null;

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

        public AlphaTimerButton()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Tick+=new EventHandler(timer_Tick);
            timer.Interval = 50;
        }

         private bool _IsDisposed = false;

         ~AlphaTimerButton() 
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
                timer.Enabled = false;
                timer.Tick -= new EventHandler(timer_Tick);
                colorPushed = null;
                colorUnpushed = null;
                backgroundImage = null;
                timer.Dispose();

            } 
            // Free any unmanaged resources in this section 

            _IsDisposed = true;
        } 


        private void timer_Tick(object sender, EventArgs e)
        {
            object s = this.Name;
            ScrollPanelEvent.DoOnVScrollBarValueChange(s, e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            pushed = true;
            timer.Enabled = true;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            pushed = false;
            timer.Enabled = false;
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

                    if (!string.IsNullOrEmpty(Text))
                    {
                        var size = gxOff.MeasureString(Text, Font);
                        using (var text = new SolidBrush(Color.White))
                            gxOff.DrawString(Text,Font,text,
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
