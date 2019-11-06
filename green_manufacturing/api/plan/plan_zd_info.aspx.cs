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
    public partial class plan_zd_info : System.Web.UI.Page
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
            DBCM.CommandText = "select 类别,工作内容,责任人,完成时限,录入日期,落实方式 from  kocelplan_bmplan where id='" + Request.Form["id"] + "'";
            DBDA.SelectCommand = DBCM;
            DBDA.Fill(lst);
            close_db();
            if (lst.Rows.Count > 0)
            {
                re = "{\"data\":[";
                //for (int i = 0; i < lst.Rows.Count; i++)
                //{
                //    re += "{\"type\":\"" + lst.Rows[i]["类别"] + "\",\"text\":\"" + lst.Rows[i]["工作内容"] + "\",\"zr_name\":\"" + lst.Rows[i]["责任人"] + "\",\"date\":\"" + lst.Rows[i]["完成时限"] + "\",\"enter\":\"" + lst.Rows[i]["录入日期"] + "\",\"ls\":\"" + lst.Rows[i]["落实方式"] + "\"},";
                //    
                //}
                for (int i = 0; i < 6; i++) {
                    re += "{\"type\":\"wbtxt\",\"text\":\"" + lst.Columns[i].ColumnName + "\",\"value\":\"" + StringToJson(lst.Rows[0][lst.Columns[i].ColumnName].ToString()) + "\"},";
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