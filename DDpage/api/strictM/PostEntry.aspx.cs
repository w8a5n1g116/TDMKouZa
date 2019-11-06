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
    public partial class PostEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCom = Request.Form["company"];
            string strDept = Request.Form["dept"];
            string strVen = Request.Form["ven"];
            string strName = Request.Form["name"];
            string strCardNo = Request.Form["ID"];

            string PType = Request.Form["PType"];
            string PItem = Request.Form["PItem"];
            string PCType = Request.Form["PCType"];
            string PDType = Request.Form["PDType"];
            string PContent = Request.Form["PContent"];
            string purl = Request.Form["purl"];
            string PResCom = Request.Form["PResCom"];
            string PResDept = Request.Form["PResDept"];
            string PResVen = Request.Form["PResVen"];
            string PResVenAss = Request.Form["PResVenAss"];
            string PResVenAssUnit = Request.Form["PResVenAssUnit"];
            string PResPer = Request.Form["PResPer"];
            string PResPerAss = Request.Form["PResPerAss"];
            string PResPerAssUnit = Request.Form["PResPerAssUnit"];
            string PDate = Request.Form["PDate"];
            string PRemark = Request.Form["PRemark"];

            string strSql = "";
            string strId = System.Guid.NewGuid().ToString();
            int t1 = 0;
            string strNo = "";
            string strVenNo = "";
            string strJson = "";
            bool stat = false;

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=BPMS;User ID=tide;Password=lan@2mail");
            con.Open();

            strSql = "select 身份证号,经营体代码 from [kocelbasedata].[dbo].[tdm_public_personinfo] where 1=1";
            strSql += " and 公司='" + PResCom + "'";
            strSql += " and 部门='" + PResDept + "'";
            strSql += " and 经营体名称='" + PResVen + "'";
            strSql += " and 姓名='" + PResPer + "'";

            SqlCommand cmdNo = new SqlCommand(strSql, con);
            SqlDataReader sdrNo= cmdNo.ExecuteReader();
            while (sdrNo.Read())
            {
                strNo = sdrNo[0].ToString();
                strVenNo = sdrNo[1].ToString();
            }
            sdrNo.Close();


            strSql = " insert into [BPMS].[dbo].[Tab_labDiscipline_list] ([ID],[category],[categoryType],[penaltyClause],[details],[photoUrl],[assessDate],[type]";
            strSql += " ,[resCompany],[resDept],[resVen],[resVenCode],[resVenAssess],[resVenAssessUnit]";
            strSql += " ,[resPerson] ,[resPersonCode],[resPersonAssess],[resPersonAssessUnit]";
            strSql += " ,[inputPerson] ,[inputCompany] ,[inputDept],[inputDate] ,[hmark],[remark])";
            strSql += " values('"+ strId + "','"+PType+"','"+PCType+"','"+PItem+"','"+PContent+"','"+purl+"','"+PDate+"','"+PDType+"','"+PResCom+"','"+PResDept+"','"+PResVen+"','"+ strVenNo + "','"+PResVenAss+"'";
            strSql += " ,'" + PResVenAssUnit + "','" + PResPer + "','"+ strNo + "','" + PResPerAss + "','" + PResPerAssUnit + "','" + strName + "','" + strCom + "','" + strDept + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','1','" + PRemark + "')";

            SqlCommand cmdEntry = new SqlCommand(strSql, con);
            t1 = cmdEntry.ExecuteNonQuery();
            if (t1 != 0)
            {
                stat = true;
            }
            strJson = "{\"stat\":\"" + stat + "\"}";

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