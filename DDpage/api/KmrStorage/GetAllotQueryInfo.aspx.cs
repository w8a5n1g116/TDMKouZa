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
    public partial class GetAllotQueryInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCom = Request.Form["Company"];
            string strDept = Request.Form["Dept"];
            string strVen = Request.Form["Ven"];
            string strName = Request.Form["Name"];
            string strOutStock = Request.Form["OutStock"];
            string strInStock = Request.Form["InStock"];
            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();

            strSql = " SELECT [PickID],[PLinkID],[PCompany],[PDept],[PVenture] ,[PName],[PTime],[PickStat]";
            strSql += " FROM [DDdatabase].[dbo].[Tab_kocelApp_Kmr_AllotMaterial] where 1=1";
            strSql += "  and [PCompany] like '%" + strCom + "%'";
            strSql += "  and [PDept] like '%" + strDept + "%'";
            strSql += "  and [PVenture] like '%" + strVen + "%'";
            strSql += "  and [PName] like '%" + strName + "%'";
            strSql += "  and PLinkID in (select distinct PLinkID FROM [DDdatabase].[dbo].[Tab_kocelApp_Kmr_AllotDetail] where MOutStockName like '%" + strOutStock + "%')";
            strSql += "  and PLinkID in (select distinct PLinkID FROM [DDdatabase].[dbo].[Tab_kocelApp_Kmr_AllotDetail] where MInStockName like '%" + strInStock + "%')";
            strSql += "  order by [PTime] desc";

            //strSql += " and MStock like '%" + strStName + "%'";


            //库管员查询权限
            //strSql += " and PName like '%" + strSName + "%'";

            //库管员查询权限

            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            string strJson = "{\"data\":[]";
            if (sdr.HasRows)
            {
                strJson = "{\"data\":[";
            }
            while (sdr.Read())
            {

                strJson += "{\"PID\":\"" + sdr[0].ToString() + "\",";
                strJson += "\"PLID\":\"" + sdr[1].ToString() + "\",";
                strJson += "\"PCom\":\"" + sdr[2].ToString() + "\",";
                strJson += "\"PDept\":\"" + sdr[3].ToString() + "\",";
                strJson += "\"PVen\":\"" + sdr[4].ToString() + "\",";
                strJson += "\"PName\":\"" + sdr[5].ToString() + "\",";
                strJson += "\"PTime\":\"" + sdr[6].ToString() + "\",";
                strJson += "\"PStat\":\"" + sdr[7].ToString() + "\"},";


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
