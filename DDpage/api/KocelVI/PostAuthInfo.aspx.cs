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
    public partial class PostAuthInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strSql = "";
            string strCompany = Request.Form["strCompanyOne"];
            string strDept = Request.Form["strDeptOne"];
            string strName = Request.Form["strNameOne"];
            string strRole = Request.Form["strRoleOne"];
            string strType = Request.Form["strTypeOne"];
            string strEditor = Request.Form["editor"];
            string strID = System.Guid.NewGuid().ToString();
            string strPhone = "";
            string mesg = "信息添加失败,请联系管理员!";

            int t1 = 0;
            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=TDM;User ID=tide;Password=lan@2mail");
            con.Open();

            //strSql = " delete from  [DDdatabase].[dbo].[Tab_KVI_AuthSet] where FCompany='" + strCompany + "' and FDept='" + strDept + "' and FType='" + strType + "'";
            //SqlCommand cmdDel = new SqlCommand(strSql, con);
            //int t1 = cmdDel.ExecuteNonQuery();
            //if (t1 != 0)
            //{
            //    mesg = "信息删除成功！";
            //}


            strSql = " select 联系电话 from [kocelbasedata].[dbo].[tdm_public_personinfo] where 公司='" + strCompany + "' and 部门='" + strDept + "' and 姓名='" + strName + "' and len(联系电话)>0";
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {

                strPhone = sdr[0].ToString();

            }

            sdr.Close();

            strSql = "  insert into [DDdatabase].[dbo].[Tab_KVI_AuthSet]([ID] ,[FCompany],[FDept] ,[FPerson] ,[FPhone],[Role],[FType],[editer],[edittime])  ";
            strSql += " values('" + strID + "','" + strCompany + "','" + strDept + "','" + strName + "','" + strPhone + "','" + strRole + "','" + strType + "','" + strEditor + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "') ";
        
            SqlCommand cmdAuth = new SqlCommand(strSql, con);
             t1 = cmdAuth.ExecuteNonQuery();
            if (t1 != 0)
            {
                mesg = "信息添加成功！";
            }



            con.Close();
            string strJson = "{\"content\":\"" + mesg + "\"}";
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
