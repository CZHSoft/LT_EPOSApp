namespace AlphaControls
{
    partial class PicShiftPanel
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
            //showImageList = null;
            if (foodModelList != null)
            {
                foodModelList.Clear();
                foodModelList = null;
            }

            this.picShiftControl.DoubleClick -= new System.EventHandler(this.picShiftControl_DoubleClick);
            this.picShiftControl.Dispose();

            this.panel.Dispose();

            this.btnNext.Click -= new System.EventHandler(this.btnNext_Click);
            this.btnNext.Dispose();

            this.btnLast.Click -= new System.EventHandler(this.btnLast_Click);
            this.btnLast.Dispose();

            this.btnBuy.Click -= new System.EventHandler(this.btnBuy_Click);
            this.btnBuy.Dispose();

            this.lbCount.Dispose();

            this.gestureRecognizer.Scroll -= new System.EventHandler<Microsoft.WindowsMobile.Gestures.GestureScrollEventArgs>(this.gestureRecognizer_Scroll);
            //this.gestureRecognizer.Dispose();

            PictureShiftPanelEvent.OnPictureSelectChange -= new PictureShiftPanelEvent.PicturnTurnDelegate(PictureTurnEvent_OnPictureSelectChange);
            
            
            
            
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
            this.panel = new System.Windows.Forms.Panel();
            this.picShiftControl = new AlphaControls.PicShiftControl();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.lbCount = new System.Windows.Forms.Label();
            this.gestureRecognizer = new Microsoft.WindowsMobile.Gestures.GestureRecognizer();
            this.btnBuy = new System.Windows.Forms.Button();
            this.lbID = new System.Windows.Forms.Label();
            this.lbName = new System.Windows.Forms.Label();
            this.lbPrice = new System.Windows.Forms.Label();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.picShiftControl);
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(180, 160);
            // 
            // picShiftControl
            // 
            this.picShiftControl.Location = new System.Drawing.Point(5, 5);
            this.picShiftControl.Name = "picShiftControl";
            this.picShiftControl.Size = new System.Drawing.Size(170, 150);
            this.picShiftControl.TabIndex = 0;
            this.picShiftControl.Text = "picShiftControl";
            this.picShiftControl.DoubleClick += new System.EventHandler(this.picShiftControl_DoubleClick);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(185, 5);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(50, 20);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "下一个";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(185, 30);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(50, 20);
            this.btnLast.TabIndex = 2;
            this.btnLast.Text = "上一个";
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // lbCount
            // 
            this.lbCount.Location = new System.Drawing.Point(184, 55);
            this.lbCount.Name = "lbCount";
            this.lbCount.Size = new System.Drawing.Size(50, 15);
            this.lbCount.Text = "?/?";
            this.lbCount.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // gestureRecognizer
            // 
            this.gestureRecognizer.TargetControl = this.picShiftControl;
            this.gestureRecognizer.Scroll += new System.EventHandler<Microsoft.WindowsMobile.Gestures.GestureScrollEventArgs>(this.gestureRecognizer_Scroll);
            // 
            // btnBuy
            // 
            this.btnBuy.Location = new System.Drawing.Point(185, 125);
            this.btnBuy.Name = "btnBuy";
            this.btnBuy.Size = new System.Drawing.Size(50, 30);
            this.btnBuy.TabIndex = 4;
            this.btnBuy.Text = "下单";
            this.btnBuy.Click += new System.EventHandler(this.btnBuy_Click);
            // 
            // lbID
            // 
            this.lbID.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.lbID.Location = new System.Drawing.Point(185, 75);
            this.lbID.Name = "lbID";
            this.lbID.Size = new System.Drawing.Size(50, 15);
            this.lbID.Text = "ID:";
            // 
            // lbName
            // 
            this.lbName.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.lbName.Location = new System.Drawing.Point(185, 90);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(50, 15);
            this.lbName.Text = "Name:";
            // 
            // lbPrice
            // 
            this.lbPrice.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.lbPrice.Location = new System.Drawing.Point(185, 105);
            this.lbPrice.Name = "lbPrice";
            this.lbPrice.Size = new System.Drawing.Size(50, 15);
            this.lbPrice.Text = "Price:";
            // 
            // PicShiftPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.lbPrice);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.lbID);
            this.Controls.Add(this.btnBuy);
            this.Controls.Add(this.lbCount);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.panel);
            this.Name = "PicShiftPanel";
            this.Size = new System.Drawing.Size(240, 160);
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PicShiftControl picShiftControl;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Label lbCount;
        private Microsoft.WindowsMobile.Gestures.GestureRecognizer gestureRecognizer;
        private System.Windows.Forms.Button btnBuy;
        private System.Windows.Forms.Label lbID;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Label lbPrice;
    }
}
