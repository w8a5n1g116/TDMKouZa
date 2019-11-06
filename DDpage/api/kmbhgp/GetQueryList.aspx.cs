using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.kmbhgp
{
    public partial class GetQueryList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCompany = Request.Form["Company"];
            string strDept = Request.Form["Dept"];
            string strVen = Request.Form["Ven"];
            string strName = Request.Form["Name"];
            string strPhone = Request.Form["phone"];

            string strGk = Request.Form["gk"];
            string strCpmc = Request.Form["cpmc"];
            string strCharge = Request.Form["charge"];



            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.0.243;Initial Catalog=TIDE;User ID=sa;Password=lan@2mail");
            con.Open();
            strSql = " select top(30) ID,jh,gk,cpmc,qxlx,qxwz,fxrq,fxdd,fxr,zt from [TIDE].[dbo].[Tab_Reject_xxb] where 1=1  ";
            strSql += " and gs like '%" + strCompany + "%'";
            strSql += " and gk like '%" + strGk + "%'";
            strSql += " and cpmc like '%" + strCpmc + "%'";
            strSql += " and jh like '%" + strCharge + "%'";
            strSql += " order by fxrq desc ";

            //库管员权限
            //strSql += "and DeName like '%" + strDeName + "%'";

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

                strJson += "{\"ID\":\"" + sdr[0].ToString() + "\",";
                strJson += "\"jh\":\"" + sdr[1].ToString() + "\",";
                strJson += "\"gk\":\"" + sdr[2].ToString() + "\",";
                strJson += "\"cpmc\":\"" + StringToJson(sdr[3].ToString()) + "\",";
                strJson += "\"qxlx\":\"" + sdr[4].ToString() + "\",";
                strJson += "\"qxwz\":\"" + sdr[5].ToString() + "\",";
                strJson += "\"fxrq\":\"" + Convert.ToDateTime(sdr[6]).ToShortDateString() + "\",";
                strJson += "\"fxdd\":\"" + sdr[7].ToString() + "\",";
                strJson += "\"fxr\":\"" + sdr[8].ToString() + "\",";
                strJson += "\"zt\":\"" + sdr[9].ToString() + "\"},";

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