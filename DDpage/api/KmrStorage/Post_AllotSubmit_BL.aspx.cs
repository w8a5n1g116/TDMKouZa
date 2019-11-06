using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDpage.YiKu311;
using System.Net;

namespace DDpage.api.KmrStorage
{
    public partial class Post_AllotSubmit_BL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string data = Request.Form["data"];
            string strCompany = Request.Form["PCompany"].ToString();
            string strDept = Request.Form["PDept"].ToString();
            string strVen = Request.Form["PVenture"].ToString();
            string strSapVenCode = Request.Form["SapVenId"].ToString();
            string strName = Request.Form["PName"].ToString();
            string strPersonCode = Request.Form["PCode"].ToString();
            string strCostCode = Request.Form["PCostCode"].ToString();
            string strInStock = Request.Form["InStock"].ToString();
            string strInStockCode = Request.Form["InStockCode"].ToString();
            string strOutStock = Request.Form["OutStock"].ToString();
            string strOutStockCode = Request.Form["OutStockCode"].ToString();
            string strTime = DateTime.Now.ToString();
            string strStat = "1";
            string dataStringOne = Request.Form["dataListOne"].ToString();
            string strGID = System.Guid.NewGuid().ToString();

            Random r = new Random();
            int i = r.Next(1000, 9999);
            Boolean stat = false;
            int t1 = 0;
            string strPID = System.DateTime.Now.ToFileTime().ToString() + i.ToString();


            SqlConnection con = new SqlConnection("Data Source=192.168.197.101;User ID=sa;Password=2018Sql101");
            con.Open();

            SqlConnection conNew = new SqlConnection("Data Source=192.168.197.101;User ID=sa;Password=2018Sql101");
            conNew.Open();


            string strSql = "insert into [DDdatabase].[dbo].[Tab_kocelApp_Kmr_AllotMaterial] VALUES('" + strPID + "','" + strCompany + "','" + strDept + "','" + strVen + "','" + strSapVenCode + "','" + strName + "','" + strPersonCode + "','" + strTime + "','" + strCostCode + "','APP','" + strStat + "','" + strGID + "')";
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
                    ZMM_GOODSMOVE_311 zmm = new ZMM_GOODSMOVE_311();

                    ZZXGOODSMOVE[] zzxs = new ZZXGOODSMOVE[1];
                    ZZXGOODSMOVE zzx = new ZZXGOODSMOVE();
                    zzx.MATNR = jObject["MCode"].ToString();
                    zzx.ERFMG = decimal.Parse(jObject["PickInventory"].ToString());
                    zzxs[0] = zzx;

                    zmm.ZGOODSMOVE = zzxs;
                    zmm.FLGORT = strOutStockCode;
                    zmm.TLGORT = strInStockCode;
                    zmm.ZWERKS = "1071";

                    ZMOVE311 serv = new ZMOVE311();

                    serv.Credentials = new NetworkCredential("RFC_USER", "pfseR91iC20an");
                    ZMM_GOODSMOVE_311Response res_return = serv.ZMM_GOODSMOVE_311(zmm);


                    string ifPost = res_return.E_MSGTY;
                    string ifMessage = "";
                    string ifNumber = res_return.E_MSGTX;
                    string ifYear = DateTime.Now.Year.ToString();

                    string strPdid = System.Guid.NewGuid().ToString();

                    if (ifPost == "S")
                    {
                        //接口调用成功
                        string strTmp = ifMessage + "||凭证号：" + ifNumber + "||凭证年度:" + ifYear;
                        GetSucRecord(strPdid, strTmp, "仓库调拨", strPersonCode);
                        //stat = true;
                        //接口调用成功

                        strSql = "insert into [DDdatabase].[dbo].[Tab_kocelApp_Kmr_AllotDetail]([PDID],[PLinkID],[MFactory],[MFactoryCode],[MOutStockCode],[MOutStockName],[MInStockCode],[MInStockName] ";
                        strSql += " ,[MGroup],[MGroupCode] ,[Material],[MCode] ,[MBatch],[MUnit],[MInventory] ,[allotNum],[PType],[PDStat])";
                        strSql += " VALUES('" + strPdid + "','" + strGID + "','" + jObject["MFactory"] + "','" + jObject["MFactoryCode"] + "','" + strOutStockCode + "','" + strOutStock + "','" + strInStockCode + "','" + strInStock + "','" + jObject["MGroup"] + "','" + jObject["MGroupCode"] + "','" + jObject["Material"] + "','" + jObject["MCode"] + "','" + jObject["MBatch"] + "','" + jObject["MUnit"] + "','" + jObject["MInventory"] + "','" + jObject["PickInventory"] + "','" + jObject["PType"] + "','1')";
                        SqlCommand cmd = new SqlCommand(strSql, con);
                        t1 = cmd.ExecuteNonQuery();
                        if (t1 != 0)
                        {
                            stat = true;
                        }

                    }
                    else
                    {
                        GetErrRecord(strPdid, ifMessage, "仓库调拨", strPersonCode);
                        stat = false;
                    }
                   
                    




                        ////写入sap接口调用数据表
                        //strSql = "insert into  [DDdatabase].[dbo].[Tab_KocelApp_Kmr_WaitSapRecord]([ID],[PType] ,[PItemType] ,[PDID],[oddNo] ,[CostCenter],[SapVenture],[MFactoryCode] ,[MStockCode],[MCode] ,[PNum],[MUnit],[MRsNo] ,[MRsPos],[Vornor] ,[MBatch] ,[personCode] ,[inputTime],[ifSap])";
                        //strSql += " values('" + System.Guid.NewGuid().ToString() + "','公司内领料','" + jObject["PType"].ToString() + "','" + strPdid + "','','" + strCostCode + "','" + strSapVenCode + "','" + jObject["MFactoryCode"] + "','" + jObject["MStockCode"] + "','" + jObject["MCode"] + "','" + jObject["PickInventory"] + "','" + jObject["MUnit"] + "','','','','" + jObject["MBatch"] + "','" + strPersonCode + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','0')";

                        //SqlCommand cmdWaitSap = new SqlCommand(strSql, conNew);
                        //t1 = cmdWaitSap.ExecuteNonQuery();
                        //if (t1 != 0)
                        //{
                        //    stat = true;
                        //}
                }
            }



            con.Close();
            conNew.Close();

            string strJson = "";

            if (stat)
            {
                strJson = "{\"info\":\"数据已提交！\"}";
                Response.Write(strJson);
            }
            else
            {
                strJson = "{\"info\":\"数据异常！\"}";
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