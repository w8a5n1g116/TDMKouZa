using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.KocelWageApp
{
    public partial class GetDeptSalaryInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strTmp = Request.Form["month"];
            string[] strArray = strTmp.Split('-');
            string strYear = strArray[0];
            string strMonth = strArray[1];
            string strCompany = Request.Form["company"];
            string strStaff = Request.Form["staff"];
            string strWages = Request.Form["wages"];
            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=TDM;User ID=tide;Password=lan@2mail");
            con.Open();



            strSql = "SELECT [FCompany],[FDept],[staffItem] ,[wagesItem] ,[countItem],[amount]  ";
            strSql += " FROM [DDdatabase].[dbo].[Tab_KWA_DeptCostList] where 1=1 ";
            //strSql += " and [FCompany]='共享集团'";
            strSql += " and [FCompany]='" + strCompany + "'";
            strSql += " and [FYear]='" + strYear + "'";
            strSql += " and [FMonth]='" + strMonth + "'";
            strSql += " and [staffItem]='" + strStaff + "'";
            strSql += " and [wagesItem]='" + strWages + "'";
            strSql += " order by FCompany,FDept,staffItem,wagesItem,showid  ";


            //库管员查询权限
            //strSql += " and DeName like '%" + strSName + "%'";

            //库管员查询权限


            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            string strJson = "{\"data\":[]";
            if (sdr.HasRows)
            {
                strJson = "{\"data\":[";
            }
            while (sdr.Read())
            {

                strJson += "{\"FCompany\":\"" + sdr[0].ToString() + "\",";
                strJson += "\"FDept\":\"" + sdr[1].ToString() + "\",";
                strJson += "\"staffItem\":\"" + sdr[2].ToString() + "\",";
                strJson += "\"wagesItem\":\"" + sdr[3].ToString() + "\",";
                strJson += "\"countItem\":\"" + sdr[4].ToString() + "\",";
                strJson += "\"amount\":\"" + sdr[5].ToString() + "\"},";

            }
            strJson = strJson.Substring(0, strJson.Length - 1);
            strJson = strJson + "]";
            strJson = strJson + ",\"FCompany\":\"" + strCompany + "\"";
            strJson = strJson + ",\"FYear\":\"" + strYear + "\"";
            strJson = strJson + ",\"FMonth\":\"" + strMonth + "\"";
            strJson = strJson + "}";

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
