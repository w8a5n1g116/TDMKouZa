using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace DDpage.api.ITServer
{
    public class MailClass
    {

        public class classEmailInfo
        {
            #region 邮件信息

            /// <summary>收件人姓名</summary>
            private string strName;
            /// <summary>收件人姓名</summary>
            public string Name
            {
                get { return strName; }
                set { strName = value; }
            }

            /// <summary>收件人邮件地址</summary>
            private string strEmail;
            /// <summary>收件人邮件地址</summary>
            public string Email
            {
                get { return strEmail; }
                set { strEmail = value; }
            }

            #endregion
        }



        public static bool SendMaintenancePlanEmail(string whzrrName, string whzrrNEIMA, string whzrrMail, string whbzzName, string whbzzNEIMA, string whbzzMail, string primid, string company, string deptname, string gzxx, string bxrName, string bxrPhone, string startTime)
        {
            #region 给公司领导发邮件进行审批

            string strWebBody = string.Empty;

            // 收件人邮件地址
            List<classEmailInfo> lstEmailInfo = new List<classEmailInfo>();
            // 抄送人邮件地址
            List<classEmailInfo> lstCCEmailInfo = new List<classEmailInfo>();

            if ("未明确" == whzrrName)
            {
                
                classEmailInfo clTempEmailInfo = new classEmailInfo();
                clTempEmailInfo.Name = "冯海莹";

                // 测试期间暂时使用我的邮箱地址
                // clTempEmailInfo.Email = "kocel.ghb.zzx@kocel.com";
                clTempEmailInfo.Email = "kocel.xxzx.fhy@kocel.com";

                lstEmailInfo.Add(clTempEmailInfo);

                classEmailInfo clTempEmailInfo2 = new classEmailInfo();
                clTempEmailInfo2.Name = "闫超";

                // 测试期间暂时使用我的邮箱地址
                //  clTempEmailInfo.Email = "kocel.ghb.zzx@kocel.com";
                clTempEmailInfo2.Email = "kocel.xxzx.yc@kocel.com";

                lstEmailInfo.Add(clTempEmailInfo2);
                strWebBody = GetHtmlCode("未明确", "闫超", "KOCEL296", primid, company, deptname, gzxx, bxrName, startTime);
                //  }

            }
            else
            {
                classEmailInfo clTempEmailInfo = new classEmailInfo();
                clTempEmailInfo.Name = whzrrName;

                // 测试期间暂时使用我的邮箱地址
                clTempEmailInfo.Email = whzrrMail;
                //  clTempEmailInfo.Email = "kocel.ghb.zzx@kocel.com";

                lstEmailInfo.Add(clTempEmailInfo);

                classEmailInfo clTempCCEmailInfo = new classEmailInfo();
                clTempCCEmailInfo.Name = whbzzName;

                // 测试期间暂时使用我的邮箱地址
                clTempCCEmailInfo.Email = whbzzMail;
                //  clTempEmailInfo.Email = "kocel.ghb.zzx@kocel.com";

                lstCCEmailInfo.Add(clTempCCEmailInfo);

                classEmailInfo cclTempCCEmailInfo = new classEmailInfo();
                cclTempCCEmailInfo.Name = "冯海莹";

                // 测试期间暂时使用我的邮箱地址
                cclTempCCEmailInfo.Email = "kocel.xxzx.fhy@kocel.com";
                lstCCEmailInfo.Add(cclTempCCEmailInfo);

                strWebBody = GetHtmlCode(whzrrName, whzrrName, whzrrNEIMA, primid, company, deptname, gzxx, bxrName, startTime);
            }


            if (null != lstEmailInfo && null != lstCCEmailInfo)
            {
                string strTempSubject = "IT服务拉式计划维护--" + company + ":" + deptname + " " + bxrName + " " + bxrPhone;
                return SendEmail(lstEmailInfo, lstCCEmailInfo, strWebBody, strTempSubject, "IT服务拉式计划");
            }
            else
            {
                return false;
            }

            #endregion
        }


        public static bool SendCollectionPolicyToLabDEmail(List<classEmailInfo> lstTempSendEmailInfo, List<classEmailInfo> lstTempCCEmailInfo, string strUrl)
        {
            // structUserInfo userInfo = new structUserInfo();
            // userInfo = ConstClass.GetUserInfoWITHNEIMA(userNEIMA, "财务辅助");
            //string strTempWebBodyContent = "<tr><td align=left><FONT size=2 color=black>&nbsp;&nbsp;&nbsp;您好！现有新的税收政策！</font><a href='http://192.168.0.6/KocelTIDESystem/FinancialAssistance/IndexWeb.aspx?ApprovalUrl="
            //            + strUrl + "' target='blank'/><FONT size=2 color=red>请点击登陆系统，查看文件内容！&nbsp;&nbsp;&nbsp;</FONT></a></td></tr>";

            string strWebBody = GetByHtmlCode(strUrl, "违规审核");
            return SendEmail(lstTempSendEmailInfo, lstTempCCEmailInfo, strWebBody, "违规审核", "严明底线管理系统");
            //return true;
        }

        private static string GetByHtmlCode(string strTempWebBodyContent, string ProjectName)
        {
            #region 根据录入年和录入周获得前台页面代码用于邮件中显示

            //  List<classEconomyInformation> lstEconomyInformation = ConstClass.GetEconomyInformation(strEntryYear, strEntryWeek, strTempCompanyName, "");

            #region 邮件HTML页面代码

            string strWebBody = string.Empty;

            strWebBody += "<html><head><title>" + ProjectName + "</title>";
            strWebBody += "<style type='text/css'>";
            strWebBody += "<!--";
            strWebBody += "td,form,select,input,p,table,.font {font-size: 12px;line-height: 20px}";
            strWebBody += "a:link {  color: #000000;  font-size: 12px; text-decoration: none}";
            strWebBody += "a:visited {  color: #000000; font-size: 12px; text-decoration: none}";
            strWebBody += "a:hover {  color: #ff7f2c; font-size: 12px; text-decoration: underline}";
            strWebBody += "-->";
            strWebBody += "</style>";
            strWebBody += "</head><body>";
            strWebBody += "<table width=100% align=cnter>";
            strWebBody += "<tr><td align=left width=100%><img src='http://192.168.0.6/KocelTIDESystem/img/tdm.gif' width='190' height='28'/>";
            strWebBody += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            strWebBody += "<FONT style='FONT-SIZE: 18px;color=black'><strong>" + ProjectName + "<strong></font>&nbsp;&nbsp;<font style='FONT-SIZE: 14px;color=black'>";
            strWebBody += DateTime.Now.Year.ToString() + "年" + DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日";
            strWebBody += "</font></td></tr>";
            strWebBody += "<table width=98% align=left>";
            strWebBody += "<tr bgColor='#D4D4D4'><td height=2></td></tr><tr><td>&nbsp;</td></tr><tr>";
            strWebBody += "<td>";
            strWebBody += "  <ul>";
            strWebBody += "    <p>";
            strWebBody += strTempWebBodyContent;
            strWebBody += "<tr bgcolor='#ffffff'><td align=left><table width=100% align=left style='background-color:white'>";
            strWebBody += "<tr bgColor='#aaaaaa'><td align=left height=1></td></tr><tr><td>&nbsp;</td></tr><tr>";
            strWebBody += "<td align=left>";
            strWebBody += "<tr><td align=left><FONT size=2 color=black>系统开发 共享集团 DT中心 Copyright &copy; 2010&nbsp;&nbsp;&nbsp;</font></td></tr>";
            strWebBody += "<tr><td align=left>服务电话：2031204（6204）；</td></tr>";
            strWebBody += "<tr><td align=left>邮箱：";
            strWebBody += "<a href='Mailto:kocel.xxzx.yc@kocel.com?Subject=系统程序问题'><FONT size=2 color=red>kocel.xxzx.yc@kocel.com</FONT> </a>。</td></tr>";

            strWebBody += "</table></div></td></tr></table>";

            #endregion

            return strWebBody;

            #endregion
        }


        private static string GetHtmlCode(string zrrtype, string whzrrName, string whzrrNEIMA, string primid, string company, string deptname, string gzxx, string bxrName, string startTime)
        {
            #region 根据录入年和录入周获得前台页面代码用于邮件中显示

            //  List<classEconomyInformation> lstEconomyInformation = ConstClass.GetEconomyInformation(strEntryYear, strEntryWeek, strTempCompanyName, "");

            #region 邮件HTML页面代码

            string strWebBody = string.Empty;

            strWebBody += "<html><head><title>IT服务拉式计划系统</title>";
            strWebBody += "<style type='text/css'>";
            strWebBody += "<!--";
            strWebBody += "td,form,select,input,p,table,.font {font-size: 12px;line-height: 20px}";
            strWebBody += "a:link {  color: #000000;  font-size: 12px; text-decoration: none}";
            strWebBody += "a:visited {  color: #000000; font-size: 12px; text-decoration: none}";
            strWebBody += "a:hover {  color: #ff7f2c; font-size: 12px; text-decoration: underline}";
            strWebBody += "-->";
            strWebBody += "</style>";
            strWebBody += "</head><body>";
            strWebBody += "<table width=100% align=cnter>";
            strWebBody += "<tr><td align=left width=100%><img src='http://192.168.0.6/MaintenancePlan/img/tdm.gif' width='190' height='28'/>";
            strWebBody += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            strWebBody += "<FONT style='FONT-SIZE: 18px;color=black'><strong>IT服务拉式计划系统<strong></font>&nbsp;&nbsp;<font style='FONT-SIZE: 14px;color=black'>";
            strWebBody += DateTime.Now.Year.ToString() + "年" + DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日";
            strWebBody += "</font></td></tr>";
            strWebBody += "<table width=98% align=left>";
            strWebBody += "<tr bgColor='#D4D4D4'><td height=2></td></tr><tr><td>&nbsp;</td></tr><tr>";
            strWebBody += "<td>";
            strWebBody += "  <ul>";
            strWebBody += "    <p>";
            if ("未明确" == zrrtype)
            {
                strWebBody += "<tr><td align=left><FONT size=2 color=black>&nbsp;&nbsp;&nbsp;您好！您有一条维护计划未明确责任人，报修公司：" + company + " 报修部门：" + deptname + "  时间：" + startTime + "</br>&nbsp;&nbsp;&nbsp;故障现象：" + gzxx + " </br></font>";
                strWebBody += "&nbsp;&nbsp;&nbsp;<a href='http://192.168.0.245:6204/ITOM/Services/IT_serv_FormReportSelect.aspx?primid=" + primid
                 + "&UserName=" + System.Web.HttpUtility.UrlEncode(whzrrName, System.Text.Encoding.UTF8) + "&UserNEIMA=" + whzrrNEIMA + "' target='blank'><FONT size=2 color=red>请点击进入查看&nbsp;&nbsp;&nbsp;</FONT></a></td></tr>";
                // strWebBody += "<tr><td align=left><FONT size=2 color=black>&nbsp;&nbsp;&nbsp;您好！您有一条维护计划未明确责任人，</font><a href='http://192.168.3.232:2620/IndexWeb.aspx?ApprovalUrl=BzzSelectMaintenancePlanWeb.aspx?primid=" + primid
                //  + "&UserName=" + whzrrName + "&UserNEIMA=" + whzrrNEIMA + "' target='blank'/><FONT size=2 color=red>请点击进入查看&nbsp;&nbsp;&nbsp;</FONT></a></td></tr>";
            }
            else
            {
                strWebBody += "<tr><td align=left><FONT size=2 color=black>&nbsp;&nbsp;&nbsp;您好！您有一条维护计划，报修公司：" + company + " 报修部门：" + deptname + "   时间：" + startTime + "</br>&nbsp;&nbsp;&nbsp;故障现象：" + gzxx + " </br></font>&nbsp;&nbsp;&nbsp;<a href='http://192.168.0.245:6204/ITOM/Services/HBMaintenancePlanWeb.aspx?primid=" + primid
                + "&UserName=" + System.Web.HttpUtility.UrlEncode(whzrrName, System.Text.Encoding.UTF8) + "&UserNEIMA=" + whzrrNEIMA + "' target='blank'><FONT size=2 color=red>请点击进入查看&nbsp;&nbsp;&nbsp;</FONT></a></td></tr>";
                //strWebBody += "<tr><td align=left><FONT size=2 color=black>&nbsp;&nbsp;&nbsp;您好！您有一条维护计划，</font><a href='http://192.168.3.232:2620/IndexWeb.aspx?ApprovalUrl=SelectMaintenancePlanWeb.aspx?primid=" + primid
                //+ "&UserName=" + whzrrName + "&UserNEIMA=" + whzrrNEIMA + "' target='blank'/><FONT size=2 color=red>请点击进入查看&nbsp;&nbsp;&nbsp;</FONT></a></td></tr>";
            }
            strWebBody += "<tr><td align=left><table width=100% align=left>";
            strWebBody += "<tr bgColor='#aaaaaa'><td align=left height=1></td></tr><tr><td>&nbsp;</td></tr><tr>";
            strWebBody += "<td align=left>";
            strWebBody += "<tr><td align=left><FONT size=2 color=black>系统开发 共享集团信息中心 Copyright &copy; 2010&nbsp;&nbsp;&nbsp;</font></td></tr>";
            strWebBody += "<tr><td align=left>服务电话：2031204（6204）；</td></tr>";
            strWebBody += "<tr><td align=left>邮箱：";
            strWebBody += "<a href='Mailto:kocel.ghb.zzx@kocel.com?Subject=系统程序问题'><FONT size=2 color=red>kocel.ghb.zzx@kocel.com</FONT> </a>。</td></tr>";

            strWebBody += "</table></div></td></tr></table>";

            #endregion

            return strWebBody;

            #endregion
        }



        public static bool SendMaintenancePlanToBxrEmail(string bxrName, string bxrMail, string primid, string gzxx, string whzrr, string startTime, string hbdate, string whgzyy, string jjcd, string whhb)
        {
            string whfs = string.Empty;
            if ("1".Equals(jjcd))
            {
                whfs = "一般维护";
            }
            else if ("2".Equals(jjcd))
            {
                whfs = "送外维修";
            }
            string bodyHtml = "<tr><td align=left><FONT size=2 color=black>&nbsp;&nbsp;&nbsp;您好！您" + startTime + "提交的一条故障现象为：" + gzxx
                + "的维护任务，已经于" + hbdate + "提交汇报。</br> &nbsp;&nbsp;&nbsp;汇报人：" + whzrr + "&nbsp;&nbsp; 故障原因：" + whgzyy + "&nbsp;&nbsp; 维护方式：" + whfs
                + "</br> &nbsp;&nbsp;&nbsp;汇报内容:" + whhb
                + "</br> &nbsp;&nbsp;&nbsp;</font><a href='http://192.168.0.245:6204/ITOM/Services/ConfirmMaintenancePlanWeb.aspx?primid="
                + primid + "' target='blank'><FONT size=2 color=red>请点击查看,并对此次维护进行确认！&nbsp;&nbsp;&nbsp;</FONT></a></td></tr>";
            string strWebBody = GetByHtmlCode(bodyHtml);
            return SendEmail(bxrMail, strWebBody, bxrName, "IT服务拉式计划反馈", "IT服务拉式计划");
        }


        private static string GetByHtmlCode(string bodyHtml)
        {
            #region 根据录入年和录入周获得前台页面代码用于邮件中显示

            //  List<classEconomyInformation> lstEconomyInformation = ConstClass.GetEconomyInformation(strEntryYear, strEntryWeek, strTempCompanyName, "");

            #region 邮件HTML页面代码

            string strWebBody = string.Empty;

            strWebBody += "<html><head><title>IT服务拉式计划系统</title>";
            strWebBody += "<style type='text/css'>";
            strWebBody += "<!--";
            strWebBody += "td,form,select,input,p,table,.font {font-size: 12px;line-height: 20px}";
            strWebBody += "a:link {  color: #000000;  font-size: 12px; text-decoration: none}";
            strWebBody += "a:visited {  color: #000000; font-size: 12px; text-decoration: none}";
            strWebBody += "a:hover {  color: #ff7f2c; font-size: 12px; text-decoration: underline}";
            strWebBody += "-->";
            strWebBody += "</style>";
            strWebBody += "</head><body>";
            strWebBody += "<table width=100% align=cnter>";
            strWebBody += "<tr><td align=left width=100%><img src='http://192.168.0.6/MaintenancePlan/img/tdm.gif' width='190' height='28'/>";
            strWebBody += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            strWebBody += "<FONT style='FONT-SIZE: 18px;color=black'><strong>IT服务拉式计划系统<strong></font>&nbsp;&nbsp;<font style='FONT-SIZE: 14px;color=black'>";
            strWebBody += DateTime.Now.Year.ToString() + "年" + DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日";
            strWebBody += "</font></td></tr>";
            strWebBody += "<table width=98% align=left>";
            strWebBody += "<tr bgColor='#D4D4D4'><td height=2></td></tr><tr><td>&nbsp;</td></tr><tr>";
            strWebBody += "<td>";
            strWebBody += "  <ul>";
            strWebBody += "    <p>";
            strWebBody += bodyHtml;
            //strWebBody += "<tr><td align=left><FONT size=2 color=black>&nbsp;&nbsp;&nbsp;您好！您有一条维护报修计划，故障现象为：" + gzxx + "。如果您对该次维护不满意，</font><a href='http://192.168.3.232:2620/ConfirmMaintenancePlanWeb.aspx?primid="
            //    + primid + "' target='blank'/><FONT size=2 color=red>请点击进入查看，并提交抱怨！&nbsp;&nbsp;&nbsp;</FONT></a></td></tr>";
            strWebBody += "<tr><td align=left><table width=100% align=left>";
            strWebBody += "<tr bgColor='#aaaaaa'><td align=left height=1></td></tr><tr><td>&nbsp;</td></tr><tr>";
            strWebBody += "<td align=left>";
            strWebBody += "<tr><td align=left><FONT size=2 color=black>系统开发 共享集团信息中心 Copyright &copy; 2013&nbsp;&nbsp;&nbsp;</font></td></tr>";
            strWebBody += "<tr><td align=left>服务电话：2031204（6204）；</td></tr>";
            strWebBody += "<tr><td align=left>邮箱：";
            strWebBody += "<a href='Mailto:kocel.ghb.zzx@kocel.com?Subject=系统程序问题'><FONT size=2 color=red>kocel.ghb.zzx@kocel.com</FONT> </a>。</td></tr>";

            strWebBody += "</table></div></td></tr></table>";

            #endregion

            return strWebBody;

            #endregion
        }


        public static bool SendEmail(List<classEmailInfo> lstTempSendEmailInfo, List<classEmailInfo> lstTempCCEmailInfo, string strWebBody, string strTempSubject, string strTempFromName)
        {
            #region 邮件审批

            string Subject = strTempSubject;
            jmail.Message Jmail = new jmail.Message();

            string FromEmail = "kocel.xxzx.yc@kocel.com";

            //Silent属性：如果设置为true,JMail不会抛出例外错误. JMail. Send( () 会根据操作结果返回true或false
            Jmail.Silent = true;

            //Jmail创建的日志，前提loging属性设置为true
            Jmail.Logging = true;

            //字符集，缺省为"US-ASCII"
            Jmail.Charset = "GB2312";

            //信件的contentype. 缺省是"text/plain"） : 字符串如果你以HTML格式发送邮件, 改为"text/html"即可。
            Jmail.ContentType = "text/html";

            //添加收件人
            for (int i = 0; i < lstTempSendEmailInfo.Count; i++)
            {
                Jmail.AddRecipient(lstTempSendEmailInfo[i].Email, lstTempSendEmailInfo[i].Name, "");
                //Jmail.AddRecipient("kocel.xxzx.lcf@kocel.com", lstTempSendEmailInfo[i].Name, "");
            }

            // 添加抄送人
            for (int j = 0; j < lstTempCCEmailInfo.Count; j++)
            {
                Jmail.AddRecipientCC(lstTempCCEmailInfo[j].Email, lstTempCCEmailInfo[j].Name, "");
                //Jmail.AddRecipientCC("kocel.xxzx.lcf@kocel.com", lstTempCCEmailInfo[j].Name, "");
            }

            Jmail.From = FromEmail;

            //发件人邮件用户名
            Jmail.MailServerUserName = FromEmail;

            // 发件人姓名
            Jmail.FromName = strTempFromName;

            //发件人邮件密码
            Jmail.MailServerPassWord = "lan@2mail";

            //设置邮件标题
            Jmail.Subject = Subject;

            //邮件内容            
            Jmail.Body = strWebBody;


            //Jmail发送的方法,可以修改,此为163邮箱服务器
            bool bSend = Jmail.Send("192.168.0.10", false);

            Jmail.Close();

            return bSend;

            #endregion
        }


        public static bool SendEmail(string strToEmail, string strWebBody, string strToName, string strTempSubject, string strTempFromName)
        {
            #region 邮件审批

            string Subject = strTempSubject;
            jmail.Message Jmail = new jmail.Message();

            string FromEmail = "kocel.xxzx.lcf@kocel.com";
            string ToEmail = strToEmail;
            //  string ToEmail = "kocel.ghb.zzx@kocel.com";

            //Silent属性：如果设置为true,JMail不会抛出例外错误. JMail. Send( () 会根据操作结果返回true或false
            Jmail.Silent = true;

            //Jmail创建的日志，前提loging属性设置为true
            Jmail.Logging = true;

            //字符集，缺省为"US-ASCII"
            Jmail.Charset = "GB2312";

            //信件的contentype. 缺省是"text/plain"） : 字符串如果你以HTML格式发送邮件, 改为"text/html"即可。
            Jmail.ContentType = "text/html";

            //添加收件人
            //Jmail.AddRecipient(ToEmail, "", "");
            Jmail.AddRecipient(ToEmail, strToName, "");

            //抄送班组长
            Jmail.AddRecipientCC("kocel.xxzx.yc@kocel.com", "闫超", "");
            Jmail.AddRecipientCC("kocel.xxzx.fhy@kocel.com", "冯海莹", "");

            Jmail.From = FromEmail;

            //发件人邮件用户名
            Jmail.MailServerUserName = FromEmail;

            // 发件人姓名
            Jmail.FromName = strTempFromName;

            //发件人邮件密码
            Jmail.MailServerPassWord = "lan@2mail";

            //设置邮件标题
            Jmail.Subject = Subject;

            //邮件内容
            //Jmail.Body = body.ToString().Trim();
            Jmail.Body = strWebBody;


            //Jmail发送的方法,可以修改,此为163邮箱服务器
            bool bSend = Jmail.Send("192.168.0.10", false);

            Jmail.Close();

            return bSend;

            #endregion
        }

    }

}