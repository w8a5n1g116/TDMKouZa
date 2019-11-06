using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace green_manufacturing
{
    public class pcdata
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
            DBCN.ConnectionString = "Data Source=192.168.0.186;Initial Catalog=BPMS;User ID=sa;password=lan@2mail";
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

        public string datet(string date)
        {
            string re = "";
            if (date.Length > 0)
            {
                DateTime zg = Convert.ToDateTime(date);
                re = zg.ToString("yyyy年MM月dd日") + "\",\"color\":\"txt";
                try
                {
                    TimeSpan t = DateTime.Now - zg;
                    if (t.Days >= 1)
                    {
                        re = "逾期" + t.Days.ToString() + "天\",\"color\":\"txt_r";
                    }
                }
                catch (Exception ex) { }
            }
            else
                re = "\",\"color\":\"txt";
            return re;
        }

        public string sh_user_list(string name, string bm, int start, int end) 
        {
            string re = "";
            open_db();
            DataTable lst = new DataTable();
            DBCM.CommandText = "select * from (select *,ROW_NUMBER() OVER (order by SortCode desc) AS  ROWNUM  from  Tab_EHS_Corrections_Info where CheckUnit = '" + bm + "' and CheckPerson='" + name + "' and IsClose is null) t where  ROWNUM   between '" + start + "' and  '" + end + "' order by SortCode desc ";
            DBDA.SelectCommand = DBCM;
            DBDA.Fill(lst);
            close_db();
            if (lst.Rows.Count > 0)
            {
                re = "{\"list\":[";
                string time = "";
                string date = "";
                for (int i = 0; i < lst.Rows.Count; i++)
                {
                    time = Convert.ToDateTime(lst.Rows[i]["EntryTime"]).ToString("yyyy年MM月dd日");
                    date = lst.Rows[i]["CorrectionsDate"].ToString();
                    date = datet(date);
                    re += "{\"id\":\"" + lst.Rows[i]["ID"] + "\",\"question_type\":\"" + lst.Rows[i]["Class"] + "\",\"date\":\"" + time + "\",\"zr_class\":\"" + lst.Rows[i]["DutyDepart"] + "\",\"zr_name\":\"" + lst.Rows[i]["DutyPerson"] + "\",\"zg_date\":\"" + date + "\"},";
                }
            
            }
            if (re.Length>0)
            {
                re = re.Substring(0, re.Length - 1);
                re += "]}";
            }
            return re;
        }
    }
}