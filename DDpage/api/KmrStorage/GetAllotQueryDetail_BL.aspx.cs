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
    public partial class GetAllotQueryDetail_BL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strID = Request.Form["LinkID"];

            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.197.101;User ID=sa;Password=2018Sql101");
            con.Open();

            strSql = " select [Material],[MCode],[MFactory],[MOutStockName],[MInStockName],[MUnit],[MInventory],[allotNum]";
            //strSql += " ,[MGroup],[MGroupCode] ,[MBatch] ,[MUnit],[MInventory],[allotNum]";
            strSql += " from [DDdatabase].[dbo].[Tab_kocelApp_Kmr_AllotDetail] where 1=1 ";
            strSql += "  and [PLinkID]='" + strID + "'";

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

                strJson += "{\"MatName\":\"" + StringToJson(sdr[0].ToString()) + "\",";
                strJson += "\"MCode\":\"" + sdr[1].ToString() + "\",";
                strJson += "\"MFac\":\"" + sdr[2].ToString() + "\",";
                strJson += "\"MOutStock\":\"" + sdr[3].ToString() + "\",";
                strJson += "\"MInStock\":\"" + sdr[4].ToString() + "\",";
                strJson += "\"MUnit\":\"" + sdr[5].ToString() + "\",";
                strJson += "\"MInven\":\"" + sdr[6].ToString() + "\",";
                strJson += "\"PNum\":\"" + sdr[7].ToString() + "\"},";
                //strJson += "\"Ptype\":\"" + sdr[6].ToString() + "\",";
                //strJson += "\"ifSap\":\"" + sdr[7].ToString() + "\",";
                //strJson += "\"pickItemType\":\"" + sdr[8].ToString() + "\",";
                //strJson += "\"pickInfo\":\"" + sdr[9].ToString() + "\"},";

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
