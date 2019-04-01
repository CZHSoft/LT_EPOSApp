using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Drawing;
using Microsoft.WindowsCE.Forms;
using OrderDish.WebReference;



/********************************************************************/
//20120610
//添加口味备注字段
//个别命名更改
//web service 订单验证和订单提交对应的字段需要修改
//数据库服务器对应字段需要修改
/********************************************************************/

namespace OrderDish.View
{
    public partial class ReportForm : Form
    {
        //private bool hadPaint = false;

        private WaittingForm waittingForm = null;

        private DataSet dsOrderDetail;

        private BLL.OrderDetailBLL orderDetailBLL ;

        public ReportForm()
        {
            InitializeComponent();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            /////////// Don't show title bar and allocate the full screen ///////////////
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.WindowState = FormWindowState.Maximized;

            SystemSettings.ScreenOrientation = ScreenOrientation.Angle90;
            /////////////////////////////////////////////////////////////////////////////

            btnSubmit.Text = GlobalVariable.GetResxString("ReportForm_btnSubmit");
            btnReflash.Text = GlobalVariable.GetResxString("ReportForm_btnReflash");
            btnReturn.Text = GlobalVariable.GetResxString("ReportForm_btnReturn");

            menuItemModCount.Text = GlobalVariable.GetResxString("OD_ModCount");
            menuItemRemark.Text = GlobalVariable.GetResxString("OD_Remark");
            menuItemDel.Text = GlobalVariable.GetResxString("OD_Del");

            ////////////////////////////////////////////////////////////////////////////

            InitializeUI();

        }

        private void InitializeUI()
        {
            if (!GlobalVariable.OrderComplete)
            {
                btnReflash.Enabled = false;
                btnReflash.Visible = false;

                if (GlobalVariable.OrderList.Count > 0)
                {
                    decimal sum = 0;

                    //这里插入表格
                    lvReport.Items.Clear();
                    lvReport.Columns.Clear();
                    lvReport.View = System.Windows.Forms.View.Details;
                    lvReport.Columns.Add(GlobalVariable.GetResxString("ReportForm_LV1"), 240, HorizontalAlignment.Center);
                    lvReport.Columns.Add(GlobalVariable.GetResxString("ReportForm_LV2"), 80, HorizontalAlignment.Center);
                    lvReport.Columns.Add(GlobalVariable.GetResxString("ReportForm_LV3"), 80, HorizontalAlignment.Center);
                    lvReport.Columns.Add(GlobalVariable.GetResxString("ReportForm_LV4"), 240, HorizontalAlignment.Left);
                    //lvReport.Columns.Add(("id"), 50, HorizontalAlignment.Center);
                    for (int count = 0; count < GlobalVariable.OrderList.Count; count++)
                    {
                        ListViewItem lvi = new ListViewItem(GlobalVariable.OrderList[count].FoodName);
                        lvi.SubItems.Add(GlobalVariable.OrderList[count].Price);
                        lvi.SubItems.Add(GlobalVariable.OrderList[count].Amount);
                        lvi.SubItems.Add(GlobalVariable.OrderList[count].Remark);
                        //lvi.SubItems.Add(GlobalVariable.OrderList[count].Id);
                        lvi.Tag = GlobalVariable.OrderList[count].Id;
                        lvReport.Items.Add(lvi);

                        sum += decimal.Parse(GlobalVariable.OrderList[count].Price) * decimal.Parse(GlobalVariable.OrderList[count].Amount);
                    }
                    btnSubmit.Enabled = true;
                    btnSubmit.Visible = true;
                    lvReport.ContextMenu = cmReport;

                    statusBar.Text = "合计：" + sum.ToString();
                }
                else if (GlobalVariable.OrderList.Count == 0)
                {
                    //空
                    lvReport.Items.Clear();
                    MessageBox.Show(GlobalVariable.GetResxString("ReportForm_None"));
                    btnSubmit.Enabled = false;
                    btnSubmit.Visible = false;

                    menuItem2.Enabled = false;

                    statusBar.Text = "合计:0";
                }
            }
            else
            {
                orderDetailBLL = new OrderDish.BLL.OrderDetailBLL();
                dsOrderDetail = orderDetailBLL.GetOrderDetailDataSet(GlobalVariable.OrderID);
                if (dsOrderDetail == null)
                {
                    MessageBox.Show(GlobalVariable.GetResxString("ReportForm_Faile2"));
                    this.Dispose();
                }
                lvReport.Items.Clear();
                lvReport.Columns.Clear();
                lvReport.View = System.Windows.Forms.View.Details;
                lvReport.Columns.Add(GlobalVariable.GetResxString("ReportForm_LV1"), 200, HorizontalAlignment.Center);
                lvReport.Columns.Add(GlobalVariable.GetResxString("ReportForm_LV2"), 80, HorizontalAlignment.Center);
                lvReport.Columns.Add(GlobalVariable.GetResxString("ReportForm_LV3"), 80, HorizontalAlignment.Center);
                lvReport.Columns.Add(GlobalVariable.GetResxString("ReportForm_LV4"), 240, HorizontalAlignment.Left);
                lvReport.Columns.Add(GlobalVariable.GetResxString("ReportForm_LV5"), 80, HorizontalAlignment.Left);
                for (int count = 0; count < dsOrderDetail.Tables[0].Rows.Count; count++)
                {
                    ListViewItem lvi = new ListViewItem(dsOrderDetail.Tables[0].Rows[count][0].ToString());
                    lvi.SubItems.Add(dsOrderDetail.Tables[0].Rows[count][1].ToString());
                    lvi.SubItems.Add(dsOrderDetail.Tables[0].Rows[count][2].ToString());
                    lvi.SubItems.Add(dsOrderDetail.Tables[0].Rows[count][3].ToString());
                    lvi.SubItems.Add(ConvertStateValue(dsOrderDetail.Tables[0].Rows[count][4].ToString()));
                    lvReport.Items.Add(lvi);
                }

                btnSubmit.Enabled = false;
                btnSubmit.Visible = false;
                btnReflash.Enabled = true;
                btnReflash.Visible = true;
                lvReport.ContextMenu = cmOrder;
                menuItem2.Enabled = false;
            }
        }

        private void WattingFormEvent_OnWaittingFormClose()
        {
            waittingForm.Dispose();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!GlobalVariable.OrderComplete)
            {
                if (lvReport.Items.Count > 0)
                {
                    DataTable table = new DataTable("order");
                    table.Columns.Add("id", System.Type.GetType("System.String"));
                    table.Columns.Add("foodname", System.Type.GetType("System.String"));
                    table.Columns.Add("foodprice", System.Type.GetType("System.String"));
                    table.Columns.Add("amount", System.Type.GetType("System.String"));
                    table.Columns.Add("remark", System.Type.GetType("System.String"));
                    for (int i = 0; i < lvReport.Items.Count; i++)
                    {
                        DataRow row = table.NewRow();
                        row[0] = lvReport.Items[i].Tag.ToString();
                        row[1] = lvReport.Items[i].SubItems[0].Text;
                        row[2] = lvReport.Items[i].SubItems[1].Text;
                        row[3] = lvReport.Items[i].SubItems[2].Text;
                        row[4] = lvReport.Items[i].SubItems[3].Text;
                        table.Rows.Add(row);
                    }

                    waittingForm = new WaittingForm();
                    //waittingForm.StrMsg = GlobalVariable.GetResxString("WaittingFrom_strMsg");
                    waittingForm.Owner = this;
                    waittingForm.Show();
                    Application.DoEvents();

                    Event.WattingFormEvent.OnWaittingFormClose += new OrderDish.Event.WattingFormEvent.WaittingFormDelegate(WattingFormEvent_OnWaittingFormClose);

                    Service s = new Service();
                    DataTable resultTable = s.VerifyOrder(table);

                    Event.WattingFormEvent.DoOnWaittingFormClose();
                    Event.WattingFormEvent.OnWaittingFormClose -= new OrderDish.Event.WattingFormEvent.WaittingFormDelegate(WattingFormEvent_OnWaittingFormClose);
                    //LVCenteredForm lvcf = new LVCenteredForm(resultTable);
                    //lvcf.Owner = this;
                    //lvcf.ShowDialog();
                    OrderVerifyForm ovf = new OrderVerifyForm(resultTable);
                    ovf.Owner = this;
                    ovf.ShowDialog();
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show(GlobalVariable.GetResxString("ReportForm_None"));
                }
            }
            else
            {
                MessageBox.Show(GlobalVariable.GetResxString("ReportForm_OK"));
            }
        }

        private void menuItemModCount_Click(object sender, EventArgs e)
        {
            if (lvReport.SelectedIndices.Count == 1)
            {
                //lvReport.Items[lvReport.SelectedIndices[0]].SubItems[2].Text;
                string count = lvReport.Items[lvReport.SelectedIndices[0]].SubItems[2].Text;
                CountSettingForm csf = new CountSettingForm();
                csf.OnGetNewCount+=new CountSettingForm.GetNewCount(OnGetNewCount);
                csf.ShowDialog();
                csf.OnGetNewCount -= new CountSettingForm.GetNewCount(OnGetNewCount);

                //this.Hide();
                //this.Show();
            }
            else
            {
                MessageBox.Show(GlobalVariable.GetResxString("ReportForm_Faile1"));
            }
        }


        private void OnGetNewCount(string newCount)
        {
            lvReport.Items[lvReport.SelectedIndices[0]].SubItems[2].Text = newCount;
            GlobalVariable.OrderList[lvReport.SelectedIndices[0]].Amount = newCount;
            InitializeUI();
        }

        private void menuItemDel_Click(object sender, EventArgs e)
        {
            if (lvReport.SelectedIndices.Count >= 1)
            {
                for (int i = 0; i < lvReport.SelectedIndices.Count; i++)
                {
                    GlobalVariable.OrderList.RemoveAt(lvReport.SelectedIndices[i]);
                    lvReport.Items.RemoveAt(lvReport.SelectedIndices[i]);
                    InitializeUI();
                }
            }
            else
            {
                MessageBox.Show(GlobalVariable.GetResxString("ReportForm_Faile3"));
            }
        }

        private void menuItemRemark_Click(object sender, EventArgs e)
        {
            if (lvReport.SelectedIndices.Count == 1)
            {
                //lvReport.Items[lvReport.SelectedIndices[0]].SubItems[2].Text;
                //MessageBox.Show(lvReport.SelectedIndices[0].ToString());
                string remark = lvReport.Items[lvReport.SelectedIndices[0]].SubItems[3].Text;
                RemarkSettingForm rsf = new RemarkSettingForm(remark);
                rsf.OnGetRemark+=new RemarkSettingForm.GetRemark(OnGetRemark);
                
                rsf.ShowDialog();

                rsf.OnGetRemark -= new RemarkSettingForm.GetRemark(OnGetRemark);

                //this.Hide();
                //this.Show();

            }
            else
            {
                MessageBox.Show(GlobalVariable.GetResxString("ReportForm_Faile1"));
            }
        }

        private void OnGetRemark(string newRemark)
        {
            lvReport.Items[lvReport.SelectedIndices[0]].SubItems[3].Text = newRemark;
            GlobalVariable.OrderList[lvReport.SelectedIndices[0]].Remark = newRemark;
            InitializeUI();
        }

        private void btnReflash_Click(object sender, EventArgs e)
        {
            dsOrderDetail = orderDetailBLL.GetOrderDetailDataSet(GlobalVariable.OrderID);
            if (dsOrderDetail == null)
            {
                MessageBox.Show(GlobalVariable.GetResxString("ReportForm_Faile2"));
                this.Dispose();
            }
            lvReport.Items.Clear();
            for (int count = 0; count < dsOrderDetail.Tables[0].Rows.Count; count++)
            {
                ListViewItem lvi = new ListViewItem(dsOrderDetail.Tables[0].Rows[count][0].ToString());
                lvi.SubItems.Add(dsOrderDetail.Tables[0].Rows[count][1].ToString());
                lvi.SubItems.Add(dsOrderDetail.Tables[0].Rows[count][2].ToString());
                lvi.SubItems.Add(dsOrderDetail.Tables[0].Rows[count][3].ToString());
                lvi.SubItems.Add(ConvertStateValue(dsOrderDetail.Tables[0].Rows[count][4].ToString()));
                lvReport.Items.Add(lvi);
            }
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Common.CaptureAPI.DoScreenCaptureTOFile();
        }

        private string ConvertStateValue(string input)
        {
            if (input == ((int)Common.FoodState.Prepare).ToString())
            {
                return "准备材料";
            }
            else if (input == ((int)Common.FoodState.Order).ToString())
            {
                return "已下单";
            }
            else if (input == ((int)Common.FoodState.Doing).ToString())
            {
                return "已下菜";
            }
            else if (input == ((int)Common.FoodState.Complete).ToString())
            {
                return "已起菜";
            }
            else if (input == ((int)Common.FoodState.CheckOut).ToString())
            {
                return "已结帐";
            }
            else
            {
                return "未知状态";
            }
        }

        private void menuItemMC_Click(object sender, EventArgs e)
        {
            menuItemModCount_Click(sender, e);
        }

        private void menuItemRM_Click(object sender, EventArgs e)
        {
            menuItemRemark_Click(sender, e);
        }

        private void menuItemDel2_Click(object sender, EventArgs e)
        {
            menuItemDel_Click(sender, e);
        }

    }
}