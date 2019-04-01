namespace AlphaControls
{
    partial class AlphaBarPanel
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
            this.timerDateTime.Enabled = false;
            this.timerDateTime.Tick -= new System.EventHandler(this.timerDateTime_Tick);
            this.timerDateTime.Dispose();
            
            this.alpBtnReturn.Click -= new System.EventHandler(this.alpBtnReturn_Click);
            this.alpBtnReturn.Dispose();

            this.alpTimer.Dispose();
            
            this.backgroundImage = null;

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.timerDateTime = new System.Windows.Forms.Timer();
            this.alpTimer = new AlphaControls.AlphaTimer();
            this.alpBtnReturn = new AlphaControls.AlphaButton();
            this.alpBtnCheck = new AlphaControls.AlphaButton();
            this.alpBtnSubmit = new AlphaControls.AlphaButton();
            this.tbFind = new System.Windows.Forms.TextBox();
            this.alpBtnFind = new AlphaControls.AlphaButton();
            this.SuspendLayout();
            // 
            // timerDateTime
            // 
            this.timerDateTime.Interval = 6000;
            this.timerDateTime.Tick += new System.EventHandler(this.timerDateTime_Tick);
            // 
            // alpTimer
            // 
            this.alpTimer.BackgroundImage = null;
            this.alpTimer.Location = new System.Drawing.Point(300, 5);
            this.alpTimer.Name = "alpTimer";
            this.alpTimer.Size = new System.Drawing.Size(60, 20);
            this.alpTimer.TabIndex = 1;
            this.alpTimer.Text = "00:00";
            // 
            // alpBtnReturn
            // 
            this.alpBtnReturn.BackgroundImage = null;
            this.alpBtnReturn.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.alpBtnReturn.Location = new System.Drawing.Point(5, 5);
            this.alpBtnReturn.Name = "alpBtnReturn";
            this.alpBtnReturn.Size = new System.Drawing.Size(50, 20);
            this.alpBtnReturn.TabIndex = 0;
            this.alpBtnReturn.Text = "返回";
            this.alpBtnReturn.Click += new System.EventHandler(this.alpBtnReturn_Click);
            // 
            // alpBtnCheck
            // 
            this.alpBtnCheck.BackgroundImage = null;
            this.alpBtnCheck.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.alpBtnCheck.Location = new System.Drawing.Point(60, 5);
            this.alpBtnCheck.Name = "alpBtnCheck";
            this.alpBtnCheck.Size = new System.Drawing.Size(50, 20);
            this.alpBtnCheck.TabIndex = 2;
            this.alpBtnCheck.Text = "查看订单";
            this.alpBtnCheck.Click += new System.EventHandler(this.alpBtnCheck_Click);
            // 
            // alpBtnSubmit
            // 
            this.alpBtnSubmit.BackgroundImage = null;
            this.alpBtnSubmit.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.alpBtnSubmit.Location = new System.Drawing.Point(115, 5);
            this.alpBtnSubmit.Name = "alpBtnSubmit";
            this.alpBtnSubmit.Size = new System.Drawing.Size(50, 20);
            this.alpBtnSubmit.TabIndex = 3;
            this.alpBtnSubmit.Text = "提交订单";
            this.alpBtnSubmit.Click += new System.EventHandler(this.alpBtnSubmit_Click);
            // 
            // tbFind
            // 
            this.tbFind.Location = new System.Drawing.Point(170, 5);
            this.tbFind.Name = "tbFind";
            this.tbFind.Size = new System.Drawing.Size(80, 21);
            this.tbFind.TabIndex = 4;
            // 
            // alpBtnFind
            // 
            this.alpBtnFind.BackgroundImage = null;
            this.alpBtnFind.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.alpBtnFind.Location = new System.Drawing.Point(255, 5);
            this.alpBtnFind.Name = "alpBtnFind";
            this.alpBtnFind.Size = new System.Drawing.Size(40, 20);
            this.alpBtnFind.TabIndex = 5;
            this.alpBtnFind.Text = "Find";
            this.alpBtnFind.Click += new System.EventHandler(this.alpBtnFind_Click);
            // 
            // AlphaBarPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.alpBtnFind);
            this.Controls.Add(this.tbFind);
            this.Controls.Add(this.alpBtnSubmit);
            this.Controls.Add(this.alpBtnCheck);
            this.Controls.Add(this.alpTimer);
            this.Controls.Add(this.alpBtnReturn);
            this.Name = "AlphaBarPanel";
            this.Size = new System.Drawing.Size(360, 30);
            this.ResumeLayout(false);

        }

        #endregion

        private AlphaButton alpBtnReturn;
        private System.Windows.Forms.Timer timerDateTime;
        private AlphaTimer alpTimer;
        private AlphaButton alpBtnCheck;
        private AlphaButton alpBtnSubmit;
        private System.Windows.Forms.TextBox tbFind;
        private AlphaButton alpBtnFind;
    }
}
