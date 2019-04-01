using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AlphaControls
{
    public class FormManageEvent
    {
        #region Form close action
        /// <summary>
        /// 委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void FormCloseDelegate();
        /// <summary>
        /// 事件
        /// </summary>
        public static event FormCloseDelegate OnFormClose;
        /// <summary>
        /// 事件处理函数:{用于触发关闭窗体操作}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DoOnFormClose()
        {
            OnFormClose();
        }
        #endregion

        #region Form to Form Transmit
        public delegate void F2FTransmitDelegate(object sender);
        public static event F2FTransmitDelegate OnF2FTransmit;
        public static void DoOnF2FTransmit(object sender)
        {
            OnF2FTransmit(sender);
        }
        #endregion
    }
}
