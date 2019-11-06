using DDpage.api.ITServer;
using Newtonsoft.Json.Linq;
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
    public partial class PostApply : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCom = Request.Form["auditCompany"];
            string strDept = Request.Form["auditDept"];
            string strName = Request.Form["auditName"];
            string dataStringOne = Request.Form["dataListOne"].ToString();
            string strOneNum = Request.Form["OneNum"];
            string strSql = "";
            string strJson = "";
            string auditPersonCode = "";
            string auditPersonPhone = "";
            bool stat = false;
            int t1 = 0;

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=BPMS;User ID=tide;Password=lan@2mail");
            con.Open();

            //获取审批人身份证号
            strSql = "select distinct 身份证号,联系电话 from [kocelbasedata].[dbo].[tdm_public_personinfo] where 1=1";
            strSql += " and 公司='" + strCom + "'";
            strSql += " and 部门='" + strDept + "'";
            strSql += " and 姓名='" + strName + "'";

            SqlCommand cmdNo = new SqlCommand(strSql, con);
            SqlDataReader sdrNo = cmdNo.ExecuteReader();
            while (sdrNo.Read())
            {
                auditPersonCode = sdrNo[0].ToString();
                auditPersonPhone = sdrNo[1].ToString();
            }
            sdrNo.Close();

            //获取审批人身份证号

            if (dataStringOne.Length > 0)
            {

                //sap201领料
                JArray jsonObjOne = JArray.Parse(dataStringOne);
                foreach (JObject jObject in jsonObjOne)
                {
                    
                    strSql = "update [BPMS].[dbo].[Tab_labDiscipline_list] set checkPerson='"+ strName + "',checkPersonCode='"+ auditPersonCode + "',hmark=2";
                    strSql += " where ID = '" + jObject["ID"].ToString() + "'";

                    SqlCommand cmd = new SqlCommand(strSql, con);
                    t1 = cmd.ExecuteNonQuery();
                    if (t1 != 0)
                    {
                        stat = true;
                    }
                }
            }

            //邮件通知
            AuditMail();

            //钉消息通知函数

            strJson = "{\"stat\":\"" + stat + "\",\"auditPhone\":\"" + auditPersonPhone + "\",\"auditCode\":\"" + auditPersonCode + "\",\"auditTime\":\"" + System.DateTime.Now.ToString() + "\"}";

            con.Close();
            Response.Write(strJson);

        }

        private void AuditMail()
        {
            string strCom = Request.Form["auditCompany"];
            string strDept = Request.Form["auditDept"];
            string strName = Request.Form["auditName"];
            string checkCompany = strCom;
            string checkDept = strDept;
            string checkPerson = strName;
            List<DDpage.api.ITServer.MailClass.classEmailInfo> lstEmailInfo = new List<DDpage.api.ITServer.MailClass.classEmailInfo>();
            DDpage.api.ITServer.MailClass.classEmailInfo clTempEmailInfoO = new DDpage.api.ITServer.MailClass.classEmailInfo();
            clTempEmailInfoO.Name = checkPerson;


            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=kocelbasedata;User ID=tide;Password=lan@2mail");
            con.Open();
            SqlCommand cmd = new SqlCommand("select 姓名,邮件地址 FROM [kocelbasedata].[dbo].[tdm_public_personinfo] where 公司='" + checkCompany + "' and 部门='" + checkDept + "' and 姓名='" + checkPerson + "' and 邮件地址 is not null", con);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                //clTempEmailInfoO.Name = "闫超";
                //clTempEmailInfoO.Email = "kocel.xxzx.yc@kocel.com";
                clTempEmailInfoO.Name = sdr[0].ToString();
                clTempEmailInfoO.Email = sdr[1].ToString();
                lstEmailInfo.Add(clTempEmailInfoO);
            }


            sdr.Close();
            con.Close();
            // 测试期间暂时使用我的邮箱地址
            //clTempEmailInfoO.Email = "kocel.xxzx.yj@kocel.com";



            string strUrl = "";//邮件体内容，表格内容

            strUrl += "<table width='98%' border='0' align='center' cellpadding='2' cellspacing='1' bgcolor='#999999'>";
            strUrl += "<tr bgcolor='#E8E8E8'>";
            strUrl += "<td height='40' colspan='9' align='center' bgcolor='#FFFFFF'><div align=left>&nbsp;";
            strUrl += "&nbsp;1、您可以点击此处<a href='http://192.168.0.245:6204/LabD/Labd_Discipline_mailAudit.aspx?checkCompany=" + Server.UrlEncode(checkCompany) + "&checkDept=" + Server.UrlEncode(checkDept) + "&checkPerson=" + Server.UrlEncode(checkPerson) + "&type=mail'><FONT size=2 color=red>违规审核</FONT> </a>直接操作；";
            strUrl += "<br><br>&nbsp;&nbsp;2、您也可以点击此处<a href='http://192.168.0.245:6204/Frame/Login.htm'><FONT size=2 color=red>TIDE2.0</FONT> </a>登录系统进行操作</div></td></tr>"; ;
            strUrl += "</table></td></tr>";

            MailClass.SendCollectionPolicyToLabDEmail(lstEmailInfo, new List<DDpage.api.ITServer.MailClass.classEmailInfo>(), strUrl);
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