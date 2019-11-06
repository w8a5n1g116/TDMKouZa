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
    public partial class GetComOutPlanBatchInventoryList : System.Web.UI.Page
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
            //strComName = "直属事业部";
            //strVenId = "1363";
            //strCostCenterId = "1100G07001";
            string strAufnrNum = "0";
            string strAllWeight = "0";
            string strSql = "";
            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();

            strSql = " select COUNT(*) as num,Round(sum(cast([NTGEW] as float)),2) as weight FROM (";
            strSql += " select  *, (cast(year(cast(convert(nvarchar(100),budat,23) as datetime)) as nvarchar)+'-'+cast(month(cast(convert(nvarchar(100),budat,23) as datetime)) as nvarchar)+'-'+cast(day(cast(convert(nvarchar(100),budat,23) as datetime)) as nvarchar))  FDate";
            strSql +=" FROM [DataFromSap].[dbo].[T_SAP_PP_getZPPRP013] ";
            strSql +=" where budat not like '%0000%' and LEN(cast(budat as nvarchar))> 0  ";
            strSql += " and cast(vornr as int) = cast('" + strProNo + "' as int) ";
            strSql +=" ) t1";
            strSql += " where Year(FDate)= '" + DateTime.Now.Year.ToString() + "' ";
            strSql += " and Month(FDate)= '" + DateTime.Now.Month.ToString() + "'";




            SqlCommand cmdNum = new SqlCommand(strSql, con);
            SqlDataReader sdrNum = cmdNum.ExecuteReader();

            while (sdrNum.Read())
            {

                strAufnrNum = sdrNum[0].ToString() ;
                strAllWeight = sdrNum[1].ToString();
                //strJson += "\"orderNum\":\"" + sdr[12].ToString() + "\"},";

            }

            sdrNum.Close();

            strSql = "  select top(10) 物料代码,物料名称,批次,基本计量单位,库存地点,库存代码,工厂,工厂代码,物料组,物料组代码,库存,物料代码+工厂代码+库存代码+批次 as ID from [DDdatabase].[dbo].[View_KocelApp_Kmr_BBInventoryInf] where 1=1 ";
            strSql += " and 库存地点 like '%" + strStockName + "%'";
            strSql += " and 公司='" + strComName + "'";
            strSql += " and 物料名称 like '%" + strMatName + "%'";
            strSql += " and 物料代码 not in (select distinct MCode FROM [DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComBatchPickDetail]  where PLinkID in (select distinct PLinkID from[DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComBatchPickMaterial] where YEAR([PTime])='" + DateTime.Now.Year.ToString() + "' and MONTH([PTime])='" + DateTime.Now.Month.ToString() + "' and PType = '计划外领料'))";

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
                strJson += "\"matID\":\"" + sdr[11].ToString() + "\",";
                strJson += "\"proWeight\":\"" + strAllWeight + "\",";
                strJson += "\"orderNum\":\"" + strAufnrNum + "\"},";

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