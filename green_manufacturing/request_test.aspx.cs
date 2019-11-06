using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TDM
{
    public partial class request_test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string get = Request.QueryString["text"];
            Response.Write("get传输参数为："+get);
        }
    }
}