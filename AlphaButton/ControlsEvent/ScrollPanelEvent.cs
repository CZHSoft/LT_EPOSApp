using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AlphaControls
{
    public class ScrollPanelEvent
    {
        /// <summary>
        /// 委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void VScrollBarDelegate(object sender, EventArgs e);
        /// <summary>
        /// 事件
        /// </summary>
        public static event VScrollBarDelegate OnVScrollBarValueChange;
        /// <summary>
        /// 事件处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DoOnVScrollBarValueChange(object sender, EventArgs e)
        {
            OnVScrollBarValueChange(sender, e);
        }
    }
}
