using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.WindowsCE.Forms;

namespace OrderDish.View
{
    public partial class LanguageChooseForm : Form
    {
        public LanguageChooseForm()
        {
            InitializeComponent();
        }

        private void LanguageChooseForm_Load(object sender, EventArgs e)
        {
            /////////// Don't show title bar and allocate the full screen ///////////////
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            SystemSettings.ScreenOrientation = ScreenOrientation.Angle90;
        }

        private void btnChinese_Click(object sender, EventArgs e)
        {
            GlobalVariable.LanguageChoose = 0;
            this.Dispose();
        }

        private void btnEnglish_Click(object sender, EventArgs e)
        {
            GlobalVariable.LanguageChoose = 1;
            this.Dispose();

        }
    }
}