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
    public partial class list : System.Web.UI.Page
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
            string cpy_info = y.user_code(id);
            string[] arr = cpy_info.Split(',');
            string jyt = arr[4];
            string bm = arr[2];
            string year = Request.Form["year"];
            open_db();
            DataTable lst = new DataTable();
            string cmd = "部门='" + bm + "' and 二级经营体名称='" + jyt + "' and 姓名='"+arr[3]+"'";
            DBCM.CommandText = "select * from card_result where " + cmd + " and 月='" + Request.Form["month"] + "' and 年='" + year + "' order by rsid desc";
            DBDA.SelectCommand = DBCM;
            DBDA.Fill(lst);
            close_db();

            if (lst.Rows.Count > 0)
            {                    
                re = "{\"name\":\"" + lst.Rows[0]["姓名"] + "\",\"jxgz\":\"" + lst.Rows[0]["绩效工资"] + "\",\"cejsy\":\"" + lst.Rows[0]["超额净收益"] + "\",\"cejsy\":\"" + lst.Rows[0]["超额净收益"] + "\",\"gwzb\":\"" + lst.Rows[0]["岗位指标"] + "\",\"gwzz\":\"" + lst.Rows[0]["岗位职责"] + "\",\"jhrw\":\"" + lst.Rows[0]["计划任务"] + "\",\"cgdx\":\"" + lst.Rows[0]["常规典型"] + "\",\"zzgl\":\"" + lst.Rows[0]["自主管理"] + "\",\"sgl\":\"" + lst.Rows[0]["6S管理"] + "\",\"px\":\"" + lst.Rows[0]["培训工时"] + "\",\"bzjs\":\"" + lst.Rows[0]["班组建设"] + "\"}"; 
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