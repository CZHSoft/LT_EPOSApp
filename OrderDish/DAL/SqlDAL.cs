using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using OrderDish.WebReference;


namespace OrderDish.DAL
{
    public class SqlDAL
    {
        /// <summary>
        /// 增删改
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool ExecuteNonQuery(string sql)
        {
            Service s = new Service();
            return s.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 查询返回第一行第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool ExecuteScalar(string sql)
        {
            Service s = new Service();
            return s.ExecuteScalar(sql);
        }

        /// <summary>
        /// 查询返回数据集DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="srcTable">DataSet中的DataTable名</param>
        /// <returns></returns>
        public DataSet DataAdapter(string sql, string srcTable)
        {
            Service s = new Service();
            return s.DataAdapter(sql,srcTable);
        }

        /// <summary>
        /// 验证设备合法性
        /// </summary>
        /// <param name="deviceID"></param>
        /// <returns></returns>
        public bool VerifyDevice(string deviceID)
        {
            Service s = new Service();
            return s.VerifyDevice(deviceID);
        }
    }
}
