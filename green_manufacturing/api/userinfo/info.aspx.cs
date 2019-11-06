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
    public partial class info : System.Web.UI.Page
    {
        SqlConnection DBCN;//数据库连接对象
        SqlCommand DBCM;//数据库命令对象
        SqlDataAdapter DBDA;//数据填充刷新对象
        SqlDataReader DBDR;//只读数据对象
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.Form["userid"];
            open_db();
            DataTable lst = new DataTable();
            DBCM.CommandText = "select * from tdm_public_personinfo where 联系电话='" + id + "' ";
            DBDA.SelectCommand = DBCM;
            DBDA.Fill(lst);
            close_db();
            string re = "";
            if (lst.Rows.Count > 0) 
            {
                re = "{\"name\":\"" + lst.Rows[0]["姓名"] + "\",\"sex\":\"" + lst.Rows[0]["性别"] + "\",\"phone\":\"" + lst.Rows[0]["联系电话"] + "\",\"marry\":\"" + lst.Rows[0]["婚姻状况"] + "\",\"home\":\"" + lst.Rows[0]["籍贯"] + "\",\"br_day\":\"" + Convert.ToDateTime(lst.Rows[0]["出生日期"]).ToString("yyyy年MM月dd日") + "\",\"sf_card\":\"" + lst.Rows[0]["身份证地址"] + "\",\"hk_ad\":\"" + lst.Rows[0]["户口所在地"] + "\",\"rsda\":\"" + lst.Rows[0]["人事档案所在地"] + "\",\"zc\":\"" + lst.Rows[0]["职称"] + "\",\"zc_date\":\"" + lst.Rows[0]["职称授予时间"] + "\",\"jn\":\"" + lst.Rows[0]["技能级别"] + "\",\"jk\":\"" + lst.Rows[0]["健康级别"] + "\",\"pz\":\"" + lst.Rows[0]["品质级别"] + "\",\"cpy\":\"" + lst.Rows[0]["公司"] + "\",\"bm\":\"" + lst.Rows[0]["部门"] + "\",\"jyt\":\"" + lst.Rows[0]["经营体名称"] + "\",\"gw\":\"" + lst.Rows[0]["岗位名称"] + "\",\"gwlb\":\"" + lst.Rows[0]["岗位类别"] + "\"}";
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
    }
}