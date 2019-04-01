namespace AlphaControls
{
    partial class ScrollPanel
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

            this.vScrollBar.ValueChanged += new System.EventHandler(this.vScrollBar_ValueChanged);
            this.vScrollBar.Dispose();

            this.panelMenu.Dispose();

            this.panelContainer.Dispose();

            this.aTBtnDown.Dispose();

            this.aTBtnUP.Dispose();



            ScrollPanelEvent.OnVScrollBarValueChange -= new ScrollPanelEvent.VScrollBarDelegate(VScrollBarEvent_OnVScrollBarValueChange);

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
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.aTBtnDown = new AlphaControls.AlphaTimerButton();
            this.aTBtnUP = new AlphaControls.AlphaTimerButton();
            this.panelContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // vScrollBar
            // 
            this.vScrollBar.Location = new System.Drawing.Point(50, 0);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(13, 240);
            this.vScrollBar.TabIndex = 0;
            this.vScrollBar.Visible = false;
            this.vScrollBar.ValueChanged += new System.EventHandler(this.vScrollBar_ValueChanged);
            // 
            // panelMenu
            // 
            this.panelMenu.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(40, 160);
            // 
            // panelContainer
            // 
            this.panelContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContainer.Controls.Add(this.panelMenu);
            this.panelContainer.Location = new System.Drawing.Point(0, 40);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(40, 160);
            // 
            // aTBtnDown
            // 
            this.aTBtnDown.BackgroundImage = null;
            this.aTBtnDown.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.aTBtnDown.Location = new System.Drawing.Point(0, 200);
            this.aTBtnDown.Name = "aTBtnDown";
            this.aTBtnDown.Size = new System.Drawing.Size(40, 40);
            this.aTBtnDown.TabIndex = 2;
            this.aTBtnDown.Text = "Down";
            // 
            // aTBtnUP
            // 
            this.aTBtnUP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.aTBtnUP.BackgroundImage = null;
            this.aTBtnUP.Location = new System.Drawing.Point(0, 0);
            this.aTBtnUP.Name = "aTBtnUP";
            this.aTBtnUP.Size = new System.Drawing.Size(40, 40);
            this.aTBtnUP.TabIndex = 1;
            this.aTBtnUP.Text = "Up";
            // 
            // ScrollPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.aTBtnDown);
            this.Controls.Add(this.aTBtnUP);
            this.Controls.Add(this.vScrollBar);
            this.Name = "ScrollPanel";
            this.Size = new System.Drawing.Size(40, 240);
            this.panelContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar vScrollBar;
        private System.Windows.Forms.Panel panelMenu;
        private AlphaTimerButton aTBtnUP;
        private AlphaTimerButton aTBtnDown;
        private System.Windows.Forms.Panel panelContainer;
    }
}
