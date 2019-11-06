using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TDM;

namespace DDpage.KmrStorage
{
    public partial class stock_tablist : System.Web.UI.Page
    {
        public String strPhone = string.Empty;
        public String user_info = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            strPhone = Request.QueryString["phone"];
            yan y = new yan();
            user_info = y.user_code(strPhone);
        }
    }
}