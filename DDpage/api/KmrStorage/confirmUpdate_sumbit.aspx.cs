using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.KmrStorage
{
    public partial class confirmUpdate_sumbit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCompany = Request.Form["PCompany"].ToString();
            string strDept = Request.Form["PDept"].ToString();
            string strVen = Request.Form["PVenture"].ToString();
            string strName = Request.Form["PName"].ToString();
            string strPersonCode = Request.Form["PCode"].ToString();
            string strSapVenCode = Request.Form["sapCode"].ToString();

            string dataString = Request.Form["dataList"].ToString();
            string strGID = System.Guid.NewGuid().ToString();

            int t1 = 0;
            //string strJson = "[";
            Boolean stat = false;
            string strPlinkId = "";



            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();
            SqlConnection conNew = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            conNew.Open();

            JArray jsonObj = JArray.Parse(dataString);
            foreach (JObject jObject in jsonObj)
            {
                //更新领料确认数据表
                string strPdid = jObject["pdid"].ToString();
                string strSql = " update [DDdatabase].[dbo].[Tab_KocelApp_Kmr_ComConfDetail] set CETime='" + DateTime.Now.ToString() + "',CStat=1 where 1=1";
                strSql += " and PDID='" + strPdid + "'";

                SqlCommand cmd01 = new SqlCommand(strSql, con);
                t1 = cmd01.ExecuteNonQuery();
                if (t1 != 0)
                {
                    stat = true;
                }
                
                //修改[DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComPickDetail]表[PDStat]=2

                strSql = "update [DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComPickDetail] set [PDStat]=4 where PDID='" + strPdid + "'";
                SqlCommand cmd02 = new SqlCommand(strSql, con);
                t1 = cmd02.ExecuteNonQuery();
                if (t1 != 0)
                {
                    stat = true;
                }

                strSql = "select [PLinkID]  from [DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComPickDetail] where PDID='" + strPdid + "'";
                SqlCommand cmdPlinkID = new SqlCommand(strSql, con);
                SqlDataReader sdrPlinkID = cmdPlinkID.ExecuteReader();
                while (sdrPlinkID.Read())
                {
                    strPlinkId = sdrPlinkID[0].ToString();
                }
                sdrPlinkID.Close();




                strSql = "select * from [DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComPickDetail] where PLinkID = '" + strPlinkId + "'  and [PDStat] < 4";

                SqlCommand cmdPlinkDetail = new SqlCommand(strSql, con);
                SqlDataReader sdrPlinkDetail = cmdPlinkDetail.ExecuteReader();
                if (!sdrPlinkDetail.HasRows)
                {
                    strSql = "update [DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComPickMaterial] set PickStat=2 where PLinkID='" + strPlinkId + "' ";
                    SqlCommand cmdPickHead = new SqlCommand(strSql, conNew);
                    t1 = cmdPickHead.ExecuteNonQuery();
                    if (t1 != 0)
                    {
                        stat = true;
                    }

                }
                sdrPlinkDetail.Close();

                //更新领料确认数据表

                //写入sap接口调用数据表
                //依据pdid查询获取基础数据，依据领料类型调用sap接口
                strSql = "select PType,oddNo,CostCenter,SapVenture,MFactoryCode,MStockCode,MCode,ConfInventory,MUnit,MRsNo,MRsPos,Vornor,MBatch  from [DDdatabase].[dbo].[View_KocelApp_Kmr_ComConfDetail] where pdid='" + strPdid + "'";
                SqlCommand cmdJsonDetail = new SqlCommand(strSql, con);
                SqlDataReader sdrJsonDetail = cmdJsonDetail.ExecuteReader();
                while (sdrJsonDetail.Read())
                {
                    string strType = sdrJsonDetail[0].ToString();
                    string strOddNo = sdrJsonDetail[1].ToString();
                    string strCoseCenter = sdrJsonDetail[2].ToString();
                    string strSapVenture = sdrJsonDetail[3].ToString();
                    string strMFactoryCode = sdrJsonDetail[4].ToString();
                    string strMStockCode = sdrJsonDetail[5].ToString();
                    string strMCode = sdrJsonDetail[6].ToString();
                    string strMInventory = sdrJsonDetail[7].ToString();
                    string strMUnit = sdrJsonDetail[8].ToString();
                    string strMRsNo = sdrJsonDetail[9].ToString();
                    string strMRsPos = sdrJsonDetail[10].ToString();
                    string strVornor = sdrJsonDetail[11].ToString();
                    string strMBatch = sdrJsonDetail[12].ToString();

                    strSql = "insert into  [DDdatabase].[dbo].[Tab_KocelApp_Kmr_WaitSapRecord]([ID],[PType] ,[PItemType] ,[PDID],[oddNo] ,[CostCenter],[SapVenture],[MFactoryCode] ,[MStockCode],[MCode] ,[PNum],[MUnit],[MRsNo] ,[MRsPos],[Vornor] ,[MBatch] ,[personCode] ,[inputTime],[ifSap])";
                    strSql += " values('"+ System.Guid.NewGuid().ToString() + "','公司内领料','"+ strType + "','"+ strPdid + "','"+ strOddNo + "','"+ strCoseCenter + "','"+ strSapVenture + "','"+ strMFactoryCode + "','"+ strMStockCode + "','"+ strMCode + "','"+ strMInventory + "','"+ strMUnit + "','"+ strMRsNo + "','"+ strMRsPos + "','"+ strVornor + "','"+ strMBatch + "','"+ strPersonCode + "','"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"','0')";

                    SqlCommand cmdWaitSap = new SqlCommand(strSql, conNew);
                    t1 = cmdWaitSap.ExecuteNonQuery();
                    if (t1 != 0)
                    {
                        stat = true;
                    }

                }

                sdrJsonDetail.Close();
                    //写入sap接口调用数据表

            }

            con.Close();
            conNew.Close();


            if (stat)
            {
                Response.Write("数据已保存！");
            }
            else
            {
                Response.Write("业务逻辑出错，请联系系统管理人员！");
            }
        }
    }
}