using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.KmrStorage
{
    public partial class GetVentureList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCompany = Request.Form["FCompany"].ToString();
            string strDept = Request.Form["FDept"].ToString();
            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();
            SqlCommand cmd = new SqlCommand("select distinct(经营体名称) as mx from [kocelbasedata].[dbo].[tdm_public_personinfo] where 公司='" + strCompany + "' and 部门='"+strDept+"'", con);
            SqlDataReader sdr = cmd.ExecuteReader();
            string strJson = "";
            strJson = "{\"data\":[]";
            if (sdr.HasRows)
            {
                strJson = "{\"data\":[";
            }
            while (sdr.Read())
            {

                strJson += "\"" + sdr[0].ToString() + "\",";

            }
            strJson = strJson.Substring(0, strJson.Length - 1);
            strJson = strJson + "]}";
            sdr.Close();
            con.Close();
            Response.Write(strJson);
        }
    }
}