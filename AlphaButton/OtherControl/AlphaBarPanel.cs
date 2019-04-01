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
    public partial class AlphaBarPanel : UserControl
    {
        public delegate void alpBtnCheckDelegate();
        public delegate void alpBtnSubmitDelegate();
        public delegate void alpBtnFindDelegate(string strFind);
        public event alpBtnCheckDelegate OnAlpBtnCheck_Click;
        public event alpBtnSubmitDelegate OnAlpBtnSubmit_Click;
        public event alpBtnFindDelegate OnAlpBtnFind_Click;

        public AlphaBarPanel()
        {
            InitializeComponent();
            timerDateTime.Enabled = true;
        }

        //背景图片
        private Image backgroundImage;

        public Image BackgroundImage
        {
            set 
            { 
                backgroundImage = value;

                this.alpBtnReturn.BackgroundImage = backgroundImage;
                this.alpBtnCheck.BackgroundImage = backgroundImage;
                this.alpBtnSubmit.BackgroundImage = backgroundImage;
                this.alpBtnFind.BackgroundImage = backgroundImage;

                this.alpTimer.BackgroundImage = backgroundImage;
            }
            get { return backgroundImage; }
        }


        //时间框宽度
        private int timerWidth;

        public int TimerWidth
        {
            get 
            {
                if (timerWidth == 0)
                {
                    return 80;
                }
                return timerWidth; 
            }
            set { timerWidth = value; }
        }

        //private Form localForm;

        //public Form LocalForm
        //{
        //    get 
        //    {
        //        if (localForm != null)
        //        {
        //            return localForm;
        //        }
        //        return null ;
        //    }
        //    set { localForm = value; }
        //}


        //时间文本
        private string timerString;

        public string TimerString
        {
            get
            {
                return timerString;
                
            }
            set 
            { 
                timerString = value;
                alpTimer.TimeText = timerString;
            }
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

        private void alpBtnReturn_Click(object sender, EventArgs e)
        {
            FormManageEvent.DoOnFormClose();
        }

        private void timerDateTime_Tick(object sender, EventArgs e)
        {

            TimerString = DateTime.Now.ToString("HH:mm");
        }

        public void SetAlpbtnReturnText(string btnText)
        {
            alpBtnReturn.Text = btnText;
        }

        private void alpBtnCheck_Click(object sender, EventArgs e)
        {
            if (OnAlpBtnCheck_Click != null)
            {
                OnAlpBtnCheck_Click();
            }
        }

        private void alpBtnSubmit_Click(object sender, EventArgs e)
        {
            if (OnAlpBtnSubmit_Click != null)
            {
                OnAlpBtnSubmit_Click();
            }
        }

        private void alpBtnFind_Click(object sender, EventArgs e)
        {
            if (OnAlpBtnFind_Click != null)
            {
                if (!string.IsNullOrEmpty(tbFind.Text))
                {
                    OnAlpBtnFind_Click(tbFind.Text);
                }
            }
        }
    }
}
