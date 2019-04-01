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
    public partial class RemarkSettingForm : Form
    {
        public event GetRemark OnGetRemark;
        public delegate void GetRemark(string newRemark);


        public RemarkSettingForm(string r)
        {
            InitializeComponent();
            tbRemark.Text = r;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (OnGetRemark != null)
            {
                OnGetRemark(tbRemark.Text);
            }
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void RemarkSettingForm_Load(object sender, EventArgs e)
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

            inputPanel.Enabled = false;
            
            label1.Text = GlobalVariable.GetResxString("RemarkSettingForm_laber");
            btnSave.Text = GlobalVariable.GetResxString("RemarkSettingForm_btnSave");
            btnCancel.Text = GlobalVariable.GetResxString("RemarkSettingForm_btnCancel");
        }

        private void tbRemark_GotFocus(object sender, EventArgs e)
        {
            inputPanel.Enabled = true;
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Common.CaptureAPI.DoScreenCaptureTOFile();
        }

    }
}