using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.WindowsMobile.Status;
using System.Threading;

namespace OrderDish.Common
{
    class WIFIConnectCheck
    {
        #region Connection Manager Methods

        [DllImport("CellCore.dll")]
        static extern int ConnMgrMapURL(string url, ref Guid networkGuid, int passZero);
        [DllImport("CellCore.dll")]
        static extern int ConnMgrEstablishConnection(ConnMgrConnectionInfo connectionInfo, ref IntPtr connectionHandle);
        [DllImport("CellCore.dll")]
        static extern int ConnMgrReleaseConnection(IntPtr connectionHandle, int cache);
        [DllImport("CellCore.dll")]
        static extern int ConnMgrConnectionStatus(IntPtr connectionHandle, ref int status);

        #endregion

        private string url = "http://198.168.0.253/testfiletran";

        public string Url
        {
            get { return url; }
            set { url = value;}
        } 


        IntPtr _connectionHandle = IntPtr.Zero;

        SystemState _cellularConnectCount;

        public WIFIConnectCheck()
        {
            _cellularConnectCount = new SystemState(SystemProperty.ConnectionsCellularCount);
            _cellularConnectCount.Changed += new ChangeEventHandler(CellularConnectCount_Changed);
        }

        public bool ConnectTest()
        {
            Guid networkGuid = Guid.Empty;
            ConnMgrMapURL(url, ref networkGuid, 0);
            ConnMgrConnectionInfo info = new ConnMgrConnectionInfo(networkGuid, false);
            ConnMgrEstablishConnection(info, ref _connectionHandle);
            Thread.Sleep(100);
            int status = 0;
            ConnMgrConnectionStatus(_connectionHandle, ref status);
            string statusText = GetStatusText(status);
            if (statusText == "Connected")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetStatusTest()
        {
            int status = 0;
            ConnMgrConnectionStatus(_connectionHandle, ref status);
            return  GetStatusText(status);
        }

        #region Display Handling

        string GetStatusText(int status)
        {
            string statusText;

            switch (status)
            {
                case (int)ConnMgrStatus.Unknown:
                    statusText = "Unknown";
                    break;
                case (int)ConnMgrStatus.Connected:
                    statusText = "Connected";
                    break;
                case (int)ConnMgrStatus.ConnectionFailed:
                    statusText = "Connection Failed";
                    break;
                case (int)ConnMgrStatus.PhoneOff:
                    statusText = "Phone radio is off";
                    break;
                case (int)ConnMgrStatus.WaitingConnection:
                    statusText = "Attempting to connect";
                    break;
                default:
                    statusText = string.Format("Status = {0:x}", status);
                    break;
            }
            return statusText;
        }

        #endregion


        void CellularConnectCount_Changed(object sender, ChangeEventArgs args)
        {
            int count = (int)args.NewValue;
            string displayMessage = count > 0 ? "Cellular Connected" :
                "Cellular DISConnected";
        }
    }

    #region Connection Manager Types

    [StructLayout(LayoutKind.Sequential)]
    class ConnMgrConnectionInfo
    {
        public ConnMgrConnectionInfo(Guid destination, bool useProxy)
        {
            cbSize = Marshal.SizeOf(typeof(ConnMgrConnectionInfo));
            dwParams = ConnMgrParam.GuidDestNet;
            dwFlags = useProxy ? ConnMgrProxy.Http : 0;
            dwPriority = ConnMgrPriority.UserInteractive;
            guidDestNet = destination;
        }
        Int32 cbSize;                   // DWORD
        ConnMgrParam dwParams;          // DWORD
        ConnMgrProxy dwFlags;           // DWORD
        ConnMgrPriority dwPriority;     // DWORD
        Int32 bExclusive = 0;           // BOOL
        Int32 bDisabled = 0;            // BOOL
        Guid guidDestNet = Guid.Empty;  // GUID
        IntPtr hWnd = IntPtr.Zero;      // HWND
        UInt32 uMsg = 0;                // UINT
        Int32 lParam = 0;               // LPARAM
        UInt32 ulMaxCost = 0;           // ULONG
        UInt32 ulMinRcvBw = 0;          // ULONG
        UInt32 ulMaxConnLatency = 0;    // ULONG 
    } ;


    [Flags]
    enum ConnMgrParam : int
    {
        GuidDestNet = 0x1,
        MaxCost = 0x2,
        MinRcvBw = 0x4,
        MaxxConnLatency = 0x8
    }

    [Flags]
    enum ConnMgrProxy : int
    {
        Http = 0x1,
        Wap = 0x2,
        Socks4 = 0x4,
        Socks5 = 0x8
    }

    [Flags]
    enum ConnMgrPriority
    {
        UserInteractive = 0x8000,
        UserBackground = 0x2000,
        UserIdle = 0x0800
    }

    enum ConnMgrStatus
    {
        Unknown = 0x00,
        Connected = 0x10,
        ConnectionFailed = 0x21,
        PhoneOff = 0x27,
        WaitingConnection = 0x40
    }

    #endregion
}
