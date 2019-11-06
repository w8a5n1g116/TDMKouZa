using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDpage.GetSapProductOrder;
using DDpage.InTimeStock;

namespace DDpage.api.KmrStorage
{
    public partial class GetComOutPlanBatchInventoryList_BL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strComName = Request.Form["comName"];
            string strDeptName = Request.Form["deptName"];
            string strStockName = Request.Form["stockName"];
            string strVenId = Request.Form["venId"];
            string strCostCenterId = Request.Form["costCenterId"];
            string strProNo = Request.Form["proNo"];
            string strMatName = Request.Form["matName"];
            string StartDate = Request.Form["StartDate"];
            string EndDate = Request.Form["EndDate"];
            //strComName = "直属事业部";
            //strVenId = "1363";
            //strCostCenterId = "1100G07001";


            ZzxGetorderconfirmations zzx = new ZzxGetorderconfirmations();

            ZzxOrderconfirmations[] orders = new ZzxOrderconfirmations[] { };
            Bapireturn[] rets = new Bapireturn[] { };

            zzx.GdtItem = orders;
            zzx.GdtReturn = rets;
            zzx.BeginDat = DateTime.Parse(StartDate).ToString("yyyyMMdd");
            zzx.EndDat = DateTime.Parse(EndDate).ToString("yyyyMMdd");

            ZZX_GETORDERCONFIRM service = new ZZX_GETORDERCONFIRM();
            service.Credentials = new NetworkCredential("RFC_USER", "pfseR91iC20an");

            ZzxGetorderconfirmationsResponse retresponse = service.ZzxGetorderconfirmations(zzx);



            ZDM_REALTIMESTORE zdm_head = new ZDM_REALTIMESTORE();
            zdm_head.ZMATNR = strMatName;
            zdm_head.ZLGORT = getStockID(strStockName);
            ZDM_MARD[] mrd = new ZDM_MARD[] { };
            zdm_head.Z_MARD = mrd;
            ZZX_SERNR[] zzx1 = new ZZX_SERNR[] { };
            zdm_head.Z_SER = zzx1;

            ZXREALTIMESTORE sap_serv = new ZXREALTIMESTORE();
            sap_serv.Credentials = new NetworkCredential("RFC_USER", "pfseR91iC20an");

            ZDM_REALTIMESTOREResponse sap_res = new ZDM_REALTIMESTOREResponse();
            sap_res = sap_serv.ZDM_REALTIMESTORE(zdm_head);


            List<Mater> retmat = new List<Mater>();

            List<ZzxOrderconfirmations> selectOrders = new List<ZzxOrderconfirmations>();

            List<string> matnrIdList = new List<string> { strMatName };

            //if (!string.IsNullOrEmpty(strMatName))
            //{
            //    matnrIdList = retresponse.GdtItem.Where(p => p.Arbpl.Contains(strCostCenterId) && p.Matnr == strMatName).Select(p => p.Matnr).Distinct().ToList();
            //}
            //else
            //{
            //    matnrIdList = retresponse.GdtItem.Where(p => p.Arbpl.Contains(strCostCenterId)).Select(p => p.Matnr).Distinct().ToList();
            //}


            foreach(var matnrid in matnrIdList)
            {
                int strAufnrNum = 0;
                decimal strAllWeight = 0;
                string unit = "";
                string charg = "";
                if (retresponse.GdtItem.Any())
                {
                    strAufnrNum = retresponse.GdtItem.Where(p =>  p.Arbpl.Contains(strVenId)).Count();
                    strAllWeight = retresponse.GdtItem.Where(p => p.Arbpl.Contains(strVenId)).Select(p => p.Ntgew).Sum();
                    var t = retresponse.GdtItem.Where(p => p.Matnr == matnrid).FirstOrDefault();
                    if (t != null)
                        unit = t.Meinh;
                }

                if (sap_res.Z_SER.Where(p => p.MATNR == matnrid).Any())
                {
                    charg = sap_res.Z_SER.Where(p => p.MATNR == matnrid).FirstOrDefault().SERNR;
                }


                var ma = sap_res.Z_MARD.Where(p => p.MATNR == matnrid).FirstOrDefault();
                if (ma != null)
                {
                    Mater mater = new Mater();
                    mater.matCode = ma.MATNR;
                    mater.matName = ma.MAKTX;
                    mater.matNo = charg;
                    mater.matWeight = unit;
                    mater.matStock = getStockName(ma.LGORT);
                    mater.matStockCode = ma.LGORT;
                    mater.matFac = "冰轮铸造工厂";
                    mater.matFacCode = "1071";
                    mater.matGroup = "";
                    mater.matGroupCode = "";
                    mater.matNum = ma.LABST.ToString();
                    mater.matID = Guid.NewGuid().ToString();
                    mater.proWeight = strAllWeight.ToString();
                    mater.orderNum = strAufnrNum.ToString();

                    retmat.Add(mater);
                }

            }

            




            
            string strJson = "{\"data\":[  ";

            foreach(var mat in retmat)
            {

                strJson += "{\"matCode\":\"" + mat.matCode + "\",";
                strJson += "\"matName\":\"" + mat.matName + "\",";
                strJson += "\"matNo\":\"" + mat.matNo + "\",";
                strJson += "\"matWeight\":\"" + mat.matWeight + "\",";
                strJson += "\"matStock\":\"" + mat.matStock + "\",";
                strJson += "\"matStockCode\":\"" + mat.matStockCode + "\",";
                strJson += "\"matFac\":\"" + mat.matFac + "\",";
                strJson += "\"matFacCode\":\"" + mat.matFacCode + "\",";
                strJson += "\"matGroup\":\"" + mat.matGroup + "\",";
                strJson += "\"matGroupCode\":\"" + mat.matGroupCode + "\",";
                strJson += "\"matNum\":\"" + mat.matNum + "\",";
                strJson += "\"matID\":\"" + mat.matID + "\",";
                strJson += "\"proWeight\":\"" + mat.proWeight + "\",";
                strJson += "\"orderNum\":\"" + mat.orderNum + "\"},";

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

        public class Mater
        {
            public string matCode { get; set; }
            public string matName { get; set; }
            public string matNo { get; set; }
            public string matWeight { get; set; }
            public string matStock { get; set; }
            public string matStockCode { get; set; }
            public string matFac { get; set; }
            public string matFacCode { get; set; }
            public string matGroup { get; set; }
            public string matGroupCode { get; set; }
            public string matNum { get; set; }
            public string matID { get; set; }
            public string proWeight { get; set; }
            public string orderNum { get; set; }
            public string aufnr { get; set; }
            public string ntgew { get; set; }
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