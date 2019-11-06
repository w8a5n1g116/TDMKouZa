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
    public partial class GetDetailTwoStat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strSql = "";
            string strID = Request.Form["strid"];
            string stat = "1";


            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=TDM;User ID=tide;Password=lan@2mail");
            con.Open();

            strSql = " select * from [DDdatabase].[dbo].[Tab_KVI_ApplyList] where 1=1";
            strSql += " and ID='" + strID + "' and hmark = 1";

            SqlCommand cmd01 = new SqlCommand(strSql, con);
            SqlDataReader sdr01 = cmd01.ExecuteReader();

            if (!sdr01.HasRows)
            {
                stat = "0";
            }

            sdr01.Close();

            strSql = "SELECT [ID],[bh],[inputCom],[inputDept],[inputVen],[inputPer],[inputDate],[inputType],[inputContent],[inPhotoUrlOne],[inPhotoUrlTwo],[inPhotoUrlTh] ";
            strSql += " FROM [DDdatabase].[dbo].[Tab_KVI_ApplyList] where 1=1 ";
            strSql += " and [ID] = '" + strID + "' ";
            strSql += " and [hmark] = '1' ";

            SqlCommand cmd02 = new SqlCommand(strSql, con);
            SqlDataReader sdr02 = cmd02.ExecuteReader();
            string strJson = "{\"stat\":\"" + stat + "\",\"data\":[]";
            if (sdr02.HasRows)
            {
                strJson = "{\"stat\":\"" + stat + "\",\"data\":[";
            }
            while (sdr02.Read())
            {

                strJson += "{\"ID\":\"" + sdr02[0].ToString() + "\",";
                strJson += "\"bh\":\"" + sdr02[1].ToString() + "\",";
                strJson += "\"com\":\"" + sdr02[2].ToString() + "\",";
                strJson += "\"dept\":\"" + sdr02[3].ToString() + "\",";
                strJson += "\"ven\":\"" + sdr02[4].ToString() + "\",";
                strJson += "\"per\":\"" + sdr02[5].ToString() + "\",";
                strJson += "\"inDate\":\"" + sdr02[6].ToString() + "\",";
                strJson += "\"inType\":\"" + sdr02[7].ToString() + "\",";
                strJson += "\"inContent\":\"" + sdr02[8].ToString() + "\",";
                strJson += "\"inPhotoUrlOne\":\"" + sdr02[9].ToString() + "\",";
                strJson += "\"inPhotoUrlTwo\":\"" + sdr02[10].ToString() + "\",";
                strJson += "\"inPhotoUrlTh\":\"" + sdr02[11].ToString() + "\"},";
            }
            strJson = strJson.Substring(0, strJson.Length - 1);
            strJson = strJson + "]}";

            sdr02.Close();
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
