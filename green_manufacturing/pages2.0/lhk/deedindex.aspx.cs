using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TDM.pages2._0.lhk
{
    public partial class deedindex : System.Web.UI.Page
    {
        public string userid = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            userid = Request.QueryString["userid"];
        }
    }
}