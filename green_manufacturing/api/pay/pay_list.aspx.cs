using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


namespace TDM.api.pay
{
    public partial class pay_list : System.Web.UI.Page
    {
        SqlConnection DBCN;//数据库连接对象
        SqlCommand DBCM;//数据库命令对象
        SqlDataAdapter DBDA;//数据填充刷新对象
        SqlDataReader DBDR;//只读数据对象
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.Form["userid"];
            string date = DateTime.Now.ToString("MM");
            yan y = new yan();
            string code = y.user_card(id);
            string re = "none";
            string yr = DateTime.Now.ToString("yyyy");
            if (code.Length > 0)
            {
                open_db();
                DataTable lst = new DataTable();
                DBCM.CommandText = "select * from card_xiaof where card_sn='"+code+"' and 月='"+Request.Form["month"]+"' and 年='"+Request.Form["year"]+"' order by id desc";
                DBDA.SelectCommand = DBCM;
                DBDA.Fill(lst);
                close_db();
                if (lst.Rows.Count > 0)
                {
                    re = "{\"list\":[";
                    for (int i = 0; i < lst.Rows.Count; i++)
                    {
                        re += "{\"date\":\"" + lst.Rows[i]["消费时间"] + "\",\"pay\":\"" + Math.Round(Convert.ToDecimal(lst.Rows[i]["消费金额"].ToString()), 2) + "\",\"less\":\"" + Math.Round(Convert.ToDecimal(lst.Rows[i]["卡上余额"].ToString()), 2) + "\",\"code\":\"" + lst.Rows[i]["消费机号"] + "\"},";
                    }
                    re = re.Substring(0, re.Length - 1);
                    re += "]}";
                }
                else {
                    open_db();
                    DataTable lsts = new DataTable();
                    DBCM.CommandText = "select top 1 * from card_xiaof where card_sn='" + code + "' order by 消费时间 desc";
                    DBDA.SelectCommand = DBCM;
                    DBDA.Fill(lsts);
                    close_db();
                    re = "{\"list\":[";
                    for (int i = 0; i < lsts.Rows.Count; i++)
                    {
                        re += "{\"date\":\"" + lsts.Rows[i]["消费时间"] + "\",\"pay\":\"" + Math.Round(Convert.ToDecimal(lsts.Rows[i]["消费金额"].ToString()), 2) + "\",\"less\":\"" + Math.Round(Convert.ToDecimal(lsts.Rows[i]["卡上余额"].ToString()), 2) + "\",\"code\":\"" + lsts.Rows[i]["消费机号"] + "\"},";
                    }
                    re = re.Substring(0, re.Length - 1);
                    re += "]}";
                }
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