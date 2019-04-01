namespace OrderDish.View
{
    partial class OrderVerifyForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.sbTotal = new System.Windows.Forms.StatusBar();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.lvOrder = new System.Windows.Forms.ListView();
            this.labelTitle = new System.Windows.Forms.Label();
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.cmbWaiter = new System.Windows.Forms.ComboBox();
            this.labelWaiter = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDesk = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(324, 33);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 20);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "返回";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // sbTotal
            // 
            this.sbTotal.Location = new System.Drawing.Point(0, 168);
            this.sbTotal.Name = "sbTotal";
            this.sbTotal.Size = new System.Drawing.Size(400, 22);
            this.sbTotal.Text = "合计:";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(4, 33);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(72, 20);
            this.btnSubmit.TabIndex = 8;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // lvOrder
            // 
            this.lvOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvOrder.Location = new System.Drawing.Point(4, 59);
            this.lvOrder.Name = "lvOrder";
            this.lvOrder.Size = new System.Drawing.Size(392, 101);
            this.lvOrder.TabIndex = 7;
            this.lvOrder.View = System.Windows.Forms.View.Details;
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTitle.Location = new System.Drawing.Point(4, 2);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(392, 18);
            this.labelTitle.Text = "清单列表";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            // cmbWaiter
            // 
            this.cmbWaiter.Location = new System.Drawing.Point(162, 31);
            this.cmbWaiter.Name = "cmbWaiter";
            this.cmbWaiter.Size = new System.Drawing.Size(55, 22);
            this.cmbWaiter.TabIndex = 12;
            // 
            // labelWaiter
            // 
            this.labelWaiter.Location = new System.Drawing.Point(82, 33);
            this.labelWaiter.Name = "labelWaiter";
            this.labelWaiter.Size = new System.Drawing.Size(79, 20);
            this.labelWaiter.Text = "服务员编号:";
            this.labelWaiter.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(220, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 20);
            this.label1.Text = "台号:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmbDesk
            // 
            this.cmbDesk.Location = new System.Drawing.Point(265, 31);
            this.cmbDesk.Name = "cmbDesk";
            this.cmbDesk.Size = new System.Drawing.Size(55, 22);
            this.cmbDesk.TabIndex = 17;
            // 
            // OrderVerifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(400, 190);
            this.ControlBox = false;
            this.Controls.Add(this.cmbDesk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelWaiter);
            this.Controls.Add(this.cmbWaiter);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.sbTotal);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.lvOrder);
            this.Controls.Add(this.labelTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "OrderVerifyForm";
            this.Load += new System.EventHandler(this.OrderVerifyForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.StatusBar sbTotal;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.ListView lvOrder;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.ComboBox cmbWaiter;
        private System.Windows.Forms.Label labelWaiter;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDesk;
    }
}