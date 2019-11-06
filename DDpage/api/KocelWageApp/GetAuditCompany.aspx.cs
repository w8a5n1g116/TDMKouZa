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
    public partial class GetAuditCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strID = Request.Form["id"];
            string strSql = "";
            string strCompany = "";
            string strYear = "";
            string strMonth = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=TDM;User ID=tide;Password=lan@2mail");
            con.Open();



            strSql = "SELECT [FCompany],[staffItem] ,[wagesItem] ,[countItem],[amount],[FYear],[FMonth]  ";
            strSql += " FROM [DDdatabase].[dbo].[Tab_KWA_CompanyAuditList] where 1=1 ";
            strSql += " and [PID]='" + strID + "'";
            strSql += " order by showid ";


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
                strJson += "\"staffItem\":\"" + sdr[1].ToString() + "\",";
                strJson += "\"wagesItem\":\"" + sdr[2].ToString() + "\",";
                strJson += "\"countItem\":\"" + sdr[3].ToString() + "\",";
                strJson += "\"amount\":\"" + sdr[4].ToString() + "\"},";

                strCompany = sdr[0].ToString();
                strYear = sdr[5].ToString();
                strMonth = sdr[6].ToString();

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
