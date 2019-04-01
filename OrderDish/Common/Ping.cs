using System;

using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Net;

namespace OrderDish.Common
{
    public class Ping : IDisposable
    {
        #region Definition
        private const int DefaultSendBufferSize = 64;
        private const int GMEM_FIXED = 0x0000;
        private const int LMEM_ZEROINIT = 0x0040;
        private const int LPTR = (GMEM_FIXED | LMEM_ZEROINIT);

        private IntPtr handle = IntPtr.Zero;
        private IntPtr replyBuffer;
        private IntPtr requestBuffer;
        private bool disposed;

        private byte[] defaultSendBuffer;
        private byte[] SendBuffer
        {
            get
            {
                if (defaultSendBuffer == null)
                {
                    defaultSendBuffer = new byte[DefaultSendBufferSize];
                    for (int i = 1; i < DefaultSendBufferSize; i++)
                    {
                        defaultSendBuffer[i] = (byte)i;
                    }
                }
                return defaultSendBuffer;
            }
        }
        #endregion

        #region interfaces
        public IcmpEchoReply Send(string uri)
        {
            return Send(uri, 2000);
        }

        public IcmpEchoReply Send(string uri, int timeout)
        {
            if ("".CompareTo(uri) >= 0)
			{
				throw new ArgumentNullException("uri");
			}
            IPAddress address;
            try
			{
				address = IPAddress.Parse(uri);
			}
			catch
			{
				try
				{
                    IPHostEntry entry = Dns.GetHostEntry(uri);
					address = entry.AddressList[0];
				}
				catch(Exception e)
				{
					throw new Exception("Impossible to resolve host or address.", e);
				}
			}
            return Send(address, timeout);
        }

        public IcmpEchoReply Send(IPAddress address)
        {
            return Send(address, 2000);
        }

        public IcmpEchoReply Send(IPAddress address, int timeout)
        {
            if (handle == IntPtr.Zero)
            {
                handle = IcmpCreateFile();
            }

            if (replyBuffer == IntPtr.Zero)
            {
                replyBuffer = LocalAlloc(LPTR, (uint)0xffff);
            }

            requestBuffer = LocalAlloc(LPTR, (uint)SendBuffer.Length);
            Marshal.Copy(SendBuffer, 0, requestBuffer, SendBuffer.Length);

            uint ip = BitConverter.ToUInt32(address.GetAddressBytes(), 0);

            IPOptions option = new IPOptions(null as PingOptions);

            IcmpSendEcho2(handle, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, ip, requestBuffer, (ushort)SendBuffer.Length, ref option, replyBuffer, 0xffff, (uint)timeout);
            if (requestBuffer != IntPtr.Zero)
            {
                LocalFree(requestBuffer);
                requestBuffer = IntPtr.Zero;
            }
            return Marshal.PtrToStructure(replyBuffer, typeof(IcmpEchoReply)) as IcmpEchoReply;
        }
        #endregion

        #region Disposing
        protected void Dispose()
        {
            if (disposed)
            {
                return;
            }
            disposed = true;

            if (this.replyBuffer != IntPtr.Zero)
            {
                LocalFree(replyBuffer);
                replyBuffer = IntPtr.Zero;
            }
            if (this.handle != IntPtr.Zero)
            {
                IcmpCloseHandle(handle);
                handle = IntPtr.Zero;
            }
        }

        void System.IDisposable.Dispose()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }

        ~Ping()
        {
            Dispose();
        }
        #endregion

        #region P/Invoke
        [DllImport("iphlpapi.dll")]
        internal static extern IntPtr IcmpCreateFile();
        
        [DllImport("iphlpapi.dll")]
        internal static extern uint IcmpSendEcho2(IntPtr icmpHandle, IntPtr Event, IntPtr apcRoutine, IntPtr apcContext, uint ipAddress, IntPtr data, ushort dataSize, ref IPOptions options, IntPtr replyBuffer, uint replySize, uint timeout);

        [DllImport("iphlpapi")]
        internal static extern bool IcmpCloseHandle(IntPtr handle);

        [DllImport("coredll.dll", EntryPoint = "LocalAlloc", SetLastError = true)]
        static extern IntPtr LocalAlloc(uint uFlags, uint Bytes);

        [DllImport("coredll.dll", EntryPoint = "LocalFree", SetLastError = true)]
        static extern IntPtr LocalFree(IntPtr hMem);
        #endregion
    }

    public class IcmpEchoReply
    {
        internal uint address = 0;
        internal uint status = 0;
        internal uint roundTripTime = 0;
        internal ushort dataSize = 0;
        internal ushort reserved = 0;
        internal IntPtr data = IntPtr.Zero;
        /* IPOptions structure */
        internal byte ttl = 0;
        internal byte tos = 0;
        internal byte flags = 0;
        internal byte optionsSize = 0;
        internal IntPtr optionsData = IntPtr.Zero;
    }

    /// <summary>
    /// Reports the status of sending an Internet Control Message Protocol (ICMP) echo message to a computer.
    /// </summary>
    public enum IPStatus
    {
        BadDestination = 0x2b0a,
        BadHeader = 0x2b22,
        BadOption = 0x2aff,
        BadRoute = 0x2b04,
        DestinationHostUnreachable = 0x2afb,
        DestinationNetworkUnreachable = 0x2afa,
        DestinationPortUnreachable = 0x2afd,
        DestinationProhibited = 0x2afc,
        DestinationProtocolUnreachable = 0x2afc,
        DestinationScopeMismatch = 0x2b25,
        DestinationUnreachable = 11040,
        HardwareError = 0x2b00,
        IcmpError = 0x2b24,
        NoResources = 0x2afe,
        PacketTooBig = 0x2b01,
        ParameterProblem = 0x2b07,
        SourceQuench = 0x2b08,
        Success = 0,
        TimedOut = 11010,
        TimeExceeded = 0x2b21,
        TtlExpired = 0x2b05,
        TtlReassemblyTimeExceeded = 0x2b06,
        Unknown = -1,
        UnrecognizedNextHeader = 0x2b23
    }

    /// <summary>
    /// The ip_option_information structure describes the options to be
    /// included in the header of an IP packet. The TTL, TOS, and Flags
    /// values are carried in specific fields in the header. The OptionsData
    /// bytes are carried in the options area following the standard IP header.
    /// With the exception of source route options, this data must be in the
    /// format to be transmitted on the wire as specified in RFC 791. A source
    /// route option should contain the full route - first hop thru final
    /// destination - in the route data. The first hop will be pulled out of the
    /// data and the option will be reformatted accordingly. Otherwise, the route
    /// option should be formatted as specified in RFC 791.
    /// </summary>
    internal struct IPOptions
    {
        internal IPOptions(PingOptions options)
        {
            ttl = 0x80;
            tos = 0;
            optionsSize = 0;
            flags = 0;
            optionsData = IntPtr.Zero;

            if (options != null)
            {
                this.ttl = (byte)options.Ttl;
                if (options.DontFragment)
                {
                    this.flags = DontFragmentFlag;
                }
            }
        }

        /// <summary>
        /// Time To Live.
        /// </summary>
        internal byte ttl;
        /// <summary>
        /// Type Of Service.
        /// </summary>
        internal byte tos;
        /// <summary>
        /// IP header flags.
        /// </summary>
        internal byte flags;
        /// <summary>
        /// Size in bytes of options data.
        /// </summary>
        internal byte optionsSize;
        internal IntPtr optionsData;

        internal const int DontFragmentFlag = 2;
        internal const int IPOptionsSize = 8;
    }

    /// <summary>
    /// Used to control how Ping data packets are transmitted.
    /// </summary>
    public class PingOptions
    {
        int ttl = 128;
        bool dontFragment;

        /// <summary>
        /// Gets or sets the number of routing nodes that can forward the Ping data before it is discarded.
        /// </summary>
        public int Ttl { set; get; }

        /// <summary>
        /// Gets or sets a Boolean value that controls fragmentation of the data sent to the remote host.
        /// </summary>
        public bool DontFragment { set; get; }
       
        /// <summary>
        /// Initializes a new instance of the PingOptions class.
        /// </summary>
        public PingOptions()
        {
        }

        internal PingOptions(IcmpEchoReply reply)
        {
            ttl = reply.ttl;
            dontFragment = (reply.flags & IPOptions.DontFragmentFlag) > 0;
        }

        /// <summary>
        /// Initializes a new instance of the PingOptions class and sets the Time to Live and fragmentation values.
        /// </summary>
        /// <param name="ttl">Specifies the number of times the Ping data packets can be forwarded.</param>
        /// <param name="dontFragment">True to prevent data sent to the remote host from being fragmented; otherwise, false.</param>
        public PingOptions(int ttl, bool dontFragment)
        {
            if (ttl <= 0)
            {
                throw new ArgumentOutOfRangeException("ttl");
            }

            this.ttl = ttl;
            this.dontFragment = dontFragment;
        }
    }
}
