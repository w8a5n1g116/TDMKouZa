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
    public partial class GetComPickInfo_BL_KuGuan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strMatName = Request.Form["matName"];
            string strCom = Request.Form["Company"];
            string strDept = Request.Form["Dept"];
            string strVen = Request.Form["Ven"];
            string strName = Request.Form["Name"];
            string strPstat = Request.Form["strPstat"];
            string strPtype = Request.Form["pType"];
            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.197.101;User ID=sa;Password=2018Sql101");
            con.Open();

            strSql += " select cpd.[Material], cpd.[MCode], case when cpd.[PDStat]=1 then '待确认' when cpd.[PDStat]=2 then '完成' when cpd.[PDStat]=4 then '完成' when cpd.[PDStat]=9 then '异常' else '异常' end as PDStat, cpd.[MStock], cpd.[MUnit], cpd.[PickInventory], cpd.[PType], cpd.[ifSap], cpd.[pickItemType], cpd.[pickInfo] , cpi.[PickID], cpi.[PLinkID], cpi.[PCompany], cpi.[PDept], cpi.[PVenture] , cpi.[PName], cpi.[PTime], cpi.[PickStat]  FROM [DDdatabase].[dbo].[View_KocelApp_Kmr_ComPickDetail] as cpd LEFT JOIN [DDdatabase].[dbo].[View_KocelApp_Kmr_ComPickInfo] as cpi  on  cpd.[PLinkID] = cpi.[PLinkID] where  1=1   and cpd.[PLinkID] in";

            strSql += " (SELECT top(30) [PLinkID]";
            strSql += " FROM [DDdatabase].[dbo].[View_KocelApp_Kmr_ComPickInfo] where 1=1";
            strSql += "  and [PCompany] like '%" + strCom + "%'";
            strSql += "  and [PDept] like '%" + strDept + "%'";
            //strSql += "  and [PVenture] like '%" + strVen + "%'";
            strSql += "  and ([PName] like '%" + strName + "%' or [CName] like '%" + strName + "%' )";
            //if(string.IsNullOrEmpty(strPstat))
            //    strSql += "  and [PickStat] = '完成'";           
            if (!string.IsNullOrEmpty(strMatName))
                strSql += "  and [PLinkID] in ( select distinct PLinkID from [DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComPickDetail] where Material like '%" + strMatName + "%' or MCode like '%" + strMatName + "%' )";
            if (!string.IsNullOrEmpty(strPtype))
                strSql += " and [PType] = '" + strPtype + "'";
            strSql += "  order by [PTime] desc )";
            strSql += "  and ( cpd.[PDStat] = 2 or cpd.[PDStat] = 4 )";
            strSql += "  order by cpd.[PTime] desc";

            //strSql += " and MStock like '%" + strStName + "%'";


            //库管员查询权限
            //strSql += " and PName like '%" + strSName + "%'";

            //库管员查询权限

            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            string strJson = "{\"data\":[ ";
            if (sdr.HasRows)
            {
                strJson = "{\"data\":[ ";
            }
            while (sdr.Read())
            {

                strJson += "{\"matName\":\"" + StringToJson(sdr[0].ToString()) + "\",";
                strJson += "\"MCode\":\"" + sdr[1].ToString() + "\",";
                strJson += "\"PDStat\":\"" + sdr[2].ToString() + "\",";
                strJson += "\"mStock\":\"" + sdr[3].ToString() + "\",";
                strJson += "\"mUnit\":\"" + sdr[4].ToString() + "\",";
                strJson += "\"Pnum\":\"" + sdr[5].ToString() + "\",";
                strJson += "\"Ptype\":\"" + sdr[6].ToString() + "\",";
                strJson += "\"ifSap\":\"" + sdr[7].ToString() + "\",";
                strJson += "\"pickItemType\":\"" + sdr[8].ToString() + "\",";                
                strJson += "\"pickInfo\":\"" + sdr[9].ToString() + "\",";
                strJson += "\"PID\":\"" + sdr[10].ToString() + "\",";
                strJson += "\"PLID\":\"" + sdr[11].ToString() + "\",";
                strJson += "\"PCom\":\"" + sdr[12].ToString() + "\",";
                strJson += "\"PDept\":\"" + sdr[13].ToString() + "\",";
                strJson += "\"PVen\":\"" + sdr[14].ToString() + "\",";
                strJson += "\"PName\":\"" + sdr[15].ToString() + "\",";
                strJson += "\"PTime\":\"" + sdr[16].ToString() + "\",";
                strJson += "\"PStat\":\"" + sdr[17].ToString() + "\"},";

            }


            //while (sdr.Read())
            //{

            //    strJson += "{\"PID\":\"" + sdr[0].ToString() + "\",";
            //    strJson += "\"PLID\":\"" + sdr[1].ToString() + "\",";
            //    strJson += "\"PCom\":\"" + sdr[2].ToString() + "\",";
            //    strJson += "\"PDept\":\"" + sdr[3].ToString() + "\",";
            //    strJson += "\"PVen\":\"" + sdr[4].ToString() + "\",";
            //    strJson += "\"PName\":\"" + sdr[5].ToString() + "\",";
            //    strJson += "\"PTime\":\"" + sdr[6].ToString() + "\",";
            //    strJson += "\"PType\":\"" + sdr[8].ToString() + "\",";
            //    strJson += "\"PStat\":\"" + sdr[7].ToString() + "\"},";


            //}

            sdr.Close();

            //strSql = @"SELECT am.[PickID],am.[PLinkID],am.[PCompany],am.[PDept],am.[PVenture] ,am.[PName],am.[PTime],am.[PickStat],'仓库调拨' as PType
            //        FROM [DDdatabase].[dbo].[Tab_kocelApp_Kmr_AllotMaterial] as am
            //        LEFT JOIN [DDdatabase].[dbo].Tab_kocelApp_Kmr_AllotDetail as ad on ad.PLinkID = am.PLinkID
            //        LEFT JOIN [DDdatabase].[dbo].Tab_kocelApp_Kmr_ComFacBaseData as cfb on cfb.FStockCode = ad.MOutStockCode
            //        where 1=1";

            //strSql += "and (am.[PName] like '%" + strName + "%' or cfb.[FKeeper] like '%" + strName + "%') order by am.[PTime] desc";



            ////strSql = " SELECT [PickID],[PLinkID],[PCompany],[PDept],[PVenture] ,[PName],[PTime],[PickStat],'仓库调拨' as PType";
            ////strSql += " FROM [DDdatabase].[dbo].[Tab_kocelApp_Kmr_AllotMaterial] where 1=1";
            ////strSql += "  and [PCompany] like '%" + strCom + "%'";
            //////strSql += "  and [PDept] like '%" + strDept + "%'";
            //////strSql += "  and [PVenture] like '%" + strVen + "%'";
            ////strSql += "  and [PName] like '%" + strName + "%'";
            ////strSql += "  order by [PTime] desc";

            ////库管员查询权限

            //SqlCommand cmd2 = new SqlCommand(strSql, con);
            //sdr = cmd2.ExecuteReader();
            //while (sdr.Read())
            //{

            //    strJson += "{\"PID\":\"" + sdr[0].ToString() + "\",";
            //    strJson += "\"PLID\":\"" + sdr[1].ToString() + "\",";
            //    strJson += "\"PCom\":\"" + sdr[2].ToString() + "\",";
            //    strJson += "\"PDept\":\"" + sdr[3].ToString() + "\",";
            //    strJson += "\"PVen\":\"" + sdr[4].ToString() + "\",";
            //    strJson += "\"PName\":\"" + sdr[5].ToString() + "\",";
            //    strJson += "\"PTime\":\"" + sdr[6].ToString() + "\",";
            //    strJson += "\"PType\":\"" + sdr[8].ToString() + "\",";
            //    strJson += "\"PStat\":\"" + sdr[7].ToString() + "\"},";


            //}




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
