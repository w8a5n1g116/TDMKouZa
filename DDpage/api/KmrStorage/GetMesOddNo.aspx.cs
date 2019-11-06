using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.KmrStorage
{
    public partial class GetMesOddNo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCompany = Request.Form["strCom"];
            string strDept = Request.Form["strDept"];
            string strVenture = Request.Form["StrVen"];
            string strOdd = Request.Form["strOdd"];


            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.0.6;Initial Catalog=ksfmes;User ID=sa;Password=lan@2mail");
            con.Open();

            //SqlConnection conKcf = new SqlConnection("Data Source=192.168.0.6;Initial Catalog=ksfmes;User ID=sa;Password=lan@2mail");
            //conKcf.Open();

            strSql = "select distinct ";
            if (strCompany.Length == 0)
            {
                strSql += " top(10) ";
            }
            strSql += " 生产订单号, 产品名称+'|'+cast(isNull(铸件号,'') as nvarchar)+'|'+isNull(对应工序,'')+'|'+cast(吨位 as nvarchar)+'|'+cast(数量 as nvarchar) as mx from [ksfmes].[dbo].[TAB_execmes_ShiftInfoAllNew] where 1=1";
            strSql += " and 公司 like '%" + strCompany + "%'";
            strSql += " and 责任部门 like '%" + strDept + "%'";
            strSql += " and 执行班组 like '%" + strVenture + "%'";
            strSql += " and (顾客 like '%"+ strOdd + "%' or 产品名称  like '%"+ strOdd + "%' or 生产订单号 like '%"+ strOdd + "%')";          
            strSql += " and LEN(生产订单号)>0";
            strSql += " and 实际完成日期 is null";          
            strSql += " and  DATEDIFF(month, 计划下达日期, GETDATE()) <= 2";
            strSql += " order by 生产订单号";


            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            string strJson = "{\"data\":[]";
            if (sdr.HasRows)
            {
                strJson = "{\"data\":[";
            }
            while (sdr.Read())
            {

                strJson += "{\"strOdd\":\"" + sdr[0].ToString() + "\",";
                strJson += "\"strTxt\":\"" + StringToJson(sdr[1].ToString()) + "\"},";

            }
            strJson = strJson.Substring(0, strJson.Length - 1);
            strJson = strJson + "]}";

            sdr.Close();
            con.Close();
            Response.Write(strJson);
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

    }
}
