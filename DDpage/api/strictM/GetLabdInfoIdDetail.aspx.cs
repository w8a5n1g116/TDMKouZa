using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.strictM
{
    public partial class GetLabdInfoIdDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strID = Request.Form["ID"];
            //strNo = "";

            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=BPMS;User ID=tide;Password=lan@2mail");
            con.Open();


            strSql = " select [category] ,[penaltyClause],[details],[photoUrl],[assessDate], ";
            strSql += " [resCompany],[resDept] ,[resVen],[resVenAssess],[resVenAssessUnit],";
            strSql += " [resPerson],[resPersonAssess],[resPersonAssessUnit]";
            strSql += " FROM [BPMS].[dbo].[Tab_labDiscipline_list] where 1=1 ";
            strSql += " and ID  = '" + strID + "'";
            strSql += " order by [assessDate] desc";
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            string strJson = "{\"data\":[]";
            if (sdr.HasRows)
            {
                strJson = "{\"data\":[";
            }
            while (sdr.Read())
            {

                strJson += "{\"type\":\"" + sdr[0].ToString() + "\",";
                strJson += "\"item\":\"" + sdr[1].ToString() + "\",";
                strJson += "\"content\":\"" + sdr[2].ToString() + "\",";
                strJson += "\"photoUrl\":\"" + sdr[3].ToString() + "\",";
                strJson += "\"pdate\":\"" + sdr[4].ToString() + "\",";
                strJson += "\"com\":\"" + sdr[5].ToString() + "\",";
                strJson += "\"dept\":\"" + sdr[6].ToString() + "\",";
                strJson += "\"ven\":\"" + sdr[7].ToString() + "\",";
                strJson += "\"venAss\":\"" + sdr[8].ToString() + sdr[9].ToString() + "\",";
                strJson += "\"per\":\"" + sdr[10].ToString() + "\",";
                strJson += "\"perAss\":\"" + sdr[11].ToString() + sdr[12].ToString() + "\"},";

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