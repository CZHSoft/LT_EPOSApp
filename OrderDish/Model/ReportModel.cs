using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace OrderDish.Model
{
    public class OrderModel
    {
         public OrderModel()
        {

        }

        public OrderModel(string id,string name,string price,string amount,string rm)
        {
            Id = id;
            FoodName = name;
            Price = price;
            Amount = amount;
            Remark = rm;
        }

        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string foodName;

        public string FoodName
        {
            get { return foodName; }
            set { foodName = value; }
        }


        private string price;

        public string Price
        {
            get { return price; }
            set { price = value; }
        }

        private string amount;

        public string Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        private string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }


        private bool _IsDisposed = false;


        #region Dispose
        ~OrderModel() 
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
            } 
            _IsDisposed = true;
        }
        #endregion
    }
}
