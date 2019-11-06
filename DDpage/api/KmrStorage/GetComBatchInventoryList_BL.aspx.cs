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
    public partial class GetComBatchInventoryList_BL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strComName = Request.Form["comName"];
            string strDeptName = Request.Form["deptName"];
            string strStockName = Request.Form["stockName"];
            string strVenId = Request.Form["venId"];
            string strCostCenterId = Request.Form["costCenterId"];

            //strComName = "直属事业部";
            //strVenId = "1363";
            //strCostCenterId = "1100G07001";

            string strSql = "";
            SqlConnection con = new SqlConnection("Data Source=192.168.197.101;User ID=sa;Password=2018Sql101");
            con.Open();

            strSql = "select top(30) 物料代码,物料名称,批次,基本计量单位,库存地点,库存代码,工厂,工厂代码,物料组,物料组代码,库存,物料代码+工厂代码+库存代码+批次 as ID from [DDdatabase].[dbo].[View_KocelApp_Kmr_BBInventoryInf] where 1=1 ";
            strSql += " and 库存地点 like '%" + strStockName + "%'";
            strSql += " and 公司='"+ strComName + "'";
            strSql += " and 物料代码 in (select distinct Right('000000000000'+[FMatCode],18)  from [DDdatabase].[dbo].[Tab_KocelApp_Kmr_BatchCCBaseData] where FCompany='" + strComName + "' and [FVenCode]='" + strVenId + "' and [FCostCenterCode]='" + strCostCenterId + "')";


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