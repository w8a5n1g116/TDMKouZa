using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace TDM.api.lhk
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
            string yue = Request.Form["yue"];
            string year = Request.Form["year"];
            string min_yue = (Convert.ToInt32(yue) - 1).ToString();
            string min_year = year;
            if (min_yue == "0") {
                min_yue = "12";
                min_year= (Convert.ToInt32(year) - 1).ToString();
            }
            yan y = new yan();
            string code=y.user_code(id);
                string s = y.status(code.Split(',')[0]);
                string[] arr = code.Split(',');
                //string year = DateTime.Now.ToString("yyyy");
                string cmd = "";
                var status = "";
                if (arr[3] == "吴志超")
                    s = "主管";
                if (arr[3] == "白晋成")
                {
                    arr[2] = "共享工业云中心";
                    s = "主管";
                }
                if (s == "主管")
                {
                    cmd = "公司='" + arr[1] + "' and 部门='" + arr[2] + "'and yue between '" + min_yue + "' and '" + yue + "' and nian between '" + min_year + "' and '" + year + "' and khstyle between '岗位职责' and '岗位指标'";
                }
                else if (s == "经理")
                {
                    cmd = "公司='" + arr[1] + "' and 部门='" + arr[2] + "'and 经营体='" + arr[4] + "' and yue between '" + min_yue + "' and '" + yue + "' and nian between '" + min_year + "' and '" + year + "' and khstyle between '岗位职责' and '岗位指标'";
                }
                else
                {
                    cmd = "公司='" + arr[1] + "' and 部门='" + arr[2] + "' and 姓名= '" + arr[3] + "' and yue between '" + min_yue + "' and '" + yue + "' and nian between '" + min_year + "' and '" + year + "' and khstyle between '岗位职责' and '岗位指标'";
                }
              
                open_db();
                DataTable lst = new DataTable();
                DBCM.CommandText = "select * from  card_lhkinfo where "+cmd+" order by id desc ";
                DBDA.SelectCommand = DBCM;
                DBDA.Fill(lst);
                close_db();

                if (lst.Rows.Count > 0)
                {
                    status = "{\"status\":\"" + s + "\",\"list\":[";
                    for (int i = 0; i < lst.Rows.Count; i++)
                    {
                        string name = "N";
                        if (lst.Rows[i]["姓名"].ToString() == arr[3])
                            name = "Y";
                        if (s == "经理")
                        {
                            string fun = "lhk_sh(this)";
                            if (lst.Rows[i]["lock"].ToString() == "2")
                                fun = "tt(this)";
                            status += "{\"name\":\"" + name + "\",\"id\":\"" + lst.Rows[i]["id"] + "\",\"type\":\"" + lst.Rows[i]["khstyle"] + "\",\"item\":\"" + lst.Rows[i]["workitem"] + "\",\"money\":\"" + lst.Rows[i]["smanhour"] + "\",\"text\":\"" + StringToJson(lst.Rows[i]["target"].ToString()) + "\",\"pj_text\":\"" + StringToJson(lst.Rows[i]["standers"].ToString()) + "\",\"zr_name\":\"" + lst.Rows[i]["姓名"] + "\",\"zr_f\":\"" + lst.Rows[i]["经营体"] + "\",\"wc\":\"" + StringToJson(lst.Rows[i]["description"].ToString()) + "\",\"zp\":\"" + lst.Rows[i]["zphour"] + "\",\"sh\":\"" + lst.Rows[i]["shhour"] + "\",\"sh_text\":\"" + StringToJson(lst.Rows[i]["shdescription"].ToString()) + "\",\"date\":\"" + lst.Rows[i]["nian"] + "-" + lst.Rows[i]["yue"] + "\",\"qz\":\"" + lst.Rows[i]["jibie"] + "\",\"fun\":\"" + fun + "\" ,\"user\":\"" + lst.Rows[i]["姓名"] + "\",\"color\":\"" + color(lst.Rows[i]["lock"].ToString()) + "\"},";
                        }
                        else if (s == "主管")
                        {
                            status += "{\"name\":\"" + name + "\",\"id\":\"" + lst.Rows[i]["id"] + "\",\"type\":\"" + lst.Rows[i]["khstyle"] + "\",\"item\":\"" + lst.Rows[i]["workitem"] + "\",\"money\":\"" + lst.Rows[i]["smanhour"] + "\",\"text\":\"" + StringToJson(lst.Rows[i]["target"].ToString()) + "\",\"pj_text\":\"" + StringToJson(lst.Rows[i]["standers"].ToString()) + "\",\"zr_name\":\"" + lst.Rows[i]["姓名"] + "\",\"zr_f\":\"" + lst.Rows[i]["经营体"] + "\",\"wc\":\"" + StringToJson(lst.Rows[i]["description"].ToString()) + "\",\"zp\":\"" + lst.Rows[i]["zphour"] + "\",\"sh\":\"" + lst.Rows[i]["shhour"] + "\",\"sh_text\":\"" + StringToJson(lst.Rows[i]["shdescription"].ToString()) + "\",\"date\":\"" + lst.Rows[i]["nian"] + "-" + lst.Rows[i]["yue"] + "\",\"qz\":\"" + lst.Rows[i]["jibie"] + "\",\"fun\":\"lhk_sh(this)\",\"user\":\"" + lst.Rows[i]["姓名"] + "\",\"color\":\"" + color(lst.Rows[i]["lock"].ToString()) + "\"},";
                        }
                        else
                        {
                            status += "{\"name\":\"" + name + "\",\"id\":\"" + lst.Rows[i]["id"] + "\",\"type\":\"" + lst.Rows[i]["khstyle"] + "\",\"item\":\"" + lst.Rows[i]["workitem"] + "\",\"money\":\"" + lst.Rows[i]["smanhour"] + "\",\"text\":\"" + StringToJson(lst.Rows[i]["target"].ToString()) + "\",\"pj_text\":\"" + StringToJson(lst.Rows[i]["standers"].ToString()) + "\",\"zr_name\":\"" + lst.Rows[i]["姓名"] + "\",\"zr_f\":\"" + lst.Rows[i]["经营体"] + "\",\"wc\":\"" + StringToJson(lst.Rows[i]["description"].ToString()) + "\",\"zp\":\"" + lst.Rows[i]["zphour"] + "\",\"sh\":\"" + lst.Rows[i]["shhour"] + "\",\"sh_text\":\"" + StringToJson(lst.Rows[i]["shdescription"].ToString()) + "\",\"date\":\"" + lst.Rows[i]["nian"] + "-" + lst.Rows[i]["yue"] + "\",\"qz\":\"" + lst.Rows[i]["jibie"] + "\",\"fun\":\"lhk_bc(this)\",\"user\":\"" + lst.Rows[i]["姓名"] + "\",\"color\":\"" + color(lst.Rows[i]["lock"].ToString()) + "\"},";
                        }
                    }
                    status = status.Substring(0, status.Length - 1);
                    status += "]}";
                }
           
            try
            {
                if (status != "none")
                {
                    JObject jo = (JObject)JsonConvert.DeserializeObject(status);
                    Response.Write(jo);
                }
                else 
                {
                    Response.Write(status);
                }

            }
            catch { }
            
        }

        public string color(string te) 
        {
            string re="#000";
            if (te == "1")
            {
                re = "#0088F5";
            }
            else if (te == "2")
                re = "#FD2515";
            return re;
        }
        public static String StringToJson(String s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\"");
                        break;
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '/':
                        sb.Append("\\/");
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString();
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