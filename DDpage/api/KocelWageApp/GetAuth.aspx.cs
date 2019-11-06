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
    public partial class GetAuth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strPhone = Request.Form["phone"];

            string strSql = "";
            string strStat = "0";
            string strRole = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=TDM;User ID=tide;Password=lan@2mail");
            con.Open();



            strSql = "SELECT Max([Role]) ";
            strSql += " FROM [DDdatabase].[dbo].[Tab_KWA_Flow] where 1=1 ";
            strSql += " and [FPhone] = '" + strPhone + "' ";


            //库管员查询权限
            //strSql += " and DeName like '%" + strSName + "%'";

            //库管员查询权限

            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            string strJson = "{\"stat\":";
            if (sdr.HasRows)
            {
                strStat = "1";
            }
            strJson = "{\"stat\":\""+strStat+"\",";
            while (sdr.Read())
            {

                strRole = sdr[0].ToString();

            }

            strJson = strJson + "\"role\":\"" + strRole + "\"}";

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
