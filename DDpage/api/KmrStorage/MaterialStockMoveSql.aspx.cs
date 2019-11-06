using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.KmrStorage
{
    public partial class MaterialStockMoveSql : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string FmaterialCode = Request["Code"];
            string FLocationEnd = Request["kcdd"];
            string FKeeperPhone = Request["phone"];
            string MAmount = Request["Amount"];

            bool Result = false;

            DataTable lst = new DataTable();
            if (FmaterialCode != "" && FmaterialCode != null && FLocationEnd != "" && FLocationEnd != null && FKeeperPhone != "" && FKeeperPhone != null)
            {

                string strSql = "EXEC [dbo].[Pro_Tab_KocelApp_Kmr_StockMove]     '" + FmaterialCode + "','" + FLocationEnd + "','" + FKeeperPhone + "','" + MAmount + "'";
                SqlConnection sqlConn = GetDatabaseConnection("BPMS");
                Result = InsertDataToDatabase(strSql, sqlConn);

            }

            Response.Write(Result);
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
        public static SqlConnection GetDatabaseConnection(string strTempDatabaseName)
        {
            #region 获得0.186数据库的连接

            try
            {
                // 服务器名：192.168.0.6; 用户名：sa; 密码：sa; 数据库名:TDM
                SqlConnection sqlConn = new SqlConnection("Server=192.168.0.186;database=" + strTempDatabaseName + ";uid=sa;pwd=lan@2mail");
                sqlConn.Open();

                return sqlConn;
            }
            catch (System.Exception ex)
            {
                return null;
            }

            #endregion
        }


        /// <summary>
        /// 根据Sql语句向数据库中插入数据
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="sqlConn">Sql连接</param>
        /// <returns>是否插入成功</returns>
        public static bool InsertDataToDatabase(string strSql, SqlConnection sqlConn)
        {
            #region 根据Sql语句向数据库中插入数据

            try
            {
                if (null != sqlConn)
                {
                    SqlCommand cmd = new SqlCommand(strSql, sqlConn);
                    cmd.ExecuteNonQuery();
                    cmd.CommandTimeout = 180;
                    sqlConn.Close();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }

            #endregion
        }
    }
}