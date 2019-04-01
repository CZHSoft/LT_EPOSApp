using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OrderDish.Event;

namespace OrderDish.Model
{
    public partial class ListModel : UserControl, AlphaControls.IExtendedListItem
    {
        public ListModel()
        {
            InitializeComponent();
            
            SelectedChanged(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="foodname">名称</param>
        /// <param name="content">描述</param>
        /// <param name="price">价格</param>
        /// <param name="foodimage">图片</param>
        /// <param name="foodindex">索引</param>
        public ListModel(string id,string foodname, string price, Image foodimage,int foodindex)
        {
            InitializeComponent();

            FoodID = id;

            FoodName = foodname;
            //Content = content;
            FoodPrice = price;
            FoodImage = foodimage;
            FoodIndex = foodindex;

            SelectedChanged(false);
        }

        private string photo;

        public string Photo
        {
            get { return photo; }
            set
            {
                photo = value;
            }
        }

        public string FoodName
        {
            get { return lbFoodName.Text; }
            set 
            { 
                lbFoodName.Text = value;
            }
        }

        //public string Content
        //{
        //    get { return lbContent.Text; }
        //    set { lbContent.Text = value; }
        //}

        private string foodPrice;

        public string FoodPrice
        {
            get { return foodPrice; }
            set 
            { 
                foodPrice = value;
                lbPrice.Text = "Price:"+foodPrice;
            }
        }

        public Image FoodImage
        {
            get { return pbFood.Image; }
            set { pbFood.Image = value; }
        }

        private int foodIndex;

        public int FoodIndex
        {
            get { return foodIndex; }
            set { foodIndex = value; }
        }

        private string foodID;

        public string FoodID
        {
            get { return foodID; }
            set 
            { 
                foodID = value;
                lbFoodID.Text = "ID:"+foodID;
            }
        }

        #region IExtendedListItem Members

        public void SelectedChanged(bool isSelected)
        {
            if (isSelected)
            {
                Height = 108;
                pbFood.Size = new Size(100, 100);
                lbPrice.Visible = true;

                lbFoodName.Location = new Point(pbFood.Location.X+pbFood.Size.Width,lbFoodName.Location.Y);
                lbFoodName.Width -= 50;

                lbFoodID.Location = new Point(pbFood.Location.X + pbFood.Size.Width, lbFoodID.Location.Y);
                lbFoodID.Width -= 50;

                //lbFoodName.Font = new Font("Tahoma", 7F, FontStyle.);
                //lbFoodName.TextAlign = ContentAlignment.TopLeft;
                
                object sender=new object();
                EventArgs e=new EventArgs();

                sender = foodIndex;

                AlphaControls.PictureShiftPanelEvent.DoOnPicturnChange(sender, e);
            }
            else
            {
                Height = 58;
                pbFood.Size = new Size(50, 50);
                lbPrice.Visible = false;

                lbFoodName.Location = new Point(pbFood.Location.X + pbFood.Size.Width, lbFoodName.Location.Y);
                lbFoodName.Width += 50;

                lbFoodID.Location = new Point(pbFood.Location.X + pbFood.Size.Width, lbFoodID.Location.Y);
                lbFoodID.Width += 50;

                //lbFoodName.Font = new Font("Tahoma", 9F,FontStyle.Regular);
                //lbFoodName.TextAlign = ContentAlignment.TopRight;
            }
        }

        public void PositionChanged(int index)
        {
            if ((index & 1) == 0)
                BackColor = SystemColors.Control;
            else
                BackColor = SystemColors.ControlLight;
        }

        public string GetPhoto()
        {
            return photo;
        }

        #endregion

        private void btnBuy_Click(object sender, EventArgs e)
        {
            string[] orderInfo = new string[3] {FoodID,FoodName,FoodPrice};
            ListModelEvent.DoOnListModelBuyClick(orderInfo);
        }



    }
}
