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
    public partial class GetQueryDeclarePlanList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strComName = Request.Form["company"];
            string strFacName = Request.Form["fac"];
            string strSDate = Request.Form["sDate"];
            string strEDate = Request.Form["eDate"];
            string strMatName = Request.Form["matName"];
            string strSql = "";
            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();


            strSql = "SELECT top(20) [matCode],[matName],[facName] ,isNull([BADAT],'1900-01-01') as BADAT ,[MSEHL],[venDeptDesc],[MENGE],[purchaseOrder],[suppName] ";
            strSql += " from [DataFromSap].[dbo].[View_SAP_Psekpo] where (matName like '%" + strMatName + "%' or matCode like '%" + strMatName + "%') ";
            strSql += " and facName like '%" + strFacName + "%'";
            if(strSDate.Length>0){
                strSql += " and [BADAT] >= '" + strSDate + "'";
            }

            if (strEDate.Length > 0)
            {
                strSql += " and [BADAT] <= '" + strEDate + "'";
            }

            strSql += " and facCode  in (select distinct 工厂代码 from [DataFromSap].[dbo].[View_SAP_SapEhrComStock] where 公司简称 like  '%" + strComName + "%')";


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
                strJson += "\"facName\":\"" + sdr[2].ToString() + "\",";
                strJson += "\"badat\":\"" + Convert.ToDateTime(sdr[3]).ToString("yyyy-MM-dd") + "\",";
                strJson += "\"msehl\":\"" + sdr[4].ToString() + "\",";
                strJson += "\"venDeptDesc\":\"" + sdr[5].ToString() + "\",";
                strJson += "\"menge\":\"" + sdr[6].ToString() + "\",";
                strJson += "\"purOrder\":\"" + sdr[7].ToString() + "\",";
                strJson += "\"suppName\":\"" + sdr[8].ToString() + "\"},";

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