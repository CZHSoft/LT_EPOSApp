namespace OrderDishLauncher
{
    partial class SplashScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelStatusMsg = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.timerCal = new System.Windows.Forms.Timer();
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // labelStatusMsg
            // 
            this.labelStatusMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStatusMsg.ForeColor = System.Drawing.Color.Blue;
            this.labelStatusMsg.Location = new System.Drawing.Point(0, 120);
            this.labelStatusMsg.Name = "labelStatusMsg";
            this.labelStatusMsg.Size = new System.Drawing.Size(400, 20);
            this.labelStatusMsg.Text = "初始化应用程序 ...";
            this.labelStatusMsg.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pbLogo
            // 
            this.pbLogo.Location = new System.Drawing.Point(147, 72);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(100, 50);
            // 
            // timerCal
            // 
            this.timerCal.Interval = 8000;
            this.timerCal.Tick += new System.EventHandler(this.timerCal_Tick);
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.menuItemExit);
            this.mainMenu.MenuItems.Add(this.menuItem1);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Text = "退出";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "截屏";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(400, 210);
            this.ControlBox = false;
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.labelStatusMsg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 0);
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "SplashScreen";
            this.Text = "MyAppLauncher";
            this.Load += new System.EventHandler(this.SplashScreen_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelStatusMsg;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Timer timerCal;
        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem menuItemExit;
        private System.Windows.Forms.MenuItem menuItem1;
    }
}

