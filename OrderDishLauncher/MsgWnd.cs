using System.Collections.Generic;
using Microsoft.WindowsCE.Forms;

namespace OrderDishLauncher
{
    /// <summary>
    /// This class is used to capture Windows messages that are send to us by the main application.
    /// These messages contain status information about the initialization phase of the main application,
    /// but also a request to close down this application is send from the main application, once it is
    /// completely initialized.
    /// </summary>
    class MsgWnd : MessageWindow
    {
        private const int WM_USER = 0x0400;
        public const int WM_STATUSUPDATE = WM_USER;
        public const int WM_DONE = WM_USER + 1;

        private SplashScreen ourForm;

        private Dictionary<int, string> statusMessageKeys;

        public MsgWnd(SplashScreen destForm, string windowName)
        {
            ourForm = destForm;
            this.Text = windowName;

            statusMessageKeys = new Dictionary<int, string>();
            statusMessageKeys.Add(1, "初始化数据库...");
            statusMessageKeys.Add(2, "检查网络连通性...");
            statusMessageKeys.Add(3, "检查设备合法性...");
            statusMessageKeys.Add(4, "检查更新...");
            statusMessageKeys.Add(5, "初始化完成！");

            statusMessageKeys.Add(11, "初始化数据库失败！");
            statusMessageKeys.Add(12, "网络连接失败！");
            statusMessageKeys.Add(13, "非法设备,请联系管理员...");
            statusMessageKeys.Add(14, "检查更新失败！");
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_STATUSUPDATE:
                    string statusMsgId = statusMessageKeys[(int)m.LParam];
                    //string statusMsg = Properties.Resources.ResourceManager.GetString(statusMsgId);
                    //string statusMsg = m.LParam.ToString();
                    ourForm.UpdateStatus(statusMsgId, (int)m.LParam);
                    
                    break;
                case WM_DONE:
                    ourForm.Close();
                    break;
            }

            base.WndProc(ref m);
        }
    }
}
