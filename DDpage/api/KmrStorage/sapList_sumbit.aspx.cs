using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.KmrStorage
{
    public partial class sapList_sumbit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string strCompany = Request.Form["PCompany"].ToString();
            string strDept = Request.Form["PDept"].ToString();
            string strVen = Request.Form["PVenture"].ToString();
            string strSapVenCode = Request.Form["sapVentureCode"].ToString();
            string strName = Request.Form["PName"].ToString();
            string strPersonCode = Request.Form["PCode"].ToString();
            string strTime = Request.Form["PTime"].ToString();
            string strOrder = Request.Form["OddNo"].ToString();
            string strProOrder = Request.Form["ProNo"].ToString();
            string strStNo = Request.Form["PStNo"].ToString();
            string strStat = Request.Form["PickStat"].ToString();
            string dataStringOne = Request.Form["dataListOne"].ToString();
            string dataStringTwo = Request.Form["dataListTwo"].ToString();
            string dataStringTh = Request.Form["dataListTh"].ToString();
            string strGID = System.Guid.NewGuid().ToString();

            Random r = new Random();
            int i = r.Next(1000, 9999);
            Boolean stat = false;
            int t1 = 0;
            string strPID = System.DateTime.Now.ToFileTime().ToString() + i.ToString();


            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();

            SqlConnection conNew = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            conNew.Open();


            string strSql = "insert into [DDdatabase].[dbo].[Tab_kocelApp_Kmr_PickMaterial] VALUES('" + strPID + "','" + strCompany + "','" + strDept + "','" + strVen + "','" + strSapVenCode + "','" + strName + "','" + strPersonCode + "','" + strTime + "','" + strOrder + "','','"+ strStNo + "','" + strProOrder + "','APP','" + strStat + "','" + strGID + "')";
            SqlCommand cmdhead = new SqlCommand(strSql, con);
            t1 = cmdhead.ExecuteNonQuery();
            if (t1 != 0)
            {
                stat = true;
            }
            //计划内领料处理模块

            if (dataStringOne != "" && dataStringOne.Length > 0)
            {
                JArray jsonObjOne = JArray.Parse(dataStringOne);
                foreach (JObject jObject in jsonObjOne)
                {
                    string strPdid = System.Guid.NewGuid().ToString();
                    //计划内领料数据补全
                    string MFactoryOne = "";
                    string strSqlOne1 = "SELECT [NAME1] FROM [DataFromSap].[dbo].[T_SAP_DM_Factory] where WERKS='" + jObject["MFactoryCode"] + "'";

                    SqlCommand cmdone1 = new SqlCommand(strSqlOne1, con);
                    SqlDataReader sdrOne1 = cmdone1.ExecuteReader();
                    while (sdrOne1.Read())
                    {
                        MFactoryOne = sdrOne1[0].ToString();
                    }
                    sdrOne1.Close();


                    string MStockOne = "";
                    string strSqlOne2 = "SELECT [LGOBE] FROM [DataFromSap].[dbo].[T_SAP_DM_Stock] where WERKS='" + jObject["MFactoryCode"] + "' and LGORT='" + jObject["MStockCode"] + "'";

                    SqlCommand cmdone2 = new SqlCommand(strSqlOne2, con);
                    SqlDataReader sdrOne2 = cmdone2.ExecuteReader();
                    while (sdrOne2.Read())
                    {
                        MStockOne = sdrOne2[0].ToString();
                    }
                    sdrOne2.Close();

                    string MGroupOne = "";
                    string strSqlOne3 = "SELECT [WGBEZ] FROM [DataFromSap].[dbo].[T_SAP_DM_MeasureGroup] where [MATKL]='" + jObject["MGroupCode"] + "' ";

                    SqlCommand cmdone3 = new SqlCommand(strSqlOne3, con);
                    SqlDataReader sdrOne3 = cmdone3.ExecuteReader();
                    while (sdrOne3.Read())
                    {
                        MGroupOne = sdrOne3[0].ToString();
                    }
                    sdrOne3.Close();

                    //计划内领料数据补全

                    //计划内领料数据写入
                    strSql = "insert into [DDdatabase].[dbo].[Tab_kocelApp_Kmr_PickDetail] VALUES('" + strPdid + "','" + strGID + "','" + MFactoryOne + "','" + jObject["MFactoryCode"] + "','" + MStockOne + "','" + jObject["MStockCode"] + "','" + MGroupOne + "','" + jObject["MGroupCode"] + "','" + jObject["MatName"] + "','" + jObject["MCode"] + "','','" + jObject["MUnit"] + "','" + jObject["MInventory"] + "','" + jObject["PickInventory"] + "','" + jObject["PType"] + "','1')";

                    SqlCommand cmd = new SqlCommand(strSql, con);
                    t1 = cmd.ExecuteNonQuery();
                    if (t1 != 0)
                    {
                        stat = true;
                    }

                    //计划内领料数据写入

                    //后台依据库房，物料组从基础数据中获取备料人员信息，并下发备料计划

                    string isWeight = "";
                    if (jObject["MUnit"].ToString() == "KG")
                    {
                        isWeight = "1";
                    }
                    else
                    {
                        isWeight = "0";
                    }

                    strSql = "select [FKeeper],[FKeeperCode],[FKeeperPhone] from [DDdatabase].[dbo].[View_KocelApp_Kmr_StockListBaseData] where pdid='" + strPdid + "'";

                    SqlCommand cmd1 = new SqlCommand(strSql, con);
                    SqlDataReader sdr = cmd1.ExecuteReader();
                    while (sdr.Read())
                    {
                        var strKeeper = sdr[0].ToString();
                        var strKeeperCode = sdr[1].ToString();
                        var strKeeperPhone = sdr[2].ToString();
                        strSql = "insert into [DDdatabase].[dbo].[Tab_KocelApp_Kmr_StockDetail] values('" + System.Guid.NewGuid().ToString() + "','" + strPdid + "','" + isWeight + "','" + jObject["MInventory"] + "','" + strKeeper + "','" + strKeeperCode + "','" + DateTime.Now.ToString() + "',NULL,'0')";
                        SqlCommand cmd2 = new SqlCommand(strSql, conNew);
                        t1 = cmd2.ExecuteNonQuery();
                        if (t1 != 0)
                        {
                            stat = true;
                        }

                        //发送钉消息通知
                        string strDPhone = strKeeperPhone;
                        //string strDPhone = "15109509909";
                        string strDName = "掌上仓储";
                        string strDSubject = "领料备餐通知";
                        string strDContent = "您有新的领料备餐信息需要确认!";
                        string strDUrl = "http://122.112.213.22/KmrStorage/KmrStorage/stock_tablist.aspx?phone=" + strDPhone + "";
                        string strReturn = MessgeToDing_Statistics(strDPhone, strDName, strDSubject, strDContent, strDUrl);

                    }
                    sdr.Close();



                    //后台依据库房，物料组从基础数据中获取备料人员信息，并下发备料计划

                }
            }
                //计划内领料处理模块

            

            //计划外领料处理模块
            if (dataStringTwo != "" && dataStringTwo.Length > 0)
            {
                JArray jsonObjTwo = JArray.Parse(dataStringTwo);
                foreach (JObject jObject in jsonObjTwo)
                {
                    string strPdid = System.Guid.NewGuid().ToString();
                    if (jObject["PType"].ToString() == "计划外领料")
                    {
                        strSql = "insert into [DDdatabase].[dbo].[Tab_kocelApp_Kmr_PickDetail] VALUES('" + strPdid + "','" + strGID + "','" + jObject["MFactory"] + "','" + jObject["MFactoryCode"] + "','" + jObject["MStock"] + "','" + jObject["MStockCode"] + "','" + jObject["MGroup"] + "','" + jObject["MGroupCode"] + "','" + jObject["Material"] + "','" + jObject["MCode"] + "','" + jObject["MBatch"] + "','" + jObject["MUnit"] + "','" + jObject["MInventory"] + "','" + jObject["PickInventory"] + "','" + jObject["PType"] + "','1')";
                    }


                    SqlCommand cmd = new SqlCommand(strSql, con);
                    t1 = cmd.ExecuteNonQuery();
                    if (t1 != 0)
                    {
                        stat = true;
                    }

                    //后台依据库房，物料组从基础数据中获取备料人员信息，并下发备料计划

                    string isWeight = "";
                    if (jObject["MUnit"].ToString() == "KG")
                    {
                        isWeight = "1";
                    }
                    else
                    {
                        isWeight = "0";
                    }

                    strSql = "select [FKeeper],[FKeeperCode],[FKeeperPhone] from [DDdatabase].[dbo].[View_KocelApp_Kmr_StockListBaseData] where pdid='" + strPdid + "'";

                    SqlCommand cmd1 = new SqlCommand(strSql, con);
                    SqlDataReader sdr = cmd1.ExecuteReader();
                    while (sdr.Read())
                    {
                        var strKeeper = sdr[0].ToString();
                        var strKeeperCode = sdr[1].ToString();
                        var strKeeperPhone = sdr[2].ToString();
                        strSql = "insert into [DDdatabase].[dbo].[Tab_KocelApp_Kmr_StockDetail] values('" + System.Guid.NewGuid().ToString() + "','" + strPdid + "','" + isWeight + "','" + jObject["PickInventory"] + "','" + strKeeper + "','" + strKeeperCode + "','" + DateTime.Now.ToString() + "',NULL,'0')";
                        SqlCommand cmd2 = new SqlCommand(strSql, conNew);
                        t1 = cmd2.ExecuteNonQuery();
                        if (t1 != 0)
                        {
                            stat = true;
                        }

                        //发送钉消息通知
                        string strDPhone = strKeeperPhone;
                        //string strDPhone = "15109509909";
                        string strDName = "掌上仓储";
                        string strDSubject = "领料备餐通知";
                        string strDContent = "您有新的领料备餐信息需要确认!";
                        string strDUrl = "http://122.112.213.22/KmrStorage/KmrStorage/stock_tablist.aspx?phone=" + strDPhone + "";
                        string strReturn = MessgeToDing_Statistics(strDPhone, strDName, strDSubject, strDContent, strDUrl);

                    }
                    sdr.Close();



                    //后台依据库房，物料组从基础数据中获取备料人员信息，并下发备料计划
                }
               
            }

            //计划外领料处理模块


            //成本中心领料处理模块
            if (dataStringTh != "" && dataStringTh.Length > 0)
            {
                JArray jsonObjTh = JArray.Parse(dataStringTh);
                foreach (JObject jObject in jsonObjTh)
                {
                    string strPdid = System.Guid.NewGuid().ToString();
                    if (jObject["PType"].ToString() == "成本中心领料")
                    {
                        strSql = "insert into [DDdatabase].[dbo].[Tab_kocelApp_Kmr_PickDetail] VALUES('" + strPdid + "','" + strGID + "','" + jObject["MFactory"] + "','" + jObject["MFactoryCode"] + "','" + jObject["MStock"] + "','" + jObject["MStockCode"] + "','" + jObject["MGroup"] + "','" + jObject["MGroupCode"] + "','" + jObject["Material"] + "','" + jObject["MCode"] + "','" + jObject["MBatch"] + "','" + jObject["MUnit"] + "','" + jObject["MInventory"] + "','" + jObject["PickInventory"] + "','" + jObject["PType"] + "','1')";
                    }


                    SqlCommand cmd = new SqlCommand(strSql, con);
                    t1 = cmd.ExecuteNonQuery();
                    if (t1 != 0)
                    {
                        stat = true;
                    }

                    //后台依据库房，物料组从基础数据中获取备料人员信息，并下发备料计划

                    string isWeight = "";
                    if (jObject["MUnit"].ToString() == "KG")
                    {
                        isWeight = "1";
                    }
                    else
                    {
                        isWeight = "0";
                    }

                    strSql = "select [FKeeper],[FKeeperCode],[FKeeperPhone] from [DDdatabase].[dbo].[View_KocelApp_Kmr_StockListBaseData] where pdid='" + strPdid + "'";

                    SqlCommand cmd1 = new SqlCommand(strSql, con);
                    SqlDataReader sdr = cmd1.ExecuteReader();
                    while (sdr.Read())
                    {
                        var strKeeper = sdr[0].ToString();
                        var strKeeperCode = sdr[1].ToString();
                        var strKeeperPhone = sdr[2].ToString();
                        strSql = "insert into [DDdatabase].[dbo].[Tab_KocelApp_Kmr_StockDetail] values('" + System.Guid.NewGuid().ToString() + "','" + strPdid + "','" + isWeight + "','" + jObject["PickInventory"] + "','" + strKeeper + "','" + strKeeperCode + "','" + DateTime.Now.ToString() + "',NULL,'0')";
                        SqlCommand cmd2 = new SqlCommand(strSql, conNew);
                        t1 = cmd2.ExecuteNonQuery();
                        if (t1 != 0)
                        {
                            stat = true;
                        }

                        //发送钉消息通知
                        string strDPhone = strKeeperPhone;
                        //string strDPhone = "15109509909";
                        string strDName = "掌上仓储";
                        string strDSubject = "领料备餐通知";
                        string strDContent = "您有新的领料备餐信息需要确认!";
                        string strDUrl = "http://122.112.213.22/KmrStorage/KmrStorage/stock_tablist.aspx?phone=" + strDPhone + "";
                        string strReturn = MessgeToDing_Statistics(strDPhone, strDName, strDSubject, strDContent, strDUrl);


                    }
                    sdr.Close();



                    //后台依据库房，物料组从基础数据中获取备料人员信息，并下发备料计划
                }

            }

            //成本中心领料处理模块



            //异常处理，及时库存中有，基础数据表中没有，导致没有下达备料计划
            strSql = "update [DDdatabase].[dbo].[Tab_kocelApp_Kmr_PickDetail] set PDStat=9 where PLinkID='" + strGID + "' and PType in ('计划内领料','计划外领料','成本中心领料') and PDID not in (select distinct PDID from [DDdatabase].[dbo].[View_KocelApp_Kmr_StockListBaseData] where PLinkID='" + strGID + "') ";
            SqlCommand cmderr = new SqlCommand(strSql, conNew);
            cmderr.ExecuteNonQuery();

            //异常处理，及时库存中有，基础数据表中没有，导致没有下达备料计划

            con.Close();
            conNew.Close();


            if (stat)
            {
                Response.Write("数据已保存！");
            }
            else
            {
                Response.Write("数据未保存，请联系系统管理员并反馈相关问题！");
            }

        }


        public static String MessgeToDing_Statistics(string phone, string strName, string strTempSubject, string strTempContent, string strUrl)
        {
            #region 消息通知
            string re = "none";
            //发送请求的数据
            string POSTURL = "http://122.112.213.22/AppMessage/api.aspx";
            Dictionary<string, string> myDictionary = new Dictionary<string, string>();
            myDictionary.Add("appid", "150465644");
            myDictionary.Add("users", phone);
            myDictionary.Add("title", strTempSubject);
            myDictionary.Add("text", strTempContent + DateTime.Now);
            myDictionary.Add("url", strUrl);
            myDictionary.Add("name", strName);
            if (Post(POSTURL, myDictionary))
            {
                re = "true";
            }
            return re;
            #endregion
        }

        /// <summary>  
        /// 指定Post地址使用Get 方式获取全部字符串  
        /// </summary>  
        /// <param name="url">请求后台地址</param>  
        /// <param name="content">Post提交数据内容(utf-8编码的)</param>  
        /// <returns></returns>  
        public static bool Post(string POSTURL, Dictionary<string, string> dic)
        {
            #region 指定Post地址使用Get 方式获取全部字符串
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(POSTURL);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            #region 添加Post 参数
            StringBuilder builder = new StringBuilder();
            int i = 0;
            foreach (var item in dic)
            {
                if (i > 0)
                    builder.Append("&");
                builder.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
            }
            byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容  
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }

            string json = JsonConvert.SerializeObject(result);
            // if("{"errcode":0,"errmsg":"ok","messageId":"decd701487b933a9801352bae77491dc","invalidparty":"","invaliduser":""}"==result)
            //{

            //}

            bool send_result = false;
            if (result.Contains("ok") || result.Contains("success"))
            {
                send_result = true;
            }

            return send_result;
            #endregion
        }

    }
}