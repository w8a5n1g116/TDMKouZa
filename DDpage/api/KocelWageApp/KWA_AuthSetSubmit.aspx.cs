using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.KocelWageApp
{
    public partial class KWA_AuthSetSubmit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strSql = "";
            string companyQuery= Request.Form["strCompany"];
            string companyOne = Request.Form["strCompanyOne"];
            string companyTwo = Request.Form["strCompanyTwo"];
            string companyTh = Request.Form["strCompanyTh"];
            string deptOne = Request.Form["strDeptOne"];
            string deptTwo = Request.Form["strDeptTwo"];
            string deptTh = Request.Form["strDeptTh"];
            string nameOne = Request.Form["strNameOne"];
            string nameTwo = Request.Form["strNameTwo"];
            string nameTh = Request.Form["strNameTh"];
            string editor = Request.Form["editor"];
            string phoneOne = "";
            string phoneTwo = "";
            string phoneTh = "";
            int t1 = 0;
            Boolean stat = false;


            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=TDM;User ID=tide;Password=lan@2mail");
            con.Open();

            strSql = "delete from [DDdatabase].[dbo].[Tab_KWA_Flow] where [Role]<>'9'";
            strSql += " and flowCompany = '"+companyQuery+"'";

            SqlCommand cmd01 = new SqlCommand(strSql, con);
            t1 = cmd01.ExecuteNonQuery();
            if (t1 != 0)
            {
                stat = true;
            }

            strSql = "select distinct([联系电话]) from [kocelbasedata].[dbo].[tdm_public_personinfo] where 1=1 ";
            strSql += " and 公司='" + companyOne + "'";
            strSql += " and 部门='" + deptOne + "'";
            strSql += " and 姓名='" + nameOne + "'";

            SqlCommand cmdPhone01 = new SqlCommand(strSql, con);
            SqlDataReader sdrPhone01 = cmdPhone01.ExecuteReader();
            while (sdrPhone01.Read())
            {

                phoneOne = sdrPhone01[0].ToString();

            }

            sdrPhone01.Close();

            strSql = "select distinct([联系电话]) from [kocelbasedata].[dbo].[tdm_public_personinfo] where 1=1 ";
            strSql += " and 公司='" + companyTwo + "'";
            strSql += " and 部门='" + deptTwo + "'";
            strSql += " and 姓名='" + nameTwo + "'";

            SqlCommand cmdPhone02 = new SqlCommand(strSql, con);
            SqlDataReader sdrPhone02 = cmdPhone02.ExecuteReader();
            while (sdrPhone02.Read())
            {

                phoneTwo = sdrPhone02[0].ToString();

            }

            sdrPhone02.Close();


            strSql = "select distinct([联系电话]) from [kocelbasedata].[dbo].[tdm_public_personinfo] where 1=1 ";
            strSql += " and 公司='" + companyTh + "'";
            strSql += " and 部门='" + deptTh + "'";
            strSql += " and 姓名='" + nameTh + "'";

            SqlCommand cmdPhone03 = new SqlCommand(strSql, con);
            SqlDataReader sdrPhone03 = cmdPhone03.ExecuteReader();
            while (sdrPhone03.Read())
            {

                phoneTh = sdrPhone03[0].ToString();

            }

            sdrPhone03.Close();


            if (phoneOne.Length == 0)
            {
                stat = false;
                string content = "流程设置失败！原因：审核发起人联系电话为空，请检查EHR系统！";
                string strJson = "{\"stat\":\"" + stat + "\",\"content\":\"" + content + "\"}";
                Response.Write(strJson);
            }
            else
            {
                if (phoneTwo.Length == 0)
                {
                    stat = false;
                    string content = "流程设置失败！原因：一级审核人联系电话为空，请检查EHR系统！";
                    string strJson = "{\"stat\":\"" + stat + "\",\"content\":\"" + content + "\"}";
                    Response.Write(strJson);
                }
                else
                {
                    if (phoneTh.Length == 0)
                    {
                        stat = false;
                        string content = "流程设置失败！原因：二级审核人联系电话为空，请检查EHR系统！";
                        string strJson = "{\"stat\":\"" + stat + "\",\"content\":\"" + content + "\"}";
                        Response.Write(strJson);
                    }
                    else
                    {
                        strSql = "insert into [DDdatabase].[dbo].[Tab_KWA_Flow]";
                        strSql += "values('" + companyQuery + "','" + companyOne + "','" + deptOne + "','" + nameOne + "','"+phoneOne+"','1','" + editor + "','" + DateTime.Now.ToString() + "')";

                        SqlCommand cmdIn01 = new SqlCommand(strSql, con);
                        t1 = cmdIn01.ExecuteNonQuery();
                        if (t1 != 0)
                        {
                            stat = true;
                        }

                        strSql = "insert into [DDdatabase].[dbo].[Tab_KWA_Flow]";
                        strSql += "values('" + companyQuery + "','" + companyTwo + "','" + deptTwo + "','" + nameTwo + "','" + phoneTwo + "','2','" + editor + "','" + DateTime.Now.ToString() + "')";

                        SqlCommand cmdIn02 = new SqlCommand(strSql, con);
                        t1 = cmdIn02.ExecuteNonQuery();
                        if (t1 != 0)
                        {
                            stat = true;
                        }

                        strSql = "insert into [DDdatabase].[dbo].[Tab_KWA_Flow]";
                        strSql += "values('" + companyQuery + "','" + companyTh + "','" + deptTh + "','" + nameTh + "','" + phoneTh + "','3','" + editor + "','" + DateTime.Now.ToString() + "')";

                        SqlCommand cmdIn03 = new SqlCommand(strSql, con);
                        t1 = cmdIn03.ExecuteNonQuery();
                        if (t1 != 0)
                        {
                            stat = true;
                        }

                        string content = "流程设置成功！";
                        string strJson = "{\"stat\":\"" + stat + "\",\"content\":\"" + content + "\"}";
                        Response.Write(strJson);

                    }
                }
            }

            con.Close();     
            
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
