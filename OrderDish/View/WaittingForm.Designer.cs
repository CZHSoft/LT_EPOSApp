namespace OrderDish.View
{
    partial class WaittingForm
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
            this.timerWatting.Enabled = false;
            this.timerWatting.Tick -= new System.EventHandler(this.timerWatting_Tick);
            this.timerWatting.Dispose();

            this.labelMsg.Dispose();

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
            this.labelMsg = new System.Windows.Forms.Label();
            this.timerWatting = new System.Windows.Forms.Timer();
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.SuspendLayout();
            // 
            // labelMsg
            // 
            this.labelMsg.BackColor = System.Drawing.SystemColors.ControlDark;
            this.labelMsg.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.labelMsg.Location = new System.Drawing.Point(140, 91);
            this.labelMsg.Name = "labelMsg";
            this.labelMsg.Size = new System.Drawing.Size(100, 20);
            this.labelMsg.Text = "Please wait...";
            this.labelMsg.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // timerWatting
            // 
            this.timerWatting.Interval = 2000;
            this.timerWatting.Tick += new System.EventHandler(this.timerWatting_Tick);
            // 
            // WaittingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(400, 190);
            this.ControlBox = false;
            this.Controls.Add(this.labelMsg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "WaittingForm";
            this.Text = "等待中...";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.WaittingForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelMsg;
        private System.Windows.Forms.Timer timerWatting;
        private System.Windows.Forms.MainMenu mainMenu;
    }
}