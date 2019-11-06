using DDpage.Get_KmrApp_PurOrder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.KmrStorage
{
    public partial class GetPurchaseInfo1900 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCompany = Request.Form["Company"];
            string strDept = Request.Form["Dept"];
            string strVen = Request.Form["Ven"];
            string strName = Request.Form["Name"];
            string strOrder = Request.Form["Order"];
            string strInOrder = Request.Form["InOrder"];
            string strSupply = Request.Form["Supply"];
            string strMatName = Request.Form["matName"];
            string strSDate = Request.Form["firDate"];
            string strEDate = Request.Form["secDate"];
            string facCode = Request.Form["facCode"];
            string facName = Request.Form["facName"];
            string stockName = Request.Form["stockName"];
            string stockCode = Request.Form["stockCode"];
            string matCode = Request.Form["matCode"];         

            //sap内向交货单获取采购订单号接口

            string strSql = "";


            SqlConnection con = new SqlConnection("Data Source=192.168.203.3;User ID=sa;Password=lan@2mail");
            con.Open();
            //SqlConnection con1 = new SqlConnection("Data Source=192.168.75.250;User ID=sa;Password=kmtSoft12345678");
            //con1.Open();
            strSql = "exec [DDdatabase].[dbo].[ProcessGetValue]";

            SqlCommand cmd1 = new SqlCommand(strSql, con);
            cmd1.ExecuteNonQuery();

            strSql = "select  * from [DDdatabase].[dbo].[Tab_KocelApp_Kmr_CheckInList] where stat = 0";

            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            string strJson = "{\"data\":[]";
            string ifGetData = "";
            if (sdr.HasRows)
            {
                strJson = "{\"data\":[ ";
            }
            
            while (sdr.Read())
            {

                strJson += "{\"purchaseOrder\":\"" + sdr[1].ToString() + "\",";
                strJson += "\"itemOrder\":\"" + sdr[2].ToString() + "\",";
                strJson += "\"companyCode\":\"" + sdr[9].ToString() + "\",";
                strJson += "\"companyName\":\"" + sdr[8].ToString() + "\",";
                strJson += "\"companyShortName\":\"" + sdr[10].ToString() + "\",";
                strJson += "\"supplyCode\":\"" + sdr[7].ToString() + "\",";
                strJson += "\"supplyName\":\"" + sdr[6].ToString() + "\",";
                strJson += "\"matCode\":\"" + sdr[4].ToString() + "\",";
                strJson += "\"matName\":\"" + StringToJson(sdr[3].ToString()) + "\",";
                strJson += "\"facCode\":\"" + sdr[12].ToString() + "\",";
                strJson += "\"facName\":\"" + sdr[11].ToString() + "\",";
                strJson += "\"matUnit\":\"" + sdr[5].ToString() + "\",";
                strJson += "\"purchaseNum\":\"" + sdr[15].ToString() + "\",";
                strJson += "\"checkNum\":\"" + sdr[17].ToString() + "\",";
                strJson += "\"pDate\":\"" + Convert.ToDateTime(sdr[18]).ToString("yyyy-MM-dd") + "\",";
                strJson += "\"stockCode\":\"" + sdr[14].ToString() + "\",";
                strJson += "\"stockName\":\"" + sdr[13].ToString() + "\",";
                strJson += "\"maozhong\":\"" + sdr[26].ToString() + "\",";
                strJson += "\"pizhong\":\"" + sdr[27].ToString() + "\",";
                strJson += "\"kouza\":\"" + sdr[28].ToString() + "\",";
                strJson += "\"koubaozhuang\":\"" + sdr[29].ToString() + "\",";
                strJson += "\"CarNo\":\"" + sdr[30].ToString() + "\"},";

            }
            strJson = strJson.Substring(0, strJson.Length - 1);

            strJson +="],";
            strJson += "\"stat\":\"" + ifGetData + "\""; 
            strJson = strJson + "}";

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