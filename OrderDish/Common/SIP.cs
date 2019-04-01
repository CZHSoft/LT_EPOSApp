using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace OrderDish.Common
{
    public class SIP
    {
        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;
        private const int GW_CHILD = 5;


        /// <summary>
        /// Shows the SIP (Software Input Panel) button.0:Hide,1:show
        /// </summary>
        /// <param name="nShowOrHide"></param>
        public static void ShowHideSIP(int nShowOrHide)
        {
            IntPtr hSipWindow = PInvokeAPI.FindWindow("MS_SIPBUTTON", "MS_SIPBUTTON");
            if (hSipWindow != IntPtr.Zero)
            {
                IntPtr hSipButton = PInvokeAPI.GetWindow(hSipWindow, GW_CHILD);
                if (hSipButton != IntPtr.Zero)
                {
                    bool res = PInvokeAPI.ShowWindow(hSipButton, nShowOrHide);
                }
            }
        }
    }
}
