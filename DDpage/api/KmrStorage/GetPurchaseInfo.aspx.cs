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
    public partial class GetPurchaseInfo : System.Web.UI.Page
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
            //string strStName = Request.Form["StName"];
            string strGetSapPurOrder = "";

            //sap内向交货单获取采购订单号接口
            if (!String.IsNullOrEmpty(strInOrder))
            {
                ZDM_EKES zdm_ekes = new ZDM_EKES();
                zdm_ekes.Z_VBELN = strInOrder;
                ZDM_EKESResponse sap_res = new ZDM_EKESResponse();

                SI_ZDM_EKESService sap_serv = new SI_ZDM_EKESService();

                sap_serv.Credentials = new NetworkCredential("prd_pisuper", "handhand0");
                sap_res = sap_serv.SI_ZDM_EKES(zdm_ekes);

                foreach (ZSTT_EKES obj in sap_res.ZT_EKES)
                {
                    if (obj.EBELN.ToString().Length > 0)
                    {

                        strGetSapPurOrder += "'" + obj.EBELN.ToString() + "',";
                    }
                }
                //strGetSapPurOrder = sap_res.ZT_EKES[0].EBELN.ToString();

                strGetSapPurOrder = strGetSapPurOrder.Substring(0, strGetSapPurOrder.Length - 1);

            }

           

            //sap内向交货单获取采购订单号接口

            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();
            strSql = "select distinct top 30  [purchaseOrder],[purchaseItem],[comCode],[comName],[comShortName],[suppCode],[suppName],[matCode],[matName], ";
            strSql += " facCode,facName,[MSEHL],menge,checkNum,cast(badat as date) as badat,stockCode,stockName FROM [DataFromSap].[dbo].[View_SAP_Psekpo] where menge >0";
            strSql += " and comShortName like '%" + strCompany + "%'";

            strSql += " and purchaseOrder = '" + strOrder + "'";

            if (!String.IsNullOrEmpty(strGetSapPurOrder))
            {
                strSql += " and purchaseOrder in  (" + strGetSapPurOrder + ")";
            }

            //strSql += " and (purchaseOrder like '%" + strOrder + "%' or purchaseOrder like '%" + strGetSapPurOrder + "%')";
            strSql += " and suppName like '%" + strSupply + "%'";
            strSql += " and (matName like '%" + strMatName + "%' or matName like '%" + strMatName + "%' )";
            if (!String.IsNullOrEmpty(strSDate))
            {
                strSql += " and [badat] >='" + strSDate + "'";
            }

            if (!String.IsNullOrEmpty(strEDate))
            {
                strSql += " and [badat] <='" + strEDate + "'";
            }


            //库管员权限
            //strSql += "and DeName like '%" + strDeName + "%'";
            //库管员权限
            strSql += " order by [purchaseOrder]";

            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            string strJson = "{\"data\":[ ";
            string ifGetData = "";
            if (sdr.HasRows)
            {
                strJson = "{\"data\":[ ";
            }
            else
            {
                strJson += "{\"purchaseOrder\":\"" + "empty" + "\",";
                strJson += "\"itemOrder\":\"" + "" + "\",";
                strJson += "\"companyCode\":\"" + "" + "\",";
                strJson += "\"companyName\":\"" + strCompany + "\",";
                strJson += "\"companyShortName\":\"" + "" + "\",";
                strJson += "\"supplyCode\":\"" + "" + "\",";
                strJson += "\"supplyName\":\"" + strSupply + "\",";
                strJson += "\"matCode\":\"" + matCode + "\",";
                strJson += "\"matName\":\"" + strMatName + "\",";
                strJson += "\"facCode\":\"" + facCode + "\",";
                strJson += "\"facName\":\"" + facName + "\",";
                strJson += "\"matUnit\":\"" + "" + "\",";
                strJson += "\"purchaseNum\":\"" + "0" + "\",";
                strJson += "\"checkNum\":\"" + "0" + "\",";
                strJson += "\"pDate\":\"" + "" + "\",";
                strJson += "\"stockCode\":\"" + stockCode + "\",";
                strJson += "\"stockName\":\"" + stockName + "\"},";
            }
            while (sdr.Read())
            {

                strJson += "{\"purchaseOrder\":\"" + sdr[0].ToString() + "\",";
                strJson += "\"itemOrder\":\"" + sdr[1].ToString() + "\",";
                strJson += "\"companyCode\":\"" + sdr[2].ToString() + "\",";
                strJson += "\"companyName\":\"" + sdr[3].ToString() + "\",";
                strJson += "\"companyShortName\":\"" + sdr[4].ToString() + "\",";
                strJson += "\"supplyCode\":\"" + sdr[5].ToString() + "\",";
                strJson += "\"supplyName\":\"" + sdr[6].ToString() + "\",";
                strJson += "\"matCode\":\"" + sdr[7].ToString() + "\",";
                strJson += "\"matName\":\"" + StringToJson(sdr[8].ToString()) + "\",";
                strJson += "\"facCode\":\"" + sdr[9].ToString() + "\",";
                strJson += "\"facName\":\"" + sdr[10].ToString() + "\",";
                strJson += "\"matUnit\":\"" + sdr[11].ToString() + "\",";
                strJson += "\"purchaseNum\":\"" + sdr[12].ToString() + "\",";
                strJson += "\"checkNum\":\"" + sdr[13].ToString() + "\",";
                strJson += "\"pDate\":\"" + Convert.ToDateTime(sdr[14]).ToString("yyyy-MM-dd") + "\",";
                strJson += "\"stockCode\":\"" + sdr[15].ToString() + "\",";
                strJson += "\"stockName\":\"" + sdr[16].ToString() + "\"},";

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