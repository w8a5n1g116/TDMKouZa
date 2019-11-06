using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace TDM
{
	public class jyt_user
	{
        SqlConnection DBCN;//数据库连接对象
        SqlCommand DBCM;//数据库命令对象
        SqlDataAdapter DBDA;//数据填充刷新对象
        SqlDataReader DBDR;//只读数据对象
  

        private void open_db()
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

        public string jyt(string cpy,string bm, string name) 
        {
            string re = "";
            open_db();
            DataTable lst = new DataTable();
            DBCM.CommandText = "select 经营体名称 from tdm_public_personinfo where 公司='" + cpy + "' and 部门='" + bm + "' and 姓名='"+name+"' ";
            DBDA.SelectCommand = DBCM;
            DBDA.Fill(lst);
            close_db();
            if (lst.Rows.Count > 0)
            {
                re = lst.Rows[0]["经营体名称"].ToString();
            }
            return re;
        }
	}
}