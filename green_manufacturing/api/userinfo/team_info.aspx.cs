using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace TDM.api.userinfo
{
    public partial class team_info : System.Web.UI.Page
    {
        SqlConnection DBCN;//数据库连接对象
        SqlCommand DBCM;//数据库命令对象
        SqlDataAdapter DBDA;//数据填充刷新对象
        SqlDataReader DBDR;//只读数据对象
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.Form["userid"];
            string re = "none";
            yan y = new yan();
            string[] arr = y.user_code(id).Split(',');
            string code = arr[0];
            string s = y.status(code);
            string cmd = "公司='" + arr[1] + "' and 部门='" + Request.Form["bm"] + "' ";
            open_db();
            DataTable lst = new DataTable();
            DBCM.CommandText = "select 姓名,岗位指标,超额净收益,工时合计 from card_result where " + cmd + " and 月='" + Request.Form["month"] + "' and 年='" + Request.Form["year"] + "' order by rsid desc";
            DBDA.SelectCommand = DBCM;
            DBDA.Fill(lst);
            close_db();
            if (lst.Rows.Count > 0)
            {
                re = "{\"team\":\"" + Request.Form["bm"] + "\",\"data\":[";
                for (int i = 0; i < lst.Rows.Count; i++)
                    re += "{\"name\":\"" + lst.Rows[i]["姓名"] + "\",\"gwzb\":\"" + lst.Rows[i]["岗位指标"] + "\",\"cejsy\":\"" + lst.Rows[i]["超额净收益"] + "\",\"gshj\":\"" + lst.Rows[i]["工时合计"] + "\"},";
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
    }
}