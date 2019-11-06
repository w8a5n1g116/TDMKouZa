using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.ITServer
{
    public partial class IT_AssessSubmit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strWhpj = Request.Form["whpj"].ToString();
            string strWcqk = Request.Form["wcqk"].ToString();
            string strId = Request.Form["id"].ToString(); ;
            string strwhPhone = "";
            int t1 = 0;
            Boolean stat = false;

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=BPMS;User ID=tide;Password=lan@2mail");
            con.Open();

            string strtmp = "";
            strtmp= "update [TDM].[dbo].[Maintenance_Plan] set bxclyj='" + strWhpj + "',cljg='" + strWcqk + "',myd='" + strWcqk + "',endTime='" + System.DateTime.Now.ToString() + "'  where primID=" + strId + "";

            SqlCommand cmd01 = new SqlCommand(strtmp, con);
            t1 = cmd01.ExecuteNonQuery();
            if (t1 != 0)
            {
                stat = true;
            }

            if (strWcqk== "抱怨")
            {
                //产生抱怨直接给冯海莹发送消息
                strwhPhone = "18295615032";
            }


            con.Close();

            string strJson = "{\"stat\":\"" + stat + "\",\"phone\":\"" + strwhPhone + "\",\"time\":\"" + System.DateTime.Now.ToString() + "\",\"id\":\"" + strId + "\"}";

            Response.Write(strJson);

        }
    }
}