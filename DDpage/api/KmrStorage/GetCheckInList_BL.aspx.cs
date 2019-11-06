using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.KmrStorage
{
    public partial class GetCheckInList_BL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCompany = Request.Form["Company"];
            string strDept = Request.Form["Dept"];
            string strVen = Request.Form["Ven"];
            string strName = Request.Form["Name"];
            string strOrder = Request.Form["Order"];
            string strSupply = Request.Form["Supply"];
            string strMatName = Request.Form["matName"];
            string strSDate = Request.Form["firDate"];
            string strEDate = Request.Form["secDate"];


            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.197.101;User ID=sa;Password=2018Sql101");
            con.Open();
            strSql = " SELECT [purOrder] ,[matName] ,[matCode],[matUnit],[supplyName] ,[comName]  ,[facName]  ,[stockName] ,[purNum] ,[finNum] ,[checkNum],[appDate] ,[proDate],[FCompany] ,[FDept],[FVen] ,[FName],[stat],[ifSap],[pickItemType],[pickInfo] FROM [DDdatabase].[dbo].[View_KocelApp_Kmr_CheckInList] where 1 = 1 ";
            //strSql += " and comShortName like '%" + strCompany + "%'";
            strSql += " and (purOrder like '%" + strOrder + "%'";
            strSql += " or supplyName like '%" + strSupply + "%'";
            strSql += " or matName like '%" + strMatName + "%') ";

            if (strSDate.Length > 0)
            {
                strSql += " and [appDate] >='" + strSDate + "'";
            }

            if (strEDate.Length > 0)
            {
                strSql += " and [appDate] <='" + strEDate + "'";
            }

            strSql += " order by [appDate] desc ";
            //库管员权限
            //strSql += "and DeName like '%" + strDeName + "%'";

            //库管员权限

            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            string strJson = "{\"data\":[]";
            if (sdr.HasRows)
            {
                strJson = "{\"data\":[";
            }
            while (sdr.Read())
            {

                strJson += "{\"purchaseOrder\":\"" + sdr[0].ToString() + "\",";
                strJson += "\"matName\":\"" + StringToJson(sdr[1].ToString()) + "\",";
                strJson += "\"matCode\":\"" + sdr[2].ToString() + "\",";
                strJson += "\"matUnit\":\"" + sdr[3].ToString() + "\",";
                strJson += "\"supplyName\":\"" + sdr[4].ToString() + "\",";
                strJson += "\"companyName\":\"" + sdr[5].ToString() + "\",";
                strJson += "\"facName\":\"" + sdr[6].ToString() + "\",";
                strJson += "\"stockName\":\"" + sdr[7].ToString() + "\",";
                strJson += "\"purchaseNum\":\"" + sdr[8].ToString() + "\",";
                strJson += "\"finNum\":\"" + sdr[9].ToString() + "\",";
                strJson += "\"checkNum\":\"" + sdr[10].ToString() + "\",";
                strJson += "\"aDate\":\"" + Convert.ToDateTime(sdr[11]).ToShortDateString()+ "\",";
                strJson += "\"pDate\":\"" + Convert.ToDateTime(sdr[12]).ToShortDateString() + "\",";
                strJson += "\"FName\":\"" + sdr[13].ToString() + "\",";
                strJson += "\"ifSap\":\"" + sdr[18].ToString() + "\",";
                strJson += "\"pickItemType\":\"" + sdr[19].ToString() + "\",";
                strJson += "\"pickInfo\":\"" + sdr[20].ToString() + "\"},";


            }
            strJson = strJson.Substring(0, strJson.Length - 1);
            strJson = strJson + "]}";

            sdr.Close();
            con.Close();
            Response.Write(strJson);
        }

        public static String StringToJson(String s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\"");
                        break;
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '/':
                        sb.Append("\\/");
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString();
        }

    }
}