using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace green_manufacturing.audit
{
    public partial class index : System.Web.UI.Page
    {
        public string userid = string.Empty;
        public string status = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            userid = Request.QueryString["userid"];
            status = Request.QueryString["status"];
        }
    }
}