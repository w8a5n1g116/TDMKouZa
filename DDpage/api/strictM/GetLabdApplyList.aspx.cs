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
    public partial class GetLabdApplyList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strNo = Request.Form["personNo"];
            //strNo = "";

            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=BPMS;User ID=tide;Password=lan@2mail");
            con.Open();


            strSql = " select [ID],[category] ,[penaltyClause],[details],[photoUrl],cast([assessDate] as date), ";
            strSql += " [resCompany],[resDept] ,[resVen],[resVenAssess],[resVenAssessUnit],";
            strSql += " [resPerson],[resPersonAssess],[resPersonAssessUnit]";
            strSql += " FROM [BPMS].[dbo].[Tab_labDiscipline_list] where 1=1 ";
            strSql += " and inputPerson   like  '%" + strNo + "%'";
            strSql += " and hmark = 1 ";
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

                strJson += "{\"ID\":\"" + sdr[0].ToString() + "\",";
                strJson += "\"type\":\"" + sdr[1].ToString() + "\",";
                strJson += "\"item\":\"" + sdr[2].ToString() + "\",";
                strJson += "\"content\":\"" + sdr[3].ToString() + "\",";
                strJson += "\"photoUrl\":\"" + sdr[4].ToString() + "\",";
                strJson += "\"pdate\":\"" + sdr[5].ToString() + "\",";
                strJson += "\"com\":\"" + sdr[6].ToString() + "\",";
                strJson += "\"dept\":\"" + sdr[7].ToString() + "\",";
                strJson += "\"ven\":\"" + sdr[8].ToString() + "\",";
                strJson += "\"venAss\":\"" + sdr[9].ToString() + sdr[10].ToString() + "\",";
                strJson += "\"per\":\"" + sdr[11].ToString() + "\",";
                strJson += "\"perAss\":\"" + sdr[12].ToString() + sdr[13].ToString() + "\"},";

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