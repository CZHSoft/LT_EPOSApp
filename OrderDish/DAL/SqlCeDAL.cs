using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.SqlServerCe;
using System.Windows.Forms;
using System.Data;

namespace OrderDish.DAL
{
    public class SqlCeDAL:IDisposable
    {
        private SqlCeConnection conn;
        private bool _IsDisposed = false;

        /// <summary>
        /// 打开数据库 conn要求未初始化
        /// </summary>
        /// <param name="DbName"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public bool DbOpen(string DataSource)
        {
            try
            {
                conn = new SqlCeConnection(DataSource);
                conn.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool DbOpenS(string dataSource)
        {
            try
            {
                conn = new SqlCeConnection(dataSource);
                conn.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 关闭数据库
        /// </summary>
        /// <param name="conn"></param>
        public void DbClose()
        {
            if (conn != null)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 增删改 单条 带关闭数据库
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public bool ExecuteNonQuery(string sql)
        {
            try
            {

                SqlCeCommand command = new SqlCeCommand(sql, conn);
                if (command.ExecuteNonQuery() == 1)
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
            finally
            {
                DbClose();
            }
        }


        /// <summary>
        /// 创建数据库专用 单条 带关闭数据库
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public bool ExecuteNonQueryWithCreateTable(string sql)
        {
            try
            {

                SqlCeCommand command = new SqlCeCommand(sql, conn);
                command.ExecuteNonQuery();
                return true;
 
            }
            catch
            {
                return false;
            }
            finally
            {
                DbClose();
            }
        }

        /// <summary>
        /// 查询 返回执行结果 成功 第一行第一列 带关闭数据库
        /// </summary>
        /// <returns></returns>
        public bool ExecuteScalar(string sql)
        {
            try
            {
                SqlCeCommand command = new SqlCeCommand(sql, conn);
                command.ExecuteScalar();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                DbClose();
            }
        }

        /// <summary>
        /// 查询返回对象(object) 成功 第一行第一列 带关闭数据库
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public object ExecuteScalarObject(string sql)
        {
            try
            {
                SqlCeCommand command = new SqlCeCommand(sql, conn);
                return command.ExecuteScalar(); ;
            }
            catch
            {
                return null;
            }
            finally
            {
                DbClose();
            }
        }

        /// <summary>
        /// 采集数据集 返回DataSet 带关闭数据库
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="srcTable"></param>
        /// <returns></returns>
        public DataSet DataAdapter(string sql, string srcTable)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCeDataAdapter da = new SqlCeDataAdapter(sql,conn);
                da.Fill(ds, srcTable);
                return ds;
            }
            catch
            {
                return null;
            }
            finally
            {
                DbClose();
            }
        }

          #region Dispose
        ~SqlCeDAL() 
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
                conn = null;
            } 
            _IsDisposed = true;
        }

        #endregion




        #region 旧版

        //////////////////////旧版，暂时不用/////////////////////////
        /// <summary>
        /// 检查数据库 
        /// </summary>
        public static void CheckDB()
        {
            if (!File.Exists(GlobalVariable.LocalPath + "\\AppDB.sdf"))
            {
                string dataSource = string.Format("Data source={0}" ,
                    GlobalVariable.LocalPath + "\\AppDB.sdf");
                SqlCeEngine sqlCeEngine = new SqlCeEngine(dataSource);
                sqlCeEngine.CreateDatabase();
                SqlCeConnection connection = new SqlCeConnection(dataSource);
                connection.Open();
                StringBuilder table1Builder = new StringBuilder();
                table1Builder.Append("Create Table Menu(");
                table1Builder.Append("id int PRIMARY KEY IDENTITY,");
                table1Builder.Append("menuName nvarchar(10) NOT NULL,");
                table1Builder.Append("menuVersion nvarchar(20) NOT NULL,");
                table1Builder.Append("pictureAddress nvarchar(50)");
                table1Builder.Append(")");

                StringBuilder table2Builder = new StringBuilder();
                table2Builder.Append("Create Table Food(");
                table2Builder.Append("id int PRIMARY KEY IDENTITY,");
                table2Builder.Append("foodName nvarchar(20) NOT NULL,");
                table2Builder.Append("foodVersion nvarchar(20) NOT NULL,");
                table2Builder.Append("menuID int NOT NULL,");
                table2Builder.Append("pictureAddress nvarchar(50)");
                table2Builder.Append(")");

                StringBuilder table3Builder = new StringBuilder();
                table3Builder.Append("Create Table Version(");
                table3Builder.Append("id int PRIMARY KEY IDENTITY,");
                table3Builder.Append("menuID int NOT NULL,");
                table3Builder.Append("menuVersion nvarchar(20) NOT NULL");
                table3Builder.Append(")");

                SqlCeCommand command1 = new SqlCeCommand(table1Builder.ToString(),connection);
                command1.ExecuteNonQuery();
                SqlCeCommand command2 = new SqlCeCommand(table2Builder.ToString(), connection);
                command2.ExecuteNonQuery();
                SqlCeCommand command3 = new SqlCeCommand(table3Builder.ToString(), connection);
                command3.ExecuteNonQuery();
                connection.Close();
            }
            
        }
        /// <summary>
        /// 检查表格
        /// </summary>
        public static void CheckTable()
        {
            string dataSource = string.Format("Data source={0}",
                   GlobalVariable.LocalPath + "\\AppDB.sdf");
            SqlCeConnection connection = new SqlCeConnection(dataSource);
            connection.Open();

            try
            {
                string checkTable1SQL = string.Format("Select * from {0}", "Menu");
                SqlCeCommand command1 = new SqlCeCommand(checkTable1SQL, connection);
                command1.ExecuteScalar();
            }
            catch
            {
                StringBuilder table1Builder = new StringBuilder();
                table1Builder.Append("Create Table Menu(");
                table1Builder.Append("id int PRIMARY KEY IDENTITY,");
                table1Builder.Append("menuName nvarchar(10) NOT NULL,");
                table1Builder.Append("pictureAddress nvarchar(50)");
                table1Builder.Append(")");
                SqlCeCommand command2 = new SqlCeCommand(table1Builder.ToString(), connection);
                command2.ExecuteNonQuery();
            }

            try
            {
                string checkTable2SQL = string.Format("Select * from {0}", "Version");
                SqlCeCommand command3 = new SqlCeCommand(checkTable2SQL, connection);
                command3.ExecuteScalar();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                StringBuilder table2Builder = new StringBuilder();
                table2Builder.Append("Create Table Food(");
                table2Builder.Append("id int PRIMARY KEY IDENTITY,");
                table2Builder.Append("foodName nvarchar(20) NOT NULL,");
                table2Builder.Append("menuID int NOT NULL,");
                table2Builder.Append("pictureAddress nvarchar(50)");
                table2Builder.Append(")");
                SqlCeCommand command4 = new SqlCeCommand(table2Builder.ToString(), connection);
                command4.ExecuteNonQuery();
            }

            try
            {
                string checkTable3SQL = string.Format("Select * from {0}", "Food");
                SqlCeCommand command5 = new SqlCeCommand(checkTable3SQL, connection);
                command5.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                StringBuilder table3Builder = new StringBuilder();
                table3Builder.Append("Create Table Version(");
                table3Builder.Append("id int PRIMARY KEY IDENTITY,");
                table3Builder.Append("menuID int NOT NULL");
                table3Builder.Append("menuVersion nvarchar(20) NOT NULL");
                table3Builder.Append(")");
                SqlCeCommand command6 = new SqlCeCommand(table3Builder.ToString(), connection);
                command6.ExecuteNonQuery();
            }

            connection.Close();
        }

        /// <summary>
        /// 检查版本
        /// </summary>
        /// <returns></returns>
        public static bool CheckVersion(string menuID,string menuVersion)
        {
            string dataSource = string.Format("Data source={0}",
                  GlobalVariable.LocalPath + "\\AppDB.sdf");
            SqlCeConnection connection = new SqlCeConnection(dataSource);
            connection.Open();
            try
            {
                string checkVersionSQL = string.Format("Select * from {0} where menuID={1} and menuVersion='{2}'",
                    "Version", menuID, menuVersion);
                SqlCeCommand command = new SqlCeCommand(checkVersionSQL, connection);
                if ((int)command.ExecuteScalar() == 1)
                {
                    return true;
                }
                else
                {
                    command.CommandText = string.Format("DELETE FROM {0} WHERE {1} = {2}",
                        "Version", "menuID", menuID);
                    command.ExecuteNonQuery();
                    command.CommandText = string.Format("INSERT INTO {0} ({1},{2}}) VALUES ({3}, {4})",
                        "Version", "menuid", "menuversion", menuID, menuVersion);
                    command.ExecuteNonQuery();
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// 更新Menu
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="version"></param>
        /// <param name="picAdd"></param>
        /// <param name="entityName"></param>
        public static void UpdateOrInsertMenuTable(string tableName,string version,string picAdd,string entityName)
        {
            string dataSource = string.Format("Data source={0}",
                  GlobalVariable.LocalPath + "\\AppDB.sdf");
            SqlCeConnection connection = new SqlCeConnection(dataSource);
            connection.Open();
            try
            {
                string updateSQL = string.Format("UPDATE {0} SET menuVersion = '{1}' and pictureAddress='{2}' WHERE menuName = '{3}'",
                    tableName, version, picAdd,entityName);
                SqlCeCommand command = new SqlCeCommand(updateSQL, connection);
                if (command.ExecuteNonQuery() == 1)
                {
                    return;
                }
                else
                {
                    command.CommandText = string.Format("INSERT INTO Menu (menuName,menuVersion,pictureAddress) VALUES ({0}, '{1}','{2}')",
                        entityName,version,picAdd);
                    command.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="version"></param>
        /// <param name="picAdd"></param>
        /// <param name="entityName"></param>
        public static void UpdateOrInsertFoodTable(string tableName, string version, string picAdd, string entityName,string entityID)
        {
            string dataSource = string.Format("Data source={0}",
                  GlobalVariable.LocalPath + "\\AppDB.sdf");
            SqlCeConnection connection = new SqlCeConnection(dataSource);
            connection.Open();
            try
            {
                string updateSQL = string.Format("UPDATE {0} SET foodVersion = '{1}' and pictureAddress='{2}' WHERE foodName = '{3}'",
                    tableName, version, picAdd, entityName);
                SqlCeCommand command = new SqlCeCommand(updateSQL, connection);
                if (command.ExecuteNonQuery() == 1)
                {
                    return;
                }
                else
                {
                    command.CommandText = string.Format("INSERT INTO Food (foodName,foodVersion,pictureAddress,menuID) VALUES ({0}, '{1}','{2}')",
                        entityName, version, picAdd,entityID);
                    command.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }
        #endregion
    }
}
