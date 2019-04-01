using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using OrderDish.Resources;

namespace OrderDish
{
    class GlobalVariable
    {
        private static string localPath;

        public static string LocalPath
        {
            get 
            {
                if (localPath == null)
                {
                    localPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
                }
                return GlobalVariable.localPath; 
            }
            set { GlobalVariable.localPath = value; }
        }
        
        private static bool orderComplete=false;

        public static bool OrderComplete
        {
            get { return GlobalVariable.orderComplete; }
            set { GlobalVariable.orderComplete = value; }
        }

        private static int languageChoose = 0;

        public static int LanguageChoose
        {
            get { return GlobalVariable.languageChoose; }
            set { GlobalVariable.languageChoose = value; }
        }

        public static string GetResxString(string stringName)
        {
            if (GlobalVariable.LanguageChoose == 0)
            {
                return Chinese.ResourceManager.GetString(stringName);
            }
            else
            {
                return English.ResourceManager.GetString(stringName);
            }
        }

        public static List<Model.OrderModel> OrderList = new List<OrderDish.Model.OrderModel>();

        private static string deviceID;

        public static string DeviceID
        {
            get { return GlobalVariable.deviceID; }
            set { GlobalVariable.deviceID = value; }
        }

        private static string webServicePath;

        public static string WebServicePath
        {
            get { return GlobalVariable.webServicePath; }
            set { GlobalVariable.webServicePath = value; }
        }

        private static string httpPath;

        public static string HttpPath
        {
            get { return GlobalVariable.httpPath; }
            set { GlobalVariable.httpPath = value; }
        }

        private static string orderID;

        public static string OrderID
        {
            get { return GlobalVariable.orderID; }
            set { GlobalVariable.orderID = value; }
        }

        private static string[] mp3Files;

        public static string[] Mp3Files
        {
            get 
            {
                if (mp3Files == null)
                {
                    return null;
                }
                else
                {
                    return GlobalVariable.mp3Files;
                }
            }
            set { GlobalVariable.mp3Files = value; }
        }
    }
}
