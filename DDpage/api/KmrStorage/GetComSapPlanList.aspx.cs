using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDpage.Get_KmrStorage_PlanList;

namespace DDpage.api.KmrStorage
{
    public partial class GetComSapPlanList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCompany = Request.Form["company"];
            ZPP_RESB sap_resb = new ZPP_RESB();
            ZSDATA_RESB[] re_data;


            sap_resb.Z_AUFNR = Request.Form["OddNo"];
            //sap_resb.Z_RSNUM = "4396";
            sap_resb.Z_RSNUM = Request.Form["RsNo"];
            //sap_resb.Z_VORNR = "0000";
            //sap_resb.Z_VORNR = Request.QueryString["OblNo"];

            ZPP_RESBResponse sap_resbRes = new ZPP_RESBResponse();


            SI_ZPP_RESB_SenderService sap_serv = new SI_ZPP_RESB_SenderService();
            sap_serv.Credentials = new NetworkCredential("prd_pisuper", "handhand0");

            sap_resbRes = sap_serv.SI_ZPP_RESB_Sender(sap_resb);

            re_data = sap_resbRes.Z_RESBINFO;

            string strJson = "{\"data\":[]";
            if (re_data.Length > 0)
            {
                strJson = "{\"data\":[";
            }


            Random r = new Random();
            int i = 0;
            string strPID;

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();

            SqlConnection conNew = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            conNew.Open();

            foreach (ZSDATA_RESB ob in re_data)
            {
                strJson += "{\"OddNo\":\"" + ob.AUFNR + "\",";
                strJson += "\"vornr\":\"" + ob.VORNR + "\",";
                strJson += "\"RsNo\":\"" + ob.RSNUM + "\",";
                strJson += "\"RsPos\":\"" + ob.RSPOS + "\",";
                strJson += "\"pmaktx\":\"" + StringToJson(ob.PRODUCE_MAKTX) + "\",";
                strJson += "\"matnr\":\"" + ob.MATNR + "\",";
                strJson += "\"rmaktx\":\"" + StringToJson(ob.REQUIRE_MAKTX) + "\",";
                strJson += "\"menge\":\"" + ob.MENGE + "\",";
                strJson += "\"factory\":\"" + ob.WERKS + "\",";
                strJson += "\"stock\":\"" + ob.LGORT + "\",";
                if (ob.LGORT.Length==0)
                {
                    //库存地点为空，检索该物料代码下的有库存的地点，如果都没有，显示该公司所有库存地点
                    strJson += "\"stockList\":[";

                    string strSql = "select distinct 库存地点,库存代码 from [DataFromSap].[dbo].[View_SAP_Inventory] where 1=1";
                    strSql += " and 物料代码 = '" + ob.MATNR + "'";
                    strSql += " and 库存 >=" + ob.MENGE + "";
                    strSql += " and 库存代码  in (select distinct 库房代码 from [DataFromSap].[dbo].[View_SAP_SapEhrComStock] where 公司简称 = '" + strCompany + "')";

                    SqlCommand cmdStock = new SqlCommand(strSql, con);
                    SqlDataReader sdrStock = cmdStock.ExecuteReader();
                    if (!sdrStock.HasRows)
                    {
                        string strSqltmp = "select distinct 库存地点,库存代码 from [DataFromSap].[dbo].[View_SAP_Inventory] where 1=1";
                        strSqltmp += " and 物料代码 = '" + ob.MATNR + "'";
                        strSqltmp += " and 库存代码  in (select distinct 库房代码 from [DataFromSap].[dbo].[View_SAP_SapEhrComStock] where 公司简称 = '" + strCompany + "')";
                        SqlCommand cmdtmp = new SqlCommand(strSqltmp, conNew);
                        SqlDataReader sdrtmp = cmdtmp.ExecuteReader();

                        while (sdrtmp.Read())
                        {
                            strJson += "{\"name\":\"" + sdrtmp[0].ToString() + "\",";
                            strJson += "\"code\":\"" + sdrtmp[1].ToString() + "\"},";
                        }
                        sdrtmp.Close();
                    }
                    else
                    {
                        while (sdrStock.Read())
                        {
                            strJson += "{\"name\":\"" + sdrStock[0].ToString() + "\",";
                            strJson += "\"code\":\"" + sdrStock[1].ToString() + "\"},";
                            
                        }
                    }

                    sdrStock.Close();

                    strJson = strJson.Substring(0, strJson.Length - 1);
                    strJson += "],";
                    //库存地点为空，检索该物料代码下的有库存的地点，如果都没有，显示该公司所有库存地点
                }
                strJson += "\"mGroup\":\"" + ob.MATKL + "\",";
                i = r.Next(1000, 9999);
                strPID = System.DateTime.Now.ToFileTime().ToString() + i.ToString();
                strJson += "\"matID\":\"" + strPID + "\",";
                strJson += "\"unit\":\"" + ob.MEINS + "\"},";
            }

            con.Close();
            conNew.Close();

            strJson = strJson.Substring(0, strJson.Length - 1);
            strJson = strJson + "],";
            if (re_data.Length > 0)
            {
                strJson += "\"stat\":\"" + re_data.Length + "\"}";
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