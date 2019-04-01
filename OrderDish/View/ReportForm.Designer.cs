namespace OrderDish.View
{
    partial class ReportForm
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
            this.lvReport = new System.Windows.Forms.ListView();
            this.cmReport = new System.Windows.Forms.ContextMenu();
            this.menuItemModCount = new System.Windows.Forms.MenuItem();
            this.menuItemRemark = new System.Windows.Forms.MenuItem();
            this.menuItemDel = new System.Windows.Forms.MenuItem();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnReflash = new System.Windows.Forms.Button();
            this.cmOrder = new System.Windows.Forms.ContextMenu();
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItemMC = new System.Windows.Forms.MenuItem();
            this.menuItemRM = new System.Windows.Forms.MenuItem();
            this.menuItemDel2 = new System.Windows.Forms.MenuItem();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.SuspendLayout();
            // 
            // lvReport
            // 
            this.lvReport.FullRowSelect = true;
            this.lvReport.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvReport.Location = new System.Drawing.Point(0, 0);
            this.lvReport.Name = "lvReport";
            this.lvReport.Size = new System.Drawing.Size(320, 165);
            this.lvReport.TabIndex = 0;
            this.lvReport.View = System.Windows.Forms.View.Details;
            // 
            // cmReport
            // 
            this.cmReport.MenuItems.Add(this.menuItemModCount);
            this.cmReport.MenuItems.Add(this.menuItemRemark);
            this.cmReport.MenuItems.Add(this.menuItemDel);
            // 
            // menuItemModCount
            // 
            this.menuItemModCount.Text = "修改数量";
            this.menuItemModCount.Click += new System.EventHandler(this.menuItemModCount_Click);
            // 
            // menuItemRemark
            // 
            this.menuItemRemark.Text = "添加口味备注";
            this.menuItemRemark.Click += new System.EventHandler(this.menuItemRemark_Click);
            // 
            // menuItemDel
            // 
            this.menuItemDel.Text = "删除";
            this.menuItemDel.Click += new System.EventHandler(this.menuItemDel_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(325, 115);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(72, 50);
            this.btnReturn.TabIndex = 1;
            this.btnReturn.Text = "返回";
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(325, 5);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(72, 50);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnReflash
            // 
            this.btnReflash.Location = new System.Drawing.Point(325, 60);
            this.btnReflash.Name = "btnReflash";
            this.btnReflash.Size = new System.Drawing.Size(72, 50);
            this.btnReflash.TabIndex = 3;
            this.btnReflash.Text = "刷新";
            this.btnReflash.Click += new System.EventHandler(this.btnReflash_Click);
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.menuItem1);
            this.mainMenu.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "截屏";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.MenuItems.Add(this.menuItemMC);
            this.menuItem2.MenuItems.Add(this.menuItemRM);
            this.menuItem2.MenuItems.Add(this.menuItemDel2);
            this.menuItem2.Text = "菜单修改";
            // 
            // menuItemMC
            // 
            this.menuItemMC.Text = "修改数量";
            this.menuItemMC.Click += new System.EventHandler(this.menuItemMC_Click);
            // 
            // menuItemRM
            // 
            this.menuItemRM.Text = "添加口味备注";
            this.menuItemRM.Click += new System.EventHandler(this.menuItemRM_Click);
            // 
            // menuItemDel2
            // 
            this.menuItemDel2.Text = "删除菜项";
            this.menuItemDel2.Click += new System.EventHandler(this.menuItemDel2_Click);
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 168);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(400, 22);
            this.statusBar.Text = "合计:";
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(400, 190);
            this.ControlBox = false;
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.btnReflash);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.lvReport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "ReportForm";
            this.Load += new System.EventHandler(this.ReportForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvReport;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.ContextMenu cmReport;
        private System.Windows.Forms.MenuItem menuItemModCount;
        private System.Windows.Forms.MenuItem menuItemDel;
        private System.Windows.Forms.MenuItem menuItemRemark;
        private System.Windows.Forms.Button btnReflash;
        private System.Windows.Forms.ContextMenu cmOrder;
        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItemMC;
        private System.Windows.Forms.MenuItem menuItemRM;
        private System.Windows.Forms.MenuItem menuItemDel2;
        private System.Windows.Forms.StatusBar statusBar;

    }
}