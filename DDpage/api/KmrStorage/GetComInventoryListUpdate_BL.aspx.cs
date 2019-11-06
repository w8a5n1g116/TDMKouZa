using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDpage.InTimeStock;
using System.Net;

namespace DDpage.api.KmrStorage
{
    public partial class GetComInventoryListUpdate_BL : System.Web.UI.Page
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

            SqlConnection con = new SqlConnection("Data Source=192.168.197.101;Initial Catalog=DDdatabase;User ID=sa;Password=2018Sql101");
            con.Open();


            ZDM_REALTIMESTORE zdm_head = new ZDM_REALTIMESTORE();
            zdm_head.ZMATNR = strMatName;
            zdm_head.ZLGORT = getStockID(strStockName);
            ZDM_MARD[] mrd = new ZDM_MARD[] { };
            zdm_head.Z_MARD = mrd;
            ZZX_SERNR[] zzx = new ZZX_SERNR[] { };
            zdm_head.Z_SER = zzx;

            ZXREALTIMESTORE sap_serv = new ZXREALTIMESTORE();
            sap_serv.Credentials = new NetworkCredential("RFC_USER", "pfseR91iC20an");

            ZDM_REALTIMESTOREResponse sap_res = new ZDM_REALTIMESTOREResponse();
            sap_res = sap_serv.ZDM_REALTIMESTORE(zdm_head);

            foreach (var sap_item in sap_res.Z_MARD)
            {
                //写入临时库存表

                string charg = "";
                if(sap_res.Z_SER.Where(p => p.MATNR == sap_item.MATNR).Any())
                {
                    charg = sap_res.Z_SER.Where(p => p.MATNR == sap_item.MATNR).FirstOrDefault().SERNR;
                }

                stat = false;
                strSql = "insert into [DDdatabase].[dbo].[Tab_KocelApp_Kmr_InventoryInf]";
                strSql += " values('" + strPid + "','" + sap_item.MATNR + "','"+ sap_item.MAKTX + "','','',";
                strSql += " '','','','','','',";
                strSql += " '" + charg + "','" + sap_item.WERKS + "','" + sap_item.LGORT + "','" + sap_item.LABST + "','')";

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
                strSql += " and 工厂代码  in (1071)";

            }
            else
            {
                strSql = "select distinct top 10 物料代码,物料名称,批次,基本计量单位,库存地点,库存代码,工厂,工厂代码,物料组,物料组代码,库存,物料代码+工厂代码+库存代码+批次 as ID from [DDdatabase].[dbo].[View_KocelApp_Kmr_InventoryInf] where 1=1 ";
                strSql += " and 库存地点 like '%" + strStockName + "%'";
                strSql += " and 工厂代码  in (1071)";
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

        public string getStockID(string stockname)
        {
            string stockName = "";

            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.197.101;User ID=sa;Password=2018Sql101");
            con.Open();
            strSql = "select MStockCode from [DDdatabase].[dbo].[Stock] where MStock = '" + stockname + "'";


            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                sdr.Read();
                stockName = sdr[0].ToString();
            }

            sdr.Close();
            con.Close();

            return stockName;
        }
    }
}