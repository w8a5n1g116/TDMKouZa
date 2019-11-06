using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;

namespace DDpage.api.ITServer
{
    public partial class GetTaskDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strID = Request.Form["strid"];

            string strSql = "";
            string stat = "1";

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=TDM;User ID=tide;Password=lan@2mail");
            con.Open();



            strSql = "SELECT [companyName],[deptName],[bxrName],[gzxx],[gzlx],[softType] ,[softName] ,[phone],[startTime],[Drawing],[zrrHbTime] ";
            strSql += " FROM [TDM].[dbo].[Maintenance_Plan] where 1=1 ";
            strSql += " and primID = '" + strID + "' ";


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

                strJson += "{\"company\":\"" + sdr[0].ToString() + "\",";
                strJson += "\"dept\":\"" + sdr[1].ToString() + "\",";
                strJson += "\"name\":\"" + sdr[2].ToString() + "\",";
                strJson += "\"gzxx\":\"" + StringToJson(sdr[3].ToString()) + "\",";
                strJson += "\"gzlx\":\"" + sdr[4].ToString() + "\",";
                strJson += "\"softType\":\"" + sdr[5].ToString() + "\",";
                strJson += "\"softName\":\"" + sdr[6].ToString() + "\",";
                strJson += "\"phone\":\"" + sdr[7].ToString() + "\",";
                strJson += "\"startTime\":\"" + sdr[8].ToString() + "\",";
                strJson += "\"url\":\"" + sdr[9].ToString() + "\"},";
                if (sdr[10].ToString().Length > 0)
                {
                    stat = "0";
                }
            }
            strJson = strJson.Substring(0, strJson.Length - 1);
            strJson = strJson + "]";

            strJson = strJson + ",\"stat\":\""+ stat + "\"}";

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
