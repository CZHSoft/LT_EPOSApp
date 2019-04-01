using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.WindowsCE.Forms;
using System.Reflection;
using OrderDish.Common;
using System.Threading;
using System.IO;
using OrderDish.WebReference;

namespace OrderDish.View
{
    public partial class MainForm : Form
    {

        private Bitmap backgroundBitmap;

        //private Common.SoundPlayer soundPlayer;
        //private Thread threadPlayer = null;

        public MainForm()
        {

            InitializeComponent();
            
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            //GlobalVariable.Mp3Files = Directory.GetFiles(GlobalVariable.LocalPath + "/mp3", "*.mid");
            //soundPlayer = new SoundPlayer();
            //threadPlayer = new Thread(PlayMp3);
            //threadPlayer.IsBackground = true;
            //threadPlayer.Start();

            this.InitializeUI();


        }

        //private void PlayMp3()
        //{
        //    if (GlobalVariable.Mp3Files != null)
        //    {
        //        if (GlobalVariable.Mp3Files.Length > 0)
        //        {
        //            soundPlayer.SoundLocation = GlobalVariable.Mp3Files[0];
        //            soundPlayer.PlayLooping();
        //        }
        //    }
            
        //}

        private void InitializeUI()
        {
            //窗体属性设置
            /////////// Don't show title bar and allocate the full screen ///////////////
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.ControlBox = false;
            //this.WindowState = FormWindowState.Maximized;
            //this.Menu = null;

            //FullScreenApp.FullScreen(this.Handle,true);
            SystemSettings.ScreenOrientation = ScreenOrientation.Angle90;
            //Common.SIP.ShowHideSIP(0);
            /////////////////////////////////////////////////////////////////////////////

            //资源初始化
            /////////////////////////////////////////////////////////////////////////////
            //initBackgroundImage(GlobalVariable.LocalPath + "\\picture\\background2.jpg");
            backgroundBitmap = Properties.Resources.background2;
            /////////////////////////////////////////////////////////////////////////////

            //控件初始化
            /////////////////////////////////////////////////////////////////////////////
            gfbtnLeft.StartColor = Color.WhiteSmoke;
            gfbtnLeft.EndColor = Color.MediumBlue;
            gfbtnLeft.FillDirection = AlphaControls.GradientFill.FillDirection.TopToBottom;
            gfbtnLeft.Enabled = false;
            gfbtnLeft.FontState = true;

            this.alpBtnExit.Text = GlobalVariable.GetResxString("MainForm_btnExit");
            this.alpBtnOrder.Text = GlobalVariable.GetResxString("MainForm_btnOrder");
            this.alpBtnList.Text = GlobalVariable.GetResxString("MainForm_btnList");
            
            this.alpBtnMMS.BackgroundImage = backgroundBitmap;
            this.alpBtnList.BackgroundImage = backgroundBitmap;
            this.alpBtnOrder.BackgroundImage = backgroundBitmap;
            this.alpBtnExit.BackgroundImage = backgroundBitmap;

            /////////////////////////////////////////////////////////////////////////////
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (var backdrop = new Bitmap(Width, Height))
            {
                using (var gxOff = Graphics.FromImage(backdrop))
                {
                    gxOff.Clear(this.BackColor);
                    gxOff.DrawImage(backgroundBitmap, 0, 0);
                    e.Graphics.DrawImage(backdrop, 0, 0);
                }
            }
        }
        protected override void OnPaintBackground(PaintEventArgs e) { }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Common.CaptureAPI.DoScreenCaptureTOFile();
        }

        private void alpBtnList_Click(object sender, EventArgs e)
        {
            FoodListForm vf = new FoodListForm();
            vf.ShowDialog();

            //this.Hide();
            //this.Show();

            //GC内存回收，暂时无用
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            //this.InitializeUI();
        }

        private void alpBtnMMS_Click(object sender, EventArgs e)
        {
            MMSForm mf = new MMSForm();
            mf.ShowDialog();
        }

        private void alpBtnOrder_Click(object sender, EventArgs e)
        {
            ReportForm rf = new ReportForm();
            rf.ShowDialog();

            //this.Hide();
            //this.Show();
        }

        private void alpBtnExit_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.OrderComplete)
            {
                Application.Exit();
            }
            else
            {
                Service s = new Service();
                if (s.CheckOrderState(GlobalVariable.OrderID))
                {
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show("菜单未完成，请上完菜后再操作!");
                }
                
            }
        }

        #region 抛弃的

        //public void initBackgroundImage(string path)
        //{
        //    backgroundBitmap = new Bitmap(path);

        //    //this.alphaButton1.BackgroundImage = backgroundBitmap;
        //    //this.alphaTestControl1.BackgroundImage = backgroundBitmap;
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    BLL.InitializationBLL init = new BLL.InitializationBLL();

        //    string insertVersionSQL = string.Format("insert into version(versionID,menuVersion) VALUES ({0}, '{1}')", "1", "1.0");
        //    //string insertVersionSQL = string.Format("safsaf{0}", "ko");
        //    if (init.RwLocalOS(insertVersionSQL))
        //    {
        //        MessageBox.Show("0");
        //    }

        //    string[] strmenu = new string[] { "热菜", "凉菜", "汤煲", "饮品" };

        //    for (int i = 0; i < strmenu.Length; i++)
        //    {
        //        string insertMenuSQL = string.Format("INSERT INTO Menu(menuName,menuVersion,pictureAddress) VALUES ('{0}', '{1}','{2}')",
        //                     strmenu[i], "1.0", "\\picture\\menu\\" + strmenu[i] + ".jpg");
        //        if (init.RwLocalOS(insertMenuSQL))
        //        {
        //            MessageBox.Show("1");
        //        }

        //    }

        //    string[] strfood1 = new string[] { "热菜1", "热菜2", "热菜3", "热菜4", "热菜5", "热菜6" };
        //    for (int i = 0; i < strfood1.Length; i++)
        //    {
        //        string insertFoodSQL = string.Format("INSERT INTO food (foodName,foodVersion,menuID,pictureAddress) VALUES ('{0}', '{1}',{2},'{3}')",
        //                  strfood1[i], "1.0", "1", "\\picture\\food\\1\\" + strfood1[i] + ".jpg");
        //        if (init.RwLocalOS(insertFoodSQL))
        //        {
        //            MessageBox.Show("2");
        //        }

        //    }

        //    string[] strfood2 = new string[] { "凉菜1", "凉菜2", "凉菜3", "凉菜4", "凉菜5", "凉菜6" };
        //    for (int i = 0; i < strfood2.Length; i++)
        //    {
        //        string insertFoodSQL = string.Format("INSERT INTO food (foodName,foodVersion,menuID,pictureAddress) VALUES ('{0}', '{1}',{2},'{3}')",
        //                  strfood2[i], "1.0", "2", "\\picture\\food\\2\\" + strfood2[i] + ".jpg");
        //        if (init.RwLocalOS(insertFoodSQL))
        //        {
        //            MessageBox.Show("3");
        //        }

        //    }

        //    string[] strfood3 = new string[] { "汤煲1", "汤煲2", "汤煲3", "汤煲4", "汤煲5", "汤煲6" };
        //    for (int i = 0; i < strfood3.Length; i++)
        //    {
        //        string insertFoodSQL = string.Format("INSERT INTO food (foodName,foodVersion,menuID,pictureAddress) VALUES ('{0}', '{1}',{2},'{3}')",
        //                  strfood3[i], "1.0", "3", "\\picture\\food\\3\\" + strfood3[i] + ".jpg");
        //        if (init.RwLocalOS(insertFoodSQL))
        //        {
        //            MessageBox.Show("4");
        //        }

        //    }

        //    string[] strfood4 = new string[] { "饮品1", "饮品2", "饮品3", "饮品4", "饮品5", "饮品6" };
        //    for (int i = 0; i < strfood4.Length; i++)
        //    {
        //        string insertFoodSQL = string.Format("INSERT INTO food (foodName,foodVersion,menuID,pictureAddress) VALUES ('{0}', '{1}',{2},'{3}')",
        //                  strfood4[i], "1.0", "4", "\\picture\\food\\4\\" + strfood4[i] + ".jpg");
        //        if (init.RwLocalOS(insertFoodSQL))
        //        {
        //            MessageBox.Show("5");
        //        }

        //    }
        //    MessageBox.Show("结束");
        //}
        #endregion

    }
}