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
    public partial class GetResCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strType ="";
            string strID = Request.Form["ID"];
            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DDdatabase;User ID=tide;Password=lan@2mail");
            con.Open();


            strSql = "select [inputType] from [DDdatabase].[dbo].[Tab_KVI_ApplyList] where ID='" + strID + "'";

            SqlCommand cmdType = new SqlCommand(strSql, con);
            SqlDataReader sdrType = cmdType.ExecuteReader();
            while (sdrType.Read())
            {

                strType = sdrType[0].ToString();

            }
            sdrType.Close();


            strSql = "SELECT distinct(FCompany)   ";
            strSql += " FROM [DDdatabase].[dbo].[Tab_KVI_AuthSet] where [Role]='1' and FType='" + strType + "'";
            strSql += " order by [FCompany]  ";



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

                strJson += "{\"company\":\"" + sdr[0].ToString() + "\"},";

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
