﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.KmrStorage
{
    public partial class GetSapVen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCom = Request.Form["strCom"];
            string strDept = Request.Form["strDept"];
            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=BPMS;User ID=tide;Password=lan@2mail");
            con.Open();
            strSql = "select distinct [经营体名称],[经营体ERPID],[财务成本中心编码]  FROM [kocelbasedata].[dbo].[tdm_public_personinfo]  where 1=1 and 职员状态='在职'  ";
            strSql += " and [公司] ='" + strCom + "'";
            strSql += " and [部门] ='" + strDept + "'";
            strSql += " and len([经营体名称])>0";
            strSql += " order by  经营体名称";
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            string strJson = "{\"data\":[]";
            if (sdr.HasRows)
            {
                strJson = "{\"data\":[";
            }
            while (sdr.Read())
            {

                strJson += "{\"ven\":\"" + sdr[0].ToString() + "\",";
                strJson += "\"SapVenId\":\"" + sdr[1].ToString() + "\",";
                strJson += "\"SapCost\":\"" + sdr[2].ToString() + "\"},";

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