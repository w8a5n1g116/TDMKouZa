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
    public partial class lhk_info : System.Web.UI.Page
    {
        SqlConnection DBCN;//数据库连接对象
        SqlCommand DBCM;//数据库命令对象
        SqlDataAdapter DBDA;//数据填充刷新对象
        SqlDataReader DBDR;//只读数据对象
        protected void Page_Load(object sender, EventArgs e)
        {
            open_db();
            DataTable lst = new DataTable();
            DBCM.CommandText = "select * from  card_lhkinfo where id='"+Request.Form["id"]+"' ";
            DBDA.SelectCommand = DBCM;
            DBDA.Fill(lst);
            close_db();
            string re = "";
            re += "{\"id\":\"" + lst.Rows[0]["id"] + "\",\"type\":\"" + lst.Rows[0]["khstyle"] + "\",\"item\":\"" + lst.Rows[0]["workitem"] + "\",\"money\":\"" + lst.Rows[0]["smanhour"] + "\",\"text\":\"" + StringToJson(lst.Rows[0]["target"].ToString()) + "\",\"pj_text\":\"" + StringToJson(lst.Rows[0]["standers"].ToString()) + "\",\"zr_name\":\"" + lst.Rows[0]["姓名"] + "\",\"zr_f\":\"" + lst.Rows[0]["经营体"] + "\",\"wc\":\"" + StringToJson(lst.Rows[0]["description"].ToString()) + "\",\"zp\":\"" + lst.Rows[0]["zphour"] + "\",\"sh\":\"" + lst.Rows[0]["shhour"] + "\",\"sh_text\":\"" + StringToJson(lst.Rows[0]["shdescription"].ToString()) + "\",\"date\":\"" + lst.Rows[0]["nian"] + "-" + lst.Rows[0]["yue"] + "\",\"qz\":\"" + lst.Rows[0]["jibie"] + "\",\"user\":\"" + lst.Rows[0]["姓名"] + "\"}";
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