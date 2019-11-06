using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.ITServer
{
    public partial class GetTastList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCompany = Request.Form["company"];
            string strDept = Request.Form["dept"];
            string strVen = Request.Form["ven"];
            string strName = Request.Form["name"];

            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=TDM;User ID=tide;Password=lan@2mail");
            con.Open();



            strSql = "select top(30) [primID],[companyName],[deptName],[bxrName],[startTime],[phone] ";
            strSql += " from [TDM].[dbo].[Maintenance_Plan] where 1=1 ";
            strSql += " and zrrHbTime  is null ";
            strSql += " and whzrr like '%" + strName + "%'";
            strSql += " order by startTime  desc";


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

                strJson += "{\"ID\":\"" + sdr[0].ToString() + "\",";
                strJson += "\"ACompany\":\"" + sdr[1].ToString() + "\",";
                strJson += "\"ADept\":\"" + sdr[2].ToString() + "\",";
                strJson += "\"AName\":\"" + sdr[3].ToString() + "\",";
                strJson += "\"ATime\":\"" + sdr[4].ToString() + "\",";
                strJson += "\"APhone\":\"" + sdr[5].ToString() + "\"},";

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
