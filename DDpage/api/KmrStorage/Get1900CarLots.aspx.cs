using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.KmrStorage
{
    public partial class Get1900CarLots : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string StdNo = Request.Form["StdNo"];

            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.75.250;Initial Catalog=CMTWeight;User ID=sa;Password=kmtSoft12345678");//
            con.Open();
            strSql = " SELECT [F_StdNo],[F_Net],[F_Gross],[F_Tare] FROM [dbo].[T_Standard] where F_IsCancel = 0 and F_StdNo =  '" + StdNo + "'"; //F_IsFinish = 0 and 

            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            string strJson = "{\"data\":[]";
            if (sdr.HasRows)
            {
                strJson = "{\"data\":[ ";
            }
            while (sdr.Read())
            {

                strJson += "{\"F_StdNo\":\"" + sdr[0].ToString() + "\",";
                strJson += "\"F_Net\":\"" + sdr[1].ToString() + "\",";
                strJson += "\"F_Gross\":\"" + sdr[2].ToString() + "\",";
                strJson += "\"F_Tare\":\"" + sdr[3].ToString() + "\"},";

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