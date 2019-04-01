using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace AlphaControls
{
    public partial class SplashControl : UserControl, IDisposable
    {
        private Image backgroundImage;

        public Image BackgroundImage
        {
            get { return backgroundImage; }
            set 
            { 
                backgroundImage = value;
                this.Width = backgroundImage.Width;
                this.Height = backgroundImage.Height;
                this.Invalidate();
            }
        }

        private string showText;

        public string ShowText
        {
            get { return showText; }
            set 
            {
                showText = value;
                //this.Invalidate();
            }
        }

        public SplashControl()
        {
            InitializeComponent();
            SplashControlEvent.OnSplashControlTextChange+=new SplashControlEvent.SplashControlDelegate(SplashControlEvent_OnSplashControlTextChange);
        }

        /// <summary>
        /// 事件处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="evt"></param>
        void SplashControlEvent_OnSplashControlTextChange(object sender, EventArgs e)
        {
            this.ShowText = sender.ToString();
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            
            if (BackgroundImage != null)
            {
                using (var backdrop = BackgroundImage)
                {
                    using (var gxOff = Graphics.FromImage(backdrop))
                    {
                        if (!string.IsNullOrEmpty(ShowText))
                        {
                            
                                var size = gxOff.MeasureString(ShowText, Font);
                                using (var textBrush = new SolidBrush(Color.Black))
                                    gxOff.DrawString(ShowText, Font, textBrush,
                                        (ClientSize.Width - size.Width),
                                        (ClientSize.Height - size.Height));

                        }
                    }

                    e.Graphics.DrawImage(backdrop, 0, 0);
                }
            }
            //this.Dispose();
        }

    }
}
