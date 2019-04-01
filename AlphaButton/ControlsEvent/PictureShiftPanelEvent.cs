using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AlphaControls
{
    public class PictureShiftPanelEvent
    {
        /// <summary>
        /// 委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void PicturnTurnDelegate(object sender, EventArgs e);
        /// <summary>
        /// 事件
        /// </summary>
        public static event PicturnTurnDelegate OnPictureSelectChange;
        /// <summary>
        /// 事件处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DoOnPicturnChange(object sender, EventArgs e)
        {
            OnPictureSelectChange(sender, e);
        }

        /// <summary>
        /// 委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void PicturnSelectDelegate(object sender);
        /// <summary>
        /// 事件
        /// </summary>
        public static event PicturnSelectDelegate OnPictureSelect;
        /// <summary>
        /// 事件处理函数:{用于LIST和滑动控件的交互}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DoOnPicturnSelect(object sender)
        {
            OnPictureSelect(sender);
        }

        /// <summary>
        /// 委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void ButtonBuyDelegate(object sender);
        /// <summary>
        /// 事件
        /// </summary>
        public static event ButtonBuyDelegate OnButtonBuyClick;
        /// <summary>
        /// 事件处理函数:{list控件上的按钮单击事件触发}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DoOnButtonBuyClick(object sender)
        {
            OnButtonBuyClick(sender);
        }
    }
}
