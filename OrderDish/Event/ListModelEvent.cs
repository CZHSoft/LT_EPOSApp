using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace OrderDish.Event
{
    public class ListModelEvent
    {
        /// <summary>
        /// 委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void ListModelDelegate(object sender);
        /// <summary>
        /// 事件
        /// </summary>
        public static event ListModelDelegate OnListModelBuyClick;
        /// <summary>
        /// 事件处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DoOnListModelBuyClick(object sender)
        {
            OnListModelBuyClick(sender);
        }
    }
}
