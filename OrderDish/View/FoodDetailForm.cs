using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Drawing;
using Microsoft.WindowsCE.Forms;


namespace OrderDish
{
    public partial class DetailForm : Form
    {
        private Bitmap foodDetailBitmap;

        private string foodId;
        private string foodStrName;
        private string foodStrPrice;

        private bool hadPaint = false;

        public DetailForm(string id,string name,string price,Bitmap imageFood)
        {
            InitializeComponent();

            /////////// Don't show title bar and allocate the full screen ///////////////
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.ControlBox = false;
            //this.Menu = null;
            //this.WindowState = FormWindowState.Maximized;

            //FullScreenApp.FullScreen(this.Handle, true);

            SystemSettings.ScreenOrientation = ScreenOrientation.Angle90;
            //Common.SIP.ShowHideSIP(0);
            /////////////////////////////////////////////////////////////////////////////

            pbpFood.aBtn.Text = GlobalVariable.GetResxString("abtnBuy");

            foodId = id;
            foodStrName = name;
            foodStrPrice = price;
            foodDetailBitmap = imageFood;

        }

        private void PictureBoxPlusEvent_OnAlpBtnClick(object sender, EventArgs e)
        {
            MessageBox.Show(sender.ToString());
        }

        private void DetailForm_Load(object sender, EventArgs e)
        {
            pbpFood.ResImage = foodDetailBitmap;
            string[] temp = new string[] { foodStrName, foodStrPrice };
            pbpFood.SetStringValue(foodStrName, foodStrPrice);
            //pbpFood.SetStringValue(temp);
            pbpFood.aBtn.Click+=new EventHandler(aBtn_Click);

            alphaRemarkPanel1.Text = "这里是菜谱的详细说明，请添加必要的备注信息！";


        }
        
        private void aBtn_Click(object sender, EventArgs e)
        {
            if (GlobalVariable.OrderComplete == false)
            {
                foreach (OrderDish.Model.OrderModel model in GlobalVariable.OrderList)
                {
                    if (model.Id == foodId)
                    {
                        MessageBox.Show(GlobalVariable.GetResxString("MSGBOX_Exist") + foodStrName);
                        return;
                    }

                }
                GlobalVariable.OrderList.Add(new Model.OrderModel(foodId, foodStrName, foodStrPrice, "1", ""));
                if (MessageBox.Show(GlobalVariable.GetResxString("MSGBOX_Add") + foodStrName, "操作", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                {
                    this.Dispose();
                }
            }
            else
            {
                MessageBox.Show(GlobalVariable.GetResxString("OrderComplete"));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (var backdrop = new Bitmap(Width, Height))
            {
                using (var gxOff = Graphics.FromImage(backdrop))
                {
                    gxOff.Clear(Color.Black);

                    if (!hadPaint)
                    {
                        e.Graphics.DrawAlpha(backdrop, 150, 0, 0);

                        hadPaint = true;
                    }
                    else
                    {
                        e.Graphics.DrawAlpha(backdrop, 0, 0, 0);
                    }
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e) { }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Common.CaptureAPI.DoScreenCaptureTOFile();
        }

        private void alpBtnReturn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }



    }
}