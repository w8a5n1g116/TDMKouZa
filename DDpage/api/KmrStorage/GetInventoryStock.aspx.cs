using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.KmrStorage
{
    public partial class GetInventoryStock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string strComName = Request.Form["comName"];
            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();
            SqlCommand cmd = new SqlCommand("select distinct(库房名称) as mx from [DataFromSap].[dbo].[View_SAP_SapEhrComStock] where 公司简称 like '%"+ strComName + "%' order by 库房名称", con);
            SqlDataReader sdr = cmd.ExecuteReader();
            string strJson = "";
            strJson = "{\"data\":[";
            while (sdr.Read())
            {
  
                strJson += "\""+ sdr[0].ToString() + "\",";

            }
            strJson = strJson.Substring(0, strJson.Length - 1);
            strJson = strJson+ "]}";
            sdr.Close();
            con.Close();
            Response.Write(strJson);

        }
    }
}