namespace OrderDish.View
{
    partial class MMSForm
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
            this.btnExample = new System.Windows.Forms.Button();
            this.WMP = new AxWMPLib.AxWindowsMediaPlayer();
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.lvFile = new System.Windows.Forms.ListView();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnExample
            // 
            this.btnExample.Location = new System.Drawing.Point(295, 5);
            this.btnExample.Name = "btnExample";
            this.btnExample.Size = new System.Drawing.Size(50, 50);
            this.btnExample.TabIndex = 13;
            this.btnExample.Text = "Play";
            this.btnExample.Click += new System.EventHandler(this.btnExample_Click);
            // 
            // WMP
            // 
            this.WMP.Dock = System.Windows.Forms.DockStyle.Left;
            this.WMP.Location = new System.Drawing.Point(0, 0);
            this.WMP.Name = "WMP";
            this.WMP.Size = new System.Drawing.Size(190, 190);
            this.WMP.TabIndex = 12;
            this.WMP.Text = "AxWindowsMediaPlayer";
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
            // lvFile
            // 
            this.lvFile.FullRowSelect = true;
            this.lvFile.Location = new System.Drawing.Point(190, 0);
            this.lvFile.Name = "lvFile";
            this.lvFile.Size = new System.Drawing.Size(100, 190);
            this.lvFile.TabIndex = 14;
            this.lvFile.View = System.Windows.Forms.View.Details;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(345, 135);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(50, 50);
            this.btnExit.TabIndex = 15;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // MMSForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(400, 190);
            this.ControlBox = false;
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lvFile);
            this.Controls.Add(this.btnExample);
            this.Controls.Add(this.WMP);
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "MMSForm";
            this.Load += new System.EventHandler(this.MMSForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExample;
        internal AxWMPLib.AxWindowsMediaPlayer WMP;
        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.ListView lvFile;
        private System.Windows.Forms.Button btnExit;
    }
}