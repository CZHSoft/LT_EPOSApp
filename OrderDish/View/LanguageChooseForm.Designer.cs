namespace OrderDish.View
{
    partial class LanguageChooseForm
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
            this.btnChinese = new System.Windows.Forms.Button();
            this.btnEnglish = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnChinese
            // 
            this.btnChinese.Location = new System.Drawing.Point(32, 37);
            this.btnChinese.Name = "btnChinese";
            this.btnChinese.Size = new System.Drawing.Size(330, 55);
            this.btnChinese.TabIndex = 0;
            this.btnChinese.Text = "中文";
            this.btnChinese.Click += new System.EventHandler(this.btnChinese_Click);
            // 
            // btnEnglish
            // 
            this.btnEnglish.Location = new System.Drawing.Point(32, 125);
            this.btnEnglish.Name = "btnEnglish";
            this.btnEnglish.Size = new System.Drawing.Size(330, 55);
            this.btnEnglish.TabIndex = 1;
            this.btnEnglish.Text = "English";
            this.btnEnglish.Click += new System.EventHandler(this.btnEnglish_Click);
            // 
            // LanguageChooseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(400, 240);
            this.ControlBox = false;
            this.Controls.Add(this.btnEnglish);
            this.Controls.Add(this.btnChinese);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "LanguageChooseForm";
            this.Text = "语言选择";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LanguageChooseForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnChinese;
        private System.Windows.Forms.Button btnEnglish;
    }
}