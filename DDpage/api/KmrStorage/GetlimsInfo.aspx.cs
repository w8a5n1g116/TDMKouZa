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
    public partial class GetlimsInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCode = Request.Form["mCode"];
            

            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();
            strSql = "select distinct top 30  [CHARG] ,[SUPPLIER],[MAKTX],[MATNR] ,[CHECKITEM] ,[REALVALUE] ,[STANDARDVALUE] ,[SINGLE] ,[ENTRUSTDATE],[REPORTNO] FROM [DDdatabase].[dbo].[Tab_SAP_LimsReport]  where MATNR like '%" + strCode + "%' ";
            strSql += " and ENTRUSTDATE = (select MAX(ENTRUSTDATE) from [DDdatabase].[dbo].[Tab_SAP_LimsReport]   where MATNR like '%" + strCode + "%')";
            //strSql += " order by [ENTRUSTDATE] desc";


            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            string strJson = "{\"data\":[]";
            if (sdr.HasRows)
            {
                strJson = "{\"data\":[";
            }
            while (sdr.Read())
            {

                strJson += "{\"charge\":\"" + sdr[0].ToString() + "\",";
                strJson += "\"supplier\":\"" + sdr[1].ToString() + "\",";
                strJson += "\"maktx\":\"" + StringToJson(sdr[2].ToString()) + "\",";
                strJson += "\"matnr\":\"" + sdr[3].ToString() + "\",";
                strJson += "\"checktiem\":\"" + sdr[4].ToString() + "\",";
                strJson += "\"realvalue\":\"" + sdr[5].ToString() + "\",";
                strJson += "\"standardvalue\":\"" + sdr[6].ToString() + "\",";
                strJson += "\"single\":\"" + sdr[7].ToString() + "\",";
                strJson += "\"edate\":\"" + sdr[8].ToString() + "\",";
                strJson += "\"limsCharge\":\"" + sdr[9].ToString() + "\"},";

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
