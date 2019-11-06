using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TDM
{
	public class yan
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
            DBCN.ConnectionString = "Data Source=192.168.0.186;Initial Catalog=kocelcard;User ID=sa;password=lan@2mail";
            //打开数据库连接
            DBCN.Open();
            //设置数据库命令对象使用哪个连接来执行.
            DBCM.Connection = DBCN;
        }

        private void open_db1()
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

        public List<string> deportment_name_list()
        {
            return null;
        }

        public string user_code(string phone) 
        {
            string re = "";
            open_db1();
            DataTable lst = new DataTable();
            DBCM.CommandText = "select * from tdm_public_personinfo where [职员状态]='在职' and 联系电话='" + phone+"' ";
            DBDA.SelectCommand = DBCM;
            DBDA.Fill(lst);
            close_db();
            if (lst.Rows.Count > 0) 
            {
                re = lst.Rows[0]["身份证号"].ToString() + ","+ lst.Rows[0]["公司"].ToString() + "," + lst.Rows[0]["部门"].ToString() + "," + lst.Rows[0]["姓名"].ToString() + "," + lst.Rows[0]["经营体名称"].ToString() + "," + lst.Rows[0]["班组"].ToString() + "," + lst.Rows[0]["经营体ERPID"].ToString() + "," + lst.Rows[0]["财务成本中心编码"].ToString();
            }
            return re;
        }

        public string user_card(string phone)
        {
            string re = "";
            open_db1();
            DataTable lst = new DataTable();
            DBCM.CommandText = "select cardno from tdm_public_personinfo where  [职员状态]='在职' and 联系电话='" + phone + "' ";
            DBDA.SelectCommand = DBCM;
            DBDA.Fill(lst);
            close_db();
            if (lst.Rows.Count > 0)
            {
                re = lst.Rows[0]["cardno"].ToString();
            }
            return re;
        }
        public string user_info(string userid) 
        {
            string re = "";
            string access_token = EnterpriseBusiness.GetToken2(Config.AppKey,Config.AppSecret);
            string url = "https://oapi.dingtalk.com/user/get?access_token=" + access_token + "&userid=" + userid;
            string result = HttpHelper.Get(url);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            re = jo["mobile"].ToString();
            return re;
        }

        public string status(string code) 
        {
            string re = "";
            open_db();
            DataTable lst = new DataTable();
            DBCM.CommandText = "select * from card_employee where 身份证号='" + code + "' ";
            DBDA.SelectCommand = DBCM;
            DBDA.Fill(lst);
            close_db();
            if (lst.Rows.Count > 0) 
            {
                re = lst.Rows[0]["权限"].ToString();
            }
            return re;
        }

        public string cpy_info(string code) 
        {
            string re = "";
            open_db1();
            DataTable lst = new DataTable();
            DBCM.CommandText = "select * from tdm_public_personinfo where 身份证号='" + code + "' ";
            DBDA.SelectCommand = DBCM;
            DBDA.Fill(lst);
            close_db();
            if (lst.Rows.Count > 0)
            {
                re = lst.Rows[0]["公司"].ToString() + "," + lst.Rows[0]["部门"].ToString() + "," + lst.Rows[0]["姓名"].ToString() + "," + lst.Rows[0]["经营体名称"].ToString() + "," + lst.Rows[0]["班组"].ToString();
            }
            return re;
        }
        
	}
}