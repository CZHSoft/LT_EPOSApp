using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace OrderDish.Common
{
    public class DeviceUniqueID
    {
        [DllImport("coredll.dll")]
        private extern static int GetDeviceUniqueID([In, Out] byte[] appdata,
                                                    int cbApplictionData,
                                                    int dwDeviceIDVersion,
                                                    [In, Out] byte[] deviceIDOuput,
                                                    out uint pcbDeviceIDOutput);
        private byte[] GetDeviceID(string AppString)
        {
            // Call the GetDeviceUniqueID
            byte[] AppData = new byte[AppString.Length];
            for (int count = 0; count < AppString.Length; count++)
                AppData[count] = (byte)AppString[count];
            int appDataSize = AppData.Length;
            byte[] DeviceOutput = new byte[20];
            uint SizeOut = 20;
            GetDeviceUniqueID(AppData, appDataSize, 1, DeviceOutput, out SizeOut);
            return DeviceOutput;
        }

        public string GetDeviceID()
        {
            byte[] buffer = GetDeviceID("MyAppString");
            StringBuilder sb = new StringBuilder();
            for (int x = 0; x < buffer.Length; x++)
            {
                sb.Append(string.Format("{0:x2}", buffer[x]));
            }
            return sb.ToString();
        }
    }
}
