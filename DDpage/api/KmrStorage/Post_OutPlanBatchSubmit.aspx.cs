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
    public partial class Post_OutPlanBatchSubmit : System.Web.UI.Page
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
            string strProOrder = Request.Form["ProNo"].ToString();
            string strOrderNum = Request.Form["OrderNum"].ToString();
            string strOrderWeight = Request.Form["OrderWeight"].ToString();
            string strStat = "1";
            string dataStringOne = Request.Form["dataListOne"].ToString();
            //string dataStringTwo = Request.Form["dataListTwo"].ToString();
            //string dataStringTh = Request.Form["dataListTh"].ToString();
            //string dataStringFour = Request.Form["dataListFour"].ToString();
            //string oneNum = Request.Form["OneNum"].ToString();
            //string twoNum = Request.Form["TwoNum"].ToString();
            //string thNum = Request.Form["ThNum"].ToString();
            //string fourNum = Request.Form["FourNum"].ToString();
            //string strGID = System.Guid.NewGuid().ToString();

            Random r = new Random();
            int i = r.Next(1000, 9999);
            Boolean stat = false;
            int t1 = 0;
            
            string strAufnr = "";
            decimal strNtgew = 0;


            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();

            SqlConnection conNew = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            conNew.Open();

            if (dataStringOne.Length > 0)
            {
                string strSql = "";
                //sap201领料
                JArray jsonObjOne = JArray.Parse(dataStringOne);
                foreach (JObject jObject in jsonObjOne)
                {
                    
                    if (jObject["PType"].ToString() == "计划外领料")
                    {
                        strSql = " select distinct [AUFNR],[NTGEW] FROM [DataFromSap].[dbo].[T_SAP_PP_getZPPRP013] where LEN(budat)> 0 ";
                        strSql += " and cast(vornr as int) = cast('" + strProOrder + "' as int) ";
                        strSql += " and YEAR(budat)= '" + DateTime.Now.Year.ToString() + "' ";
                        strSql += " and MONTH(budat)= '" + DateTime.Now.Month.ToString() + "'";

                        SqlCommand cmdNum = new SqlCommand(strSql, con);
                        SqlDataReader sdrNum = cmdNum.ExecuteReader();

                        while (sdrNum.Read())
                        {

                            strAufnr = sdrNum[0].ToString();
                            strNtgew = Convert.ToDecimal(sdrNum[1].ToString());
                            string strPID = System.DateTime.Now.ToFileTime().ToString() + i.ToString();
                            string strGID = System.Guid.NewGuid().ToString();
                            string strPdid = System.Guid.NewGuid().ToString();
                            //获取订单号，生产领料计划

                            //写入领料信息表
                            string strSqlOne = "insert into [DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComBatchPickMaterial] VALUES('" + strPID + "','" + strCompany + "','" + strDept + "','" + strVen + "','" + strSapVenCode + "','" + strName + "','" + strPersonCode + "','" + strTime + "','"+ strAufnr + "','','"+ strProOrder + "','" + strCostCode + "','APP','" + strStat + "','" + strGID + "')";
                            SqlCommand cmdhead = new SqlCommand(strSql, conNew);
                            t1 = cmdhead.ExecuteNonQuery();
                            if (t1 != 0)
                            {
                                stat = true;
                            }

                            //写入领料明细表
                            int strWeight = 0;

                            if (jObject["PlanName"].ToString()== "均分方案")
                            {
                                strWeight = Convert.ToInt32(jObject["PickInventory"].ToString()) / Convert.ToInt32(strOrderNum);

                               

                            }
                            else if (jObject["PlanName"].ToString() == "重量比例方案")
                            {
                                strWeight = Convert.ToInt16(strNtgew * (Convert.ToDecimal(strOrderWeight) / Convert.ToDecimal(strOrderNum)));
                            }

                            strSql = "insert into [DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComBatchPickDetail] VALUES('" + strPdid + "','" + strGID + "','" + jObject["MFactory"] + "','" + jObject["MFactoryCode"] + "','" + jObject["MStock"] + "','" + jObject["MStockCode"] + "','" + jObject["MGroup"] + "','" + jObject["MGroupCode"] + "','','','','" + jObject["Material"] + "','" + jObject["MCode"] + "','" + jObject["MBatch"] + "','" + jObject["MUnit"] + "','" + jObject["MInventory"] + "','" + strWeight + "','" + jObject["PType"] + "','1')";

                            SqlCommand cmd = new SqlCommand(strSql, conNew);
                            t1 = cmd.ExecuteNonQuery();
                            if (t1 != 0)
                            {
                                stat = true;
                            }

                            //写入sap接口调用数据表
                            strSql = "insert into  [DDdatabase].[dbo].[Tab_KocelApp_Kmr_WaitSapRecord]([ID],[PType] ,[PItemType] ,[PDID],[oddNo] ,[CostCenter],[SapVenture],[MFactoryCode] ,[MStockCode],[MCode] ,[PNum],[MUnit],[MRsNo] ,[MRsPos],[Vornor] ,[MBatch] ,[personCode] ,[inputTime],[ifSap])";
                            strSql += " values('" + System.Guid.NewGuid().ToString() + "','公司内领料','" + jObject["PType"].ToString() + "','" + strPdid + "','" + strAufnr + "','" + strCostCode + "','" + strSapVenCode + "','" + jObject["MFactoryCode"] + "','" + jObject["MStockCode"] + "','" + jObject["MCode"] + "','" + strWeight + "','" + jObject["MUnit"] + "','','','" + strProOrder + "','" + jObject["MBatch"] + "','" + strPersonCode + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','0')";

                            SqlCommand cmdWaitSap = new SqlCommand(strSql, conNew);
                            t1 = cmdWaitSap.ExecuteNonQuery();
                            if (t1 != 0)
                            {
                                stat = true;
                            }

                        }

                        sdrNum.Close();

                    }
                }
            }


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