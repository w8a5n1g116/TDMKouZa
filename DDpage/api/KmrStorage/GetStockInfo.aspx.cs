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
    public partial class GetStockInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCompany = Request.Form["stCompany"];
            string strDept = Request.Form["stDept"];
            string strVen = Request.Form["stVen"];
            //string strOrder = Request.Form["strOrder"];
            //string strMatName = Request.Form["stMatName"];
            string strStName = Request.Form["stStName"];
            string strSName = Request.Form["stName"];
            
            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();
            strSql = "select distinct top 30  [PCompany],[PDept],[PVenture],[MStock],[SName],[PNum] FROM [DDdatabase].[dbo].[View_KocelApp_Kmr_StockListInfo] where PCompany like '%"+strCompany+"%' and PDept like '%"+strDept+ "%' and PVenture like '%"+strVen+ "%' and MStock like '%"+strStName+"%' ";

            //库管员权限
            //strSql += "and SName like '%" + strSName + "%'";

            //库管员权限

            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            string strJson = "{\"data\":[]";
            if (sdr.HasRows)
            {
                strJson = "{\"data\":[";
            }
            while (sdr.Read())
            {

                strJson += "{\"PCompany\":\"" + sdr[0].ToString() + "\",";
                strJson += "\"PDept\":\"" + sdr[1].ToString() + "\",";
                strJson += "\"PVenture\":\"" + sdr[2].ToString() + "\",";
                strJson += "\"MStock\":\"" + sdr[3].ToString() + "\",";
                strJson += "\"SName\":\"" + sdr[4].ToString() + "\",";
                strJson += "\"PNum\":\"" + sdr[5].ToString() + "\"},";

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