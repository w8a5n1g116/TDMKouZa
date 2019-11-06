using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.KocelVI
{
    public partial class PostApplySubmit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCompany = Request.Form["company"].ToString();
            string strDept = Request.Form["dept"].ToString();
            string strVen = Request.Form["ven"].ToString();
            string strName = Request.Form["name"].ToString();
            string strPhone = Request.Form["phone"].ToString();
            string strType = Request.Form["dataType"].ToString();
            string strContent = Request.Form["content"].ToString();
            string strUrlOne = Request.Form["purlOne"].ToString();
            string strUrlTwo = Request.Form["purlTwo"].ToString();
            string strUrlTh = Request.Form["purlTh"].ToString();

            string strTime = Request.Form["ptime"].ToString();
            string strSql = "";
            Boolean stat = false;
            string reMesg = "";
            string reCom = "";
            string reDept = "";
            string rePer = "";
            string rePhone = "";

            Random r = new Random();
            int i = r.Next(1000, 9999);
            int t1 = 0;
            string strPID = System.DateTime.Now.ToFileTime().ToString() + i.ToString();
            string strID = System.Guid.NewGuid().ToString();


            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=BPMS;User ID=tide;Password=lan@2mail");
            con.Open();



            strSql = " select [FCompany],[FDept],[FPerson],[FPhone] from [DDdatabase].[dbo].[Tab_KVI_AuthSet] where Role='2' and FType='" + strType + "' and FCompany='" + strCompany + "'";

            SqlCommand cmd01 = new SqlCommand(strSql, con);
            SqlDataReader sdr01 = cmd01.ExecuteReader();
            if (sdr01.HasRows)
            {
                while (sdr01.Read())
                {
                    stat = true;
                    reCom = sdr01[0].ToString();
                    reDept = sdr01[1].ToString();
                    rePer = sdr01[2].ToString();
                    rePhone = sdr01[3].ToString();
                    reMesg = "数据提交成功！";
                }
            }
            else
            {
                stat = false;
                reMesg = "未设置管理人员，请联系业务部门反馈！";
            }

            sdr01.Close();

            strSql = " insert into [DDdatabase].[dbo].[Tab_KVI_ApplyList]([ID],[bh],[inputCom],[inputDept],[inputVen],[inputPer],[inputPhone] ,[inputDate] ,[inputType],[inputContent],[inPhotoUrlOne],[inPhotoUrlTwo],[inPhotoUrlTh],[hmark])";
            strSql += " values('" + strID + "','" + strPID + "','" + strCompany + "','" + strDept + "','" + strVen + "','" + strName + "','" + strPhone + "','" + strTime + "','" + strType + "','" + strContent + "','" + strUrlOne + "','" + strUrlTwo + "','" + strUrlTh + "','0')";

            SqlCommand cmd02 = new SqlCommand(strSql, con);
            t1 = cmd02.ExecuteNonQuery();
            if (t1 != 0)
            {
                stat = true;
                reMesg = "数据提交成功！";
            }

            string strAuditID = System.Guid.NewGuid().ToString();

            strSql = " insert into [DDdatabase].[dbo].[Tab_KVI_AuditList]([ID],[PID],[shmc],[FCom],[FDept],[FPer],[FPhone],[ArrDate],[hmark])";
            strSql += " values('" + strAuditID + "','" + strID + "','是否整改','" + reCom + "','" + reDept + "','" + rePer + "','" + rePhone + "','" + strTime + "','0')";

            SqlCommand cmd03 = new SqlCommand(strSql, con);
            t1 = cmd03.ExecuteNonQuery();
            if (t1 != 0)
            {
                stat = true;
                reMesg = "数据提交成功！";
            }


            con.Close();


            string strJson = "{\"stat\":\"" + stat + "\",\"mesg\":\"" + reMesg + "\",\"phone\":\"" + rePhone + "\",\"time\":\"" + System.DateTime.Now.ToString() + "\",\"id\":\"" + strID + "\",\"bh\":\"" + strPID + "\"}";

            Response.Write(strJson);



        }

    }
}