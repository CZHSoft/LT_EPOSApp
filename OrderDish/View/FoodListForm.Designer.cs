namespace OrderDish.View
{
    partial class FoodListForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            this.dsMenu = null;
            this.dsFood = null;
            this.backgroundBitmap = null;

            this.waittingForm.Dispose();

            Event.WattingFormEvent.OnWaittingFormClose -= new OrderDish.Event.WattingFormEvent.WaittingFormDelegate(WattingFormEvent_OnWaittingFormClose);
            Event.ListModelEvent.OnListModelBuyClick -= new OrderDish.Event.ListModelEvent.ListModelDelegate(ListModelEvent_OnListModelBuyClick);

            this.foodList.Clear();

            AlphaControls.FormManageEvent.OnFormClose -= new AlphaControls.FormManageEvent.FormCloseDelegate(FormManageEvent_OnFormClose);
            AlphaControls.PictureShiftPanelEvent.OnPictureSelect -= new AlphaControls.PictureShiftPanelEvent.PicturnSelectDelegate(PictureSelectEvent_OnPictureSelect);
            AlphaControls.PictureShiftPanelEvent.OnButtonBuyClick -= new AlphaControls.PictureShiftPanelEvent.ButtonBuyDelegate(PictureShiftPanelEvent_OnButtonBuyClick);

            abpActionBar.OnAlpBtnCheck_Click -= new AlphaControls.AlphaBarPanel.alpBtnCheckDelegate(abpActionBar_OnAlpBtnCheck_Click);
            abpActionBar.OnAlpBtnSubmit_Click -= new AlphaControls.AlphaBarPanel.alpBtnSubmitDelegate(abpActionBar_OnAlpBtnSubmit_Click);
            abpActionBar.OnAlpBtnFind_Click -= new AlphaControls.AlphaBarPanel.alpBtnFindDelegate(abpActionBar_OnAlpBtnFind_Click);

            this.pbLogo.Dispose();
            this.pspFoodShow.Dispose();

            this.slbViewList.RemoveAllItem();
            this.slbViewList.Dispose();

            this.abpActionBar.Dispose();
            this.scrollPanelMenu.Dispose();
            this.panelFoodView.Dispose();
            this.panelLogo.Dispose();
            this.panelBar.Dispose();
            this.panelScrollMenu.Dispose();
            this.panelLV.Dispose();


            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panelFoodView = new System.Windows.Forms.Panel();
            this.pspFoodShow = new AlphaControls.PicShiftPanel();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.panelLV = new System.Windows.Forms.Panel();
            this.slbViewList = new AlphaControls.SmoothListboxVertical();
            this.panelBar = new System.Windows.Forms.Panel();
            this.abpActionBar = new AlphaControls.AlphaBarPanel();
            this.panelScrollMenu = new System.Windows.Forms.Panel();
            this.scrollPanelMenu = new AlphaControls.ScrollPanel();
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.panelFoodView.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.panelLV.SuspendLayout();
            this.panelBar.SuspendLayout();
            this.panelScrollMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelFoodView
            // 
            this.panelFoodView.Controls.Add(this.pspFoodShow);
            this.panelFoodView.Location = new System.Drawing.Point(120, 30);
            this.panelFoodView.Name = "panelFoodView";
            this.panelFoodView.Size = new System.Drawing.Size(240, 160);
            // 
            // pspFoodShow
            // 
            this.pspFoodShow.Location = new System.Drawing.Point(0, 0);
            this.pspFoodShow.Name = "pspFoodShow";
            this.pspFoodShow.Size = new System.Drawing.Size(240, 160);
            this.pspFoodShow.TabIndex = 0;
            // 
            // pbLogo
            // 
            this.pbLogo.Location = new System.Drawing.Point(0, 0);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(120, 60);
            // 
            // panelLogo
            // 
            this.panelLogo.Controls.Add(this.pbLogo);
            this.panelLogo.Location = new System.Drawing.Point(0, 30);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(120, 60);
            // 
            // panelLV
            // 
            this.panelLV.Controls.Add(this.slbViewList);
            this.panelLV.Location = new System.Drawing.Point(0, 90);
            this.panelLV.Name = "panelLV";
            this.panelLV.Size = new System.Drawing.Size(120, 100);
            // 
            // slbViewList
            // 
            this.slbViewList.Location = new System.Drawing.Point(5, 5);
            this.slbViewList.Name = "slbViewList";
            this.slbViewList.Size = new System.Drawing.Size(110, 90);
            this.slbViewList.TabIndex = 0;
            // 
            // panelBar
            // 
            this.panelBar.Controls.Add(this.abpActionBar);
            this.panelBar.Location = new System.Drawing.Point(0, 0);
            this.panelBar.Name = "panelBar";
            this.panelBar.Size = new System.Drawing.Size(360, 30);
            // 
            // abpActionBar
            // 
            this.abpActionBar.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.abpActionBar.Location = new System.Drawing.Point(0, 0);
            this.abpActionBar.Name = "abpActionBar";
            this.abpActionBar.Size = new System.Drawing.Size(360, 30);
            this.abpActionBar.TabIndex = 21;
            // 
            // panelScrollMenu
            // 
            this.panelScrollMenu.Controls.Add(this.scrollPanelMenu);
            this.panelScrollMenu.Location = new System.Drawing.Point(360, 0);
            this.panelScrollMenu.Name = "panelScrollMenu";
            this.panelScrollMenu.Size = new System.Drawing.Size(40, 190);
            // 
            // scrollPanelMenu
            // 
            this.scrollPanelMenu.Location = new System.Drawing.Point(0, 0);
            this.scrollPanelMenu.Name = "scrollPanelMenu";
            this.scrollPanelMenu.Size = new System.Drawing.Size(40, 190);
            this.scrollPanelMenu.TabIndex = 0;
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "截屏";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // FoodListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(400, 190);
            this.ControlBox = false;
            this.Controls.Add(this.panelScrollMenu);
            this.Controls.Add(this.panelBar);
            this.Controls.Add(this.panelLV);
            this.Controls.Add(this.panelFoodView);
            this.Controls.Add(this.panelLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "FoodListForm";
            this.Load += new System.EventHandler(this.ViewForm_Load);
            this.panelFoodView.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panelLV.ResumeLayout(false);
            this.panelBar.ResumeLayout(false);
            this.panelScrollMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelFoodView;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.PictureBox pbLogo;
        private AlphaControls.PicShiftPanel pspFoodShow;
        private System.Windows.Forms.Panel panelLV;
        private AlphaControls.SmoothListboxVertical slbViewList;
        private AlphaControls.AlphaBarPanel abpActionBar;
        private System.Windows.Forms.Panel panelBar;
        private System.Windows.Forms.Panel panelScrollMenu;
        private AlphaControls.ScrollPanel scrollPanelMenu;
        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem menuItem1;
    }
}