using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.KmrStorage
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string t1 = Request.Form["t1"];
            string t2 = Request.Form["t2"];


            string strJson = "{\"data\":[\"" + t1 + "\",\"" + t2 + "\"],\"name\":\"2\"}";
            Response.Write(strJson);
        }
    }
}