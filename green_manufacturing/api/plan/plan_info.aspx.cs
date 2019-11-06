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
    public partial class plan_info : System.Web.UI.Page
    {
        SqlConnection DBCN;//数据库连接对象
        SqlCommand DBCM;//数据库命令对象
        SqlDataAdapter DBDA;//数据填充刷新对象
        SqlDataReader DBDR;//只读数据对象
        protected void Page_Load(object sender, EventArgs e)
        {
            string re = "none";
            open_db();
            DataTable lst = new DataTable();
            DBCM.CommandText = "select B.工作内容,A.完成情况,A.完成,A.汇报日期 from kocelplan_bmplanhb A left join kocelplan_bmplan B on A.pid=B.id  where B.id='" + Request.Form["id"] + "' order by A.汇报日期 desc";
            DBDA.SelectCommand = DBCM;
            DBDA.Fill(lst);
            close_db();
            if (lst.Rows.Count > 0)
            {
                re = "{\"data\":[";
                for (int i = 0; i < lst.Rows.Count; i++) {
                    re += "{\"works\":\""+StringToJson(lst.Rows[i]["工作内容"].ToString())+"\",\"complete_stage\":\""+StringToJson(lst.Rows[i]["完成情况"].ToString())+"\",\"text\":\""+StringToJson(lst.Rows[i]["完成"].ToString())+"\",\"date\":\""+lst.Rows[i]["汇报日期"]+"\"},";
                }
                re = re.Substring(0, re.Length - 1);
                re += "]}";
            }
            Response.Write(re);
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