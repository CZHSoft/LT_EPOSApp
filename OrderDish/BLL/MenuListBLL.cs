using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OrderDish.DAL;
using System.Data;

namespace OrderDish.BLL
{
    public class MenuListBLL:IDisposable
    {
        private string dataSource;
        private DAL.SqlCeDAL sqlCeDal;
        private bool _IsDisposed = false;


        public string DataSource
        {
            get
            {
                if (string.IsNullOrEmpty(dataSource))
                {
                    dataSource = string.Format("Data source={0}",
                        GlobalVariable.LocalPath + "\\AppDB.sdf");
                }
                return dataSource;
            }
            set { dataSource = value; }
        }

        /// <summary>
        /// 获取本地菜单 返回数据集
        /// </summary>
        /// <param name="querySQL"></param>
        /// <param name="srcTable"></param>
        /// <returns></returns>
        public DataSet GetMenu(string sql,string srcTable)
        {
            sqlCeDal = new SqlCeDAL();

            if (!sqlCeDal.DbOpen(DataSource))
            {
                return null;
            }
            try
            {
                return sqlCeDal.DataAdapter(sql, srcTable);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取本地食物 返回数据集
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="srcTable"></param>
        /// <returns></returns>
        public DataSet GetFood(string sql, string srcTable)
        {
            sqlCeDal = new SqlCeDAL();

            if (!sqlCeDal.DbOpen(DataSource))
            {
                return null;
            }
            try
            {
                return sqlCeDal.DataAdapter(sql, srcTable);
            }
            catch
            {
                return null;
            }
        }


         #region Dispose
        ~MenuListBLL() 
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
                sqlCeDal = null;
                sqlCeDal.Dispose();
            } 
            _IsDisposed = true;
        }

        #endregion

    }
}
