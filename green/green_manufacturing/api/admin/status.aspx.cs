using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Net;
using green_manufacturing.admin;


namespace green_manufacturing.api.admin
{
    public partial class status : System.Web.UI.Page
    {
        SqlConnection DBCN;//数据库连接对象
        SqlCommand DBCM;//数据库命令对象
        SqlDataAdapter DBDA;//数据填充刷新对象
        SqlDataReader DBDR;//只读数据对象
        protected void Page_Load(object sender, EventArgs e)
        {
            string userid = Request.Form["id"];
            string cmd = "";
            open_db();
            DataTable lst = new DataTable();
            DBCM.CommandText = "select * from [DDdatabase].[dbo].[view_bpms_user_status] where 联系电话='" + userid + "'";
            DBDA.SelectCommand = DBCM;
            DBDA.Fill(lst);
            if (lst.Rows.Count > 0)
            {
                cmd = "update [DDdatabase].[dbo].[view_bpms_user_status] set 权限='" + Request.Form["text"] + "' where 联系电话='" + userid + "'";
            }
            else
            {
                userinfo y = new userinfo();
                string[] arr = y.user_code(userid).Split(',');
                cmd = "insert into [DDdatabase].[dbo].[view_bpms_user_status] (公司,部门,班组,姓名,联系电话,经营体,权限) values ('" + arr[1] + "','" + arr[2] + "','" + arr[5] + "','" + arr[3] + "','" + userid + "','" + arr[4] + "','" + Request.Form["text"] + "')";
            }
            DBCM.CommandText = cmd;
            DBCM.ExecuteNonQuery();
            close_db();
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