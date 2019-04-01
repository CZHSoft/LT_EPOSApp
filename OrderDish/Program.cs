using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using OrderDish.View;
using System.Data;
using System.Text.RegularExpressions;

namespace OrderDish
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [MTAThread]
        static void Main(string[] args)
        {

            string launcherWindowName = (args != null & args.Length > 0) ? args[0] : string.Empty;
            InitForm initForm = new InitForm(launcherWindowName);
            initForm.ShowDialog();
            LanguageChooseForm languageChooseForm = new LanguageChooseForm();
            languageChooseForm.ShowDialog();
            Application.Run(new MainForm());
        }

    }
}