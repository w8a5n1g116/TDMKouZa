using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace TDM.api.lhk
{
    public partial class gr_list : System.Web.UI.Page
    {
        SqlConnection DBCN;//数据库连接对象
        SqlCommand DBCM;//数据库命令对象
        SqlDataAdapter DBDA;//数据填充刷新对象
        SqlDataReader DBDR;//只读数据对象
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.Form["userid"];
            string yue = Request.Form["yue"];
            string year = Request.Form["year"];
            yan y = new yan();
           
            string code = y.user_code(id);

            string re = "";
            if (code.Length > 0)
            {
                string cpy_info = y.cpy_info(code.Split(',')[0]);
                string[] arr = cpy_info.Split(',');
                string jyt = arr[3];
                string bm = arr[1];
                string cmd = "";
                open_db();
                DataTable lst = new DataTable();
                DBCM.CommandText = "select * from card_typical where coname='" + arr[0] + "' and departname='" + arr[1] + "' and yue='" + yue + "' and nian='" + year + "' order by dxid desc";
                DBDA.SelectCommand = DBCM;
                DBDA.Fill(lst);
                close_db();

                if (lst.Rows.Count > 0)
                {
                    re = "{\"username\":\""+arr[2]+"\",\"list\":[";
                    for (int i = 0; i < lst.Rows.Count; i++)
                    {
                        re += "{\"inputname\":\"" + lst.Rows[i]["inputname"].ToString() + "\",\"text\":\"" + StringToJson(lst.Rows[i]["dxdescription"].ToString()) + "\",\"zp\":\"" + lst.Rows[i]["zphour"] + "\",\"sh\":\"" + lst.Rows[i]["shhour"] + "\",\"emname\":\"" + lst.Rows[i]["emname"].ToString() + "\"},";
                    }
                    re = re.Substring(0, re.Length - 1);
                    re += "]}";
                }
            }
            else
                re = "none";
            Response.Write(re);
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


        private void open_db()
        {
            //实例化对象
            DBCN = new SqlConnection();
            DBCM = new SqlCommand();
            DBDA = new SqlDataAdapter();
            //设置数据库连接字符串x
            DBCN.ConnectionString = "Data Source=192.168.0.186;Initial Catalog=kocelcard;User ID=sa;password=lan@2mail";
            //打开数据库连接
            DBCN.Open();
            //设置数据库命令对象使用哪个连接来执行.
            DBCM.Connection = DBCN;
        }

        //数据库关闭方法
        private void close_db()
        {
            //关闭数据库连接
            DBCN.Close();
            //销毁无用对象释放资源
            DBCN.Dispose();
            DBCM.Dispose();
            DBDA.Dispose();
        }
    }

}