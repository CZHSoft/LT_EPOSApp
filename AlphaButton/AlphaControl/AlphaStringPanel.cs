using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace AlphaControls
{
    public partial class AlphaStringPanel : Control, IDisposable
    {

        private Image backgroundImage;

        public Image BackgroundImage
        {
            set { backgroundImage = value; }
            get { return backgroundImage; }
        }

        private string[] strMsg;

        public string[] StrMsg
        {
            get 
            {
                if (StrMsg != null)
                {
                    return strMsg;
                }

                return null;
            }
            set 
            { 
                strMsg = value;
                this.Invalidate();
            }
        }

        private string foodName;

        public string FoodName
        {
            get { return foodName; }
            set { foodName = value; }
        }

        private string price;

        public string Price
        {
            get { return price; }
            set { price = value; }
        }

        public AlphaStringPanel()
        {
            
        }

         private bool _IsDisposed = false;

         ~AlphaStringPanel() 
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
                if (strMsg != null)
                {
                    strMsg = null;
                }

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
                    var rect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);

                    gxOff.FillRectangle(new SolidBrush(Color.White), rect);

                    using (var border = new Pen(Color.White))
                        gxOff.DrawRectangle(border, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);

                    //测试
                    int localY = 5;
                    if (!string.IsNullOrEmpty(FoodName))
                    {
                        var size = gxOff.MeasureString(FoodName, Font);
                        using (var textBrush = new SolidBrush(Color.Black))
                        {
                            gxOff.DrawString(FoodName, Font, textBrush, (ClientSize.Width - size.Width), localY);
                            localY += (int)size.Height;
                        }
                    }
                    if (!string.IsNullOrEmpty(Price))
                    {
                        var size = gxOff.MeasureString(Price, Font);
                        using (var textBrush = new SolidBrush(Color.Black))
                        {
                            gxOff.DrawString(Price, Font, textBrush, (ClientSize.Width - size.Width), localY);
                            localY += (int)size.Height;
                        }
                    }

                    //if (StrMsg != null)
                    //{
                    //    for (int i = 0; i < StrMsg.Length; i++)
                    //    {
                    //        if (!string.IsNullOrEmpty(StrMsg[i]))
                    //        {
                    //            var size = gxOff.MeasureString(StrMsg[i], Font);
                    //            using (var textBrush = new SolidBrush(Color.Black))
                    //            {
                    //                gxOff.DrawString(StrMsg[i], Font, textBrush, (ClientSize.Width - size.Width), localY);
                    //            }
                    //            localY += (int)size.Height;
                    //        }
                    //    }
                    //}
                    //this.Width = localY;
                    //if (!string.IsNullOrEmpty(Text))
                    //{
                    //    var size = gxOff.MeasureString(Text, Font);
                    //    using (var text = new SolidBrush(Color.Black))
                    //    {
                    //        gxOff.DrawString(Text, Font, text, (ClientSize.Width - size.Width), 0);

                    //        gxOff.DrawString(Text, Font, text, (ClientSize.Width - size.Width), 20);
                    //        gxOff.DrawString(Text, Font, text, (ClientSize.Width - size.Width), 40);
                    //    }
                    //}

                    try
                    {
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
    }
}
