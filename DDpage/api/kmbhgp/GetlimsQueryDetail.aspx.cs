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
    public partial class GetlimsQueryDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strId = Request.Form["ID"];

            string strSql = "";
            string strStat = "1";

            SqlConnection con = new SqlConnection("Data Source=192.168.0.243;Initial Catalog=TIDE;User ID=sa;Password=lan@2mail");
            con.Open();

            strSql = " select * from Tab_Reject_lims_shxxb where ID='"+ strId + "' and shmc in ('一级审核') and hmark=0";
            SqlCommand cmdstat = new SqlCommand(strSql, con);
            SqlDataReader sdrstat = cmdstat.ExecuteReader();
            if (!sdrstat.HasRows)
            {
                strStat = "0";
            }
            sdrstat.Close();


            strSql = " select  [gk],[ggxh],[th] ,[cpmc],[dz] ,[cz] ,[fl],[jd],[cpdm] ,[jh] ,[fxrq] ,[scrq],[fxdd],[sl],[xz]  ,[qxlx],[qxwz] ,[qxms] ,[fxr]  ,[zt] from [TIDE].[dbo].[Tab_Reject_lims_xxb] where 1=1  ";
            strSql += " and ID = '" + strId + "'";
            strSql += " order by fxrq desc ";

            //库管员权限
            //strSql += "and DeName like '%" + strDeName + "%'";

            //库管员权限

            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            string strHead = "\"head\":[]";
            if (sdr.HasRows)
            {
                strHead = "\"head\":[";
            }
            while (sdr.Read())
            {
                strHead += "{\"gk\":\"" + sdr[0].ToString() + "\",";
                strHead += "\"ggxh\":\"" + sdr[1].ToString() + "\",";
                strHead += "\"th\":\"" + sdr[2].ToString() + "\",";
                strHead += "\"cpmc\":\"" + StringToJson(sdr[3].ToString()) + "\",";
                strHead += "\"dz\":\"" + sdr[4].ToString() + "\",";
                strHead += "\"cz\":\"" + sdr[5].ToString() + "\",";
                strHead += "\"fl\":\"" + sdr[6].ToString() + "\",";
                strHead += "\"jd\":\"" + sdr[7].ToString() + "\",";
                strHead += "\"cpdm\":\"" + sdr[8].ToString() + "\",";
                strHead += "\"jh\":\"" + sdr[9].ToString() + "\",";
                strHead += "\"fxrq\":\"" + Convert.ToDateTime(sdr[10]).ToShortDateString() + "\",";
                strHead += "\"scrq\":\"" + Convert.ToDateTime(sdr[11]).ToShortDateString() + "\",";
                strHead += "\"fxdd\":\"" + sdr[12].ToString() + "\",";
                strHead += "\"sl\":\"" + sdr[13].ToString() + "\",";
                strHead += "\"xz\":\"" + sdr[14].ToString() + "\",";
             
                strHead += "\"qxlx\":\"" + sdr[15].ToString() + "\",";
                strHead += "\"qxwz\":\"" + sdr[16].ToString() + "\",";
                strHead += "\"qxms\":\"" + StringToJson(sdr[17].ToString()) + "\",";
                strHead += "\"fxr\":\"" + sdr[18].ToString() + "\",";
                strHead += "\"zt\":\"" + sdr[19].ToString() + "\"},";

            }
            strHead = strHead.Substring(0, strHead.Length - 1);
            strHead = strHead + "]";

            sdr.Close();

            strSql = " select [shmc],[shr],[shyj],[xz],[bfbl],[zrdw],[shsj],[shjy],[sfbh]  from [TIDE].[dbo].[Tab_Reject_lims_shxxb] where id='" + strId + "' and hmark=1  ";
            strSql += " order by shsj ";

            SqlCommand cmdbody = new SqlCommand(strSql, con);
            SqlDataReader sdrbody = cmdbody.ExecuteReader();
            string strBody = "\"body\":[]";
            if (sdrbody.HasRows)
            {
                strBody = "\"body\":[";
            }
            while (sdrbody.Read())
            {
                strBody += "{\"shmc\":\"" + sdrbody[0].ToString() + "\",";
                strBody += "\"shr\":\"" + sdrbody[1].ToString() + "\",";
                strBody += "\"shyj\":\"" + sdrbody[2].ToString() + "\",";
                //strBody += "\"pssl\":\"" + sdrbody[3].ToString() + "\",";
                strBody += "\"xz\":\"" + sdrbody[3].ToString() + "\",";
                strBody += "\"bfbl\":\"" + sdrbody[4].ToString() + "\",";
                strBody += "\"zrdw\":\"" + sdrbody[5].ToString() + "\",";
                strBody += "\"shrq\":\"" + sdrbody[6].ToString() + "\",";
                strBody += "\"shjy\":\"" + sdrbody[7].ToString() + "\",";
                strBody += "\"sfbh\":\"" + sdrbody[8].ToString() + "\"},";

            }
            strBody = strBody.Substring(0, strBody.Length - 1);
            strBody = strBody + "]";
            con.Close();

            string strJson = "{" + strHead + "," + strBody + ",\"stat\":\"" + strStat + "\"}";

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