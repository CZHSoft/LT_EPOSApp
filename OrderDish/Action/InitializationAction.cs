using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OrderDish.BLL;
using OrderDish.Common;
using System.Data;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Text.RegularExpressions;

/********************************************************************/
//20120608
//各初始化的过程走查完毕
//更改网络检查功能
//新增设备合法性检查
/********************************************************************/

namespace OrderDish.Action
{
    public class InitializationAction
    {
        private BLL.InitializationBLL init = new InitializationBLL();

        /// <summary>
        /// 本地数据源检查
        /// </summary>
        /// <returns></returns>
        public bool DoDbInitialization()
        {
            //***************************************//
            //本地数据源检查
            //1.检查文件
            //2.检查表
            //***************************************//
            try
            {
                //检查数据源文件
                if (!init.CheckDbExist())
                {
                    try
                    {
                        Directory.Delete(GlobalVariable.LocalPath + "\\picture", true);
                    }
                    catch
                    {

                    }
                    if (!init.CreateDb())
                    {
                        return false;
                    }
                }

                //检查
                if (!init.CheckTableExist("Select count(*) from BaseInfo"))
                {
                    StringBuilder tableBuilder = new StringBuilder();
                    tableBuilder.Append("Create Table BaseInfo(");
                    tableBuilder.Append("id int NOT NULL UNIQUE,");
                    tableBuilder.Append("operatingStatus int NOT NULL,");
                    tableBuilder.Append("companyName nvarchar(50) NOT NULL,");
                    tableBuilder.Append("operatingTime datetime NOT NULL,");
                    tableBuilder.Append("odVersion nvarchar(20) NOT NULL");
                    tableBuilder.Append(")");
                    if (!init.CreateTable(tableBuilder.ToString()))
                    {
                        return false;
                    }
                }

                if (!init.CheckTableExist("Select count(*) from menu"))
                {
                    StringBuilder tableBuilder = new StringBuilder();
                    tableBuilder.Append("Create Table Menu(");
                    tableBuilder.Append("id int NOT NULL UNIQUE,");
                    tableBuilder.Append("menuName nvarchar(10) NOT NULL,");
                    tableBuilder.Append("menuVersion nvarchar(20),");
                    tableBuilder.Append("pictureAddress nvarchar(50),");
                    tableBuilder.Append("pictureByte int");
                    tableBuilder.Append(")");
                    if (!init.CreateTable(tableBuilder.ToString()))
                    {
                        return false;
                    }
                }

                if (!init.CheckTableExist("Select * from food"))
                {
                    StringBuilder tableBuilder = new StringBuilder();
                    tableBuilder.Append("Create Table Food(");
                    tableBuilder.Append("id int NOT NULL UNIQUE,");
                    tableBuilder.Append("foodName nvarchar(20) NOT NULL,");
                    tableBuilder.Append("foodPrice money NOT NULL,");
                    tableBuilder.Append("foodVersion nvarchar(20) NOT NULL,");
                    tableBuilder.Append("menuID int NOT NULL,");
                    tableBuilder.Append("pictureAddress nvarchar(50) NOT NULL,");
                    tableBuilder.Append("pictureByte int");
                    tableBuilder.Append(")");
                    if (!init.CreateTable(tableBuilder.ToString()))
                    {
                        return false;
                    }
                }

                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool DoCheckEnvironment()
        {
            if (!File.Exists(GlobalVariable.LocalPath + "//setting.txt"))
            {
                return false;
            }
            
            using (StreamReader sr = new StreamReader(GlobalVariable.LocalPath + "//setting.txt", Encoding.Default))
            {
                string readLineStr = "";
                while ((readLineStr = sr.ReadLine()) != null)
                {
                    string[] strArray = readLineStr.Split(':');
                    if (strArray[0] == "webservice")
                    {
                        if (Regex.IsMatch(strArray[1] + ":" + strArray[2], @"^http://([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?$"))
                        {

                            GlobalVariable.WebServicePath = strArray[1] +":" +strArray[2];
                        }
                        else
                        {
                            return false;
                        }
                    }
                    if (strArray[0] == "http")
                        
                    {
                        if (Regex.IsMatch(readLineStr, @"^http://([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?$"))
                        {
                            GlobalVariable.HttpPath = readLineStr;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }

        public bool DoCheckNetWork()
        {
            //try
            //{
            //    Common.Ping ping = new Ping();

            //    IPAddress address = IPAddress.Parse("198.168.0.253");

            //    for (int i = 0; i < 4; i++)
            //    {
            //        Common.IcmpEchoReply reply = ping.Send(address);
            //        if (reply.status == (uint)IPStatus.Success)
            //        {
            //            IPAddress addr = new IPAddress(reply.address);
            //            string mess = String.Format("Reply from {0}: Echo size={1} time<{2}ms TTL={3}", addr, reply.dataSize, reply.roundTripTime, reply.ttl);
            //            Console.WriteLine(mess);
            //        }
            //        else
            //        {
            //            return false;
            //        }
            //    }
            //    return true;
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    return false;
            //}
            try
            {
                WIFIConnectCheck wifiConnectCheck = new WIFIConnectCheck();
                wifiConnectCheck.Url = GlobalVariable.HttpPath;
                if (wifiConnectCheck.ConnectTest())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool DoVerifyDevice()
        {
            DeviceUniqueID devid = new DeviceUniqueID();
            GlobalVariable.DeviceID = devid.GetDeviceID();
            if (init.VerifyDevice(GlobalVariable.DeviceID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DoCheckUpdate()
        {
            try
            {
                DataSet dsOS = new DataSet();

                dsOS = init.GetServerDataSet("select * from DUTY_OperatingStatus", "os");

                if (dsOS == null)
                {
                    return false;
                }

                string strID = dsOS.Tables[0].Rows[0][0].ToString();
                string strOS = dsOS.Tables[0].Rows[0][1].ToString();
                string strCN = dsOS.Tables[0].Rows[0][2].ToString();
                string strOT = dsOS.Tables[0].Rows[0][3].ToString();
                string strVer = dsOS.Tables[0].Rows[0][4].ToString();

                //服务器端DUTY_OperatingStatus的状态 正常ID为1
                string checkBaseInfoSQL = string.Format("Select * from baseinfo where id={0}", strID);

                if (init.CheckLocalOS(checkBaseInfoSQL))
                {
                    string checkVersionSQL = string.Format("Select count(*) from baseinfo where id={0} and odVersion='{1}'",
                        strID, strVer);
                    //匹配服务器端的版本号
                    if (!init.CheckLocalOS(checkVersionSQL))
                    {
                        //更新菜单和菜谱
                        if (!UpdateMenuAndFood())
                        {
                            return false;
                        }

                        string updateBaseInfoSQL = string.Format("update baseinfo set operatingStatus={0},companyName='{1}',operatingTime='{2}',odVersion='{3}' where id={4}",
                            strOS, strCN, strOT, strVer, strID);

                        //更新版本信息
                        if (init.LocalExecuteNonQuery(updateBaseInfoSQL))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }

                }
                else
                {
                    if (!UpdateMenuAndFood())
                    {
                        return false;
                    }

                    string insertBaseInfoSQL = string.Format("insert into baseinfo(id,operatingStatus,companyName,operatingTime,odVersion) values({0},'{1}','{2}','{3}','{4}') ",
                        strID, strOS, strCN, strOT, strVer);

                    if (!init.LocalExecuteNonQuery(insertBaseInfoSQL))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool UpdateMenuAndFood()
        {
            bool ErrorFlag = false;

            List<string> insertList = new List<string>();
            List<string> updateList = new List<string>();

            DataSet dsServerMenu = init.GetServerDataSet("select * from OD_Menu", "Smenu");

            if (dsServerMenu == null)
            {
                return false;
            }

            DataSet dsLocalMenu = init.GetLocalDataSet("select * from Menu", "Lmenu");

            if (dsLocalMenu == null)
            {
                return false;
            }

            if (!Directory.Exists(GlobalVariable.LocalPath + "\\picture"))
            {
                Directory.CreateDirectory(GlobalVariable.LocalPath + "\\picture");
            }

            if (!Directory.Exists(GlobalVariable.LocalPath + "\\picture\\menu"))
            {
                Directory.CreateDirectory(GlobalVariable.LocalPath + "\\picture\\menu");
            }


            insertList.Clear();
            updateList.Clear();

            //一次遍历menu:匹配更改
            foreach (DataRow rowS in dsServerMenu.Tables["Smenu"].Rows)
            {
                bool FindFlag = false;

                foreach (DataRow rowL in dsLocalMenu.Tables["Lmenu"].Rows)
                {
                    //ID
                    if (rowS[0].ToString() == rowL[0].ToString())
                    {
                        //VERSION
                        if (rowS[2].ToString() != rowL[2].ToString())
                        {
                            //delete local file
                            File.Delete(GlobalVariable.LocalPath + rowL[3].ToString());

                            //DownLoadFile
                            if (Common.HttpFileControl.DownLoadFile(GlobalVariable.HttpPath + rowS[3].ToString()
                                , GlobalVariable.LocalPath + rowS[3].ToString()))
                            {

                                string updateMenuSQL = string.Format("UPDATE menu SET menuName='{0}',menuVersion = '{1}',pictureAddress='{2}',pictureByte={3}  WHERE id = {1}",
                                    rowS[1].ToString(), rowS[2].ToString(), rowS[3].ToString(), rowS[4].ToString(), rowS[0].ToString());

                                if (!init.LocalExecuteNonQuery(updateMenuSQL))
                                {
                                    //replace
                                    ErrorFlag = true;
                                    updateList.Add(rowS[0].ToString());
                                    FindFlag = true;
                                    break;
                                }
                                else
                                {
                                    FindFlag = true;
                                    break;
                                }
                            }
                            else
                            {
                                //replace
                                ErrorFlag = true;
                                updateList.Add(rowS[0].ToString());
                                FindFlag = true;
                                break;
                            }

                        }
                        else
                        {
                            FindFlag = true;
                            break;
                        }
                    }
                }
                //didnot find
                if (!FindFlag)
                {
                    //insert
                    if (Common.HttpFileControl.DownLoadFile(GlobalVariable.HttpPath + rowS[3].ToString()
                                , GlobalVariable.LocalPath + rowS[3].ToString()))
                    {
                        string insertMenuSQL = string.Format("INSERT INTO Menu(id,menuName,menuVersion,pictureAddress,pictureByte) VALUES({0},'{1}','{2}','{3}',{4})",
                            rowS[0].ToString(), rowS[1].ToString(), rowS[2].ToString(), rowS[3].ToString(), rowS[4].ToString());

                        if (!init.LocalExecuteNonQuery(insertMenuSQL))
                        {
                            //replace
                            ErrorFlag = true;
                            insertList.Add(rowS["MenuName"].ToString());
                        }
                    }
                    else
                    {
                        //replace
                        ErrorFlag = true;
                        insertList.Add(rowS["MenuName"].ToString());
                    }
                }
            }

            //二次操作menu:补错
            if (ErrorFlag)
            {
                //insert
                if (insertList.Count > 0)
                {
                    for (int insertCount = 0; insertCount < insertList.Count; insertCount++)
                    {
                        bool FindFlag = false;

                        foreach (DataRow rowS in dsServerMenu.Tables["Smenu"].Rows)
                        {
                            if (rowS[0].ToString() == insertList[insertCount])
                            {
                                if (Common.HttpFileControl.DownLoadFile(GlobalVariable.HttpPath + rowS[3].ToString()
                                    , GlobalVariable.LocalPath + rowS[3].ToString()))
                                {
                                    string insertMenuSQL = string.Format("INSERT INTO Menu(id,menuName,menuVersion,pictureAddress,pictureByte) VALUES({0},'{1}','{2}','{3}','{4}')",
                            rowS[0].ToString(), rowS[1].ToString(), rowS[2].ToString(), rowS[3].ToString(), rowS[4].ToString());

                                    if (!init.LocalExecuteNonQuery(insertMenuSQL))
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        FindFlag = true;
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }

                        if (!FindFlag)
                        {
                            return false;
                        }

                    }
                }
                //update
                if (updateList.Count > 0)
                {
                    for (int updateCount = 0; updateCount < updateList.Count; updateCount++)
                    {
                        bool FindFlag = false;

                        foreach (DataRow rowS in dsServerMenu.Tables["Smenu"].Rows)
                        {
                            if (rowS[0].ToString() == updateList[updateCount])
                            {
                                if (Common.HttpFileControl.DownLoadFile(GlobalVariable.HttpPath + rowS[3].ToString()
                                    , GlobalVariable.LocalPath + rowS[3].ToString()))
                                {
                                    string updateMenuSQL = string.Format("UPDATE menu SET menuName='{0}',menuVersion = '{1}',pictureAddress='{2}',pictureByte={3}  WHERE id = {1}",
                                        rowS[1].ToString(), rowS[2].ToString(), rowS[3].ToString(), rowS[4].ToString(), rowS[0].ToString());

                                    if (!init.LocalExecuteNonQuery(updateMenuSQL))
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        FindFlag = true;
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }

                        if (!FindFlag)
                        {
                            return false;
                        }

                    }
                }

            }
            //三次删除
            dsLocalMenu = init.GetLocalDataSet("select * from Menu", "Lmenu");

            foreach (DataRow rowL in dsLocalMenu.Tables[0].Rows)
            {
                bool FindFlag = false;

                foreach (DataRow rowS in dsServerMenu.Tables[0].Rows)
                {
                    if (rowL[0].ToString() == rowS[0].ToString())
                    {
                        FindFlag = true;
                        break;
                    }
                }

                if (!FindFlag)
                {
                    if (init.LocalExecuteNonQuery(string.Format("delete from Menu where id={0}", rowL[0].ToString())))
                    {
                        File.Delete(GlobalVariable.LocalPath + rowL[3].ToString());
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            ErrorFlag = false;
            insertList.Clear();
            updateList.Clear();

            //更新Menu
            dsLocalMenu = init.GetLocalDataSet("select * from Menu", "Lmenu");

            //************************FOOD*********************************//
            
            DataSet dsServerFood =init.GetServerDataSet("select * from OD_Food", "Sfood");

            if (dsServerFood == null)
            {
                return false;
            }

            DataSet dsLocalFood = init.GetLocalDataSet("select * from Food", "Lfood");

            if (dsLocalFood == null)
            {
                return false;
            }

            if (!Directory.Exists(GlobalVariable.LocalPath + "\\picture\\food"))
            {
                Directory.CreateDirectory(GlobalVariable.LocalPath + "\\picture\\food");
            }

            //一次遍历food
            foreach (DataRow rowS in dsServerFood.Tables["Sfood"].Rows)
            {
                bool FindFlag = false;

                string MenuName = "";

                //get MenuName
                foreach (DataRow rowLMenu in dsLocalMenu.Tables[0].Rows)
                {
                    if (rowS[4].ToString() == rowLMenu[0].ToString())
                    {
                        MenuName = rowLMenu[1].ToString();
                        break;
                    }
                }

                //judge Directory
                if (!string.IsNullOrEmpty(MenuName))
                {
                    if (!Directory.Exists(GlobalVariable.LocalPath + "\\picture\\food\\" + MenuName))
                    {
                        Directory.CreateDirectory(GlobalVariable.LocalPath + "\\picture\\food\\" + MenuName);
                    }
                }
                else
                {
                    return false;
                }

                foreach (DataRow rowL in dsLocalFood.Tables["Lfood"].Rows)
                {
                    //ID
                    if (rowS[0].ToString() == rowL[0].ToString())
                    {
                        //VERSION
                        if (rowS[3].ToString() != rowL[3].ToString())
                        {
                            //delete old file 
                            File.Delete(GlobalVariable.LocalPath + rowL[5].ToString());

                            //DownLoadFile
                            if (Common.HttpFileControl.DownLoadFile(GlobalVariable.HttpPath + rowS[5].ToString()
                                , GlobalVariable.LocalPath + rowS[5].ToString()))
                            {
                                string updateFoodSQL = string.Format("UPDATE food SET foodName='{0}',foodPrice={1},foodVersion = '{2}',menuID={3},pictureAddress='{4}',pictureByte={5}  WHERE id = {6}",
                                    rowS[1].ToString(), rowS[2].ToString(), rowS[3].ToString(), rowS[4].ToString(), rowS[5].ToString(), rowS[6].ToString(), rowS[0].ToString());

                                if (!init.LocalExecuteNonQuery(updateFoodSQL))
                                {
                                    //replace
                                    ErrorFlag = true;
                                    updateList.Add(rowS[0].ToString());
                                    FindFlag = true;
                                    break;
                                }
                                else
                                {
                                    FindFlag = true;
                                    break;
                                }
                            }
                            else
                            {
                                //replace
                                ErrorFlag = true;
                                updateList.Add(rowS[0].ToString());
                            }
                        }
                        else
                        {
                            FindFlag = true;
                            break;
                        }
                    }
                }

                //didnot find
                if (!FindFlag)
                {
                    //insert
                    if (Common.HttpFileControl.DownLoadFile(GlobalVariable.HttpPath + rowS[5].ToString()
                                , GlobalVariable.LocalPath + rowS[5].ToString()))
                    {
                        string insertFoodSQL = string.Format("INSERT INTO food(id,foodName,foodPrice,foodVersion,menuID,pictureAddress,pictureByte) VALUES({0},'{1}',{2},'{3}',{4},'{5}','{6}')",
                            rowS[0].ToString(), rowS[1].ToString(), rowS[2].ToString(),
                            rowS[3].ToString(), rowS[4].ToString(), rowS[5].ToString(), rowS[6].ToString());

                        if (!init.LocalExecuteNonQuery(insertFoodSQL))
                        {
                            //replace
                            ErrorFlag = true;
                            insertList.Add(rowS[0].ToString());
                        }
                    }
                    else
                    {
                        ErrorFlag = true;
                        insertList.Add(rowS[0].ToString());
                    }

                }

            }

            //二次操作
            if (ErrorFlag)
            {
                //insert
                if (insertList.Count > 0)
                {
                    for (int insertCount = 0; insertCount < insertList.Count; insertCount++)
                    {
                        bool FindFlag = false;

                        foreach (DataRow rowS in dsServerFood.Tables["Smenu"].Rows)
                        {
                            if (rowS[0].ToString() == insertList[insertCount])
                            {
                                if (Common.HttpFileControl.DownLoadFile(GlobalVariable.HttpPath + rowS[5].ToString()
                                    , GlobalVariable.LocalPath + rowS[5].ToString()))
                                {

                                    string insertFoodSQL = string.Format("INSERT INTO food(id,foodName,foodPrice,foodVersion,menuID,pictureAddress,pictureByte) VALUES({0},'{1}',{2},'{3}',{4},'{5}','{6}')",
                             rowS[0].ToString(), rowS[1].ToString(), rowS[2].ToString(),
                             rowS[3].ToString(), rowS[4].ToString(), rowS[5].ToString(), rowS[6].ToString());

                                    if (!init.LocalExecuteNonQuery(insertFoodSQL))
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        FindFlag=true;
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }

                        if (!FindFlag)
                        {
                            return false;
                        }

                    }
                }
                //update
                if (updateList.Count > 0)
                {
                    for (int updateCount = 0; updateCount < updateList.Count; updateCount++)
                    {
                        bool FindFlag = false;

                        foreach (DataRow rowS in dsServerFood.Tables["Smenu"].Rows)
                        {
                            if (rowS[0].ToString() == updateList[updateCount])
                            {
                                if (Common.HttpFileControl.DownLoadFile(GlobalVariable.HttpPath + rowS["PictureAddress"].ToString()
                                    , GlobalVariable.LocalPath + rowS["PictureAddress"].ToString()))
                                {
                                    string updateFoodSQL = string.Format("UPDATE food SET foodName='{0}',foodPrice={1},foodVersion = '{2}',menuID={3},pictureAddress='{4}',pictureByte={5}  WHERE id = {6}",
                                    rowS[1].ToString(), rowS[2].ToString(), rowS[3].ToString(), rowS[4].ToString(), rowS[5].ToString(), rowS[6].ToString(), rowS[0].ToString());

                                    if (!init.LocalExecuteNonQuery(updateFoodSQL))
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        FindFlag = true;
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }

                        if (!FindFlag)
                        {
                            return false;
                        }

                    }
                }

            }

            //三次删除
            dsLocalFood = init.GetLocalDataSet("select * from Food", "Lfood");

            foreach (DataRow rowL in dsLocalFood.Tables[0].Rows)
            {
                bool FindFlag = false;

                foreach (DataRow rowS in dsServerFood.Tables[0].Rows)
                {
                    if (rowL[0].ToString() == rowS[0].ToString())
                    {
                        FindFlag = true;
                        break;
                    }
                }

                if (!FindFlag)
                {
                    if (init.LocalExecuteNonQuery(string.Format("delete from Food where id={0}", rowL[0].ToString())))
                    {
                        File.Delete(GlobalVariable.LocalPath + rowL[5].ToString());
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            insertList.Clear();
            updateList.Clear();
            dsLocalFood.Dispose();
            dsLocalMenu.Dispose();
            dsServerFood.Dispose();
            dsServerMenu.Dispose();
            insertList = null;
            updateList = null;
            return true;
        }

        //private void ListViewAction(object sender, EventArgs e,string name,string status)
        //{
        //    System.Windows.Forms.ListViewItem lvi = new System.Windows.Forms.ListViewItem(name);
        //    lvi.SubItems.Add(status);
        //    sender = lvi;
        //    AlphaControls.FormManageEvent.DoOnF2FTransmit(sender);
        //}
    }
}
