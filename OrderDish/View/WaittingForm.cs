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
using System.Threading;

namespace OrderDish.View
{
    public partial class WaittingForm : Form
    {

        private Rectangle splashRegion = new Rectangle(0, 0, 0, 0);
        
        private string strWT = "";
        private Thread treadMsg = null;
        private bool runFlag = false;

        private bool hadPaint = false;

        private string strMsg;

        public string StrMsg
        {
            get
            {
                if (string.IsNullOrEmpty(strMsg))
                    strMsg = "Please wait...";
                return strMsg; 
            }
            set { strMsg = value; }
        }

        public WaittingForm()
        {
            InitializeComponent();
        }

        public void CloseTimer()
        {
            this.timerWatting.Enabled=false;
        }

        public void CloseThread()
        {
            try
            {
                treadMsg.Abort();
                treadMsg = null;
            }
            catch
            {

            }
        }

        private void WaittingForm_Load(object sender, EventArgs e)
        {
            /////////// Don't show title bar and allocate the full screen ///////////////
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.WindowState = FormWindowState.Maximized;

            SystemSettings.ScreenOrientation = ScreenOrientation.Angle90;
            /////////////////////////////////////////////////////////////////////////////
            splashRegion.X = (Screen.PrimaryScreen.Bounds.Width - this.labelMsg.Width) / 2;
            splashRegion.Y = (Screen.PrimaryScreen.Bounds.Height - this.labelMsg.Height) / 2;
            splashRegion.Width = this.labelMsg.Width;
            splashRegion.Height = this.labelMsg.Height;

            labelMsg.Text = StrMsg;
            //treadMsg = new Thread(ThreadRun);
            //treadMsg.IsBackground = true;
            //runFlag = true;
            //treadMsg.Start();

        }

        private void ThreadRun()
        {
            while (runFlag)
            {
                if (strWT.Length > 5)
                {
                    strWT = "";
                }
                strWT += ".";

                this.Invoke((EventHandler)delegate { labelMsg.Text = strMsg + strWT; });
                Thread.Sleep(1000);
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
                        
                    }
                }
        }

        protected override void OnPaintBackground(PaintEventArgs e) { }

        private void timerWatting_Tick(object sender, EventArgs e)
        {
            if (strWT.Length > 5)
            {
                strWT = "";
            }
            strWT += ".";
            labelMsg.Text = strMsg + strWT;
            labelMsg.Invalidate();
        }


    }
}