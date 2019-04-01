namespace OrderDish
{
    partial class DetailForm
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
            foodDetailBitmap = null;

            pbpFood.aBtn.Click -= new System.EventHandler(aBtn_Click);
            pbpFood.Dispose();

            this.alpBtnReturn.Click -= new System.EventHandler(this.alpBtnReturn_Click);
            this.alpBtnReturn.Dispose();

            

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
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.alphaRemarkPanel1 = new AlphaControls.RemarkPanel();
            this.alpBtnReturn = new AlphaControls.AlphaButton();
            this.pbpFood = new AlphaControls.PictureBoxPlus();
            this.SuspendLayout();
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
            // alphaRemarkPanel1
            // 
            this.alphaRemarkPanel1.BackColor = System.Drawing.SystemColors.Info;
            this.alphaRemarkPanel1.Location = new System.Drawing.Point(180, 0);
            this.alphaRemarkPanel1.Name = "alphaRemarkPanel1";
            this.alphaRemarkPanel1.Size = new System.Drawing.Size(150, 190);
            this.alphaRemarkPanel1.TabIndex = 4;
            this.alphaRemarkPanel1.Text = "alphaRemarkPanel1";
            // 
            // alpBtnReturn
            // 
            this.alpBtnReturn.Location = new System.Drawing.Point(350, 140);
            this.alpBtnReturn.Name = "alpBtnReturn";
            this.alpBtnReturn.Size = new System.Drawing.Size(50, 50);
            this.alpBtnReturn.TabIndex = 3;
            this.alpBtnReturn.Text = "Return";
            this.alpBtnReturn.Click += new System.EventHandler(this.alpBtnReturn_Click);
            // 
            // pbpFood
            // 
            this.pbpFood.Location = new System.Drawing.Point(0, 0);
            this.pbpFood.Name = "pbpFood";
            this.pbpFood.Size = new System.Drawing.Size(180, 190);
            this.pbpFood.TabIndex = 2;
            // 
            // DetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(400, 190);
            this.ControlBox = false;
            this.Controls.Add(this.alphaRemarkPanel1);
            this.Controls.Add(this.alpBtnReturn);
            this.Controls.Add(this.pbpFood);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "DetailForm";
            this.Load += new System.EventHandler(this.DetailForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private AlphaControls.PictureBoxPlus pbpFood;
        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem menuItem1;
        private AlphaControls.AlphaButton alpBtnReturn;
        private AlphaControls.RemarkPanel alphaRemarkPanel1;
    }
}