using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDpage.GetSapProductOrder;
using System.Net;
using DDpage.PlanOut;

namespace DDpage.api.KmrStorage
{
    public partial class Post_OutPlanBatchSubmit_BL : System.Web.UI.Page
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
            string StartDate = Request.Form["StartDate"];
            string EndDate = Request.Form["EndDate"];

            
            Boolean stat = false;
            int t1 = 0;
            
            string strAufnr = "";
            decimal strNtgew = 0;


            SqlConnection con = new SqlConnection("Data Source=192.168.197.101;User ID=sa;Password=2018Sql101");
            con.Open();

            SqlConnection conNew = new SqlConnection("Data Source=192.168.197.101;User ID=sa;Password=2018Sql101");
            conNew.Open();

            ZzxGetorderconfirmations zzx = new ZzxGetorderconfirmations();

            ZzxOrderconfirmations[] orders = new ZzxOrderconfirmations[] { };
            Bapireturn[] rets = new Bapireturn[] { };

            zzx.GdtItem = orders;
            zzx.GdtReturn = rets;
            zzx.BeginDat = DateTime.Parse(StartDate).ToString("yyyyMMdd");
            zzx.EndDat = DateTime.Parse(EndDate).ToString("yyyyMMdd");

            ZZX_GETORDERCONFIRM service = new ZZX_GETORDERCONFIRM();
            service.Credentials = new NetworkCredential("RFC_USER", "pfseR91iC20an");

            ZzxGetorderconfirmationsResponse retresponse = service.ZzxGetorderconfirmations(zzx);


            string strJson = "";

            if (dataStringOne.Length > 0 && retresponse.GdtItem.Any())
            {
                string strSql = "";
                //sap201领料
                JArray jsonObjOne = JArray.Parse(dataStringOne);
                foreach (JObject jObject in jsonObjOne)
                {
                    if (jObject["PType"].ToString() == "计划外领料")
                    {

                        var orderList = retresponse.GdtItem.Where(p => p.Arbpl.Contains(strSapVenCode)).ToList();

                        foreach(var order in orderList)
                        {

                            Random r = new Random();
                            int i = r.Next(1000, 9999);

                            strAufnr = order.Aufnr;
                            strNtgew = order.Ntgew;
                            string strPID = System.DateTime.Now.ToFileTime().ToString() + i.ToString();
                            string strGID = System.Guid.NewGuid().ToString();
                            string strPdid = System.Guid.NewGuid().ToString();
                            //获取订单号，生产领料计划

                            //写入领料信息表
                            string strSqlOne = "insert into [DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComBatchPickMaterial] VALUES('" + strPID + "','" + strCompany + "','" + strDept + "','" + strVen + "','" + strSapVenCode + "','" + strName + "','" + strPersonCode + "','" + strTime + "','"+ strAufnr + "','','"+ strProOrder + "','" + strCostCode + "','APP','" + strStat + "','" + strGID + "')";
                            SqlCommand cmdhead = new SqlCommand(strSqlOne, conNew);
                            t1 = cmdhead.ExecuteNonQuery();
                            if (t1 != 0)
                            {
                                stat = true;
                            }

                            //写入领料明细表
                            decimal strWeight = 0;

                            if (jObject["PlanName"].ToString()== "均分方案")
                            {
                                strWeight = Convert.ToDecimal(jObject["PickInventory"].ToString()) / Convert.ToDecimal(strOrderNum);

                               

                            }
                            else if (jObject["PlanName"].ToString() == "重量比例方案")
                            {
                                strWeight = Convert.ToDecimal(strNtgew * Convert.ToDecimal(jObject["PickInventory"].ToString()) / Convert.ToDecimal(strOrderWeight) ) ;
                            }

                            strSql = "insert into [DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComBatchPickDetail] VALUES('" + strPdid + "','" + strGID + "','" + jObject["MFactory"] + "','" + jObject["MFactoryCode"] + "','" + jObject["MStock"] + "','" + jObject["MStockCode"] + "','" + jObject["MGroup"] + "','" + jObject["MGroupCode"] + "','','','','" + jObject["Material"] + "','" + jObject["MCode"] + "','" + jObject["MBatch"] + "','" + jObject["MUnit"] + "','" + jObject["MInventory"] + "','" + strWeight + "','" + jObject["PType"] + "','1')";

                            SqlCommand cmd = new SqlCommand(strSql, conNew);
                            t1 = cmd.ExecuteNonQuery();
                            if (t1 != 0)
                            {
                                stat = true;
                            }

                            ////写入sap接口调用数据表
                            //strSql = "insert into  [DDdatabase].[dbo].[Tab_KocelApp_Kmr_WaitSapRecord]([ID],[PType] ,[PItemType] ,[PDID],[oddNo] ,[CostCenter],[SapVenture],[MFactoryCode] ,[MStockCode],[MCode] ,[PNum],[MUnit],[MRsNo] ,[MRsPos],[Vornor] ,[MBatch] ,[personCode] ,[inputTime],[ifSap])";
                            //strSql += " values('" + System.Guid.NewGuid().ToString() + "','公司内领料','" + jObject["PType"].ToString() + "','" + strPdid + "','" + strAufnr + "','" + strCostCode + "','" + strSapVenCode + "','" + jObject["MFactoryCode"] + "','" + jObject["MStockCode"] + "','" + jObject["MCode"] + "','" + strWeight + "','" + jObject["MUnit"] + "','','','" + strProOrder + "','" + jObject["MBatch"] + "','" + strPersonCode + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','0')";

                            //SqlCommand cmdWaitSap = new SqlCommand(strSql, conNew);
                            //t1 = cmdWaitSap.ExecuteNonQuery();
                            //if (t1 != 0)
                            //{
                            //    stat = true;
                            //}

                            ZPP_NO_PLAN planout = new ZPP_NO_PLAN();
                            PlanOut.ZPP_PLAN_HEAD head = new PlanOut.ZPP_PLAN_HEAD();
                            head.AUFNR = order.Aufnr;
                            head.SGTXT = order.Vornr;
                            PlanOut.ZPP_PLAN_MSG[] msg = new PlanOut.ZPP_PLAN_MSG[] { };
                            PlanOut.BAPIRET2[] ret2 = new PlanOut.BAPIRET2[] { };
                            PlanOut.ZPP_PLAN_ITEM[] item = new PlanOut.ZPP_PLAN_ITEM[1];

                            PlanOut.ZPP_PLAN_ITEM sap_itemtmp = new PlanOut.ZPP_PLAN_ITEM();
                            sap_itemtmp.MATNR = jObject["MCode"].ToString();
                            sap_itemtmp.LGORT = jObject["MStockCode"].ToString();
                            sap_itemtmp.MENGE = System.Decimal.Round(strWeight, 2);
                            sap_itemtmp.WERKS = "1071";

                            item[0] = sap_itemtmp;

                            planout.ET_MSG = msg;
                            planout.ET_RETURN = ret2;
                            planout.IT_ITEM = item;
                            planout.IS_HEAD = head;


                            ZZXNOPLAN zzxplan = new ZZXNOPLAN();
                            zzxplan.Credentials = new NetworkCredential("RFC_USER", "pfseR91iC20an");

                            ZPP_NO_PLANResponse zppplanret = zzxplan.ZPP_NO_PLAN(planout);



                            string ifPost = zppplanret.E_MSGTY;
                            string ifMessage = "";
                            string ifNumber = zppplanret.E_MSGTX;
                            string ifYear = DateTime.Now.Year.ToString();

                            if (ifPost == "S")
                            {
                                //接口调用成功
                                string strTmp = ifMessage + "||凭证号：" + ifNumber + "||凭证年度:" + ifYear;
                                GetSucRecord(strPdid, strTmp, "计划外领料", strPersonCode);
                                stat = true;
                                //接口调用成功
                            }
                            else
                            {
                                GetErrRecord(strPdid, ifMessage, "计划外领料", strPersonCode);
                                stat = false;
                            }

                        }

                    }
                    
                }
            }
            else
            {
                strJson = "{\"info\":\"无订单数据！提交失败！\"}";
                Response.Write(strJson);
                return;
            }

            con.Close();
            conNew.Close();

            

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





        public void GetErrRecord(string recordId, string errMessage, string errType, string perCode)
        {
            //记录错误信息,错误数据表，ID，错误信息，错误类型
            SqlConnection con = new SqlConnection("Data Source=192.168.197.101;User ID=sa;Password=2018Sql101");
            con.Open();

            string strSql = "insert into [DDdatabase].[dbo].[Tab_KocelApp_Kmr_PostSapRecord]([ID],[ifSap] ,[pickType],[pickID],[pickInfo],[pickItemType],[pickDate],[pickPersonId]) ";
            strSql += " VALUES('" + System.Guid.NewGuid().ToString() + "','0','公司内领料','" + recordId + "','" + errMessage + "','" + errType + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + perCode + "')";
            SqlCommand cmdsuc = new SqlCommand(strSql, con);
            cmdsuc.ExecuteNonQuery();

            con.Close();
        }

        public void GetSucRecord(string recordId, string sucMessage, string sucType, string perCode)
        {
            //记录成功信息,错误数据表，ID，错误信息，错误类型
            SqlConnection con = new SqlConnection("Data Source=192.168.197.101;User ID=sa;Password=2018Sql101");
            con.Open();

            string strSql = "insert into [DDdatabase].[dbo].[Tab_KocelApp_Kmr_PostSapRecord]([ID],[ifSap] ,[pickType],[pickID],[pickInfo],[pickItemType],[pickDate],[pickPersonId]) ";
            strSql += " VALUES('" + System.Guid.NewGuid().ToString() + "','1','公司内领料','" + recordId + "','" + sucMessage + "','" + sucType + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + perCode + "')";
            SqlCommand cmdsuc = new SqlCommand(strSql, con);
            cmdsuc.ExecuteNonQuery();

            con.Close();

        }


    }
}