using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using Microsoft.Drawing;

namespace OrderDishLauncher
{
    public partial class SplashScreen : Form
    {
        private MsgWnd msgWnd;
        private string _targetAppPath = string.Empty;

        private Rectangle splashRegion = new Rectangle(0, 0, 0, 0);

        private TimeSpan tsStart;

        public SplashScreen()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Update the label on our form with messages coming from the main application.
        /// </summary>
        /// <param name="statusMsg">New status message to be displayed.</param>
        public void UpdateStatus(string statusMsg,int key)
        {
            labelStatusMsg.Text = statusMsg;

            if ((key == 11) || (key == 12) || (key == 13) || (key == 14))
            {
                Application.Exit();
            }
            if (key == 5)
            {
                Application.Exit();
            }

        }

        /// <summary>
        /// Either load the main application or bring it to the foreground.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SplashScreen_Load(object sender, EventArgs e)
        {
            //
            this.Text = "";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            //this.ControlBox = false;
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.WindowState = FormWindowState.Maximized;
            //this.Menu = null;

            splashRegion.X = (Screen.PrimaryScreen.Bounds.Width - Properties.Resources.logo.Width) / 2;
            splashRegion.Y = (Screen.PrimaryScreen.Bounds.Height - Properties.Resources.logo.Height) / 2;
            splashRegion.Width = Properties.Resources.logo.Width;
            splashRegion.Height = Properties.Resources.logo.Height;

            pbLogo.Image = Properties.Resources.logo;
            pbLogo.Location = new System.Drawing.Point(splashRegion.X, splashRegion.Y);
            pbLogo.Size = new System.Drawing.Size(splashRegion.Width, splashRegion.Height);

            labelStatusMsg.Location = new Point(pbLogo.Location.X, pbLogo.Location.Y + pbLogo.Size.Height);
            labelStatusMsg.Width = pbLogo.Width;

            tsStart = new TimeSpan(DateTime.Now.Ticks);

            timerCal.Enabled = true;

            //AlphaControls.FormManageEvent.OnF2FTransmit+=new AlphaControls.FormManageEvent.F2FTransmitDelegate(FormManageEvent_OnF2FTransmit);

            // Get the path to the target application
            //_targetAppPath = System.IO.Path.GetDirectoryName(this.GetType().Assembly.GetName().CodeBase)
            //     + "\\" + "MyApplication.exe";
            _targetAppPath = @"\Program Files\orderdish\OrderDish.exe";

            bool targetAppInMemory = Native.FindWindow(null, "InitForm") != IntPtr.Zero;

            // We need to listen for status messages only on a fresh startup
            if (!targetAppInMemory)
                this.msgWnd = new MsgWnd(this, this.Name);

            // If target app not running, this will launch it
            // In case the app is already running, this call will bring it up in focus
            ProcessStartInfo startinfo = new ProcessStartInfo(_targetAppPath, "SplashScreen");
            Process.Start(startinfo);

            // If the app was already running, then shut ourself down
            if (targetAppInMemory)
                this.Close();
        }

        //private void FormManageEvent_OnF2FTransmit(object sender, EventArgs e)
        //{
        //    if (sender is ListViewItem)
        //    {
        //        lvAction.Items.Add((ListViewItem)sender);
        //        lvAction.Refresh();
        //    }
        //}

        protected override void OnPaint(PaintEventArgs e)
        {
            //
            using (var backdrop = new Bitmap(Width, Height))
            {
                using (var gxOff = Graphics.FromImage(backdrop))
                {
                    gxOff.Clear(Color.Black);
                    e.Graphics.DrawAlpha(backdrop, 0, 0, 0);
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e) { }

        private void timerCal_Tick(object sender, EventArgs e)
        {
            if (tsStart != null)
            {
                TimeSpan tsNow = new TimeSpan(DateTime.Now.Ticks);
                TimeSpan tsTemp = tsNow.Subtract(tsStart).Duration();
                if (tsTemp.Seconds > 60)
                {
                    MessageBox.Show("程序运行超时！");
                    Application.Exit();
                }
            }
            else
            {
                Application.Exit();
            }
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            OrderDishLauncher.CaptureAPI.DoScreenCaptureTOFile();
        }
    }
}