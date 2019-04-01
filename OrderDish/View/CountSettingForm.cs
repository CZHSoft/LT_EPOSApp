using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.WindowsCE.Forms;
using Microsoft.Drawing;

namespace OrderDish.View
{
    public partial class CountSettingForm : Form
    {
        public event GetNewCount OnGetNewCount;
        public delegate void GetNewCount(string newCount);

        private bool hadPaint = false;

        public CountSettingForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (OnGetNewCount != null)
            {
                OnGetNewCount(nupCount.Value.ToString());
            }
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void CountSettingForm_Load(object sender, EventArgs e)
        {
            /////////// Don't show title bar and allocate the full screen ///////////////
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.ControlBox = false;
            //this.Menu = null;
            //this.WindowState = FormWindowState.Maximized;

            //FullScreenApp.FullScreen(this.Handle, true);
            SystemSettings.ScreenOrientation = ScreenOrientation.Angle90;
            //Common.SIP.ShowHideSIP(0);
            /////////////////////////////////////////////////////////////////////////////
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Common.CaptureAPI.DoScreenCaptureTOFile();
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
    }
}