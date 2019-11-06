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
    public partial class Post_CostCenterBatchSubmit : System.Web.UI.Page
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
            string strSapVenCode = Request.Form["SapVenId"].ToString();
            string strName = Request.Form["PName"].ToString();
            string strPersonCode = Request.Form["PCode"].ToString();
            string strCostCode = Request.Form["PCostCode"].ToString();
            string strTime = DateTime.Now.ToString();
            //string strOrder = Request.Form["OddNo"].ToString();
            //string strProOrder = Request.Form["ProNo"].ToString();
            string strStat = "1";
            string dataStringOne = Request.Form["dataListOne"].ToString();
            //string dataStringTwo = Request.Form["dataListTwo"].ToString();
            //string dataStringTh = Request.Form["dataListTh"].ToString();
            //string dataStringFour = Request.Form["dataListFour"].ToString();
            //string oneNum = Request.Form["OneNum"].ToString();
            //string twoNum = Request.Form["TwoNum"].ToString();
            //string thNum = Request.Form["ThNum"].ToString();
            //string fourNum = Request.Form["FourNum"].ToString();
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


            string strSql = "insert into [DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComBatchPickMaterial] VALUES('" + strPID + "','" + strCompany + "','" + strDept + "','" + strVen + "','" + strSapVenCode + "','" + strName + "','" + strPersonCode + "','" + strTime + "','','','','" + strCostCode + "','APP','" + strStat + "','" + strGID + "')";
            SqlCommand cmdhead = new SqlCommand(strSql, con);
            t1 = cmdhead.ExecuteNonQuery();
            if (t1 != 0)
            {
                stat = true;
            }


            
            

            if (dataStringOne.Length > 0)
            {
                
                //sap201领料
                JArray jsonObjOne = JArray.Parse(dataStringOne);
                foreach (JObject jObject in jsonObjOne)
                {
                    string strPdid = System.Guid.NewGuid().ToString();
                    if (jObject["PType"].ToString() == "成本中心领料")
                    {
                        strSql = "insert into [DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComBatchPickDetail] VALUES('" + strPdid + "','" + strGID + "','" + jObject["MFactory"] + "','" + jObject["MFactoryCode"] + "','" + jObject["MStock"] + "','" + jObject["MStockCode"] + "','" + jObject["MGroup"] + "','" + jObject["MGroupCode"] + "','','','','" + jObject["Material"] + "','" + jObject["MCode"] + "','" + jObject["MBatch"] + "','" + jObject["MUnit"] + "','" + jObject["MInventory"] + "','" + jObject["PickInventory"] + "','" + jObject["PType"] + "','1')";

                        SqlCommand cmd = new SqlCommand(strSql, con);
                        t1 = cmd.ExecuteNonQuery();
                        if (t1 != 0)
                        {
                            stat = true;
                        }

                        //写入sap接口调用数据表
                        strSql = "insert into  [DDdatabase].[dbo].[Tab_KocelApp_Kmr_WaitSapRecord]([ID],[PType] ,[PItemType] ,[PDID],[oddNo] ,[CostCenter],[SapVenture],[MFactoryCode] ,[MStockCode],[MCode] ,[PNum],[MUnit],[MRsNo] ,[MRsPos],[Vornor] ,[MBatch] ,[personCode] ,[inputTime],[ifSap])";
                        strSql += " values('" + System.Guid.NewGuid().ToString() + "','公司内领料','" + jObject["PType"].ToString() + "','" + strPdid + "','','" + strCostCode + "','" + strSapVenCode + "','" + jObject["MFactoryCode"] + "','" + jObject["MStockCode"] + "','" + jObject["MCode"] + "','" + jObject["PickInventory"] + "','" + jObject["MUnit"] + "','','','','" + jObject["MBatch"] + "','" + strPersonCode + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','0')";

                        SqlCommand cmdWaitSap = new SqlCommand(strSql, conNew);
                        t1 = cmdWaitSap.ExecuteNonQuery();
                        if (t1 != 0)
                        {
                            stat = true;
                        }

                    }
                }
            }



            //判断该领料单下是否全部异常，如是则改变领料单状态1--》2

            //strSql = "select * from [DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComPickDetail] where 1=1 ";
            //strSql += " and PLinkID='" + strGID + "'";
            //strSql += " and [PDStat]=1 ";

            //SqlCommand cmdCheck = new SqlCommand(strSql, con);
            //SqlDataReader sdrCheck = cmdCheck.ExecuteReader();
            //if (!sdrCheck.HasRows)
            //{
            //    strSql = " update [DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComPickMaterial] set [PickStat] = 2  where 1=1";
            //    strSql += " and PLinkID='" + strGID + "'";
            //    SqlCommand cmdCheckErr = new SqlCommand(strSql, conNew);
            //    t1 = cmdCheckErr.ExecuteNonQuery();
            //    if (t1 != 0)
            //    {
            //        stat = true;
            //    }

            //}

            //sdrCheck.Close();



            con.Close();
            conNew.Close();

            string strJson = "";

            if (stat)
            {
                strJson = "{\"info\":\"数据已保存！\"}";
                Response.Write(strJson);
            }
            else
            {
                strJson = "{\"info\":\"领料数据异常！\"}";
                Response.Write(strJson);
            }
        }




       
       


    }
}