using DDpage.Post_KmrApp_CheckIn;
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
    public partial class checkInList_toDiBang1900 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCompany = Request.Form["PCompany"].ToString();
            string strDept = Request.Form["PDept"].ToString();
            string strVen = Request.Form["PVenture"].ToString();
            string strName = Request.Form["PName"].ToString();
            string strPersonCode = Request.Form["PCode"].ToString();
            string strTime = Request.Form["PTime"].ToString();
            string CarNo = Request.Form["CarNo"].ToString();
            string CarPhone = Request.Form["CarPhone"].ToString();
            string StdNo = Request.Form["StdNo"].ToString();
            string dataString = Request.Form["dataList"].ToString();
            Boolean stat = false;
            int t1 = 0;
            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.203.3;User ID=sa;Password=lan@2mail");
            con.Open();
            SqlConnection con1 = new SqlConnection("Data Source=192.168.75.250;User ID=sa;Password=kmtSoft12345678");
            con1.Open();


            JArray jsonObj = JArray.Parse(dataString);
            foreach (JObject jObject in jsonObj)
            {
                
                string strPdid = System.Guid.NewGuid().ToString();
              
                strSql = "insert into [DDdatabase].[dbo].[Tab_KocelApp_Kmr_CheckInList] ";
                strSql += " VALUES('" + strPdid + "','" + jObject["purOrder"] + "','" + jObject["itemOrder"] + "','" + jObject["matName"] + "','" + jObject["matCode"] + "','" + jObject["matUnit"] + "','" + jObject["supplyName"] + "','" + jObject["supplyCode"] + "','" + jObject["companyName"] + "','" + jObject["companyCode"] + "','" + jObject["companyShortName"] + "','" + jObject["facName"] + "','" + jObject["facCode"] + "','" + jObject["stockName"] + "','" + jObject["stockCode"] + "','" + jObject["purNum"] + "','" + jObject["checkNum"] + "','" + jObject["pnum"] + "','" + jObject["adate"] + "','" + jObject["pdate"] + "','"+strCompany+"','"+strDept+"','"+strVen+"','"+ strName + "','0','','" + jObject["pmz"] + "','" + jObject["ppz"] + "','" + jObject["pkz"] + "','" + jObject["pkbz"] + "','" + CarNo + "','" + StdNo + "')";
              

                SqlCommand cmd = new SqlCommand(strSql, con);
                t1 = cmd.ExecuteNonQuery();
                if (t1 != 0)
                {
                    stat = true;
                }

                string execSql = "exec [CMTWeight].[dbo].[proc_UpdateStand] @CarNo='" + CarNo + "',@UnitFrom ='" + jObject["supplyName"] + "',@Proname ='" + jObject["matName"] + "',@ProModel ='',@UnitTo ='" + jObject["companyName"] + "',@OrderNo ='" + jObject["purOrder"] + "',@Deduct =" + jObject["pkz"] + ",@Deduct2 =" + jObject["pkbz"] + "   ";

                SqlCommand cmd1 = new SqlCommand(execSql, con1);
                t1 = cmd1.ExecuteNonQuery();
                if (t1 != 0)
                {
                    stat = true;
                }
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

    }
}