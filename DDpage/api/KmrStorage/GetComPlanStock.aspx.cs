using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.KmrStorage
{
    public partial class GetComPlanStock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCompany = Request.Form["strCom"];
            string strMatCode = Request.Form["strMatCode"];
            string strMatNum = Request.Form["strMatNum"];


            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();

            SqlConnection conNew = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            conNew.Open();

            strSql = "select distinct 库存地点,库存代码 from [DataFromSap].[dbo].[View_SAP_Inventory] where 1=1";
            strSql += " and 物料代码 = '" + strMatCode + "'";
            strSql += " and 库存 >=" + strMatNum + "";
            strSql += " and 库存代码  in (select distinct 库房代码 from [DataFromSap].[dbo].[View_SAP_SapEhrComStock] where 公司简称 = '" + strCompany + "')";

            SqlCommand cmdStock = new SqlCommand(strSql, con);
            SqlDataReader sdrStock = cmdStock.ExecuteReader();
            string strJson = "{\"data\":[]";
            if (!sdrStock.HasRows)
            {
                string strSqltmp = "select distinct 库存地点,库存代码 from [DataFromSap].[dbo].[View_SAP_Inventory] where 1=1";
                strSqltmp += " and 物料代码 = '" + strMatCode + "'";
                strSqltmp += " and 库存代码  in (select distinct 库房代码 from [DataFromSap].[dbo].[View_SAP_SapEhrComStock] where 公司简称 = '" + strCompany + "')";
                SqlCommand cmdtmp = new SqlCommand(strSqltmp, conNew);
                SqlDataReader sdrtmp = cmdtmp.ExecuteReader();

                strJson = "{\"data\":[";
                while (sdrtmp.Read())
                {                   
                    strJson += "{\"name\":\"" + sdrtmp[0].ToString() + "\",";
                    strJson += "\"code\":\"" + sdrtmp[1].ToString() + "\"},";
                }
                sdrtmp.Close();
                conNew.Close();
            }
            else
            {
                strJson = "{\"data\":[";
                while (sdrStock.Read())
                {
                    strJson += "{\"name\":\"" + sdrStock[0].ToString() + "\",";
                    strJson += "\"code\":\"" + sdrStock[1].ToString() + "\"},";

                }
            }

            strJson = strJson.Substring(0, strJson.Length - 1);
            strJson = strJson + "]}";
            sdrStock.Close();

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