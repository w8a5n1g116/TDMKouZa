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
    public partial class GetDeQueryInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCompany = Request.Form["deCompany"];
            string strDept = Request.Form["deDept"];
            string strVen = Request.Form["deVen"];
            //string strOrder = Request.Form["strOrder"];
            //string strMatName = Request.Form["stMatName"];
            string strStName = Request.Form["deStName"];
            string strSName = Request.Form["deName"];

            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();



            strSql = "SELECT top 30 [PickID],[PCompany] ,[PDept],[PVenture] ,[PName],[PTime] ,[PStat] ,[PLinkID],[typeNum] ";
            strSql += " FROM [DDdatabase].[dbo].[View_KocelApp_Kmr_DeQueryInfo] where 1=1 ";
            strSql += " and PCompany like '%" + strCompany + "%'";
            strSql += " and PDept like '%" + strDept + "%'";
            strSql += " and PVenture like '%" + strVen + "%'";
            //strSql += " and MStock like '%" + strStName + "%'";
            strSql += " order by PTime desc";


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

                strJson += "{\"PickID\":\"" + sdr[0].ToString() + "\",";
                strJson += "\"PCompany\":\"" + sdr[1].ToString() + "\",";
                strJson += "\"PDept\":\"" + sdr[2].ToString() + "\",";
                strJson += "\"PVenture\":\"" + sdr[3].ToString() + "\",";
                strJson += "\"PName\":\"" + sdr[4].ToString() + "\",";
                strJson += "\"PTime\":\"" + sdr[5].ToString() + "\",";
                strJson += "\"PStat\":\"" + sdr[6].ToString() + "\",";
                strJson += "\"PLinkID\":\"" + sdr[7].ToString() + "\",";
                strJson += "\"PNum\":\"" + sdr[8].ToString() + "\"},";

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
