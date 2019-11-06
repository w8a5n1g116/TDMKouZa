using DDpage.PoRuku;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.KmrStorage
{
    public partial class checkInList_sumbit_BL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCompany = Request.Form["PCompany"].ToString();
            string strDept = Request.Form["PDept"].ToString();
            string strVen = Request.Form["PVenture"].ToString();
            string strName = Request.Form["PName"].ToString();
            string strPersonCode = Request.Form["PCode"].ToString();
            string strTime = Request.Form["PTime"].ToString();
            string dataString = Request.Form["dataList"].ToString();
            Boolean stat = false;
            int t1 = 0;
            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.197.101;User ID=sa;Password=2018Sql101");
            con.Open();


            JArray jsonObj = JArray.Parse(dataString);
            foreach (JObject jObject in jsonObj)
            {
                
                string strPdid = System.Guid.NewGuid().ToString();
              
                strSql = "insert into [DDdatabase].[dbo].[Tab_KocelApp_Kmr_CheckInList] ";
                strSql += " VALUES('" + strPdid + "','" + jObject["purOrder"] + "','" + jObject["itemOrder"] + "','" + jObject["matName"] + "','" + jObject["matCode"] + "','" + jObject["matUnit"] + "','" + jObject["supplyName"] + "','" + jObject["supplyCode"] + "','" + jObject["companyName"] + "','" + jObject["companyCode"] + "','" + jObject["companyShortName"] + "','" + jObject["facName"] + "','" + jObject["facCode"] + "','" + jObject["stockName"] + "','" + jObject["stockCode"] + "','" + jObject["purNum"] + "','" + jObject["checkNum"] + "','" + jObject["pnum"] + "','" + jObject["adate"] + "','" + jObject["pdate"] + "','"+strCompany+"','"+strDept+"','"+strVen+"','"+ strName + "','0','')";
              

                SqlCommand cmd = new SqlCommand(strSql, con);
                t1 = cmd.ExecuteNonQuery();
                if (t1 != 0)
                {
                    stat = true;
                }


                //调用sap入库过账接口赋值
                ZPP_PO_MOVE sap_data = new ZPP_PO_MOVE();

                if(string.IsNullOrEmpty(jObject["insmk"].ToString()))
                    sap_data.ZBWART = "101";
                else
                    sap_data.ZBWART = "103";
                sap_data.ZEBELN = jObject["purOrder"].ToString();
                sap_data.ZEBELP = jObject["itemOrder"].ToString();
                sap_data.ZWEMNG = Convert.ToDecimal(jObject["pnum"].ToString());

                ZPOMIGO sap_serv = new ZPOMIGO();
                sap_serv.Credentials = new NetworkCredential("RFC_USER", "pfseR91iC20an");

                ZPP_PO_MOVEResponse sap_return = sap_serv.ZPP_PO_MOVE(sap_data);

                string ifPost = sap_return.E_MSGTY;
                string ifMessage = "";
                string ifNumber = sap_return.E_MSGTX;
                string ifYear = DateTime.Now.Year.ToString();

               

                if (ifPost == "S")
                {
                    //接口调用成功
                    string strTmp = ifPost + "||" + ifMessage + "||凭证号：" + ifNumber + "||凭证年度:" + ifYear;
                    GetSucRecord(strPdid, strTmp, "验收入库", strPersonCode);
                    //接口调用成功
                }
                else
                {
                    string strTmp = ifPost + "||" + ifMessage + "||凭证号：" + ifNumber + "||凭证年度:" + ifYear;
                    GetErrRecord(strPdid, strTmp, "验收入库", strPersonCode);
                }
                //sap 接口传递并接受返回值

            }

            //调用sap入库过账接口




            con.Close();

            if (stat)
            {
                Response.Write("数据已提交！");
            }
            else
            {
                Response.Write("数据未保存，请联系系统管理员并反馈相关问题！");
            }

           
        }

        public void GetErrRecord(string recordId, string errMessage, string errType, string perCode)
        {
            //记录错误信息,错误数据表，ID，错误信息，错误类型
            SqlConnection con = new SqlConnection("Data Source=192.168.197.101;User ID=sa;Password=2018Sql101");
            con.Open();

            string strSql = "insert into [DDdatabase].[dbo].[Tab_KocelApp_Kmr_PostSapRecord]([ID],[ifSap] ,[pickType],[pickID],[pickInfo],[pickItemType],[pickDate],[pickPersonId]) ";
            strSql += " VALUES('" + System.Guid.NewGuid().ToString() + "','0','验收入库','" + recordId + "','" + errMessage + "','" + errType + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + perCode + "')";
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
            strSql += " VALUES('" + System.Guid.NewGuid().ToString() + "','1','验收入库','" + recordId + "','" + sucMessage + "','" + sucType + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + perCode + "')";
            SqlCommand cmdsuc = new SqlCommand(strSql, con);
            cmdsuc.ExecuteNonQuery();

            con.Close();

        }

    }
}