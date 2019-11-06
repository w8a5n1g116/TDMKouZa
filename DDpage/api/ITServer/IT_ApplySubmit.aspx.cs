using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.ITServer
{
    public partial class IT_ApplySubmit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCompany = Request.Form["company"].ToString();
            string strDept = Request.Form["dept"].ToString();
            string strVen = Request.Form["ven"].ToString();
            string strName = Request.Form["name"].ToString();
            string strPhone = Request.Form["phone"].ToString();         
            string strGzxx = Request.Form["gzxx"].ToString();
            string strUrl = Request.Form["purl"].ToString();
            string strOne = Request.Form["one"].ToString();
            string strTwo = Request.Form["two"].ToString();
            string strTh = Request.Form["th"].ToString();
            string strTime = Request.Form["ptime"].ToString();
            string strwhPhone = "";
            string strMail = "";
            string strId = "";
            int t1 = 0;
            Boolean stat = false;

            

            //pc端参数
            string whzrrName = string.Empty;
            string whzrrNEIMA = string.Empty;
            string whzrrMail = string.Empty;
            string whbzzName = string.Empty;
            string whbzzNEIMA = string.Empty;
            string whbzzMail = string.Empty;
            //pc端参数

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=BPMS;User ID=tide;Password=lan@2mail");
            con.Open();

            SqlConnection conNew = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=BPMS;User ID=tide;Password=lan@2mail");
            conNew.Open();

            //SqlConnection conNew = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=BPMS;User ID=tide;Password=lan@2mail");
            //conNew.Open();

            string zrrSql = string.Empty;
            if ("系统".Equals(strOne))
            {
                zrrSql = "select whzrrName,whzrrNEIMA,whzrrMail,whbzzName,whbzzNEIMA,whbzzMail  from   [BPMS].[dbo].[Tab_ITOM_MtArea] where whType='系统' and softtype='" + strTwo + "' and softname='" + strTh + "'";
            }
            else
            {
                zrrSql = "select whzrrName,whzrrNEIMA,whzrrMail,whbzzName,whbzzNEIMA,whbzzMail from   [BPMS].[dbo].[Tab_ITOM_MtArea] where  whType='硬件' and companyName='" + strCompany + "' and deptName='" + strDept + "'";
            }

            SqlCommand cmd = new SqlCommand(zrrSql, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {

                    whzrrName = sdr[0].ToString();
                    whzrrNEIMA = sdr[1].ToString();
                    whzrrMail = sdr[2].ToString();
                    whbzzName = sdr[3].ToString();
                    whbzzNEIMA = sdr[4].ToString();
                    whbzzMail = sdr[5].ToString();
                }


                //有责任人处理流程
                string strPrimidSql01 = "select MAX(primID)+1 as primID from [TDM].[dbo].[Maintenance_Plan]";
                SqlCommand cmd01 = new SqlCommand(strPrimidSql01, conNew);
                SqlDataReader sdr01 = cmd01.ExecuteReader();
                while (sdr01.Read())
                {

                    strId = sdr01[0].ToString();

                }

                sdr01.Close();

                //获取strMail值
                string strBxrMail = "select 邮件地址 from [kocelbasedata].[dbo].[tdm_public_personinfo] where 公司='" + strCompany + "' and 部门='" + strDept + "' and 姓名='" + strName + "'";
                SqlCommand cmd02 = new SqlCommand(strBxrMail, conNew);
                SqlDataReader sdr02 = cmd02.ExecuteReader();
                while (sdr02.Read())
                {

                    strMail = sdr02[0].ToString();

                }

                sdr02.Close();

                //获取strMail值

                //获取维护人手机号码
                string strWhSql = "select 联系电话 from [kocelbasedata].[dbo].[tdm_public_personinfo] where fitemid='" + whzrrNEIMA + "' ";
                SqlCommand cmdwh = new SqlCommand(strWhSql, conNew);
                SqlDataReader sdrwh = cmdwh.ExecuteReader();
                while (sdrwh.Read())
                {

                    strwhPhone = sdrwh[0].ToString();

                }

                sdrwh.Close();

                //获取维护人手机号码



                string strtmp = "";

                if ("系统".Equals(strOne))
                {

                    strtmp = "insert into [TDM].[dbo].[Maintenance_Plan](companyName,deptName,bxrName,bxrMail,gzxx,gzlx,phone,whzrr,userIp,softType,softName,startTime,updateTime,Drawing,DrawingAddress) values('" + strCompany + "','" + strDept + "','" + strName + "','" + strMail + "','" + strGzxx + "','" + strOne + "','" + strPhone + "','" + whzrrName + "','0.0.0.0','" + strTwo + "','" + strTh + "','" + System.DateTime.Now.ToString() + "','" + System.DateTime.Now.ToString() + "','" + strUrl + "','')";

                }
                else
                {

                    strtmp = "insert into [TDM].[dbo].[Maintenance_Plan](companyName,deptName,bxrName,bxrMail,gzxx,gzlx,phone,whzrr,userIp,startTime,updateTime,Drawing,DrawingAddress) values('" + strCompany + "','" + strDept + "','" + strName + "','" + strMail + "','" + strGzxx + "','" + strOne + "','" + strPhone + "','" + whzrrName + "','0.0.0.0','" + System.DateTime.Now.ToString() + "','" + System.DateTime.Now.ToString() + "','" + strUrl + "','')";
                }

                SqlCommand cmd03 = new SqlCommand(strtmp, conNew);
                t1 = cmd03.ExecuteNonQuery();
                if (t1 != 0)
                {
                    stat = true;
                }


                conNew.Close();
                //conNew.Close();

                //邮件通知
                bool sendMailSuccess = false;
                sendMailSuccess = MailClass.SendMaintenancePlanEmail(whzrrName, whzrrNEIMA, whzrrMail, whbzzName, whbzzNEIMA, whbzzMail, strId, strCompany, strDept, strGzxx, strName, strPhone, System.DateTime.Now.ToString());

                //邮件地址

                string strJson = "{\"stat\":\"" + stat + "\",\"phone\":\"" + strwhPhone + "\",\"time\":\"" + System.DateTime.Now.ToString() + "\",\"id\":\"" + strId + "\"}";

                Response.Write(strJson);

            }
            else
            {

                //无责任人处理流程
                whzrrName = "未明确";
                strwhPhone = "00000000000";
                string strPrimidSql01 = "select MAX(primID)+1 as primID from [TDM].[dbo].[Maintenance_Plan]";
                SqlCommand cmd01 = new SqlCommand(strPrimidSql01, conNew);
                SqlDataReader sdr01 = cmd01.ExecuteReader();
                while (sdr01.Read())
                {

                    strId = sdr01[0].ToString();

                }

                sdr01.Close();

                //获取strMail值
                string strBxrMail = "select 邮件地址 from [kocelbasedata].[dbo].[tdm_public_personinfo] where 公司='" + strCompany + "' and 部门='" + strDept + "' and 姓名='" + strName + "'";
                SqlCommand cmd02 = new SqlCommand(strBxrMail, conNew);
                SqlDataReader sdr02 = cmd02.ExecuteReader();
                while (sdr02.Read())
                {

                    strMail = sdr02[0].ToString();

                }

                sdr02.Close();

                string strtmp = "";

                if ("系统".Equals(strOne))
                {

                    strtmp = "insert into [TDM].[dbo].[Maintenance_Plan](companyName,deptName,bxrName,bxrMail,gzxx,gzlx,phone,whzrr,userIp,softType,softName,startTime,updateTime,Drawing,DrawingAddress) values('" + strCompany + "','" + strDept + "','" + strName + "','" + strMail + "','" + strGzxx + "','" + strOne + "','" + strPhone + "','" + whzrrName + "','0.0.0.0','" + strTwo + "','" + strTh + "','" + System.DateTime.Now.ToString() + "','" + System.DateTime.Now.ToString() + "','" + strUrl + "','')";

                }
                else
                {

                    strtmp = "insert into [TDM].[dbo].[Maintenance_Plan](companyName,deptName,bxrName,bxrMail,gzxx,gzlx,phone,whzrr,userIp,startTime,updateTime,Drawing,DrawingAddress) values('" + strCompany + "','" + strDept + "','" + strName + "','" + strMail + "','" + strGzxx + "','" + strOne + "','" + strPhone + "','" + whzrrName + "','0.0.0.0','" + System.DateTime.Now.ToString() + "','" + System.DateTime.Now.ToString() + "','" + strUrl + "','')";
                }

                SqlCommand cmd03 = new SqlCommand(strtmp, conNew);
                t1 = cmd03.ExecuteNonQuery();
                if (t1 != 0)
                {
                    stat = true;
                }


                conNew.Close();
                //conNew.Close();

                //邮件通知
                bool sendMailSuccess = false;
                sendMailSuccess = MailClass.SendMaintenancePlanEmail(whzrrName, whzrrNEIMA, whzrrMail, whbzzName, whbzzNEIMA, whbzzMail, strId, strCompany, strDept, strGzxx, strName, strPhone, System.DateTime.Now.ToString());

                //邮件地址

                string strJson = "{\"stat\":\"" + stat + "\",\"phone\":\"" + strwhPhone + "\",\"time\":\"" + System.DateTime.Now.ToString() + "\",\"id\":\"" + strId + "\"}";

                Response.Write(strJson);


            }

            sdr.Close();
            con.Close();

            


        }

    }
}