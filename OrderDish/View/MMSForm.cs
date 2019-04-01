using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OrderDish.View
{
    public partial class MMSForm : Form
    {
        List<string> strList;
        public MMSForm()
        {
            InitializeComponent();
        }

        private void MMSForm_Load(object sender, EventArgs e)
        {
            //WMP.URL = GlobalVariable.LocalPath + "\\foo.wmv";
            //WMP.uiMode = "None";

            lvFile.Items.Clear();
            lvFile.View = System.Windows.Forms.View.Details;
            lvFile.Columns.Add("播放文件", 150, HorizontalAlignment.Center);

            strList = new List<string>();
            strList.Clear();

            InitializeUI();

        }

        private void InitializeUI()
        {
            strList = Common.HttpFileControl.GetPathFileList(GlobalVariable.HttpPath);

            if (strList.Count > 0)
            {
                lvFile.Items.Clear();

                foreach (string str in strList)
                {
                    ListViewItem lvi = new ListViewItem(str);
                    lvFile.Items.Add(lvi);
                }
            }
        }

        private void WMP_StatusChange(object sender, EventArgs e)
        {
            if (WMP.status == "已停止")
            {
                WMP.uiMode = "Mini";
            }
            else
            {
                WMP.uiMode = "None";
            }
        }

        private void btnExample_Click(object sender, EventArgs e)
        {
            //WMP.URL = "http://198.168.0.253/testfiletran/chicken.wmv";
            if (lvFile.SelectedIndices.Count == 1)
            {
                WMP.URL = GlobalVariable.HttpPath + "//" + lvFile.Items[lvFile.SelectedIndices[0]].SubItems[0].Text;
            }
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Common.CaptureAPI.DoScreenCaptureTOFile();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


    }
}