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
    public partial class GetAuditFlow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strID = Request.Form["id"];
            string strCompany = "";
            string strJson = "";
            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=TDM;User ID=tide;Password=lan@2mail");
            con.Open();



            strSql = "SELECT [OneName] ,[OneTime],[TwoName],[TwoTime],[ThName],[ThTime],[FRemark],[SRemark],[TRemark],[ID] ";
            strSql += " FROM [DDdatabase].[dbo].[Tab_KWA_AuditList] where 1=1 ";
            //strSql += " and [FCompany]='共享集团'";
            strSql += " and [PID]='" + strID + "'";



            //库管员查询权限
            //strSql += " and DeName like '%" + strSName + "%'";

            //库管员查询权限


            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            strJson = "{\"data\":[]";
            if (sdr.HasRows)
            {
                strJson = "{\"data\":[";
            }
            while (sdr.Read())
            {

                strJson += "{\"oneName\":\"" + sdr[0].ToString() + "\",";
                strJson += "\"oneTime\":\"" + sdr[1].ToString() + "\",";
                strJson += "\"twoName\":\"" + sdr[2].ToString() + "\",";
                strJson += "\"twoTime\":\"" + sdr[3].ToString() + "\",";
                strJson += "\"thName\":\"" + sdr[4].ToString() + "\",";
                strJson += "\"thTime\":\"" + sdr[5].ToString() + "\",";
                strJson += "\"fRemark\":\"" + sdr[6].ToString() + "\",";
                strJson += "\"sRemark\":\"" + sdr[7].ToString() + "\",";
                strJson += "\"tRemark\":\"" + sdr[8].ToString() + "\",";
                strJson += "\"id\":\"" + sdr[9].ToString() + "\"},";

            }
            strJson = strJson.Substring(0, strJson.Length - 1);
            strJson = strJson + "]";
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
