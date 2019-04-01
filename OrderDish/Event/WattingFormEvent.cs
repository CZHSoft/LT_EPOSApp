using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace OrderDish.Event
{
    public class WattingFormEvent
    {
        /// <summary>
        /// 委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void WaittingFormDelegate();
        /// <summary>
        /// 事件
        /// </summary>
        public static event WaittingFormDelegate OnWaittingFormClose;
        /// <summary>
        /// 事件处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DoOnWaittingFormClose()
        {
            OnWaittingFormClose();
        }
    }
}
