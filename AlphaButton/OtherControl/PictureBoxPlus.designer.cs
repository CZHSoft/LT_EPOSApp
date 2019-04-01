namespace AlphaControls
{
    partial class PictureBoxPlus
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
            this.resImage = null;

            this.aBtn.Dispose();

            this.aStrPanel.Dispose();

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
            this.aBtn = new AlphaControls.AlphaButton();
            this.aStrPanel = new AlphaControls.AlphaStringPanel();
            this.SuspendLayout();
            // 
            // aBtn
            // 
            this.aBtn.BackgroundImage = null;
            this.aBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.aBtn.Location = new System.Drawing.Point(0, 131);
            this.aBtn.Name = "aBtn";
            this.aBtn.Size = new System.Drawing.Size(150, 19);
            this.aBtn.TabIndex = 1;
            this.aBtn.Text = "购买";
            // 
            // aStrPanel
            // 
            this.aStrPanel.BackgroundImage = null;
            this.aStrPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.aStrPanel.Location = new System.Drawing.Point(0, 0);
            this.aStrPanel.Name = "aStrPanel";
            this.aStrPanel.Size = new System.Drawing.Size(150, 36);
            this.aStrPanel.TabIndex = 0;
            // 
            // PictureBoxPlus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.aBtn);
            this.Controls.Add(this.aStrPanel);
            this.Name = "PictureBoxPlus";
            this.ResumeLayout(false);

        }

        #endregion

        private AlphaStringPanel aStrPanel;
        public AlphaButton aBtn;

    }
}
