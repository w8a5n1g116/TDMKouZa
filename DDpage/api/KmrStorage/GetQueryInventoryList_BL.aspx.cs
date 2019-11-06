using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDpage.InTimeStock;

namespace DDpage.api.KmrStorage
{
    public partial class GetQueryInventoryList_BL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strComName = Request.Form["company"];
            string strFacName = Request.Form["fac"];
            string strStockName = Request.Form["stock"];
            string strMatName = Request.Form["matName"];

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


            string strJson = "{\"data\":[  ";

            foreach (var sap_item in sap_res.Z_MARD)
            {
                string stockname = getStockName(sap_item.LGORT);

                
                if(!string.IsNullOrEmpty(strMatName) )
                {
                    if (stockname == strStockName && (sap_item.MAKTX.Contains(strMatName)|| sap_item.MATNR.Contains(strMatName)))
                    {
                        strJson += "{\"matCode\":\"" + sap_item.MATNR + "\",";
                        strJson += "\"matName\":\"" + sap_item.MAKTX + "\",";
                        strJson += "\"matXh\":\"" + "" + "\",";
                        strJson += "\"matNo\":\"" + "" + "\",";
                        strJson += "\"matWeight\":\"" + "" + "\",";
                        strJson += "\"matStock\":\"" + stockname + "\",";
                        strJson += "\"matStockCode\":\"" + sap_item.LGORT + "\",";
                        strJson += "\"matFac\":\"" + "冰轮铸造工厂" + "\",";
                        strJson += "\"matFacCode\":\"" + "1071" + "\",";
                        strJson += "\"matGroup\":\"" + "" + "\",";
                        strJson += "\"matGroupCode\":\"" + "" + "\",";
                        strJson += "\"matNum\":\"" + sap_item.LABST + "\",";
                        strJson += "\"matID\":\"" + Guid.NewGuid().ToString("N") + "\"},";
                    }
                }
                else
                {
                    if (stockname == strStockName)
                    {
                        strJson += "{\"matCode\":\"" + sap_item.MATNR + "\",";
                        strJson += "\"matName\":\"" + sap_item.MAKTX + "\",";
                        strJson += "\"matXh\":\"" + "" + "\",";
                        strJson += "\"matNo\":\"" + "" + "\",";
                        strJson += "\"matWeight\":\"" + "" + "\",";
                        strJson += "\"matStock\":\"" + stockname + "\",";
                        strJson += "\"matStockCode\":\"" + sap_item.LGORT + "\",";
                        strJson += "\"matFac\":\"" + "冰轮铸造工厂" + "\",";
                        strJson += "\"matFacCode\":\"" + "1071" + "\",";
                        strJson += "\"matGroup\":\"" + "" + "\",";
                        strJson += "\"matGroupCode\":\"" + "" + "\",";
                        strJson += "\"matNum\":\"" + sap_item.LABST + "\",";
                        strJson += "\"matID\":\"" + Guid.NewGuid().ToString("N") + "\"},";
                    }
                }

                

            }

            strJson = strJson.Substring(0, strJson.Length - 1);
            strJson = strJson + "]}";

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

        public string getStockName(string stockid)
        {
            string stockName = "";

            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.197.101;User ID=sa;Password=2018Sql101");
            con.Open();
            strSql = "select MStock from [DDdatabase].[dbo].[Stock] where MStockCode = '" + stockid + "'";


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