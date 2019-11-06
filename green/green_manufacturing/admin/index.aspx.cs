using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace green_manufacturing.admin
{
    public partial class index : System.Web.UI.Page
    {
        SqlConnection DBCN;//数据库连接对象
        SqlCommand DBCM;//数据库命令对象
        SqlDataAdapter DBDA;//数据填充刷新对象
        SqlDataReader DBDR;//只读数据对象
        public string jyt_list = string.Empty;
        public string status = string.Empty;
        public string userid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string re = "none";
            userid = Request.QueryString["userid"];
            string status = Request.QueryString["status"];
            if (userid == null)
                return;
            open_db();
            DataTable lst = new DataTable();
            DBCM.CommandText = "select A.经营体名称 as jyt, A.姓名 as name,A.联系电话 as phone,B.权限 as status from [kocelbasedata].[dbo].[tdm_public_personinfo] A left join [DDdatabase].[dbo].[view_bpms_user_status] B on A.联系电话=B.联系电话 where A.经营体名称=(select 经营体名称 from [kocelbasedata].[dbo].[tdm_public_personinfo] where  联系电话='" + userid + "')";
            DBDA.SelectCommand = DBCM;
            DBDA.Fill(lst);
            close_db();
            if (lst.Rows.Count > 0) {
                re = "{\"data\":[";
                for (int i = 0; i < lst.Rows.Count; i++) {
                    re += "{";
                    for (int j = 0; j < lst.Columns.Count; j++)
                    {
                        if (lst.Columns[j].ColumnName=="status"){
                                if (lst.Rows[i][lst.Columns[j].ColumnName] == DBNull.Value)
                                {
                                    re += "\"" + lst.Columns[j].ColumnName + "\":\"员工\",";
                                }
                                else
                                    re += "\"" + lst.Columns[j].ColumnName + "\":\"" + lst.Rows[i][lst.Columns[j].ColumnName] + "\",";
                            }
                        else
                            re += "\"" + lst.Columns[j].ColumnName + "\":\"" + lst.Rows[i][lst.Columns[j].ColumnName] + "\",";
                    }
                    re = re.Substring(0, re.Length - 1);
                    re += "},";
                }
                re = re.Substring(0, re.Length - 1);
                re+="]}";
            }
            jyt_list = re;
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