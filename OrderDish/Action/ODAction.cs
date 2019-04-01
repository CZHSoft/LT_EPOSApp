using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using OrderDish.BLL;

namespace OrderDish.Action
{
    public class ODAction:IDisposable
    {
        private bool _IsDisposed = false;

        private BLL.MenuListBLL meunListBLL;

        /// <summary>
        /// ListForm 用 获取本地DB menu 数据集
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllMenuInfo()
        {
            meunListBLL = new MenuListBLL();
            return meunListBLL.GetMenu("Select * from menu","menu");
        }

        /// <summary>
        /// ListForm 用 获取本地DB food 数据集
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllFoodInfo()
        {
           meunListBLL = new MenuListBLL();
            return meunListBLL.GetFood("Select * from food", "food");
        }

        #region Dispose
        ~ODAction() 
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
                meunListBLL = null;
            } 
            _IsDisposed = true;
        }

        #endregion
    }
}
