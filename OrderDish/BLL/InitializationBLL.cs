using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.SqlServerCe;
using OrderDish.DAL;
using System.Data;

namespace OrderDish.BLL
{
    public class InitializationBLL
    {
        private string dataSource;

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


        #region 
        /// <summary>
        /// 检查本地数据库是否存在,存在:True
        /// </summary>
        /// <returns>存在:True</returns>
        public bool CheckDbExist()
        {
            if (File.Exists(GlobalVariable.LocalPath + "\\AppDB.sdf"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 通过查询数据获取表的存在性
        /// </summary>
        /// <param name="checkTableSQL"></param>
        /// <returns></returns>
        public bool CheckTableExist(string checkTableSQL)
        {
            DAL.SqlCeDAL sqlceDAl = new SqlCeDAL();
            if (!sqlceDAl.DbOpen(DataSource))
            {
                return false;
            }
            try
            {
                if (sqlceDAl.ExecuteScalar(checkTableSQL))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 创建本地数据源
        /// </summary>
        /// <returns></returns>
        public bool CreateDb()
        {
            try
            {
                SqlCeEngine sqlCeEngine = new SqlCeEngine(DataSource);
                sqlCeEngine.CreateDatabase();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 本地数据源新建表
        /// </summary>
        /// <param name="createTableSQL"></param>
        /// <returns></returns>
        public bool CreateTable(string createTableSQL)
        {
            DAL.SqlCeDAL sqlceDAl = new SqlCeDAL();
            if (!sqlceDAl.DbOpen(DataSource))
            {
                return false;
            }
            try
            {
                sqlceDAl.ExecuteNonQueryWithCreateTable(createTableSQL);

                return true;

            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// ExecuteScalar0返回第一行第一列并检查是否为"1"
        /// </summary>
        /// <param name="checkOSSQL"></param>
        /// <returns></returns>
        public bool CheckLocalOS(string checkOSSQL)
        {
            DAL.SqlCeDAL sqlceDAl = new SqlCeDAL();
            if (!sqlceDAl.DbOpen(DataSource))
            {
                return false;
            }
            try
            {
                if ((int)sqlceDAl.ExecuteScalarObject(checkOSSQL) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取本地数据源DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="srcTable"></param>
        /// <returns></returns>
        public DataSet GetLocalDataSet(string sql, string srcTable)
        {
            SqlCeDAL sqlCeDal = new SqlCeDAL();
            if (!sqlCeDal.DbOpen(DataSource))
            {
                return null;
            }
            DataSet ds = sqlCeDal.DataAdapter(sql, srcTable);
            return ds;
        }

        /// <summary>
        /// 本地数据库 增删改
        /// </summary>
        /// <param name="RwFoodSQL"></param>
        /// <returns></returns>
        /// 
        public bool LocalExecuteNonQuery(string sql)
        {
            DAL.SqlCeDAL sqlceDAl = new SqlCeDAL();
            if (!sqlceDAl.DbOpen(DataSource))
            {
                return false;
            }
            try
            {
                if (sqlceDAl.ExecuteNonQuery(sql))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 验证设备合法性
        /// </summary>
        /// <param name="deviceID"></param>
        /// <returns></returns>
        public bool VerifyDevice(string deviceID)
        {
            SqlDAL sqlDal = new SqlDAL();
            return sqlDal.VerifyDevice(deviceID);
        }

        /// <summary>
        /// 服务器端获取DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="srcTable"></param>
        /// <returns></returns>
        public DataSet GetServerDataSet(string sql, string srcTable)
        {
            SqlDAL sqlDal = new SqlDAL();
            DataSet ds = sqlDal.DataAdapter(sql, srcTable);
            return ds;
        }

        //public bool RwLocalFood(string RwFoodSQL)
        //{
        //    DAL.SqlCeDAL sqlceDAl = new SqlCeDAL();
        //    if (!sqlceDAl.DbOpen(DataSource))
        //    {
        //        return false;
        //    }
        //    try
        //    {
        //        if (sqlceDAl.ExecuteNonQuery(RwFoodSQL))
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //public bool RwLocalOS(string RwVerSQL)
        //{
        //    DAL.SqlCeDAL sqlceDAl = new SqlCeDAL();
        //    if (!sqlceDAl.DbOpen(DataSource))
        //    {
        //        return false;
        //    }
        //    try
        //    {
        //        if (sqlceDAl.ExecuteNonQuery(RwVerSQL))
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}


        //public bool RwLocalMenu(string RwMenuSQL)
        //{
        //    DAL.SqlCeDAL sqlceDAl = new SqlCeDAL();
        //    if (!sqlceDAl.DbOpen(DataSource))
        //    {
        //        return false;
        //    }
        //    try
        //    {
        //        if (sqlceDAl.ExecuteNonQuery(RwMenuSQL))
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //public DataSet GetServerOS(string sql, string srcTable)
        //{
        //    SqlDAL sqlDal = new SqlDAL();
        //    DataSet ds = sqlDal.DataAdapter(sql, srcTable);
        //    return ds;
        //}

        //public DataSet GetServerMenu(string sql, string srcTable)
        //{
        //    SqlDAL sqlDal = new SqlDAL();
        //    DataSet ds = sqlDal.DataAdapter(sql, srcTable);
        //    return ds;
        //}

        //public DataSet GetLocalMenu(string sql, string srcTable)
        //{
        //    SqlCeDAL sqlCeDal = new SqlCeDAL();
        //    if (!sqlCeDal.DbOpen(DataSource))
        //    {
        //        return null;
        //    }
        //    DataSet ds = sqlCeDal.DataAdapter(sql, srcTable);
        //    return ds;
        //}

        //public DataSet GetServerFood(string sql, string srcTable)
        //{
        //    SqlDAL sqlDal = new SqlDAL();
        //    DataSet ds = sqlDal.DataAdapter(sql, srcTable);
        //    return ds;
        //}

        //public DataSet GetLocalFood(string sql, string srcTable)
        //{
        //    SqlCeDAL sqlCeDal = new SqlCeDAL();
        //    if (!sqlCeDal.DbOpen(DataSource))
        //    {
        //        return null;
        //    }
        //    DataSet ds = sqlCeDal.DataAdapter(sql, srcTable);
        //    return ds;
        //}

        #endregion

    }
}
