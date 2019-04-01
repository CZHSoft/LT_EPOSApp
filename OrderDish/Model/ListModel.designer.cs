namespace OrderDish.Model
{
    partial class ListModel
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
            //pbFood.Dispose();
            //btnBuy.Dispose();
            //lbFoodName.Dispose();
            //lbPrice.Dispose();
            //lbContent.Dispose();

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbFood = new System.Windows.Forms.PictureBox();
            this.lbFoodName = new System.Windows.Forms.Label();
            this.lbPrice = new System.Windows.Forms.Label();
            this.btnBuy = new System.Windows.Forms.Button();
            this.lbFoodID = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pbFood
            // 
            this.pbFood.Location = new System.Drawing.Point(4, 4);
            this.pbFood.Name = "pbFood";
            this.pbFood.Size = new System.Drawing.Size(64, 64);
            this.pbFood.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // lbFoodName
            // 
            this.lbFoodName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFoodName.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.lbFoodName.Location = new System.Drawing.Point(80, 20);
            this.lbFoodName.Name = "lbFoodName";
            this.lbFoodName.Size = new System.Drawing.Size(132, 20);
            this.lbFoodName.Text = "<FoodName>";
            // 
            // lbPrice
            // 
            this.lbPrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPrice.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.lbPrice.Location = new System.Drawing.Point(80, 40);
            this.lbPrice.Name = "lbPrice";
            this.lbPrice.Size = new System.Drawing.Size(132, 20);
            this.lbPrice.Text = "<Price>";
            this.lbPrice.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnBuy
            // 
            this.btnBuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuy.Location = new System.Drawing.Point(152, 46);
            this.btnBuy.Name = "btnBuy";
            this.btnBuy.Size = new System.Drawing.Size(60, 26);
            this.btnBuy.TabIndex = 4;
            this.btnBuy.Text = "¹ºÂò";
            this.btnBuy.Click += new System.EventHandler(this.btnBuy_Click);
            // 
            // lbFoodID
            // 
            this.lbFoodID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFoodID.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.lbFoodID.Location = new System.Drawing.Point(80, 0);
            this.lbFoodID.Name = "lbFoodID";
            this.lbFoodID.Size = new System.Drawing.Size(132, 20);
            this.lbFoodID.Text = "<ID>";
            // 
            // ListModel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.lbFoodID);
            this.Controls.Add(this.btnBuy);
            this.Controls.Add(this.lbFoodName);
            this.Controls.Add(this.pbFood);
            this.Controls.Add(this.lbPrice);
            this.Name = "ListModel";
            this.Size = new System.Drawing.Size(212, 72);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbFood;
        private System.Windows.Forms.Label lbFoodName;
        private System.Windows.Forms.Label lbPrice;
        private System.Windows.Forms.Button btnBuy;
        private System.Windows.Forms.Label lbFoodID;
    }
}
