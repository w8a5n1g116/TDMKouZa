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
    public partial class GetCheckInFac_BL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string strCompany = Request.Form["comName"];
            string strJson = "{\"data\":[{\"facCode\":\"1071\",\"facName\":\"冰轮铸造工厂\"}]}";

            //SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            //con.Open();
            //strSql = " select distinct [工厂代码],[工厂名称] FROM [DataFromSap].[dbo].[View_SAP_SapEhrComStock] where 1=1 ";
            //strSql += " and [公司简称] like '%" + strCompany + "%'";

            //SqlCommand cmd = new SqlCommand(strSql, con);
            //SqlDataReader sdr = cmd.ExecuteReader();
            //string strJson = "{\"data\":[]";
            //if (sdr.HasRows)
            //{
            //    strJson = "{\"data\":[";
            //}
            //while (sdr.Read())
            //{

            //    strJson += "{\"facCode\":\"" + sdr[0].ToString() + "\",";
            //    strJson += "\"facName\":\"" + sdr[1].ToString() + "\"},";

            //}
            //strJson = strJson.Substring(0, strJson.Length - 1);
            //strJson = strJson + "]}";
            //sdr.Close();
            //con.Close();



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