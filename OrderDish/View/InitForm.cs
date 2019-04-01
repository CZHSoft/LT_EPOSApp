using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using OrderDish.Common;
using OrderDish.Action;
using System.Diagnostics;
using Microsoft.Drawing;


namespace OrderDish.View
{
    public partial class InitForm : Form
    {

        private IntPtr _launchWindow = IntPtr.Zero;
        private bool _isFirstTime = true;
        private bool CheckNormal = true;
        //private Rectangle splashRegion = new Rectangle(0, 0, 0, 0);
        //private bool hadPaint = false;

        public InitForm(string launchWindowName)
        {

            InitializeComponent();

            if (!String.IsNullOrEmpty(launchWindowName))
            {
                _launchWindow = Native.FindWindow(null, launchWindowName);
            }

            //this.Hide();
        }

        private void InitForm_Load(object sender, EventArgs e)
        {
            this.Text = "";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.Menu = null;

            //splashRegion.X = (Screen.PrimaryScreen.Bounds.Width - Properties.Resources.logo.Width) / 2;
            //splashRegion.Y = (Screen.PrimaryScreen.Bounds.Height - Properties.Resources.logo.Height) / 2;
            //splashRegion.Width = Properties.Resources.logo.Width;
            //splashRegion.Height = Properties.Resources.logo.Height;

            //pbLogo.Image = Properties.Resources.logo;
            //pbLogo.Location = new System.Drawing.Point(splashRegion.X, splashRegion.Y);
            //pbLogo.Size = new System.Drawing.Size(splashRegion.Width, splashRegion.Height);

            //labelStatusMsg.Location = new Point(0, pbLogo.Location.Y + pbLogo.Size.Height);

            InitializeApplication();
        }

        /// <summary>
        /// This method is used to simulate a long lasting initialization. If we were started from a separate
        /// splash screen application, we will send that application messages about initialization progress.
        /// </summary>
        private void InitializeApplication()
        {
            InitializationAction initAction = new InitializationAction();

            initAction.DoCheckEnvironment();

            if (CheckNormal)
            {
                //初始化数据库...
                UpdateStatus(Native.StatusMsg.InitializingStage1);
                if (!initAction.DoDbInitialization())
                {
                    CheckNormal = false;
                    MessageBox.Show("初始化数据失败...");
                    UpdateStatus(Native.StatusMsg.InitializingStage11);
                    Application.Exit();
                }
            }

            if (CheckNormal)
            {
                //初始化数据库...
                UpdateStatus(Native.StatusMsg.InitializingStage1);
                if (!initAction.DoCheckEnvironment())
                {
                    CheckNormal = false;
                    MessageBox.Show("本地配置失败...");
                    UpdateStatus(Native.StatusMsg.InitializingStage11);
                    Application.Exit();
                }
            }

            if (CheckNormal)
            {
                //检查网络..
                UpdateStatus(Native.StatusMsg.InitializingStage2);
                if (!initAction.DoCheckNetWork())
                {
                    CheckNormal = false;
                    MessageBox.Show("访问远程服务器失败,请检查网络...");
                    UpdateStatus(Native.StatusMsg.InitializingStage12);
                    Application.Exit();
                }
            }

            if (CheckNormal)
            {
                //检查设备合法性...
                UpdateStatus(Native.StatusMsg.InitializingStage3);
                if (!initAction.DoVerifyDevice())
                {
                    CheckNormal = false;
                    MessageBox.Show("非法设备,请联系管理员...");
                    UpdateStatus(Native.StatusMsg.InitializingStage13);
                    Application.Exit();
                }
            }

            if (CheckNormal)
            {
                //检查更新...
                UpdateStatus(Native.StatusMsg.InitializingStage4);
                if (!initAction.DoCheckUpdate())
                {
                    CheckNormal = false;
                    MessageBox.Show("数据更新失败...");
                    UpdateStatus(Native.StatusMsg.InitializingStage14);
                    Application.Exit();
                }
            }

            if (CheckNormal)
            {
                UpdateStatus(Native.StatusMsg.InitializingStage5);
                this.Dispose();
                //MainForm mf = new MainForm();
                //mf.ShowDialog();

                //LanguageChooseForm lcf = new LanguageChooseForm();
                //lcf.ShowDialog();

            }
            else
            {
                Application.Exit();
            }
            
        }

        /// <summary>
        /// Notifies the launcher windows of a status changes and passes the new status code to it.
        /// </summary>
        /// <param name="status">new status to be shown in the splash screen</param>
        private void UpdateStatus(Native.StatusMsg msg)
        {
            // Only if we have a valid handle ...
            if (_launchWindow != IntPtr.Zero)
            {
                // ... send the message to the splash screen.
                Native.SendMessage(_launchWindow, Native.WM_STATUSUPDATE, 0, (int)msg);
                //Application.DoEvents();
            }

        }

        private void InitForm_Activated(object sender, EventArgs e)
        {
            // Shut down the splash screen only on first time activate
            if (_isFirstTime)
            {
                // Only if we have a valid handle
                if (_launchWindow != IntPtr.Zero)
                {
                    Native.SendMessage(_launchWindow, Native.WM_DONE, 0, 0);
                }
            }

            _isFirstTime = false;
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    //
        //    if (!hadPaint)
        //    {
        //        using (var backdrop = new Bitmap(Width, Height))
        //        {
        //            using (var gxOff = Graphics.FromImage(backdrop))
        //            {
        //                gxOff.Clear(Color.Black);
        //                e.Graphics.DrawAlpha(backdrop, 0, 0, 0);
        //                hadPaint = true;
        //            }
        //        }
        //    }
        //}

        //protected override void OnPaintBackground(PaintEventArgs e) { }




    }
}