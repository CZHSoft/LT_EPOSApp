namespace OrderDish.View
{
    partial class MainForm
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
            this.backgroundBitmap.Dispose();

            this.gfbtnLeft.Dispose();

            //this.threadPlayer.Join();

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
            this.panelWeather = new System.Windows.Forms.Panel();
            this.gfbtnLeft = new AlphaControls.GradientFilledButton();
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.alpBtnList = new AlphaControls.AlphaButton();
            this.alpBtnMMS = new AlphaControls.AlphaButton();
            this.alpBtnOrder = new AlphaControls.AlphaButton();
            this.alpBtnExit = new AlphaControls.AlphaButton();
            this.SuspendLayout();
            // 
            // panelWeather
            // 
            this.panelWeather.Location = new System.Drawing.Point(40, 5);
            this.panelWeather.Name = "panelWeather";
            this.panelWeather.Size = new System.Drawing.Size(80, 180);
            // 
            // gfbtnLeft
            // 
            this.gfbtnLeft.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.gfbtnLeft.Location = new System.Drawing.Point(5, 5);
            this.gfbtnLeft.Name = "gfbtnLeft";
            this.gfbtnLeft.Size = new System.Drawing.Size(30, 180);
            this.gfbtnLeft.TabIndex = 7;
            this.gfbtnLeft.Text = "佛山市蓝天网络科技有限公司";
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
            // alpBtnList
            // 
            this.alpBtnList.BackgroundImage = null;
            this.alpBtnList.Location = new System.Drawing.Point(127, 5);
            this.alpBtnList.Name = "alpBtnList";
            this.alpBtnList.Size = new System.Drawing.Size(100, 85);
            this.alpBtnList.TabIndex = 15;
            this.alpBtnList.Click += new System.EventHandler(this.alpBtnList_Click);
            // 
            // alpBtnMMS
            // 
            this.alpBtnMMS.BackgroundImage = null;
            this.alpBtnMMS.Location = new System.Drawing.Point(233, 5);
            this.alpBtnMMS.Name = "alpBtnMMS";
            this.alpBtnMMS.Size = new System.Drawing.Size(100, 85);
            this.alpBtnMMS.TabIndex = 16;
            this.alpBtnMMS.Text = "MMS";
            this.alpBtnMMS.Click += new System.EventHandler(this.alpBtnMMS_Click);
            // 
            // alpBtnOrder
            // 
            this.alpBtnOrder.BackgroundImage = null;
            this.alpBtnOrder.Location = new System.Drawing.Point(127, 95);
            this.alpBtnOrder.Name = "alpBtnOrder";
            this.alpBtnOrder.Size = new System.Drawing.Size(100, 85);
            this.alpBtnOrder.TabIndex = 17;
            this.alpBtnOrder.Click += new System.EventHandler(this.alpBtnOrder_Click);
            // 
            // alpBtnExit
            // 
            this.alpBtnExit.BackgroundImage = null;
            this.alpBtnExit.Location = new System.Drawing.Point(233, 95);
            this.alpBtnExit.Name = "alpBtnExit";
            this.alpBtnExit.Size = new System.Drawing.Size(100, 85);
            this.alpBtnExit.TabIndex = 18;
            this.alpBtnExit.Click += new System.EventHandler(this.alpBtnExit_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(400, 190);
            this.ControlBox = false;
            this.Controls.Add(this.alpBtnExit);
            this.Controls.Add(this.alpBtnOrder);
            this.Controls.Add(this.alpBtnMMS);
            this.Controls.Add(this.alpBtnList);
            this.Controls.Add(this.gfbtnLeft);
            this.Controls.Add(this.panelWeather);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MenuForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelWeather;
        private AlphaControls.GradientFilledButton gfbtnLeft;
        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem menuItem1;
        private AlphaControls.AlphaButton alpBtnList;
        private AlphaControls.AlphaButton alpBtnMMS;
        private AlphaControls.AlphaButton alpBtnOrder;
        private AlphaControls.AlphaButton alpBtnExit;
    }
}