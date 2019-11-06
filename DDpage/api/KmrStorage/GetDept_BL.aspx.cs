using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TDM;

namespace DDpage.api.KmrStorage
{
    public partial class GetDept_BL : System.Web.UI.Page
    {
        yan y = new yan();
        protected void Page_Load(object sender, EventArgs e)
        {
            string strJson = "{\"data\":[";

            List<string> namelist = y.deportment_name_list();
            
            foreach(var name in namelist)
            {
                strJson += "{\"dept\":\"" + name + "\"},";
            }
                           
            strJson = strJson.Substring(0, strJson.Length - 1);
            strJson = strJson + "]}";

            Response.Write(strJson);

        }

        public static String StringToJson(String s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\"");
                        break;
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '/':
                        sb.Append("\\/");
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString();
        }

    }
}