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
    public partial class GetChDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Company = Request.Form["chCompany"];
            string Dept = Request.Form["chDept"];
            string Ven = Request.Form["chVen"];
            string stName = Request.Form["chStName"];
            string pName = Request.Form["chPName"];
            string order = Request.Form["chOrder"];
            string matName = Request.Form["chMatName"];
            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();
            strSql = "select PLinkID,PDID,Material,MCode,MBatch,PName,MStock,PickInventory,StockInventory,MUnit,CheckID,DePhoto,PTime from [DDdatabase].[dbo].[View_KocelApp_Kmr_CheckListDetail] where CStat=0 ";
            strSql += " and PCompany like '%" + Company + "%'";
            strSql += " and PDept like '%" + Dept + "%'";
            strSql += " and PVenture like '%" + Ven + "%'";
            strSql += " and MStock like '%" + stName + "%'";
            strSql += " and OddNo like '%" + order + "%'";
            strSql += " and Material like '%" + matName + "%'";
            //strSql += " and PName like '%" + pName + "%'";
            strSql += " order by PTime desc";
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            string strJson = "{\"data\":[]";
            if (sdr.HasRows)
            {
                strJson = "{\"data\":[";
            }
            while (sdr.Read())
            {

                strJson += "{\"PLinkID\":\"" + sdr[0].ToString() + "\",";
                strJson += "\"PDID\":\"" + sdr[1].ToString() + "\",";
                strJson += "\"Material\":\"" + StringToJson(sdr[2].ToString()) + "\",";           
                strJson += "\"MCode\":\"" + sdr[3].ToString() + "\",";
                strJson += "\"MBatch\":\"" + sdr[4].ToString() + "\",";
                strJson += "\"PName\":\"" + sdr[5].ToString() + "\",";
                strJson += "\"MStock\":\"" + sdr[6].ToString() + "\",";
                strJson += "\"PickInventory\":\"" + sdr[7].ToString() + "\",";
                strJson += "\"StockInventory\":\"" + sdr[8].ToString() + "\",";
                strJson += "\"MUnit\":\"" + sdr[9].ToString() + "\",";
                strJson += "\"CheckID\":\"" + sdr[10].ToString() + "\",";
                strJson += "\"Photo\":\"" + sdr[11].ToString() + "\",";
                strJson += "\"PTime\":\"" + sdr[12].ToString() + "\"},";

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
