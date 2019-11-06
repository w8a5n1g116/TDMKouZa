using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TDM
{
	public partial class user_info : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            string id = Request.Form["userid"];
            yan y = new yan();

            string re = "";
            re = y.user_info(id);
            
            Response.Write(re);
		}
	}
}