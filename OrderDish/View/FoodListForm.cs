using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.WindowsCE.Forms;
using System.Reflection;
using System.Threading;
using OrderDish.Action;
using OrderDish.Common;
using OrderDish.WebReference;

/********************************************************************/
//20120608
//程序逻辑检查完毕
//更改部分控件命名
//未作优化
/********************************************************************/
namespace OrderDish.View
{
    public partial class FoodListForm : Form
    {
        private DataSet dsMenu = null;
        private DataSet dsFood = null;
        private Bitmap backgroundBitmap=null;
        private List<Model.MenuModel> foodList;
        private WaittingForm waittingForm = null;
        private string menuTab = "";

        public FoodListForm()
        {
            //显示等待窗口
            waittingForm = new WaittingForm();
            waittingForm.Owner = this;
            waittingForm.Show();
            Application.DoEvents();

            InitializeComponent();

            //本地事件初始化
            Event.WattingFormEvent.OnWaittingFormClose += new OrderDish.Event.WattingFormEvent.WaittingFormDelegate(WattingFormEvent_OnWaittingFormClose);
            Event.ListModelEvent.OnListModelBuyClick+=new OrderDish.Event.ListModelEvent.ListModelDelegate(ListModelEvent_OnListModelBuyClick);

            //DLL控件事件初始化
            AlphaControls.FormManageEvent.OnFormClose+=new AlphaControls.FormManageEvent.FormCloseDelegate(FormManageEvent_OnFormClose);
            AlphaControls.PictureShiftPanelEvent.OnPictureSelect += new AlphaControls.PictureShiftPanelEvent.PicturnSelectDelegate(PictureSelectEvent_OnPictureSelect);
            AlphaControls.PictureShiftPanelEvent.OnButtonBuyClick +=new AlphaControls.PictureShiftPanelEvent.ButtonBuyDelegate(PictureShiftPanelEvent_OnButtonBuyClick);
            abpActionBar.OnAlpBtnCheck_Click+=new AlphaControls.AlphaBarPanel.alpBtnCheckDelegate(abpActionBar_OnAlpBtnCheck_Click);
            abpActionBar.OnAlpBtnSubmit_Click+=new AlphaControls.AlphaBarPanel.alpBtnSubmitDelegate(abpActionBar_OnAlpBtnSubmit_Click);
            abpActionBar.OnAlpBtnFind_Click+=new AlphaControls.AlphaBarPanel.alpBtnFindDelegate(abpActionBar_OnAlpBtnFind_Click);
        }

        #region 事件委托函数

        private void WattingFormEvent_OnWaittingFormClose()
        {
            waittingForm.Dispose();
        }

        private void ListModelEvent_OnListModelBuyClick(object sender)
        {
            if (GlobalVariable.OrderComplete == false)
            {
                foreach (OrderDish.Model.OrderModel model in GlobalVariable.OrderList)
                {
                    if (model.Id == ((string[])sender)[0])
                    {
                        MessageBox.Show(GlobalVariable.GetResxString("MSGBOX_Exist") + ((string[])sender)[1]);
                        return;
                    }

                }
                GlobalVariable.OrderList.Add(new Model.OrderModel(((string[])sender)[0], ((string[])sender)[1], ((string[])sender)[2], "1", ""));
                MessageBox.Show(GlobalVariable.GetResxString("MSGBOX_Add") + ((string[])sender)[1], "操作",
                    MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show(GlobalVariable.GetResxString("OrderComplete"));
            }
        }

        private void FormManageEvent_OnFormClose()
        {
            this.Dispose();
        }

        private void PictureSelectEvent_OnPictureSelect(object sender)
        {
            
            int index = (int)sender;

            DetailForm df = new DetailForm(foodList[index - 1].FoodId,
                foodList[index - 1].FoodName,
                foodList[index - 1].Price,
                (Bitmap)foodList[index - 1].FoodImage);
            df.ShowDialog();

            //this.Hide();
            //this.Show();
        }

        private void PictureShiftPanelEvent_OnButtonBuyClick(object sender)
        {
            if (GlobalVariable.OrderComplete == false)
            {
                foreach (OrderDish.Model.OrderModel model in GlobalVariable.OrderList)
                {
                    if (model.Id == ((string[])sender)[0])
                    {
                        MessageBox.Show(GlobalVariable.GetResxString("MSGBOX_Exist") + ((string[])sender)[1]);
                        return;
                    }

                }
                GlobalVariable.OrderList.Add(new Model.OrderModel(((string[])sender)[0], ((string[])sender)[1], ((string[])sender)[2], "1", ""));
                MessageBox.Show(GlobalVariable.GetResxString("MSGBOX_Add") + ((string[])sender)[1], "操作", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show(GlobalVariable.GetResxString("OrderComplete"));
            }           
        }

        private void abpActionBar_OnAlpBtnCheck_Click()
        {
            ReportForm rf = new ReportForm();
            rf.ShowDialog();
        }

        private void abpActionBar_OnAlpBtnSubmit_Click()
        {
            if (!GlobalVariable.OrderComplete)
            {
                if (GlobalVariable.OrderList.Count > 0)
                {
                    DataTable table = new DataTable("order");
                    table.Columns.Add("id", System.Type.GetType("System.String"));
                    table.Columns.Add("foodname", System.Type.GetType("System.String"));
                    table.Columns.Add("foodprice", System.Type.GetType("System.String"));
                    table.Columns.Add("amount", System.Type.GetType("System.String"));
                    table.Columns.Add("remark", System.Type.GetType("System.String"));
                    for (int i = 0; i < GlobalVariable.OrderList.Count; i++)
                    {
                        DataRow row = table.NewRow();
                        row[0] = GlobalVariable.OrderList[i].Id;
                        row[1] = GlobalVariable.OrderList[i].FoodName;
                        row[2] = GlobalVariable.OrderList[i].Price;
                        row[3] = GlobalVariable.OrderList[i].Amount;
                        row[4] = GlobalVariable.OrderList[i].Remark;
                        table.Rows.Add(row);
                    }

                    waittingForm = new WaittingForm();
                   
                    waittingForm.Owner = this;
                    waittingForm.Show();
                    Application.DoEvents();

                    Event.WattingFormEvent.OnWaittingFormClose += new OrderDish.Event.WattingFormEvent.WaittingFormDelegate(WattingFormEvent_OnWaittingFormClose);

                    Service s = new Service();
                    DataTable resultTable = s.VerifyOrder(table);

                    Event.WattingFormEvent.DoOnWaittingFormClose();
                    Event.WattingFormEvent.OnWaittingFormClose -= new OrderDish.Event.WattingFormEvent.WaittingFormDelegate(WattingFormEvent_OnWaittingFormClose);

                    OrderVerifyForm ovf = new OrderVerifyForm(resultTable);
                    ovf.Owner = this;
                    ovf.ShowDialog();
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

        private void abpActionBar_OnAlpBtnFind_Click(string strFind)
        {
            InitializeUI2Find(strFind);
        }

        #endregion

        private void ViewForm_Load(object sender, EventArgs e)
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

            /////////////////////////////////////////////////////////////////////////////
            //pbLogo.Image = new Bitmap(GlobalVariable.LocalPath + "\\picture\\logo.gif");
            pbLogo.Image = Properties.Resources.logo;
            backgroundBitmap = Properties.Resources.background2;
            //initBackgroundImage(GlobalVariable.LocalPath + "\\picture\\background2.jpg");
            /////////////////////////////////////////////////////////////////////////////

            /////////////////////////////////////////////////////////////////////////////
            abpActionBar.BackgroundImage = backgroundBitmap;
            abpActionBar.SetAlpbtnReturnText(GlobalVariable.GetResxString("ListForm_abpActionBar_abtn"));
            pspFoodShow.SetbtnNextText(GlobalVariable.GetResxString("ListForm_pspFoodShow_btnNext"));
            pspFoodShow.SetbtnLastText(GlobalVariable.GetResxString("ListForm_pspFoodShow_btnLast"));
            pspFoodShow.SetbtnBuyText(GlobalVariable.GetResxString("ListForm_pspFoodShow_btnBuy"));
            /////////////////////////////////////////////////////////////////////////////

            /////////////////////////////////////////////////////////////////////////////
            foodList= new List<OrderDish.Model.MenuModel>();
            foodList.Clear();

            using (Action.ODAction odAction = new ODAction())
            {
                dsMenu = odAction.GetAllMenuInfo();
                dsFood = odAction.GetAllFoodInfo();
            }

            InitializeMenuBar();

            Event.WattingFormEvent.DoOnWaittingFormClose();

            if (!string.IsNullOrEmpty(menuTab))
            {
                InitializeUI(menuTab);
            }
        }

        public void InitializeMenuBar()
        {
            if (dsMenu != null)
            {
                int count = 0;
                foreach (DataRow rowMenu in dsMenu.Tables[0].Rows)
                {
                    AlphaControls.AlphaVerticalButton aVBtn = new AlphaControls.AlphaVerticalButton();
                    aVBtn.Width = scrollPanelMenu.Width;
                    aVBtn.Height = 50;
                    aVBtn.Name = "Button" + rowMenu[0].ToString();
                    aVBtn.Text = rowMenu[1].ToString();

                    aVBtn.Location = new Point(0, count * 50);

                    aVBtn.Tag = rowMenu[0].ToString();

                    aVBtn.Click += new EventHandler(aVBtn_Click);

                    scrollPanelMenu.FillControlsToPanelMenu(aVBtn);

                    if (count == 0)
                    {
                        menuTab = rowMenu[0].ToString();
                    }

                    count++;
                }

                scrollPanelMenu.VScrollBarMaximum = scrollPanelMenu.PanelMenuHeight;

                //for (int i = 0; i < dsMenu.Tables[0].Rows.Count; i++)
                //{
                //    DataRow rowMenu = dsMenu.Tables[0].NewRow();

                //    AlphaControls.AlphaVerticalButton aVBtn = new AlphaControls.AlphaVerticalButton();
                //    aVBtn.Width = spMenu.Width;
                //    aVBtn.Height = 50;
                //    aVBtn.Name = "Button" + rowMenu[0].ToString();
                //    aVBtn.Text = rowMenu[1].ToString();

                //    aVBtn.Location = new Point(0, i * 50);

                //    aVBtn.Tag = rowMenu[0].ToString();

                //    aVBtn.Click += new EventHandler(aVBtn_Click);

                //    spMenu.FillControlsToPanelMenu(aVBtn);

                //    if (i == 0)
                //    {
                //        menuTab = rowMenu[0].ToString();
                //    }

                //}

                
            }
            else
            {
                MessageBox.Show(GlobalVariable.GetResxString("MSGBOX_InitFail"));
                this.Dispose();
            }
        }

        private void aVBtn_Click(object sender, EventArgs e)
        {
            if (((AlphaControls.AlphaVerticalButton)sender).Tag is string)
            {
                InitializeUI(((AlphaControls.AlphaVerticalButton)sender).Tag.ToString());
            }
        }

        private void InitializeUI(string id)
        {
            //***************************************//
            //根据菜单获取菜谱信息和文件并放置到队列中
            //1.判断菜单
            //2.判断菜谱
            //3.抓取文件
            //***************************************//

            //显示等待窗口
            waittingForm = new WaittingForm();
            waittingForm.Owner = this;
            waittingForm.Show();
            Application.DoEvents();

            if (dsMenu != null)
            {
                foreach (DataRow rowMenu in dsMenu.Tables[0].Rows)
                {
                    if (rowMenu[0].ToString() == id)
                    {
                        foodList.Clear();

                        foreach (DataRow rowFood in dsFood.Tables[0].Rows)
                        {
                            if (rowFood[4].ToString() == id)
                            {
                                if (File.Exists(GlobalVariable.LocalPath + rowFood[5].ToString()))
                                {
                                    foodList.Add(new Model.MenuModel(rowFood[0].ToString(),
                                        rowFood[1].ToString(),
                                        new Bitmap(GlobalVariable.LocalPath + rowFood[5].ToString()),
                                        rowFood[2].ToString()));
                                }
                                else
                                {
                                    if (Common.HttpFileControl.DownLoadFile(GlobalVariable.HttpPath + rowFood[5].ToString(),
                                        GlobalVariable.LocalPath + rowFood[5].ToString()))
                                    {
                                        foodList.Add(new Model.MenuModel(rowFood[0].ToString(),
                                        rowFood[1].ToString(),
                                        new Bitmap(GlobalVariable.LocalPath + rowFood[5].ToString()),
                                        rowFood[2].ToString()));
                                    }
                                    else
                                    {
                                        MessageBox.Show(GlobalVariable.LocalPath + rowFood[5].ToString()+"文件丢失,请联系管理员!");
                                        this.Dispose();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(GlobalVariable.GetResxString("MSGBOX_InitFail"));
                this.Dispose();
            }

            //控件释放并装载数据
            if (foodList.Count > 0)
            {
                pspFoodShow.FoodModelList.Clear();
                //pspPicShow.FoodImageList.Clear();
                slbViewList.RemoveAllItem();
                slbViewList.Reset();

                for (int i = 0; i < foodList.Count; i++)
                {
                    //pspPicShow.FoodImageList.Add(foodList[i].FoodImage);

                    pspFoodShow.FoodModelList.Add(new AlphaControls.FoodModel(foodList[i].FoodId,
                        foodList[i].FoodName, foodList[i].Price, foodList[i].FoodImage));

                    slbViewList.AddItem(new Model.ListModel(foodList[i].FoodId,foodList[i].FoodName, foodList[i].Price, foodList[i].FoodImage, i));
                }

                pspFoodShow.PicInit();
            }

            Event.WattingFormEvent.DoOnWaittingFormClose();
        }

        private void InitializeUI2Find(string text)
        {
            //***************************************//
            //***************************************//
            int findCount = 0;

            //显示等待窗口
            waittingForm = new WaittingForm();
            waittingForm.Owner = this;
            waittingForm.Show();
            Application.DoEvents();

            if (dsMenu != null)
            {
                if (dsFood != null)
                {
                    foodList.Clear();


                    //find foodname
                    foreach (DataRow rowFood in dsFood.Tables[0].Rows)
                    {
                        if (rowFood[1].ToString().Contains(text) == true)
                        {
                            findCount++;

                            if (File.Exists(GlobalVariable.LocalPath + rowFood[5].ToString()))
                            {
                                foodList.Add(new Model.MenuModel(rowFood[0].ToString(),
                                    rowFood[1].ToString(),
                                    new Bitmap(GlobalVariable.LocalPath + rowFood[5].ToString()),
                                    rowFood[2].ToString()));
                            }
                            else
                            {
                                if (Common.HttpFileControl.DownLoadFile(GlobalVariable.HttpPath + rowFood[5].ToString(),
                                    GlobalVariable.LocalPath + rowFood[5].ToString()))
                                {
                                    foodList.Add(new Model.MenuModel(rowFood[0].ToString(),
                                    rowFood[1].ToString(),
                                    new Bitmap(GlobalVariable.LocalPath + rowFood[5].ToString()),
                                    rowFood[2].ToString()));
                                }
                                else
                                {
                                    MessageBox.Show(GlobalVariable.LocalPath + rowFood[5].ToString() + "文件丢失,请联系管理员!");
                                    this.Dispose();
                                }
                            }
                        }
                    }

                    //find id 
                    foreach (DataRow rowFood in dsFood.Tables[0].Rows)
                    {
                        if (rowFood[0].ToString().Contains(text) == true)
                        {
                            findCount++;

                            if (File.Exists(GlobalVariable.LocalPath + rowFood[5].ToString()))
                            {
                                foodList.Add(new Model.MenuModel(rowFood[0].ToString(),
                                    rowFood[1].ToString(),
                                    new Bitmap(GlobalVariable.LocalPath + rowFood[5].ToString()),
                                    rowFood[2].ToString()));
                            }
                            else
                            {
                                if (Common.HttpFileControl.DownLoadFile(GlobalVariable.HttpPath + rowFood[5].ToString(),
                                    GlobalVariable.LocalPath + rowFood[5].ToString()))
                                {
                                    foodList.Add(new Model.MenuModel(rowFood[0].ToString(),
                                    rowFood[1].ToString(),
                                    new Bitmap(GlobalVariable.LocalPath + rowFood[5].ToString()),
                                    rowFood[2].ToString()));
                                }
                                else
                                {
                                    MessageBox.Show(GlobalVariable.LocalPath + rowFood[5].ToString() + "文件丢失,请联系管理员!");
                                    this.Dispose();
                                }
                            }
                        }
                    }




                }
            }
            else
            {
                MessageBox.Show(GlobalVariable.GetResxString("MSGBOX_InitFail"));
                this.Dispose();
            }

            //控件释放并装载数据
            if (foodList.Count > 0)
            {
                pspFoodShow.FoodModelList.Clear();
                //pspPicShow.FoodImageList.Clear();
                slbViewList.RemoveAllItem();
                slbViewList.Reset();

                for (int i = 0; i < foodList.Count; i++)
                {
                    //pspPicShow.FoodImageList.Add(foodList[i].FoodImage);

                    pspFoodShow.FoodModelList.Add(new AlphaControls.FoodModel(foodList[i].FoodId,
                        foodList[i].FoodName, foodList[i].Price, foodList[i].FoodImage));

                    slbViewList.AddItem(new Model.ListModel(foodList[i].FoodId, foodList[i].FoodName, foodList[i].Price, foodList[i].FoodImage, i));
                }

                pspFoodShow.PicInit();
            }

            Event.WattingFormEvent.DoOnWaittingFormClose();

            if (findCount == 0)
            {
                MessageBox.Show("没有找到匹配的信息！");
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (var backdrop = new Bitmap(Width, Height))
            {
                using (var gxOff = Graphics.FromImage(backdrop))
                {
                    gxOff.Clear(this.BackColor);
                    gxOff.DrawImage(backgroundBitmap,0,0);
                    e.Graphics.DrawImage(backdrop,0,0);
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e) { }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Common.CaptureAPI.DoScreenCaptureTOFile();
        }

        #region
        //public void initBackgroundImage(string path)
        //{
        //    //backgroundBitmap = new Bitmap(path);
        //    backgroundBitmap = Properties.Resources.background2;
        //    this.abpActionBar.BackgroundImage = backgroundBitmap;
        //}
        #endregion
    }
}