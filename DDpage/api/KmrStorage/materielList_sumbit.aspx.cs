using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.KmrStorage
{
    public partial class materielList_sumbit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            string data = Request.Form["data"];

            //JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            ////string s = jsonSerializer.Serialize(data);
            //Object s = jsonSerializer.Deserialize<object>(data);

            string strCompany = Request.Form["PCompany"].ToString();
            string strDept = Request.Form["PDept"].ToString();
            string strVen = Request.Form["PVenture"].ToString();
            string strName = Request.Form["PName"].ToString();
            string strPersonCode = Request.Form["PCode"].ToString();
            string strTime = Request.Form["PTime"].ToString();
            string strOrder = Request.Form["OddNo"].ToString();
            string strStat = Request.Form["PickStat"].ToString();
            string dataString = Request.Form["dataList"].ToString();
            string strGID = System.Guid.NewGuid().ToString();
            
            Random r = new Random();
            int i = r.Next(1000, 9999);
            Boolean stat = false;
            int t1 = 0;
            string strPID = System.DateTime.Now.ToFileTime().ToString()+i.ToString();


            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();

            SqlConnection conNew = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            conNew.Open();


            string strSql = "insert into [DDdatabase].[dbo].[Tab_kocelApp_Kmr_PickMaterial] VALUES('" + strPID + "','" + strCompany + "','" + strDept + "','" + strVen + "','" + strName + "','" + strPersonCode + "','"+ strTime + "','"+ strOrder + "','"+ strStat + "','"+ strGID + "')";
            SqlCommand cmdhead = new SqlCommand(strSql, con);
            t1 = cmdhead.ExecuteNonQuery();
            if (t1 != 0)
            {
                stat = true;
            }



            JArray jsonObj = JArray.Parse(dataString);
            foreach (JObject jObject in jsonObj)
            {
                string strPdid = System.Guid.NewGuid().ToString();
                if (jObject["PType"].ToString() == "即时领料")
                {
                    strSql = "insert into [DDdatabase].[dbo].[Tab_kocelApp_Kmr_PickDetail] VALUES('" + strPdid + "','" + strGID + "','" + jObject["MFactory"] + "','" + jObject["MFactoryCode"] + "','" + jObject["MStock"] + "','" + jObject["MStockCode"] + "','" + jObject["MGroup"] + "','" + jObject["MGroupCode"] + "','" + jObject["Material"] + "','" + jObject["MCode"] + "','" + jObject["MBatch"] + "','" + jObject["MUnit"] + "','" + jObject["MInventory"] + "','" + jObject["PickInventory"] + "','" + jObject["PType"] + "','4')";
                }
                else
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
                if (jObject["PType"].ToString() != "即时领料")
                {
                    string isWeight = "";
                    if (jObject["MUnit"].ToString()== "KG")
                    {
                         isWeight = "1"; 
                    }
                    else
                    {
                         isWeight = "0";
                    }

                    strSql = "select [FKeeper],[FKeeperCode] from [DDdatabase].[dbo].[View_KocelApp_Kmr_StockListBaseData] where pdid='" + strPdid + "'";
                
                    SqlCommand cmd1 = new SqlCommand(strSql, con);
                    SqlDataReader sdr = cmd1.ExecuteReader();
                    while (sdr.Read())
                    {
                        var strKeeper = sdr[0].ToString();
                        var strKeeperCode = sdr[1].ToString();
                        strSql = "insert into [DDdatabase].[dbo].[Tab_KocelApp_Kmr_StockDetail] values('"+ System.Guid.NewGuid().ToString() + "','"+ strPdid + "','"+isWeight+"','"+ jObject["PickInventory"] + "','"+ strKeeper + "','"+ strKeeperCode + "','"+ DateTime.Now.ToString() + "',NULL,'0')";
                        SqlCommand cmd2 = new SqlCommand(strSql, conNew);
                        t1 = cmd2.ExecuteNonQuery();
                        if (t1 != 0)
                        {
                            stat = true;
                        }
                    }
                    sdr.Close();


                }
                //后台依据库房，物料组从基础数据中获取备料人员信息，并下发备料计划
            }

            //异常处理，及时库存中有，基础数据表中没有，导致没有下达备料计划
            strSql = "update [DDdatabase].[dbo].[Tab_kocelApp_Kmr_PickDetail] set PDStat=9 where PLinkID='"+ strGID + "' and PType = '现场领料' and PDID not in (select distinct PDID from [DDdatabase].[dbo].[View_KocelApp_Kmr_StockListBaseData] where PLinkID='" + strGID + "') ";
            SqlCommand cmderr = new SqlCommand(strSql, conNew);
            cmderr.ExecuteNonQuery();

            //异常处理，及时库存中有，基础数据表中没有，导致没有下达备料计划

            con.Close();
            conNew.Close();


            if (stat) {
                Response.Write("数据已保存！");
            }
            else
            {
                Response.Write("数据未保存，请联系系统管理员并反馈相关问题！");
            }
        }
    }
}