using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace green_manufacturing.api.Public
{
    public partial class userinfo : System.Web.UI.Page
    {
        SqlConnection DBCN;//数据库连接对象
        SqlCommand DBCM;//数据库命令对象
        SqlDataAdapter DBDA;//数据填充刷新对象
        SqlDataReader DBDR;//只读数据对象
        protected void Page_Load(object sender, EventArgs e)
        {
            string re = "none";
            string phone = Request.Form["userid"];
            open_db();
            DataTable lst = new DataTable();
            DBCM.CommandText = "select A.公司,A.部门,A.经营体名称 as 经营体,A.班组,A.姓名,A.联系电话,B.权限,B.钉钉员工编号 from [kocelbasedata].[dbo].[tdm_public_personinfo] A left join [DDdatabase].[dbo].[view_bpms_user_status] B on A.联系电话=B.联系电话 where A.联系电话='"+phone+"'";
            DBDA.SelectCommand = DBCM;
            DBDA.Fill(lst);
            close_db();
            if (lst.Rows.Count > 0) {
                re = "{\"company\":\"" + lst.Rows[0]["公司"] + "\",\"bm\":\"" + lst.Rows[0]["部门"] + "\"\"jyt\":\"" + lst.Rows[0]["经营体"] + "\",\"bz\":\"" + lst.Rows[0]["班组"] + "\",\"name\":\"" + lst.Rows[0]["姓名"] + "\",\"phone\":\"" + lst.Rows[0]["联系电话"] + "\",\"status\":\"" + lst.Rows[0]["权限"] + "\",\"ddcode\":\"" + lst.Rows[0]["钉钉员工编号"] + "\"}";
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