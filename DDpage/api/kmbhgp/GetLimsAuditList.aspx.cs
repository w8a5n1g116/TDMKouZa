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
    public partial class GetLimsAuditList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCompany = Request.Form["Company"];
            string strDept = Request.Form["Dept"];
            string strVen = Request.Form["Ven"];
            string strName = Request.Form["Name"];
            string strPhone = Request.Form["phone"];
            string strGk = Request.Form["gk"];
            string strFl = Request.Form["fl"];
            string strSDate = Request.Form["sdate"];
            string strEDate = Request.Form["edate"];

            strCompany = "直属事业部";
            strDept = "质量管理中心";
            strName = "张会贤";

            string strQx = "'录入','一级审核','二级审核','三级审核'";

            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.0.243;Initial Catalog=TIDE;User ID=sa;Password=lan@2mail");
            con.Open();

            strSql = " select [yjsh],[ejsh] ,[sjsh]  from [TIDE].[dbo].[View_Reject_qx] where gs='" + strCompany + "' and bm='" + strDept + "' and xm='" + strName + "'";

            SqlCommand cmdqx = new SqlCommand(strSql, con);
            SqlDataReader sdrqx = cmdqx.ExecuteReader();
            while (sdrqx.Read())
            {
                if (sdrqx[0].ToString() == "1")
                {
                    strQx += ",'一级审核'";
                }
                if (sdrqx[1].ToString() == "1")
                {
                    strQx += ",'二级审核'";
                }
                if (sdrqx[2].ToString() == "1")
                {
                    strQx += ",'三级审核'";
                }

            }
            sdrqx.Close();



            strSql = " select top(30) ID, jh, gk, cpmc, fl, qxlx, qxwz, fxrq, fxdd, fxr, zt from View_Reject_lims_shList where 1=1 and  shHmark = 0 ";
            strSql += " and (fl is null or fl in (select FType from Tab_Reject_lims_assign where FCompany = '"+ strCompany + "' and FDept = '"+ strDept + "' and FPerson = '"+ strName + "'))";
            if (strGk.Length>0)
            {
                strSql += " and gk like '%" + strGk + "%'";
            }
            if(strFl.Length > 0)
            {
                strSql += " and fl like '%" + strFl + "%'";
            }
            if (strSDate.Length > 0)
            {
                strSql += " and fxrq >= '" + strSDate + "'";
            }
            if (strEDate.Length>0)
            {
                strSql += " and fxrq <='" + strEDate + "'";
            }
            if(strSDate.Length==0 && strEDate.Length == 0)
            {
                strSql += " and year(fxrq)='" + DateTime.Now.Year.ToString() + "' and month(fxrq)='" + DateTime.Now.Month.ToString() + "'";
            }

            strSql += " and  zt in (" + strQx + ")";

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
                strJson += "\"fl\":\"" + sdr[4].ToString() + "\",";
                strJson += "\"qxlx\":\"" + sdr[5].ToString() + "\",";
                strJson += "\"qxwz\":\"" + sdr[6].ToString() + "\",";              
                strJson += "\"fxrq\":\"" + Convert.ToDateTime(sdr[7]).ToShortDateString() + "\",";
                strJson += "\"fxdd\":\"" + sdr[8].ToString() + "\",";
                strJson += "\"fxr\":\"" + sdr[9].ToString() + "\",";
                strJson += "\"zt\":\"" + sdr[10].ToString() + "\"},";

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