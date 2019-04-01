using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AlphaControls
{
    public class PictureBoxPlusEvent
    {
        public delegate void AlphaButtonClickDelegate(object sender,EventArgs e);

        public static event AlphaButtonClickDelegate OnAlpBtnClick;

        public static void DoOnAlpBtnClick(object sender, EventArgs e)
        {
            OnAlpBtnClick(sender, e);
        }
    }
}
