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
    public partial class GetTastQueryList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strSql = "";
            string strCompany = Request.Form["company"];
            string strDept = Request.Form["dept"];
            string strName = Request.Form["name"];
            string strPhone = Request.Form["phone"];

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=TDM;User ID=tide;Password=lan@2mail");
            con.Open();



            strSql = "SELECT [PID],[FCompany],[FYear],[FMonth],[OneCompany] ,[OneDept],[OneName],[OneTime],[stat]   ";
            strSql += " FROM [DDdatabase].[dbo].[Tab_KWA_AuditList] where 1=1 ";
            strSql += " and [FCompany] in (select distinct([flowCompany]) from [DDdatabase].[dbo].[Tab_KWA_Flow] where FPhone='" + strPhone + "')";


            strSql += " order by [OneTime] desc";


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

                strJson += "{\"Pid\":\"" + sdr[0].ToString() + "\",";
                strJson += "\"FCompany\":\"" + sdr[1].ToString() + "\",";
                strJson += "\"FYear\":\"" + sdr[2].ToString() + "\",";
                strJson += "\"FMonth\":\"" + sdr[3].ToString() + "\",";
                strJson += "\"OneCompany\":\"" + sdr[4].ToString() + "\",";
                strJson += "\"OneDept\":\"" + sdr[5].ToString() + "\",";
                strJson += "\"OneName\":\"" + sdr[6].ToString() + "\",";
                strJson += "\"OneTime\":\"" + sdr[7].ToString() + "\",";
                strJson += "\"stat\":\"" + sdr[8].ToString() + "\"},";

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
