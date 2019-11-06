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
    public partial class GetQueryInventoryList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strComName = Request.Form["company"];
            string strFacName = Request.Form["fac"];
            string strStockName = Request.Form["stock"];
            string strMatName = Request.Form["matName"];
            string strSql = "";
            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();

            strSql = "select distinct top 30 物料代码,物料名称,物料型号,批次,基本计量单位,库存地点,库存代码,工厂,工厂代码,物料组,物料组代码,库存,物料代码+工厂代码+库存代码+批次 as ID from [DataFromSap].[dbo].[View_SAP_Inventory] where (物料名称 like '%" + strMatName + "%' or 物料代码 like '%" + strMatName + "%') ";
            strSql += " and 库存地点 like '%" + strStockName + "%'";
            strSql += " and 工厂 like '%" + strFacName + "%'";
            strSql += " and 工厂代码  in (select distinct 工厂代码 from [DataFromSap].[dbo].[View_SAP_SapEhrComStock] where 公司简称 like  '%" + strComName + "%')";


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
                strJson += "\"matXh\":\"" + sdr[2].ToString() + "\",";
                strJson += "\"matNo\":\"" + sdr[3].ToString() + "\",";
                strJson += "\"matWeight\":\"" + sdr[4].ToString() + "\",";
                strJson += "\"matStock\":\"" + sdr[5].ToString() + "\",";
                strJson += "\"matStockCode\":\"" + sdr[6].ToString() + "\",";
                strJson += "\"matFac\":\"" + sdr[7].ToString() + "\",";
                strJson += "\"matFacCode\":\"" + sdr[8].ToString() + "\",";
                strJson += "\"matGroup\":\"" + StringToJson(sdr[9].ToString()) + "\",";
                strJson += "\"matGroupCode\":\"" + sdr[10].ToString() + "\",";
                strJson += "\"matNum\":\"" + sdr[11].ToString() + "\",";
                strJson += "\"matID\":\"" + sdr[12].ToString() + "\"},";

            }
            strJson = strJson.Substring(0, strJson.Length - 1);
            strJson = strJson + "]}";

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