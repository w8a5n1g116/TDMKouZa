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
    public partial class PostLimsAuditOneSubmit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strId = Request.Form["id"];
            string strCom = Request.Form["company"];
            string strDept = Request.Form["dept"];
            string strName = Request.Form["name"];
            string strPsyj = Request.Form["psyj"];
            string strBhgpxz = Request.Form["bhgpxz"];
            string strZrdw = Request.Form["zrdw"];
            string strBfbl = Request.Form["bfbl"];
            string strPsjy = Request.Form["psjy"];
            string strXjsh = Request.Form["xjsh"];
            string strXjshgs = Request.Form["company"];
            string strXjshbm = Request.Form["xjshbm"];
            string strXjshr = Request.Form["xjshr"];

            string strSql = "";
            string strStat = "1";
            Boolean stat = false;
            int t1;

            SqlConnection con = new SqlConnection("Data Source=192.168.0.243;Initial Catalog=TIDE;User ID=sa;Password=lan@2mail");
            con.Open();


            strSql = "update  [TIDE].[dbo].[Tab_Reject_lims_shxxb] set shgs='" + strCom + "',shbm='" + strDept + "',shr='" + strName + "',shyj='" + strPsyj + "',shsj='" + DateTime.Now.ToString() + "',shjy='" + strPsjy + "',hmark=1,xz='" + strBhgpxz + "',bfbl='" + strBfbl + "',zrdw='" + strZrdw + "' where ID='" + strId + "' and hmark=0 and shmc in ('一级审核')";

            SqlCommand cmd01 = new SqlCommand(strSql, con);
            t1 = cmd01.ExecuteNonQuery();
            if (t1 != 0)
            {
                stat = true;
            }


            if (strXjsh=="1")
            {
                strSql = " insert into [TIDE].[dbo].[Tab_Reject_lims_shxxb](ID, shmc, xfrq, shgs, shbm, shr, hmark, fmark)";
                strSql += "values('"+ strId + "','二级审核','" + DateTime.Now.ToString()+"','"+strXjshgs+"','"+strXjshbm+"','"+strXjshr+"','0','0')";

                SqlCommand cmd02 = new SqlCommand(strSql, con);
                t1 = cmd02.ExecuteNonQuery();
                if (t1 != 0)
                {
                    stat = true;
                }
            }
            else
            {
                strSql = "update  [TIDE].[dbo].[Tab_Reject_lims_xxb] set zt='流程结束',hmark=2 where ID='" + strId + "'";
                SqlCommand cmd03 = new SqlCommand(strSql, con);
                t1 = cmd03.ExecuteNonQuery();
                if (t1 != 0)
                {
                    stat = true;
                }

                strSql = "update [TIDE].[dbo].[Tab_Reject_lims_shxxb] set fmark=1 where PID in (select max(pid) from Tab_Reject_lims_shxxb where ID ='" + strId + "') and shmc in ('一级审核')";
                SqlCommand cmd04 = new SqlCommand(strSql, con);
                t1 = cmd04.ExecuteNonQuery();
                if (t1 != 0)
                {
                    stat = true;
                }

            }
            if (stat==true)
            {
                strStat = "1";
            }
            else
            {
                strStat = "0";
            }
           
            string strJson = "{\"stat\":\"" + strStat + "\"}";

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