using DDpage.Post_KmrApp_CheckIn;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.KmrStorage
{
    public partial class checkInList_sumbit1900 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCompany = Request.Form["PCompany"].ToString();
            string strDept = Request.Form["PDept"].ToString();
            string strVen = Request.Form["PVenture"].ToString();
            string strName = Request.Form["PName"].ToString();
            string strPersonCode = Request.Form["PCode"].ToString();
            string strTime = Request.Form["PTime"].ToString();
            string dataString = Request.Form["dataList"].ToString();
            Boolean stat = false;
            int t1 = 0;
            string strSql = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();


            JArray jsonObj = JArray.Parse(dataString);
            foreach (JObject jObject in jsonObj)
            {       


                //调用sap入库过账接口赋值
                ZPP_GOODSINVOCHER sap_data = new ZPP_GOODSINVOCHER();
                ZPP_GOODSINVOCHERResponse sap_return = new ZPP_GOODSINVOCHERResponse();
                ZSD_GOODSINVOCHERITEM[] sap_item = new ZSD_GOODSINVOCHERITEM[1];
                ZSD_GOODSMVTHEADER[] sap_head = new ZSD_GOODSMVTHEADER[1];
                ZSD_GOODSMVTHEADER item_head = new ZSD_GOODSMVTHEADER();
                item_head.BUDAT = DateTime.Now.ToString("yyyy-MM-dd");
                sap_head[0] = item_head;
                sap_data.ZT_GOODSMVTHEADER = sap_head;

                int x = 0;
                ZSD_GOODSINVOCHERITEM item_item = new ZSD_GOODSINVOCHERITEM();
                item_item.MATERIAL = jObject["matCode"].ToString();
                item_item.PLANT = jObject["facCode"].ToString();
                item_item.STGE_LOC = jObject["stockCode"].ToString();
                item_item.MOVE_TYPE = "101";
                item_item.ENTRY_QNT = Convert.ToDecimal(jObject["pnum"].ToString()) - Convert.ToDecimal(jObject["pkz"].ToString()) - Convert.ToDecimal(jObject["pkbz"].ToString());
                item_item.ENTRY_UOM = "";
                item_item.PO_NUMBER = jObject["purOrder"].ToString();
                item_item.PO_ITEM = jObject["itemOrder"].ToString();
                item_item.MVT_IND = "B";
                item_item.MOVE_MAT = jObject["matCode"].ToString();
                item_item.MOVE_PLANT = jObject["facCode"].ToString();
                item_item.MOVE_STLOC = jObject["stockCode"].ToString();
                item_item.NB_SLIPS = "1";

                sap_item[x] = item_item;

                x += 1;

                sap_data.ZT_GOODSINVOCHERITEM = sap_item;

                SI_ZPP_GOODSINVOCHERService sap_serv = new SI_ZPP_GOODSINVOCHERService();
                sap_serv.Credentials = new NetworkCredential("prd_pisuper", "handhand0");

                sap_return = sap_serv.SI_ZPP_GOODSINVOCHER(sap_data);

                string ifPost = sap_return.MESSG;
                string ifMessage = sap_return.INFO;
                string ifNumber = sap_return.MBLNR;
                string ifYear = sap_return.MJAHR;

               

                if (ifPost == "S")
                {
                    stat = true;
                }
                

            }

            //调用sap入库过账接口




            con.Close();

            if (stat)
            {
                Response.Write("数据已提交！");
            }
            else
            {
                Response.Write("数据未保存，请联系系统管理员并反馈相关问题！");
            }

           
        }

        public void GetErrRecord(string recordId, string errMessage, string errType, string perCode)
        {
            //记录错误信息,错误数据表，ID，错误信息，错误类型
            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();

            string strSql = "insert into [DDdatabase].[dbo].[Tab_KocelApp_Kmr_PostSapRecord]([ID],[ifSap] ,[pickType],[pickID],[pickInfo],[pickItemType],[pickDate],[pickPersonId]) ";
            strSql += " VALUES('" + System.Guid.NewGuid().ToString() + "','0','验收入库','" + recordId + "','" + errMessage + "','" + errType + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + perCode + "')";
            SqlCommand cmdsuc = new SqlCommand(strSql, con);
            cmdsuc.ExecuteNonQuery();

            con.Close();
        }

        public void GetSucRecord(string recordId, string sucMessage, string sucType, string perCode)
        {
            //记录成功信息,错误数据表，ID，错误信息，错误类型
            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();

            string strSql = "insert into [DDdatabase].[dbo].[Tab_KocelApp_Kmr_PostSapRecord]([ID],[ifSap] ,[pickType],[pickID],[pickInfo],[pickItemType],[pickDate],[pickPersonId]) ";
            strSql += " VALUES('" + System.Guid.NewGuid().ToString() + "','1','验收入库','" + recordId + "','" + sucMessage + "','" + sucType + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + perCode + "')";
            SqlCommand cmdsuc = new SqlCommand(strSql, con);
            cmdsuc.ExecuteNonQuery();

            con.Close();

        }

    }
}