using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace TDM.api.lhk
{
    public partial class make_sl : System.Web.UI.Page
    {
        SqlConnection DBCN;//数据库连接对象
        SqlCommand DBCM;//数据库命令对象
        SqlDataAdapter DBDA;//数据填充刷新对象
        SqlDataReader DBDR;//只读数据对象
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.Form["userid"];
            string date = Request.Form["date"];
            yan y = new yan();
            string dd_info = y.user_info(id);
            string[] arr = dd_info.Split(',');
            string phone = arr[0];
            string email = arr[1];
            string code = y.user_code(phone);
            string re = "";
            if (code.Length > 0)
            {
                string cpy_info = y.cpy_info(code);
                arr = cpy_info.Split(',');
                open_db();
                DBCM.CommandText = "insert into card_typical (dxdescription,khfs,zphour,nian,yue,emname,inputname,hourstyle,coname,departname,二级经营体名称,unitname,IDnumber) values ('" + Request.Form["text"] + "','个人','" + Request.Form["money"] + "','" + arr[2] + "','" + arr[2] + "','常规典型','" + arr[0] + "','" + arr[1] + "','" + arr[3] + "','" + arr[4] + "','" + code + "')";
                DBCM.ExecuteNonQuery();
                close_db();
                re = "success";
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