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
    public partial class ScrollPanel : UserControl, IDisposable
    {
        public ScrollPanel()
        {
            InitializeComponent();
            vScrollBar.Minimum = 0;
            ScrollPanelEvent.OnVScrollBarValueChange += new ScrollPanelEvent.VScrollBarDelegate(VScrollBarEvent_OnVScrollBarValueChange);
        }

        private int vScrollBarMaximum;

        public int VScrollBarMaximum
        {
            get { return vScrollBarMaximum; }
            set { vScrollBarMaximum = value; }
        }


        public void FillControlsToPanelMenu(Control control)
        {
            panelMenu.Controls.Add(control);
        }


        public int PanelMenuHeight
        {
            get { return panelMenu.Height; }
        }

        /// <summary>
        /// 事件处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="evt"></param>
        private void VScrollBarEvent_OnVScrollBarValueChange(object sender, EventArgs e)
        {
            if (sender.ToString() == "aTBtnUP")
            {
                vScrollBar.Value -= 10;
            }
            if (sender.ToString() == "aTBtnDown")
            {
                vScrollBar.Value += 10;
            }
        }

        private void vScrollBar_ValueChanged(object sender, EventArgs e)
        {
            panelMenu.Top = -vScrollBar.Value;
        }
    }
}
