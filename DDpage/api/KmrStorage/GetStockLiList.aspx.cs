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
    public partial class GetStockLiList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string stockCompany = Request.Form["stockCompany"];
            string stockDept = Request.Form["stockDept"];
            string stockVen = Request.Form["stockVen"];
            string stockName = Request.Form["stockName"];
            string stockSName = Request.Form["stockSName"];
            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();
            strSql = "select PLinkID,PDID,Material,MCode,PName,PickInventory,StockInventory,MUnit,IsWeigh,StockID,PTime from [DDdatabase].[dbo].[View_KocelApp_Kmr_StockListDetail] where SStat=0 ";
            strSql += " and PCompany like '%"+ stockCompany + "%'";
            strSql += " and PDept like '%" + stockDept + "%'";
            strSql += " and PVenture like '%" + stockVen + "%'";
            strSql += " and MStock like '%" + stockName + "%'";
            strSql += " and SName like '%" + stockSName + "%'";
            strSql += " order by PTime";
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            string strJson = "{\"data\":[]";
            if (sdr.HasRows)
            {
                strJson = "{\"data\":[";
            }
            while (sdr.Read())
            {

                strJson += "{\"stPLinkID\":\"" + sdr[0].ToString() + "\",";
                strJson += "\"stPDID\":\"" + sdr[1].ToString() + "\",";
                strJson += "\"stMaterial\":\"" + StringToJson(sdr[2].ToString()) + "\",";
                strJson += "\"stMCode\":\"" + sdr[3].ToString() + "\",";
                strJson += "\"stPName\":\"" + sdr[4].ToString() + "\",";
                strJson += "\"stPickInventory\":\"" + sdr[5].ToString() + "\",";
                strJson += "\"stStockInventory\":\"" + sdr[6].ToString() + "\",";
                strJson += "\"stMUnit\":\"" + sdr[7].ToString() + "\",";
                strJson += "\"stWeight\":\"" + sdr[8].ToString() + "\",";
                strJson += "\"stStockID\":\"" + sdr[9].ToString() + "\",";
                strJson += "\"stPTime\":\"" + sdr[10].ToString() + "\"},";

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