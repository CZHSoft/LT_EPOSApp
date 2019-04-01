using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AlphaControls
{
    public class SplashControlEvent
    {
        /// <summary>
        /// 委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void SplashControlDelegate(object sender, EventArgs e);
        /// <summary>
        /// 事件
        /// </summary>
        public static event SplashControlDelegate OnSplashControlTextChange;
        /// <summary>
        /// 事件处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DoOnTextChange(object sender, EventArgs e)
        {
            OnSplashControlTextChange(sender, e);
        }
    }
}
