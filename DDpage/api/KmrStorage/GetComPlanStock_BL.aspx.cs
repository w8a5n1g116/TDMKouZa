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
    public partial class GetComPlanStock_BL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCompany = Request.Form["strCom"];
            string strMatCode = Request.Form["strMatCode"];
            string strMatNum = Request.Form["strMatNum"];


            ZDM_REALTIMESTORE zdm_head = new ZDM_REALTIMESTORE();
            zdm_head.ZMATNR = strMatCode;
            zdm_head.ZLGORT = "";
            ZDM_MARD[] mrd = new ZDM_MARD[] { };
            zdm_head.Z_MARD = mrd;
            ZZX_SERNR[] zzx = new ZZX_SERNR[] { };
            zdm_head.Z_SER = zzx;

            ZXREALTIMESTORE sap_serv = new ZXREALTIMESTORE();
            sap_serv.Credentials = new NetworkCredential("RFC_USER", "pfseR91iC20an");

            ZDM_REALTIMESTOREResponse sap_res = new ZDM_REALTIMESTOREResponse();
            sap_res = sap_serv.ZDM_REALTIMESTORE(zdm_head);

            string strJson = "{\"data\":[";

            foreach (var sap_item in sap_res.Z_MARD)
            {
                if (sap_item.MATNR.Contains(strMatCode) && sap_item.LABST >= decimal.Parse(strMatNum))
                {
                    string stockname = getStockName(sap_item.LGORT);

                    strJson += "{\"name\":\"" + stockname + "\",";
                    strJson += "\"code\":\"" + sap_item.LGORT + "\"},";
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

    }
}