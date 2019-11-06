using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.KocelVI
{
    public partial class GetTastList : System.Web.UI.Page
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

            strSql = " select ID,bh,hmark,inputCom, inputDept, inputPer, inputDate";
            strSql += " from [DDdatabase].[dbo].[Tab_KVI_ApplyList] where hmark < 3 ";
            strSql += " and ID in (select [PID] from [DDdatabase].[dbo].[Tab_KVI_AuditList] where [FPhone]='" + strPhone + "' and hmark = 0)";
            strSql += " order by inputDate";

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
                strJson += "\"inpBh\":\"" + sdr[1].ToString() + "\",";
                strJson += "\"inpHmark\":\"" + sdr[2].ToString() + "\",";
                strJson += "\"inpCom\":\"" + sdr[3].ToString() + "\",";
                strJson += "\"inpDept\":\"" + sdr[4].ToString() + "\",";
                strJson += "\"inpPer\":\"" + sdr[5].ToString() + "\",";
                strJson += "\"inpDate\":\"" + Convert.ToDateTime(sdr[6]).ToString("yyyy-MM-dd") + "\"},";

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
