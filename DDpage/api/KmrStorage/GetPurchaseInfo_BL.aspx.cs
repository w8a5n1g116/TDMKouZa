using DDpage.PO;
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
    public partial class GetPurchaseInfo_BL : System.Web.UI.Page
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
            //string strStName = Request.Form["StName"];

            //sap内向交货单获取采购订单号接口
            
            ZzxGetpo po = new ZzxGetpo();

            Bapireturn[] returns = new Bapireturn[] { };
            ZzxPoinfo[] poinfos = new ZzxPoinfo[] { };
            po.GdtPoinfo = poinfos;
            po.GdtReturn = returns;

            if (!String.IsNullOrEmpty(strOrder))
            {
                po.Ebeln = strOrder;
            }
            else if(!String.IsNullOrEmpty(strSDate) && !String.IsNullOrEmpty(strEDate))
            {
                DateTime a = DateTime.Parse(strSDate);
                DateTime b = DateTime.Parse(strEDate);

                po.BeginDat = a.ToString("yyyyMMdd");
                po.EndDat = b.ToString("yyyyMMdd");
            }

            ZZX_GETPO sap_serv = new ZZX_GETPO();

            sap_serv.Credentials = new NetworkCredential("RFC_USER", "pfseR91iC20an");
            ZzxGetpoResponse sap_res = sap_serv.ZzxGetpo(po);

            string strJson = "{\"data\":[ ";
            string ifGetData = "";

            if (sap_res.GdtPoinfo.Any())
            {
                foreach (var obj in sap_res.GdtPoinfo)
                {
                    string stockname = getStockName(obj.Lgort);
                    if (!string.IsNullOrEmpty(strMatName))
                    {
                        if (obj.Txz01.Contains(strMatName) || obj.Matnr.Contains(strMatName))
                        {
                            strJson += "{\"purchaseOrder\":\"" + obj.Ebeln + "\",";
                            strJson += "\"itemOrder\":\"" + obj.Ebelp + "\",";
                            strJson += "\"companyCode\":\"" + "1071" + "\",";
                            strJson += "\"companyName\":\"" + "烟台冰轮重型机件有限公司" + "\",";
                            strJson += "\"companyShortName\":\"" + obj.Name1 + "\",";
                            strJson += "\"supplyCode\":\"" + obj.Lifnr + "\",";
                            strJson += "\"supplyName\":\"" + obj.Name1 + "\",";
                            strJson += "\"matCode\":\"" + obj.Matnr + "\",";
                            strJson += "\"matName\":\"" + obj.Txz01 + "\",";
                            strJson += "\"facCode\":\"" + "1071" + "\",";
                            strJson += "\"facName\":\"" + "冰轮铸造工厂" + "\",";
                            strJson += "\"matUnit\":\"" + obj.Meins + "\",";
                            strJson += "\"purchaseNum\":\"" + obj.Menge + "\",";
                            strJson += "\"checkNum\":\"" + obj.Zrkmng + "\",";
                            strJson += "\"pDate\":\"" + obj.Eeind + "\",";
                            strJson += "\"insmk\":\"" + obj.Insmk + "\",";
                            strJson += "\"stockCode\":\"" + obj.Lgort + "\",";
                            strJson += "\"stockName\":\"" + stockname + "\"},";
                        }
                    }
                    else
                    {
                        strJson += "{\"purchaseOrder\":\"" + obj.Ebeln + "\",";
                        strJson += "\"itemOrder\":\"" + obj.Ebelp + "\",";
                        strJson += "\"companyCode\":\"" + "1071" + "\",";
                        strJson += "\"companyName\":\"" + "烟台冰轮重型机件有限公司" + "\",";
                        strJson += "\"companyShortName\":\"" + obj.Name1 + "\",";
                        strJson += "\"supplyCode\":\"" + obj.Lifnr + "\",";
                        strJson += "\"supplyName\":\"" + obj.Name1 + "\",";
                        strJson += "\"matCode\":\"" + obj.Matnr + "\",";
                        strJson += "\"matName\":\"" + obj.Txz01 + "\",";
                        strJson += "\"facCode\":\"" + "1071" + "\",";
                        strJson += "\"facName\":\"" + "冰轮铸造工厂" + "\",";
                        strJson += "\"matUnit\":\"" + obj.Meins + "\",";
                        strJson += "\"purchaseNum\":\"" + obj.Menge + "\",";
                        strJson += "\"checkNum\":\"" + obj.Zrkmng + "\",";
                        strJson += "\"pDate\":\"" + obj.Eeind + "\",";
                        strJson += "\"insmk\":\"" + obj.Insmk + "\",";
                        strJson += "\"stockCode\":\"" + obj.Lgort + "\",";
                        strJson += "\"stockName\":\"" + stockname + "\"},";
                    }
                }
            }
            else
            {
                ifGetData = "已经到底了！";
            }

            strJson = strJson.Substring(0, strJson.Length - 1);

            strJson += "],";
            strJson += "\"stat\":\"" + ifGetData + "\"";
            strJson = strJson + "}";


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