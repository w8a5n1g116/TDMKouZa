using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.KocelVI
{
    public partial class PostAuditThSubmit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strPID = Request.Form["id"].ToString();
            string strIfOk = Request.Form["ifOk"].ToString();
            string strOpinion = Request.Form["opinion"].ToString();
            
            string strPhone = "";
            string strSql = "";
            Boolean stat = false;
            string reMesg = "";
            string strAuditID = "";
            int t1 = 0;
            string strID = System.Guid.NewGuid().ToString();
            string strBh = "";
            string strJson = "";


            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=BPMS;User ID=tide;Password=lan@2mail");
            con.Open();


            if (strIfOk == "1")
            {
                //修改当前状态
                strSql = " update [DDdatabase].[dbo].[Tab_KVI_ApplyList] set hmark= 3  where 1=1";
                strSql += " and ID='" + strPID + "'";

                SqlCommand cmd01 = new SqlCommand(strSql, con);
                t1 = cmd01.ExecuteNonQuery();
                if (t1 != 0)
                {
                    stat = true;
                    reMesg = "数据提交成功！";
                }

                //获取strAuditID
                strSql = " select ID from [DDdatabase].[dbo].[Tab_KVI_AuditList] where PID='" + strPID + "' and hmark = 0";

                SqlCommand cmd02 = new SqlCommand(strSql, con);
                SqlDataReader sdr02 = cmd02.ExecuteReader();
                while (sdr02.Read())
                {

                    stat = true;
                    strAuditID = sdr02[0].ToString();

                    reMesg = "数据提交成功！";
                }

                sdr02.Close();

                strSql = " update [DDdatabase].[dbo].[Tab_KVI_AuditList] set [AuditDate]='" + DateTime.Now.ToString() + "',[AuditContent]='" + strOpinion + "',";
                strSql += " [ifNext]='" + strIfOk + "',[ifOk]='" + strIfOk + "',hmark = 1";
                strSql += " where ID='" + strAuditID + "' and hmark = 0";

                SqlCommand cmd03 = new SqlCommand(strSql, con);
                t1 = cmd03.ExecuteNonQuery();
                if (t1 != 0)
                {
                    stat = true;
                    reMesg = "数据提交成功！";
                }

                //获取录入人信息
                strSql = " select [bh] ,[inputPhone] from [DDdatabase].[dbo].[Tab_KVI_ApplyList] where 1=1";
                strSql += " and [ID]='" + strPID + "'";

                SqlCommand cmd04 = new SqlCommand(strSql, con);
                SqlDataReader sdr04 = cmd04.ExecuteReader();
                while (sdr04.Read())
                {

                    stat = true;
                    strBh = sdr04[0].ToString();
                    strPhone = sdr04[1].ToString();

                    reMesg = "数据提交成功！";
                }

                sdr04.Close();
              
                strJson = "{\"stat\":\"" + stat + "\",\"mesg\":\"" + reMesg + "\",\"phone\":\"" + strPhone + "\",\"time\":\"" + System.DateTime.Now.ToString() + "\",\"id\":\"" + strPID + "\",\"bh\":\"" + strBh + "\"}";

            }
            else
            {
                //修改当前状态
                strSql = " update [DDdatabase].[dbo].[Tab_KVI_ApplyList] set hmark= 1  where 1=1";
                strSql += " and ID='" + strPID + "'";

                SqlCommand cmd01 = new SqlCommand(strSql, con);
                t1 = cmd01.ExecuteNonQuery();
                if (t1 != 0)
                {
                    stat = true;
                    reMesg = "数据提交成功！";
                }


                //获取strAuditID
                strSql = " select ID from [DDdatabase].[dbo].[Tab_KVI_AuditList] where PID='" + strPID + "' and hmark = 0";

                SqlCommand cmd02 = new SqlCommand(strSql, con);
                SqlDataReader sdr02 = cmd02.ExecuteReader();
                while (sdr02.Read())
                {

                    stat = true;
                    strAuditID = sdr02[0].ToString();

                    reMesg = "数据提交成功！";
                }

                sdr02.Close();

                strSql = " update [DDdatabase].[dbo].[Tab_KVI_AuditList] set [AuditDate]='" + DateTime.Now.ToString() + "',[AuditContent]='" + strOpinion + "',";
                strSql += " [ifNext]='" + strIfOk + "',[ifOk]='" + strIfOk + "',hmark = 1";
                strSql += " where ID='" + strAuditID + "' and hmark = 0";

                SqlCommand cmd03 = new SqlCommand(strSql, con);
                t1 = cmd03.ExecuteNonQuery();
                if (t1 != 0)
                {
                    stat = true;
                    reMesg = "数据提交成功！";
                }


                //获取汇报人信息
                string strCom = "";
                string strDept = "";
                string strPer = "";
                strSql = " select [FCom] ,[FDept] ,[FPer],[FPhone]  from [DDdatabase].[dbo].[Tab_KVI_AuditList] where PID='" + strPID + "' and shmc='整改汇报'";
                SqlCommand cmd04 = new SqlCommand(strSql, con);
                SqlDataReader sdr04 = cmd04.ExecuteReader();
                while (sdr04.Read())
                {

                    stat = true;      
                    strCom = sdr04[0].ToString();
                    strDept = sdr04[1].ToString();
                    strPer = sdr04[2].ToString();
                    strPhone = sdr04[3].ToString();

                    reMesg = "数据提交成功！";
                }

                sdr04.Close();

                //整改计划重新下达

                strSql = " insert into [DDdatabase].[dbo].[Tab_KVI_AuditList]([ID],[PID],[shmc],[FCom],[FDept],[FPer],[FPhone],[ArrDate],[hmark])";
                strSql += " values('" + strID + "','" + strPID + "','整改汇报','" + strCom + "','" + strDept + "','" + strPer + "','" + strPhone + "','" + DateTime.Now.ToString() + "','0')";

                SqlCommand cmd05 = new SqlCommand(strSql, con);
                t1 = cmd05.ExecuteNonQuery();
                if (t1 != 0)
                {
                    stat = true;
                    reMesg = "数据提交成功！";
                }

                //获取编号

                strSql = " select [bh] from [DDdatabase].[dbo].[Tab_KVI_ApplyList] where 1=1";
                strSql += " and [ID]='" + strPID + "'";

                SqlCommand cmd06 = new SqlCommand(strSql, con);
                SqlDataReader sdr06 = cmd06.ExecuteReader();
                while (sdr06.Read())
                {

                    stat = true;
                    strBh = sdr06[0].ToString();

                    reMesg = "数据提交成功！";
                }

                sdr06.Close();

                strJson = "{\"stat\":\"" + stat + "\",\"mesg\":\"" + reMesg + "\",\"phone\":\"" + strPhone + "\",\"time\":\"" + System.DateTime.Now.ToString() + "\",\"id\":\"" + strPID + "\",\"bh\":\"" + strBh + "\"}";
            }

            con.Close();
            Response.Write(strJson);

        }

    }
}