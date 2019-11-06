using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


namespace TDM.api.plan
{
    public partial class hb : System.Web.UI.Page
    {
        SqlConnection DBCN;//数据库连接对象
        SqlCommand DBCM;//数据库命令对象
        SqlDataAdapter DBDA;//数据填充刷新对象
        SqlDataReader DBDR;//只读数据对象
        protected void Page_Load(object sender, EventArgs e)
        {
            open_db();
            DBCM.CommandText = "insert into kocelplan_bmplanhb (pid,完成情况,完成,汇报日期) values ('" + Request.Form["code"] + "','" + Request.Form["text"] + "','" + Request.Form["type"] + "','" + DateTime.Now + "')";
            DBCM.ExecuteNonQuery();
            if (Request.Form["type"] == "Yes") {
                DBCM.CommandText = "UPDATE  kocelplan_bmplan SET wcflag = '1'  WHERE id = '" + Request.Form["code"] + "'";
                DBCM.ExecuteNonQuery();
            }
            close_db();
            string re = "success";
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