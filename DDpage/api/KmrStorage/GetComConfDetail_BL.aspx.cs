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
    public partial class GetComConfDetail_BL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCompany = Request.Form["Company"];
            string strDept = Request.Form["Dept"];
            string strVen = Request.Form["Ven"];
            string strName = Request.Form["Name"];
            string strMatName = Request.Form["MatName"];
            string pType = Request.Form["pType"];
            string PDID = Request.Form["PDID"];

            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.197.101;User ID=sa;Password=2018Sql101");
            con.Open();


            strSql = " select PDID, PCompany, PDept, PVenture,PName, PType, PTime, Material, PickInventory ,MStock,MCode,MRsNo";
            strSql += " FROM [DDdatabase].[dbo].[View_KocelApp_Kmr_ComConfDetail] where 1=1 and cstat=0 ";
            strSql += " and Material like '%" + strMatName + "%'";
            strSql += " and PDept like '%" + strDept + "%'";
            strSql += " and CName like '%" + strName + "%'";
            if(!string.IsNullOrEmpty(pType))
                strSql += " and PType like '%" + pType + "%'";
            if (!string.IsNullOrEmpty(PDID))
                strSql += " and PDID like '%" + PDID + "%'";
            strSql += " order by PTime desc";


            //库管员查询权限
            //strSql += " and PName like '%" + strSName + "%'";

            //库管员查询权限

            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            string strJson = "{\"data\":[]";
            if (sdr.HasRows)
            {
                strJson = "{\"data\":[";
            }
            while (sdr.Read())
            {

                strJson += "{\"PDID\":\"" + sdr[0].ToString() + "\",";
                strJson += "\"PCompany\":\"" + sdr[1].ToString() + "\",";
                strJson += "\"PDept\":\"" + sdr[2].ToString() + "\",";
                strJson += "\"PVen\":\"" + sdr[3].ToString() + "\",";
                strJson += "\"PName\":\"" + sdr[4].ToString() + "\",";
                strJson += "\"PType\":\"" + sdr[5].ToString() + "\",";
                strJson += "\"PTime\":\"" + sdr[6].ToString() + "\",";
                strJson += "\"PMatName\":\"" + StringToJson(sdr[7].ToString()) + "\",";
                strJson += "\"PNum\":\"" + sdr[8].ToString() + "\",";
                strJson += "\"MCode\":\"" + sdr[10].ToString() + "\",";
                strJson += "\"MRsNo\":\"" + sdr[11].ToString() + "\",";
                strJson += "\"PStock\":\"" + sdr[9].ToString() + "\"},";

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