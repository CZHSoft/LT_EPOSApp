using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OrderDish.WebReference;
using Microsoft.WindowsCE.Forms;

namespace OrderDish.View
{
    public partial class OrderVerifyForm : Form
    {
        private DataTable dtOrder = null;

        private WaittingForm waittingForm = null;

        private BLL.WaiterIDListBLL waiterIDListBLL;

        private BLL.DeskListBLL deskListBLL;

        private DataSet dsWaiter;

        private DataSet dsDesk;

        public OrderVerifyForm(DataTable tb)
        {
            InitializeComponent();

            dtOrder = tb;
        }

        private void OrderVerifyForm_Load(object sender, EventArgs e)
        {
            /////////// Don't show title bar and allocate the full screen ///////////////
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.ControlBox = false;
            //this.Menu = null;
            //this.WindowState = FormWindowState.Maximized;

            //FullScreenApp.FullScreen(this.Handle, true);
            SystemSettings.ScreenOrientation = ScreenOrientation.Angle90;
            //Common.SIP.ShowHideSIP(0);
            /////////////////////////////////////////////////////////////////////////////
            
            btnSubmit.Text = GlobalVariable.GetResxString("btnSubmit");
            btnCancel.Text = GlobalVariable.GetResxString("btnCancel");
            labelTitle.Text = GlobalVariable.GetResxString("labelTitle");

            /////////////////////////////////////////////////////////////////////////////

            lvOrder.Items.Clear();
            lvOrder.View = System.Windows.Forms.View.Details;
            lvOrder.Columns.Add(GlobalVariable.GetResxString("LV_FN"), 240, HorizontalAlignment.Center);
            lvOrder.Columns.Add(GlobalVariable.GetResxString("LV_PR"), 80, HorizontalAlignment.Center);
            lvOrder.Columns.Add(GlobalVariable.GetResxString("LV_QU"), 80, HorizontalAlignment.Center);
            lvOrder.Columns.Add(GlobalVariable.GetResxString("LV_TO"), 80, HorizontalAlignment.Center);
            lvOrder.Columns.Add(GlobalVariable.GetResxString("LV_TR"), 240, HorizontalAlignment.Left);

            InitializeDataAndUI();
        }

        private void InitializeDataAndUI()
        {
            if (dtOrder != null)
            {
                for (int i = 0; i < dtOrder.Rows.Count; i++)
                {
                    if (i != dtOrder.Rows.Count - 1)
                    {
                        ListViewItem lvi = new ListViewItem(dtOrder.Rows[i][1].ToString());
                        lvi.SubItems.Add(dtOrder.Rows[i][2].ToString());
                        lvi.SubItems.Add(dtOrder.Rows[i][3].ToString());
                        lvi.SubItems.Add(dtOrder.Rows[i][4].ToString());
                        lvi.SubItems.Add(dtOrder.Rows[i][5].ToString());
                        lvOrder.Items.Add(lvi);
                    }

                }
                //lvOrder.Refresh();
                sbTotal.Text = dtOrder.Rows[dtOrder.Rows.Count - 1][0].ToString() + ":" + dtOrder.Rows[dtOrder.Rows.Count - 1][4].ToString();
            }

            cmbWaiter.Items.Clear();
            waiterIDListBLL = new OrderDish.BLL.WaiterIDListBLL();
            dsWaiter = waiterIDListBLL.GetWaiterDataSet();
            if (dsWaiter != null)
            {
                foreach (DataRow row in dsWaiter.Tables[0].Rows)
                {
                    cmbWaiter.Items.Add(row[0].ToString());
                }
            }

            cmbDesk.Items.Clear();
            deskListBLL = new OrderDish.BLL.DeskListBLL();
            dsDesk = deskListBLL.GetDeskDataSet();
            if (dsDesk != null)
            {
                foreach (DataRow row in dsDesk.Tables[0].Rows)
                {
                    cmbDesk.Items.Add(row[0].ToString());
                }
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (lvOrder.Items.Count > 0)
            {
                if (cmbWaiter.SelectedIndex != -1 && cmbDesk.SelectedIndex != -1)
                {
                    waittingForm = new WaittingForm();
                    //waittingForm.StrMsg = GlobalVariable.GetResxString("WaittingFrom_strMs");
                    waittingForm.Owner = this;
                    waittingForm.Show();
                    Application.DoEvents();
                    Event.WattingFormEvent.OnWaittingFormClose += new OrderDish.Event.WattingFormEvent.WaittingFormDelegate(WattingFormEvent_OnWaittingFormClose);

                    Service s = new Service();
                    string orderid = s.PlaceOrder(dtOrder, cmbWaiter.Text, cmbDesk.Text, GlobalVariable.DeviceID);
                    if (!string.IsNullOrEmpty(orderid))
                    {
                        Event.WattingFormEvent.DoOnWaittingFormClose();

                        GlobalVariable.OrderList.Clear();
                        GlobalVariable.OrderComplete = true;
                        GlobalVariable.OrderID = orderid;

                        MessageBox.Show(GlobalVariable.GetResxString("MSGBOX_Succ"));
                        Event.WattingFormEvent.OnWaittingFormClose -= new OrderDish.Event.WattingFormEvent.WaittingFormDelegate(WattingFormEvent_OnWaittingFormClose);
                        this.Dispose();
                    }
                    else
                    {
                        Event.WattingFormEvent.DoOnWaittingFormClose();
                        MessageBox.Show(GlobalVariable.GetResxString("MSGBOX_Fail"));
                        Event.WattingFormEvent.OnWaittingFormClose -= new OrderDish.Event.WattingFormEvent.WaittingFormDelegate(WattingFormEvent_OnWaittingFormClose);
                    }
                }
                else
                {
                    MessageBox.Show("请选择服务员！");
                }
            }
            else
            {
                MessageBox.Show("无效：订单数量不能为空！");
            }
        }

        private void WattingFormEvent_OnWaittingFormClose()
        {
            waittingForm.Dispose();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Common.CaptureAPI.DoScreenCaptureTOFile();
        }

    }
}