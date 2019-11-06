using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace TDM.api.plan
{
    public partial class hb_list : System.Web.UI.Page
    {
        SqlConnection DBCN;//数据库连接对象
        SqlCommand DBCM;//数据库命令对象
        SqlDataAdapter DBDA;//数据填充刷新对象
        SqlDataReader DBDR;//只读数据对象
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.Form["userid"];
            string date = DateTime.Now.ToString("MM");
            yan y = new yan();
            string dd_info = y.user_code(id);
            string re = "none";
            string[] arr = dd_info.Split(',');
            string s = y.status(arr[0]);
            string yue = Request.Form["yue"];
            string year = Request.Form["year"];
            string min_yue = (Convert.ToInt32(yue) - 1).ToString();
            string min_year = year;
            if (min_yue == "0")
            {
                min_yue = "12";
                min_year = (Convert.ToInt32(year) - 1).ToString();
            }
            open_db();
            DataTable lst = new DataTable();
            DBCM.CommandText = "select * from card_lhkinfo where 公司='" + arr[1] + "' and 部门='" + arr[2] + "' and 姓名='"+arr[3]+"' and yue between '" + min_yue + "' and '" + yue + "' and nian between '" + min_year + "' and '" + year + "' order by id desc";
            DBDA.SelectCommand = DBCM;
            DBDA.Fill(lst);
            close_db();
            if (arr[3] == "吴志超")
                s = "主管";
            if (lst.Rows.Count > 0)
            {
                re = "{\"status\":\"" + s + "\",\"list\":[";

                for (int i = 0; i < lst.Rows.Count; i++)
                {
                    re += "{\"id\":\"" + lst.Rows[i]["id"] + "\",\"type\":\"" + lst.Rows[i]["khstyle"] + "\",\"item\":\"" + StringToJson(lst.Rows[i]["workitem"].ToString()) + "\",\"money\":\"" + lst.Rows[i]["smanhour"] + "\",\"text\":\"" + StringToJson(lst.Rows[i]["target"].ToString()) + "\",\"pj_text\":\"" + StringToJson(lst.Rows[i]["standers"].ToString()) + "\",\"zr_name\":\"" + lst.Rows[i]["姓名"] + "\",\"zr_f\":\"" + lst.Rows[i]["经营体"] + "\",\"wc\":\"" + StringToJson(lst.Rows[i]["description"].ToString()) + "\",\"zp\":\"" + lst.Rows[i]["zphour"] + "\",\"sh\":\"" + lst.Rows[i]["shhour"] + "\",\"sh_text\":\"" + StringToJson(lst.Rows[i]["shdescription"].ToString()) + "\",\"date\":\"" + lst.Rows[i]["nian"] + "-" + lst.Rows[i]["yue"] + "\",\"qz\":\"" + lst.Rows[i]["jibie"] + "\",\"fun\":\"lhk_bc(this)\",\"user\":\"" + lst.Rows[i]["姓名"] + "\",\"color\":\"" + color(lst.Rows[i]["lock"].ToString()) + "\"},";
                }
                re = re.Substring(0, re.Length - 1);
                re += "]}";
            }

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
        public string color(string te)
        {
            string re = "#000";
            if (te == "1")
            {
                re = "#0088F5";
            }
            else if (te == "2")
                re = "#E1553E";
            return re;
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