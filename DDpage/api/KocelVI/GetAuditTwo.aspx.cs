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
    public partial class GetAuditTwo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string strID = Request.Form["strid"];
            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=TDM;User ID=tide;Password=lan@2mail");
            con.Open();

            strSql = " select FCom, FDept, FPer, AuditDate, AuditContent ,shmc,FinDate,AuditPhotoUrl,hmark ";
            strSql += " FROM [DDdatabase].[dbo].[Tab_KVI_AuditList] where  hmark = 1 ";
            strSql += " and PID = '" + strID + "'";
            strSql += " order by AuditDate  ";



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

                strJson += "{\"reCom\":\"" + sdr[0].ToString() + "\",";
                strJson += "\"reDept\":\"" + sdr[1].ToString() + "\",";
                strJson += "\"rePer\":\"" + sdr[2].ToString() + "\",";
                strJson += "\"reDate\":\"" + Convert.ToDateTime(sdr[3]).ToString("yyyy-MM-dd") + "\",";
                strJson += "\"reContent\":\"" + StringToJson(sdr[4].ToString()) + "\",";
                strJson += "\"reShmc\":\"" + sdr[5].ToString() + "\",";
                if (sdr[6].ToString().Length > 0)
                {
                    strJson += "\"reFDate\":\"" + Convert.ToDateTime(sdr[6]).ToString("yyyy-MM-dd") + "\",";
                }
                else
                {
                    strJson += "\"reFDate\":\"" + sdr[6].ToString() + "\",";
                }
                
                strJson += "\"rePhotoUrl\":\"" + sdr[7].ToString() + "\",";
                strJson += "\"reHmark\":\"" + sdr[8].ToString() + "\"},";

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
