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
    public partial class PictureBoxPlus : UserControl, IDisposable
    {
        private Image resImage;

        public Image ResImage
        {
            get { return resImage; }
            set 
            { 
                resImage = value;
                this.Width = resImage.Width;
                this.Height = resImage.Height;
            }
        }


        public void SetAbtnText(string text)
        {
            aBtn.Text = text;
        }

        public void SetStringValue(string[] stringArray)
        {
            aStrPanel.StrMsg = stringArray;
        }

        public void SetStringValue(string foodname,string price)
        {
            aStrPanel.FoodName = foodname;
            aStrPanel.Price = price;
        }

        public PictureBoxPlus()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (var backdrop = new Bitmap(Width, Height))
            {
                using (var gxOff = Graphics.FromImage(backdrop))
                {
                    var rect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);

                    if (ResImage != null)
                    {
                        gxOff.DrawImage(ResImage, 0, 0);
                        //测试
                        if (ResImage != null)
                        {
                            aStrPanel.BackgroundImage = ResImage;
                            aBtn.BackgroundImage = ResImage;
                        }
                    }
                }

                e.Graphics.DrawImage(backdrop, 0, 0);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        private void aBtn_Click(object sender, EventArgs e)
        {
            PictureBoxPlusEvent.DoOnAlpBtnClick(sender,e);
        }
    }
}
