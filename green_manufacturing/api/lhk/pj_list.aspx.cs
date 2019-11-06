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
    public partial class pj_list : System.Web.UI.Page
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

            string code = y.user_code(phone);
            string re = "";
            if (code.Length > 0)
            {
                string cpy_info = y.cpy_info(code);
                arr = cpy_info.Split(',');
                string jyt = arr[3];
                string bm = arr[1];
                string cmd = "";
                string status = y.status(code);
                if (status == "主管")
                    cmd = "部门='" + bm + "'";
                else
                    cmd = "二级经营体名称='" + jyt + "'";
                
                open_db();
                DataTable lst = new DataTable();
                string ye = DateTime.Now.ToString("yyyy");
                date = Convert.ToInt32(DateTime.Now.ToString("MM")).ToString();
                DBCM.CommandText = "select * from card_result where " + cmd + " and 月='" + date + "' and 年='" + ye + "' order by rsid desc";
                DBDA.SelectCommand = DBCM;
                DBDA.Fill(lst);
                close_db();

                if (lst.Rows.Count > 0)
                {
                    re = "{\"list\":[";
                    for (int i = 0; i < lst.Rows.Count; i++)
                    {

                        re += "{\"name\":\"" + lst.Rows[i]["姓名"] + "\",\"jxgz\":\"" + lst.Rows[i]["绩效工资"] + "\",\"cejsy\":\"" + lst.Rows[i]["超额净收益"] + "\",\"cejsy\":\"" + lst.Rows[i]["超额净收益"] + "\",\"gwzb\":\"" + lst.Rows[i]["岗位指标"] + "\",\"gwzz\":\"" + lst.Rows[i]["岗位职责"] + "\",\"jhrw\":\"" + lst.Rows[i]["计划任务"] + "\",\"cgdx\":\"" + lst.Rows[i]["常规典型"] + "\",\"zzgl\":\"" + lst.Rows[i]["自主管理"] + "\",\"sgl\":\"" + lst.Rows[i]["6S管理"] + "\",\"px\":\"" + lst.Rows[i]["培训工时"] + "\",\"bzjs\":\"" + lst.Rows[i]["班组建设"] + "\"},";
                    }
                    re = re.Substring(0, re.Length - 1);
                    re += "]}";
                }
            }
            else
                re = "none";
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