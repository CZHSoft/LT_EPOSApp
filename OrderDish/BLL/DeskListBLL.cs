using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace OrderDish.BLL
{
    public class DeskListBLL
    {
        private DAL.SqlDAL sqlDal = new OrderDish.DAL.SqlDAL();

        public DataSet GetDeskDataSet()
        {
            string sql = "select deskname from mis_desk";
            return sqlDal.DataAdapter(sql, "desk");
        }

    }
}
