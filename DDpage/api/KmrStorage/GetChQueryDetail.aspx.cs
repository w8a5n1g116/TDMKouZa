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
    public partial class GetChQueryDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strLid = Request.Form["LinkID"];
            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();



            strSql = "SELECT top 30 [Material],[MCode],[PName],[PTime],[MStock],[MUnit],[MBatch],[PickInventory],[StockInventory],[PType],[PDStat],case when [ifSap]=1 then '是' else '否' end as ifSap,[pickItemType],[pickInfo] ";
            strSql += " FROM [DDdatabase].[dbo].[View_KocelApp_Kmr_CheckListDetail] where 1=1 ";
            strSql += " and PLinkID like '%" + strLid + "%'";
            strSql += " order by PTime desc";

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

                strJson += "{\"Material\":\"" + StringToJson(sdr[0].ToString()) + "\",";
                strJson += "\"MCode\":\"" + sdr[1].ToString() + "\",";
                strJson += "\"PName\":\"" + sdr[2].ToString() + "\",";
                strJson += "\"PTime\":\"" + sdr[3].ToString() + "\",";
                strJson += "\"MStock\":\"" + sdr[4].ToString() + "\",";
                strJson += "\"MUnit\":\"" + sdr[5].ToString() + "\",";
                strJson += "\"MBatch\":\"" + sdr[6].ToString() + "\",";
                strJson += "\"PickInventory\":\"" + sdr[7].ToString() + "\",";
                strJson += "\"StockInventory\":\"" + sdr[8].ToString() + "\",";
                strJson += "\"PType\":\"" + sdr[9].ToString() + "\",";
                strJson += "\"PDStat\":\"" + sdr[10].ToString() + "\",";
                strJson += "\"ifSap\":\"" + sdr[11].ToString() + "\",";
                strJson += "\"pickItemType\":\"" + sdr[12].ToString() + "\",";
                strJson += "\"pickInfo\":\"" + sdr[13].ToString() + "\"},";

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
