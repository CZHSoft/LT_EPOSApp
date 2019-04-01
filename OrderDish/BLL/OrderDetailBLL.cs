using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace OrderDish.BLL
{
    public class OrderDetailBLL
    {
        private DAL.SqlDAL sqlDal=new OrderDish.DAL.SqlDAL();

        /// <summary>
        /// 获取菜单详细 返回数据集
        /// </summary>
        /// <param name="querySQL"></param>
        /// <param name="srcTable"></param>
        /// <returns></returns>
        //public DataSet GetOrderDetailDataSet(string deviceID)
        //{
        //    string sql = string.Format("select foodname as 菜名,OD_OrderDetail.foodprice as 单价,FoodAmount as 份数,Remark as 口味备注,state as 状态 " +
        //        "from OD_OrderDetail,od_food " +
        //        "where od_food.id=OD_OrderDetail.FoodID and state!='结账' and DeviceID='{0}'", deviceID);

        //    return sqlDal.DataAdapter(sql, "orderdetail");
        //}

        public DataSet GetOrderDetailDataSet(string orderid)
        {
            string sql = string.Format("select foodname as 菜名,OD_OrderDetail.foodprice as 单价,FoodAmount as 份数,Remark as 口味备注,state as 状态 " +
                "from OD_OrderDetail,od_food " +
                "where od_food.id=OD_OrderDetail.FoodID and state!=8 and orderid='{0}'", orderid);

            return sqlDal.DataAdapter(sql, "orderdetail");
        }
    }
}
