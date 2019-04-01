using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace OrderDish.BLL
{
    public class WaiterIDListBLL
    {
        private DAL.SqlDAL sqlDal = new OrderDish.DAL.SqlDAL();

        public DataSet GetWaiterDataSet()
        {
            string sql = "select id from mis_waiter";
            return sqlDal.DataAdapter(sql, "waiter");
        }

    }
}
