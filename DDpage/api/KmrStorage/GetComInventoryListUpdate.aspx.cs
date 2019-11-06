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
    public partial class GetComInventoryListUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strComName = Request.Form["comName"];
            string strMatName = Request.Form["matName"];
            string strFacCode = Request.Form["facCode"];
            string strStockName = Request.Form["stockName"];
            string strStockCode = Request.Form["stockCode"];
            string strMatNum = Request.Form["dataNum"];
            string strSql = "";
            bool stat = false;
            string strPid = System.Guid.NewGuid().ToString();

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();


            ZDM_INVENTORYINF zdm_head = new ZDM_INVENTORYINF();
            zdm_head.Z_LGORT = strStockCode;
            zdm_head.Z_WERKS = strFacCode;
            zdm_head.Z_MATNR = strMatName;

            SI_ZDM_INVENTORY_SenderService sap_serv = new SI_ZDM_INVENTORY_SenderService();
            sap_serv.Credentials = new NetworkCredential("prd_pisuper", "handhand0");

            ZDM_INVENTORYINFResponse sap_res = new ZDM_INVENTORYINFResponse();
            sap_res = sap_serv.SI_ZDM_INVENTORY_Sender(zdm_head);

            foreach (ZSDM_INVENTORYINF sap_item in sap_res.ZTDM_INVENTORYINF)
            {
                //写入临时库存表

                stat = false;
                strSql = "insert into [DDdatabase].[dbo].[Tab_KocelApp_Kmr_InventoryInf]";
                strSql += " values('" + strPid + "','" + sap_item.MATNR + "','" + StringToJson(sap_item.MAKTX) + "','" + sap_item.MTART + "','" + StringToJson(sap_item.GROES) + "',";
                strSql += " '" + sap_item.MATKL + "','" + sap_item.MEINS + "','" + sap_item.NTGEW + "','" + sap_item.GEWEI + "','" + sap_item.ZEINR + "','" + sap_item.BISMT + "',";
                strSql += " '" + sap_item.CHARG + "','" + sap_item.WERKS + "','" + sap_item.LGORT + "','" + sap_item.LABST + "','" + sap_item.LVORM + "')";

                SqlCommand cmdhead = new SqlCommand(strSql, con);
                int t1 = cmdhead.ExecuteNonQuery();
                if (t1 != 0)
                {
                    stat = true;
                }

            }

            //从临时库存表查询数据
            if (strMatNum != "0")
            {
                strSql = "select distinct top 30 物料代码,物料名称,批次,基本计量单位,库存地点,库存代码,工厂,工厂代码,物料组,物料组代码,库存,物料代码+工厂代码+库存代码+批次 as ID from [DDdatabase].[dbo].[View_KocelApp_Kmr_InventoryInf] where (物料名称 like '%" + strMatName + "%' or 物料代码 like '%" + strMatName + "%') ";
                strSql += " and 库存地点 like '%" + strStockName + "%'";
                strSql += " and 工厂代码  in (select distinct 工厂代码 from [DataFromSap].[dbo].[View_SAP_SapEhrComStock] where 公司简称 = '" + strComName + "')";

            }
            else
            {
                strSql = "select distinct top 10 物料代码,物料名称,批次,基本计量单位,库存地点,库存代码,工厂,工厂代码,物料组,物料组代码,库存,物料代码+工厂代码+库存代码+批次 as ID from [DDdatabase].[dbo].[View_KocelApp_Kmr_InventoryInf] where 1=1 ";
                strSql += " and 库存地点 like '%" + strStockName + "%'";
                strSql += " and 工厂代码  in (select distinct 工厂代码 from [DataFromSap].[dbo].[View_SAP_SapEhrComStock] where 公司简称 = '" + strComName + "')";
            }


            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            string strJson = "{\"data\":[]";
            if (sdr.HasRows)
            {
                strJson = "{\"data\":[";
            }
            while (sdr.Read())
            {

                strJson += "{\"matCode\":\"" + sdr[0].ToString() + "\",";
                strJson += "\"matName\":\"" + StringToJson(sdr[1].ToString()) + "\",";
                strJson += "\"matNo\":\"" + sdr[2].ToString() + "\",";
                strJson += "\"matWeight\":\"" + sdr[3].ToString() + "\",";
                strJson += "\"matStock\":\"" + sdr[4].ToString() + "\",";
                strJson += "\"matStockCode\":\"" + sdr[5].ToString() + "\",";
                strJson += "\"matFac\":\"" + sdr[6].ToString() + "\",";
                strJson += "\"matFacCode\":\"" + sdr[7].ToString() + "\",";
                strJson += "\"matGroup\":\"" + StringToJson(sdr[8].ToString()) + "\",";
                strJson += "\"matGroupCode\":\"" + sdr[9].ToString() + "\",";
                strJson += "\"matNum\":\"" + sdr[10].ToString() + "\",";
                strJson += "\"matID\":\"" + sdr[11].ToString() + "\"},";

            }
            strJson = strJson.Substring(0, strJson.Length - 1);
            strJson = strJson + "]}";
            sdr.Close();

            //删除临时库存表数据
            strSql = " delete from [DDdatabase].[dbo].[Tab_KocelApp_Kmr_InventoryInf] where ID = '" + strPid + "'";
            SqlCommand cmdDel = new SqlCommand(strSql, con);
            int t2 = cmdDel.ExecuteNonQuery();
            if (t2 != 0)
            {
                stat = true;
            }

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