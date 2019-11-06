using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDpage.Get_KmrApp_Inventory;
using System.Net;

namespace DDpage.api.KmrStorage
{
    public partial class GetComBatchBaseData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strComName = Request.Form["comName"];
            string strMatName = Request.Form["matName"];
            string strStockName = Request.Form["stockName"];
            string strMatNum = Request.Form["dataNum"];
            string strSql = "";
            bool stat = false;
            string strPid = System.Guid.NewGuid().ToString();

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();

            SqlConnection conNew = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            conNew.Open();

            strSql = "select distinct [FMatCode] from  [DDdatabase].[dbo].[Tab_KocelApp_Kmr_BatchCCBaseData]";
            SqlCommand cmd01 = new SqlCommand(strSql, con);
            SqlDataReader sdr01 = cmd01.ExecuteReader();

            if (sdr01.HasRows)
            {
                while (sdr01.Read())
                {

                    ZDM_INVENTORYINF zdm_head = new ZDM_INVENTORYINF();
                    zdm_head.Z_LGORT = strStockName;
                    zdm_head.Z_MATNR = sdr01[0].ToString();

                    SI_ZDM_INVENTORY_SenderService sap_serv = new SI_ZDM_INVENTORY_SenderService();
                    sap_serv.Credentials = new NetworkCredential("prd_pisuper", "handhand0");

                    ZDM_INVENTORYINFResponse sap_res = new ZDM_INVENTORYINFResponse();
                    sap_res = sap_serv.SI_ZDM_INVENTORY_Sender(zdm_head);

                    foreach (ZSDM_INVENTORYINF sap_item in sap_res.ZTDM_INVENTORYINF)
                    {
                        //写入临时库存表

                        stat = false;
                        strSql = "insert into [DDdatabase].[dbo].[Tab_KocelApp_Kmr_BBInventoryInf]";
                        strSql += " values('" + strPid + "','" + sap_item.MATNR + "','" + StringToJson(sap_item.MAKTX) + "','" + sap_item.MTART + "','" + StringToJson(sap_item.GROES) + "',";
                        strSql += " '" + sap_item.MATKL + "','" + sap_item.MEINS + "','" + sap_item.NTGEW + "','" + sap_item.GEWEI + "','" + sap_item.ZEINR + "','" + sap_item.BISMT + "',";
                        strSql += " '" + sap_item.CHARG + "','" + sap_item.WERKS + "','" + sap_item.LGORT + "','" + sap_item.LABST + "','" + sap_item.LVORM + "')";

                        SqlCommand cmdhead = new SqlCommand(strSql, conNew);
                        int t1 = cmdhead.ExecuteNonQuery();
                        if (t1 != 0)
                        {
                            stat = true;
                        }

                    }


                }
            }

            sdr01.Close();         
            con.Close();
            conNew.Close();

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