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
    public partial class bm_plan_list : System.Web.UI.Page
    {
        SqlConnection DBCN;//数据库连接对象
        SqlCommand DBCM;//数据库命令对象
        SqlDataAdapter DBDA;//数据填充刷新对象
        SqlDataReader DBDR;//只读数据对象
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.Form["userid"];
            string status = "none";
            string end = DateTime.Now.ToString("MM");
            int month = Convert.ToInt32(end) - 1;
            string start = month.ToString();
            string year = DateTime.Now.ToString("yyyy");
            if (month < 10)
                start = "0" + start;
            if (month < 1)
            {
                start = "12";
                year = (Convert.ToInt32(year) - 1).ToString();
            }  
            yan y = new yan();
            string[] arr = y.user_code(id).Split(',');
            string cmd = "公司='" + arr[1] + "' and 主管部门='" + Request.Form["bm"] + "' and YEAR(完成时限)='" + year + "' and MONTH(完成时限) between '" + start + "' and '" + end + "' and pflag='false' ";
            open_db();
            DataTable ls = new DataTable();
            DBCM.CommandText = "select * from kocelplan_bmplan where " + cmd + " order by 完成时限 asc";
            DBDA.SelectCommand = DBCM;
            DBDA.Fill(ls);
           
            close_db();
            if (ls.Rows.Count > 0)
            {
                status = "{\"status\":\"" + arr[2] + "\",\"data\":[";
                status += data_list(ls, arr[3]);
                status += "]}";
            }

            Response.Write(status);
        }


        public string data_list(DataTable lst, string id)
        {
            string re = "";
            for (int i = 0; i < lst.Rows.Count; i++)
            {
                string date = Convert.ToDateTime(lst.Rows[i]["完成时限"]).ToString("yyyy-MM-dd");
                string type = st(lst.Rows[i]["wcflag"].ToString(), lst.Rows[i]["flag"].ToString(), lst.Rows[i]["完成时限"].ToString());
                string name = "N";
                if (lst.Rows[i]["责任人"].ToString() == id)
                    name = "Y";
                re += "{\"userid\":\"" + name + "\",\"id\":\"" + lst.Rows[i]["id"] + "\",\"plan_type\":\"" + lst.Rows[i]["类别"] + "\",\"name\":\"" + lst.Rows[i]["责任人"] + "\",\"date\":\"" + date + "\",\"text\":\"" + StringToJson(lst.Rows[i]["工作内容"].ToString()) + "\",\"type\":\"" + type + "\"},";
            }
            re = re.Substring(0, re.Length - 1);
            return re;
        }

        public string st(string wc, string flag, string date)
        {
            string re = "";
            if (wc == "0")
            {
                if (flag == "False")
                {
                    re = "制定";
                }
                else
                {
                    re = "进行";
                    TimeSpan t = DateTime.Now - Convert.ToDateTime(date);
                    if (t.Days > 1)
                    {
                        re = "逾期";
                    }
                }
            }
            else
            {
                re = "完成";
            }
            return re;
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

        private void open_db1()
        {
            //实例化对象
            DBCN = new SqlConnection();
            DBCM = new SqlCommand();
            DBDA = new SqlDataAdapter();
            //设置数据库连接字符串x
            DBCN.ConnectionString = "Data Source=192.168.0.186;Initial Catalog=kocelbasedata;User ID=sa;password=lan@2mail";
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