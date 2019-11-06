using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDpage.ProductOrderYuLiu;
using DDpage.InTimeStock;

namespace DDpage.api.KmrStorage
{
    public partial class GetSapPlanList_BL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string oddno = Request.Form["OddNo"];
            string rsno = Request.Form["RsNo"];

            ZPP_RESB1 sap_resb = new ZPP_RESB1();           
            sap_resb.Z_AUFNR = oddno;
            if(!string.IsNullOrEmpty(rsno))
                sap_resb.Z_VORNR = Request.Form["RsNo"];

            ZPP_RESB[] info = new ZPP_RESB[] { };//new List<ZPP_RESB>().ToArray();
            sap_resb.Z_RESBINFO = info;

            ZPP_RESBResponse sap_resbRes = new ZPP_RESBResponse();


            ZZXPRODUCTRESB sap_serv = new ZZXPRODUCTRESB();
            sap_serv.Credentials = new NetworkCredential("RFC_USER", "pfseR91iC20an");
            sap_resbRes = sap_serv.ZPP_RESB(sap_resb);

            ZPP_RESB[] re_data;
            re_data = sap_resbRes.Z_RESBINFO;

            string matListString = "";

            var matList = re_data.Select(p => p.MATNR).Distinct().ToList();

            foreach(var matstring in matList)
            {
                if(matList.LastOrDefault() == matstring)
                {
                    matListString += matstring;
                }
                else
                {
                    matListString += matstring + ",";
                }
            }
            ///
            ZDM_REALTIMESTORE zdm_head = new ZDM_REALTIMESTORE();
            zdm_head.ZMATNR = matListString;
            zdm_head.ZLGORT = "";
            ZDM_MARD[] mrd = new ZDM_MARD[] { };
            zdm_head.Z_MARD = mrd;
            ZZX_SERNR[] zzx = new ZZX_SERNR[] { };
            zdm_head.Z_SER = zzx;

            ZXREALTIMESTORE sap_serv2 = new ZXREALTIMESTORE();
            sap_serv.Credentials = new NetworkCredential("RFC_USER", "pfseR91iC20an");

            ZDM_REALTIMESTOREResponse sap_res = new ZDM_REALTIMESTOREResponse();
            sap_res = sap_serv2.ZDM_REALTIMESTORE(zdm_head);
            ///


            string strJson = "{\"data\":[]";
            if (re_data.Length > 0)
            {
                strJson = "{\"data\":[ ";
            }


            Random r = new Random();
            int i = 0;
            string strPID ;

            foreach (var ob in re_data)
            {
                if (ob.BDMNG - ob.ENMNG == 0)
                    continue;

                decimal inventory = 0;
                string stock = "";

                var ma = sap_res.Z_MARD.Where(p => p.MATNR == ob.MATNR).FirstOrDefault();
                if (ma != null)
                {
                    inventory = ma.LABST;
                    stock = ma.LGORT;
                }
                    

                if (!string.IsNullOrEmpty(rsno))
                {
                    if(ob.VORNR.Contains(rsno))
                    {
                        strJson += "{\"OddNo\":\"" + ob.AUFNR + "\",";
                        strJson += "\"vornr\":\"" + ob.VORNR + "\",";
                        strJson += "\"RsNo\":\"" + ob.RSNUM + "\",";
                        strJson += "\"RsPos\":\"" + ob.RSPOS + "\",";
                        strJson += "\"pmaktx\":\"" + StringToJson(ob.PRODUCE_MAKTX) + "\",";
                        strJson += "\"matnr\":\"" + ob.MATNR + "\",";
                        strJson += "\"rmaktx\":\"" + StringToJson(ob.REQUIRE_MAKTX) + "\",";
                        strJson += "\"menge\":\"" + ob.BDMNG + "\",";
                        strJson += "\"menge2\":\"" + (ob.BDMNG - ob.ENMNG) + "\",";
                        strJson += "\"factory\":\"" + ob.WERKS + "\",";
                        strJson += "\"stock\":\"" + stock + "\",";
                        strJson += "\"inventory\":\"" + inventory + "\",";
                        strJson += "\"mGroup\":\"" + ob.MATKL + "\",";
                        strJson += "\"kdauf\":\"" + ob.KDAUF + "\",";
                        strJson += "\"kdpos\":\"" + ob.KDPOS + "\",";
                        i = r.Next(1000, 9999);
                        strPID = System.DateTime.Now.ToFileTime().ToString() + i.ToString();
                        strJson += "\"matID\":\"" + strPID + "\",";
                        strJson += "\"unit\":\"" + ob.MEINS + "\"},";
                    }
                    
                }
                else
                {
                    strJson += "{\"OddNo\":\"" + ob.AUFNR + "\",";
                    strJson += "\"vornr\":\"" + ob.VORNR + "\",";
                    strJson += "\"RsNo\":\"" + ob.RSNUM + "\",";
                    strJson += "\"RsPos\":\"" + ob.RSPOS + "\",";
                    strJson += "\"pmaktx\":\"" + StringToJson(ob.PRODUCE_MAKTX) + "\",";
                    strJson += "\"matnr\":\"" + ob.MATNR + "\",";
                    strJson += "\"rmaktx\":\"" + StringToJson(ob.REQUIRE_MAKTX) + "\",";
                    strJson += "\"menge\":\"" + ob.BDMNG + "\",";
                    strJson += "\"menge2\":\"" + (ob.BDMNG - ob.ENMNG) + "\",";
                    strJson += "\"factory\":\"" + ob.WERKS + "\",";
                    strJson += "\"stock\":\"" + stock + "\",";
                    strJson += "\"inventory\":\"" + inventory + "\",";
                    strJson += "\"mGroup\":\"" + ob.MATKL + "\",";
                    strJson += "\"kdauf\":\"" + ob.KDAUF + "\",";
                    strJson += "\"kdpos\":\"" + ob.KDPOS + "\",";
                    i = r.Next(1000, 9999);
                    strPID = System.DateTime.Now.ToFileTime().ToString() + i.ToString();
                    strJson += "\"matID\":\"" + strPID + "\",";
                    strJson += "\"unit\":\"" + ob.MEINS + "\"},";
                }
                    
            }

            strJson = strJson.Substring(0, strJson.Length - 1);
            strJson = strJson + "],";
            if (re_data.Length > 0)
            {
                strJson += "\"stat\":\""+re_data.Length+"\"}";
            }
            else
            {
                strJson += "\"stat\":\"查询无数据!\"}";
            }

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