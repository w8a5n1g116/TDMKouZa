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
    public partial class KWA_ApplySubmit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strSql = "";
            string strFlowCompany = Request.Form["flowCompany"];
            string strCompany = Request.Form["company"];
            string strDept = Request.Form["dept"];
            string strName = Request.Form["name"];
            string strYear = Request.Form["year"];
            string strMonth = Request.Form["month"];
            string strRemark1 = Request.Form["remark1"];
            string strRemark2 = Request.Form["remark2"];
            string strRemark3 = Request.Form["remark3"];
            string strTwoCompany = "";
            string strTwoDept = "";
            string strTwoName = "";
            string strTwoPhone = "";
            string strJson = "";
            string strMsg = "";
            Boolean stat = false;
            int t1 = 0;
            

            //生成审批编号
            Random r = new Random();
            int i = r.Next(1000, 9999);
            string strPID = System.DateTime.Now.ToFileTime().ToString() + i.ToString();
            //生成审批编号

            SqlConnection conOne = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=TDM;User ID=tide;Password=lan@2mail");
            conOne.Open();

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=TDM;User ID=tide;Password=lan@2mail");
            con.Open();



            //审批流程查询，没有审批流程则返回

            strSql = "  SELECT [FCompany],[FDept],[FPerson],[FPhone]   ";
            strSql += " FROM [DDdatabase].[dbo].[Tab_KWA_Flow] where 1=1 ";
            strSql += " and [flowCompany] = '" + strFlowCompany + "' ";
            strSql += " and [Role] = '2' ";

            SqlCommand cmd01 = new SqlCommand(strSql, conOne);
            SqlDataReader sdr01 = cmd01.ExecuteReader();
            if (sdr01.HasRows)
            {
                while (sdr01.Read())
                {

                    strTwoCompany = sdr01[0].ToString();
                    strTwoDept = sdr01[1].ToString();
                    strTwoName = sdr01[2].ToString();
                    strTwoPhone = sdr01[3].ToString();

                }
                string strID = System.Guid.NewGuid().ToString();
                //写入审批表
                strSql = "  insert into [DDdatabase].[dbo].[Tab_KWA_AuditList] ([ID] ,[PID],[FCompany],[FYear],[FMonth],[OneCompany],[OneDept],[OneName],[OneRemark],[OneTime],[FRemark],[SRemark],[TRemark],[TwoCompany],[TwoDept] ,[TwoName],[stat])  ";
                strSql += " values('"+ strID + "','"+ strPID + "','"+ strFlowCompany + "','"+strYear+"','"+strMonth+"','"+strCompany+"','"+strDept+"','"+strName+"','','"+DateTime.Now.ToString()+"','"+strRemark1+"','"+strRemark2+"','"+strRemark3+"','"+ strTwoCompany + "','"+strTwoDept+"','"+strTwoName+"','1')";

                SqlCommand cmd02 = new SqlCommand(strSql, con);
                t1 = cmd02.ExecuteNonQuery();
                if (t1 != 0)
                {
                    stat = true;
                }

                //写入审批表

                //公司数据写入
                strSql = " insert into [DDdatabase].[dbo].[Tab_KWA_CompanyAuditList]";
                strSql += " select NEWID(),"+ strPID + ",[showId],[FYear],[FMonth],[FCompany] ,[staffItem],[wagesItem],[countItem],[amount]";
                strSql += " FROM [DDdatabase].[dbo].[Tab_KWA_CompanyCostList] where 1=1";
                strSql += " and [FCompany]='" + strFlowCompany + "'";
                strSql += " and [FYear]='" + strYear + "'";
                strSql += " and [FMonth]='" + strMonth + "'";

                SqlCommand cmd03 = new SqlCommand(strSql, con);
                t1 = cmd03.ExecuteNonQuery();
                if (t1 != 0)
                {
                    stat = true;
                }

                //公司数据写入
                //部门数据写入

                strSql = " insert into [DDdatabase].[dbo].[Tab_KWA_DeptAuditList]";
                strSql += " select NEWID()," + strPID + ",[showId],[FYear],[FMonth],[FCompany],[FDept],[staffItem],[wagesItem],[countItem],[amount]";
                strSql += " FROM [DDdatabase].[dbo].[Tab_KWA_DeptCostList] where 1=1";
                strSql += " and [FCompany]='" + strFlowCompany + "'";
                strSql += " and [FYear]='" + strYear + "'";
                strSql += " and [FMonth]='" + strMonth + "'";

                SqlCommand cmd04 = new SqlCommand(strSql, con);
                t1 = cmd04.ExecuteNonQuery();
                if (t1 != 0)
                {
                    stat = true;
                }

                //部门数据写入
                string strRID = System.Guid.NewGuid().ToString();
                strID = strID + "1";
                //返回json数据
                strJson = "{\"stat\":\"" + stat + "\",\"err\":\"" + strMsg + "\",\"rid\":\"" + strRID + "\",\"oid\": \"" + strID + "\" ,\"id\":\"" + strPID + "\",\"name\":\"" + strTwoName + "\",\"phone\":\"" + strTwoPhone + "\"}";

            }
            else
            {
                strMsg = "" + strCompany + "未设置审批流程，请联系管理人员进行设置！";
                strJson = "{\"stat\":\"" + stat + "\",\"err\":\"" + strMsg + "\",\"id\":\"" + strPID + "\",\"name\":\"" + strTwoName + "\",\"phone\":\"" + strTwoPhone + "\"}";
            }

            sdr01.Close();
            conOne.Close();
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
