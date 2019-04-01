using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace OrderDish.Model
{
    public class MenuModel:IDisposable
    {
        //private ImageList foodImageList=new ImageList();

        //private List<Image> _foodImageList = new List<Image>();

        //public List<Image> FoodImageList1
        //{
        //    get { return _foodImageList; }
        //    set { _foodImageList = value; }
        //}
        //public ImageList FoodImageList
        //{
        //    get { return foodImageList; }
        //    set { foodImageList = value; }
        //}

        public MenuModel()
        {

        }

        public MenuModel(string id,string n,Image im,string p)
        {
            FoodId = id;
            FoodName = n;
            FoodImage = im;
            Price = p;
        }

        private string foodId;

        public string FoodId
        {
            get { return foodId; }
            set { foodId = value; }
        }

        private string foodName;

        public string FoodName
        {
            get { return foodName; }
            set { foodName = value; }
        }

        private Image foodImage;

        public Image FoodImage
        {
            get { return foodImage; }
            set { foodImage = value; }
        }

        private string price;

        public string Price
        {
            get { return price; }
            set { price = value; }
        }


        private bool _IsDisposed = false;


        #region Dispose
        ~MenuModel() 
        { 
            Dispose(false); 
        }
        public void Dispose() 
        { 
            Dispose(true);
            GC.SuppressFinalize(true); 
        } 
        protected virtual void Dispose(bool IsDisposing) 
        { 
            if (_IsDisposed) return; 

            if (IsDisposing) 
            {
                foodImage.Dispose();
                //foodImageList.Images.Clear();
                //_foodImageList.Clear();
            } 
            _IsDisposed = true;
        }

        #endregion
    }
}
