using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.KmrStorage
{
    public partial class MaterialStockMoveInfoSql : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string MatreialCode = Request.Form["MatreialCode"].ToString();
            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT [FmaterialCode],[FLocationStart],[FLocationEnd],[FKeeper],[EditDate],[MAmount]   FROM [DDdatabase].[dbo].[Tab_KocelApp_Kmr_StockMove] where FmaterialCode like '%" + MatreialCode + "%'", con);
            SqlDataReader sdr = cmd.ExecuteReader();
            string strJson = "{\"data\":[]";
            if (sdr.HasRows)
            {
                strJson = "{\"data\":[";
            }
            while (sdr.Read())
            {

                strJson += "{\"FmaterialCode\":\"" + StringToJson(sdr[0].ToString()) + "\",";
                strJson += "\"FLocationStart\":\"" + StringToJson(sdr[1].ToString()) + "\",";
                strJson += "\"FLocationEnd\":\"" + StringToJson(sdr[2].ToString()) + "\",";
                strJson += "\"FKeeper\":\"" + StringToJson(sdr[3].ToString()) + "\",";
                strJson += "\"EditDate\":\"" + StringToJson(sdr[4].ToString()) + "\",";
                strJson += "\"MAmount\":\"" + StringToJson(sdr[5].ToString()) + "\"},";

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