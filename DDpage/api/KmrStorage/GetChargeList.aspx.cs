using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.KmrStorage
{
    public partial class GetChargeList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strOrder = Request.Form["strOrder"];
            string strMatCode = Request.Form["strMatCode"];
            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();

            strSql = " select distinct t2.CHARG from( select distinct EBELN, EBELP FROM[DataFromSap].[dbo].[T_SAP_DM_PSekpo] where EBELN = '" + strOrder + "' and MATNR = '" + strMatCode + "'";
            strSql += " ) t1 left join( select distinct  CHARG, EBELN, EBELP from[DataFromSap].[dbo].[T_SAP_DM_PSekbe] ) t2 on t1.EBELN = t2.EBELN and t1.EBELP = t2.EBELP";
            strSql += " where len(t2.CHARG)> 0";
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            string strJson = "{\"data\":[]";
            if (sdr.HasRows)
            {
                strJson = "{\"data\":[";
            }
            while (sdr.Read())
            {

                strJson += "{\"charg\":\"" + sdr[0].ToString() + "\"},";

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