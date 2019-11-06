using DDpage.Get_KmrStorage_PlanList;
using DDpage.KmrAppMatCreateRsNo;
using DDpage.Post_KmrApp_ComResearchPick;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.KmrStorage
{
    public partial class interfaceTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int t1 = 0;
            Boolean stat = false;
            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DDdatabase;User ID=tide;Password=lan@2mail");
            con.Open();
            SqlConnection conNew = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DDdatabase;User ID=tide;Password=lan@2mail");
            conNew.Open();


            //获取tab_kocelapp_Kmr_WaitSapRecord数据  ifSap=0
            string strWaitSapID = "";
            string strPDID = "";
            string strType = "";
            string strItemType = "";
            string strOddNo = "";
            string strCostCenter = "";
            string strSapVenture = "";
            string strComCode = "";
            string strMFactoryCode = "";
            string strMStockCode = "";
            string strMCode = "";
            string strMInventory = "";
            string strMUnit = "";
            string strMRsNo = "";
            string strMRsPos = "";
            string strVornor = "";
            string strMBatch = "";
            string strPersonCode = "";

            string strSql = "select [ID],[PType],[PItemType],[PDID],[oddNo],[CostCenter],[SapVenture],[FCompanyCode],[MFactoryCode],[MStockCode],[MCode],[PNum],[MUnit],[MRsNo],[MRsPos],[Vornor],[MBatch],[personCode] ";
            strSql += " from [DDdatabase].[dbo].[Tab_kocelApp_Kmr_WaitSapRecord] where ifSap = 0";
            strSql += " and [PDID] not in (select distinct pickID from [DDdatabase].[dbo].[Tab_kocelApp_Kmr_PostSapRecord])";
            strSql += " and PDID='5b530809-8de0-4674-89dc-0f86ab70eace'";
            SqlCommand cmdWaitSapList = new SqlCommand(strSql, con);
            SqlDataReader sdrWaitSapList = cmdWaitSapList.ExecuteReader();
            while (sdrWaitSapList.Read())
            {
                //获取数据
                strWaitSapID = sdrWaitSapList["ID"].ToString();
                strType = sdrWaitSapList["PType"].ToString();
                strItemType = sdrWaitSapList["PItemType"].ToString();
                strPDID = sdrWaitSapList["PDID"].ToString();
                strOddNo = sdrWaitSapList["oddNo"].ToString();
                strCostCenter = sdrWaitSapList["CostCenter"].ToString();
                strSapVenture = sdrWaitSapList["SapVenture"].ToString();
                strComCode = sdrWaitSapList["FCompanyCode"].ToString();
                strMFactoryCode = sdrWaitSapList["MFactoryCode"].ToString();
                strMStockCode = sdrWaitSapList["MStockCode"].ToString();
                strMCode = sdrWaitSapList["MCode"].ToString();
                strMInventory = sdrWaitSapList["PNum"].ToString();
                strMUnit = sdrWaitSapList["MUnit"].ToString();
                strMRsNo = sdrWaitSapList["MRsNo"].ToString();
                strMRsPos = sdrWaitSapList["MRsPos"].ToString();
                strVornor = sdrWaitSapList["Vornor"].ToString();
                strMBatch = sdrWaitSapList["MBatch"].ToString();
                strPersonCode = sdrWaitSapList["personCode"].ToString();

                if (strType == "公司内领料")
                {

                    if (strItemType == "研发类领料")
                    {
                        //创建预留获取预留编号

                        ZPP_RESERVATION_CREATE res_create = new ZPP_RESERVATION_CREATE();
                        ZSD_RESERVATION_HEADER[] res_head = new ZSD_RESERVATION_HEADER[1];
                        ZSD_RESERVATION_LINE[] res_line = new ZSD_RESERVATION_LINE[1];
                        ZPP_RESERVATION_CREATEResponse res_return = new ZPP_RESERVATION_CREATEResponse();

                        ZSD_RESERVATION_HEADER sap_headItem = new ZSD_RESERVATION_HEADER();

                        sap_headItem.RSDAT = DateTime.Now.ToString("yyyy-MM-dd");
                        sap_headItem.BWART = "Z03";
                        sap_headItem.KOSTL = strCostCenter.ToUpper();
                        sap_headItem.AUFNR = strOddNo;
                        //sap_headItem.GSBER = 
                        res_head[0] = sap_headItem;

                        res_create.ZSD_RESERVATION_HEADER = res_head;

                        //sap接口调用
                        ZSD_RESERVATION_LINE res_itemtmp = new ZSD_RESERVATION_LINE();
                        res_itemtmp.MATNR = strMCode;
                        res_itemtmp.ERFMG = Convert.ToDecimal(strMInventory);
                        res_itemtmp.ERFME = strMUnit;
                        res_itemtmp.LGORT = strMStockCode;
                        res_itemtmp.CHARG = strMBatch;
                        res_itemtmp.WERKS = strMFactoryCode;

                        res_line[0] = res_itemtmp;

                        //sap接口调用
                        //sap 生成预留

                        res_create.ZSD_RESERVATION_LINE = res_line;
                        SI_ZPP_RESERVATION_CREATE_SenderService sap_server = new SI_ZPP_RESERVATION_CREATE_SenderService();
                        sap_server.Credentials = new NetworkCredential("prd_pisuper", "handhand0");
                        res_return = sap_server.SI_ZPP_RESERVATION_CREATE_Sender(res_create);

                        string strResRsNo = res_return.MESSAGE;

                        //sap 生成预留
                        //判断strResRsNo是不是数字，如果是数字，预留生成，如果不是保留错误代码
                        if (IsNumeric(strResRsNo))
                        {
                            //依据预留获取预留信息,201领料接口调用
                            stat = PostResearchRsNo(strPDID, strResRsNo, "1", strType, strItemType, strPersonCode);
                            //依据预留获取预留信息,201领料接口调用
                        }
                        else
                        {
                            GetErrRecord(strPDID, strResRsNo, strType, strItemType, strPersonCode);
                            stat = false;
                        }
                    }

                }


            }
        }

        static bool IsNumeric(string s)
        {
            // 用正则表达式判断是否为数字
            return Regex.IsMatch(s, @"^[+-]?\d*[.]?\d*$");
        }

        public Boolean PostResearchRsNo(string strPid, string rsNo, string thNum, string type, string itemType, string perCode)
        {
            //获取预留信息
            ZPP_RESB sap_resb = new ZPP_RESB();
            ZSDATA_RESB[] re_data;
            sap_resb.Z_AUFNR = "";
            sap_resb.Z_RSNUM = rsNo;
            ZPP_RESBResponse sap_resbRes = new ZPP_RESBResponse();

            SI_ZPP_RESB_SenderService sap_serv = new SI_ZPP_RESB_SenderService();
            sap_serv.Credentials = new NetworkCredential("prd_pisuper", "handhand0");
            sap_resbRes = sap_serv.SI_ZPP_RESB_Sender(sap_resb);
            re_data = sap_resbRes.Z_RESBINFO;
            //获取预留信息
            //创建Z03领料信息
            int x = 0;
            ZPP_GOODSMVT_Z04 sap_goodsmvt = new ZPP_GOODSMVT_Z04();
            ZSD_GOODSMVT_HEAD[] sap_head = new ZSD_GOODSMVT_HEAD[1];
            ZSD_GOODSMVT_ITEM[] sap_item = new ZSD_GOODSMVT_ITEM[Convert.ToInt16(thNum)];
            ZPP_GOODSMVT_Z04Response[] sap_return = new ZPP_GOODSMVT_Z04Response[1];

            ZSD_GOODSMVT_HEAD sap_headItem = new ZSD_GOODSMVT_HEAD();
            sap_headItem.BWART = "Z03";

            sap_head[0] = sap_headItem;

            sap_goodsmvt.ZSD_GOODSMVT_HEAD = sap_head;

            //创建Z03领料信息
            foreach (ZSDATA_RESB ob in re_data)
            {
                ZSD_GOODSMVT_ITEM sap_itemtmp = new ZSD_GOODSMVT_ITEM();
                sap_itemtmp.RSNUM = ob.RSNUM;
                sap_itemtmp.RSPOS = ob.RSPOS;
                sap_itemtmp.BDMNG = ob.BDMNG;

                sap_item[x] = sap_itemtmp;
                x += 1;
            }
            //sap 接口传递并接受返回值
            sap_goodsmvt.ZSD_GOODSMVT_ITEM = sap_item;
            SI_ZPP_GOODSMVT_Z04Service sap_server = new SI_ZPP_GOODSMVT_Z04Service();
            sap_server.Credentials = new NetworkCredential("prd_pisuper", "handhand0");

            ZPP_GOODSMVT_Z04Response res_return = sap_server.SI_ZPP_GOODSMVT_Z04(sap_goodsmvt);

            string ifPost = res_return.Z_MSGTY;
            string ifMessage = res_return.Z_MSGTX;
            //string ifNumber = res_return.MBLNR;
            //string ifYear = res_return.MJAHR;

            if (ifPost == "S")
            {
                //接口调用成功
                string strTmp = ifMessage;
                GetSucRecord(strPid, strTmp, type, itemType, perCode);
                return true;
                //接口调用成功
            }
            else
            {
                GetErrRecord(strPid, ifMessage, type, itemType, perCode);
                return false;
            }
            //sap 接口传递并接受返回值

        }


        public void GetErrRecord(string recordId, string errMessage, string errType, string errItemType, string perCode)
        {
            //记录错误信息,错误数据表，ID，错误信息，错误类型
            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();

            string strSql = "insert into [DDdatabase].[dbo].[Tab_KocelApp_Kmr_PostSapRecord]([ID],[ifSap] ,[pickType],[pickID],[pickInfo],[pickItemType],[pickDate],[pickPersonId]) ";
            strSql += " VALUES('" + System.Guid.NewGuid().ToString() + "','0','" + errType + "','" + recordId + "','" + errMessage + "','" + errItemType + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + perCode + "')";
            SqlCommand cmdsuc = new SqlCommand(strSql, con);
            cmdsuc.ExecuteNonQuery();

            con.Close();
        }

        public void GetSucRecord(string recordId, string sucMessage, string sucType, string sucItemType, string perCode)
        {
            //记录成功信息,错误数据表，ID，错误信息，错误类型
            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();

            string strSql = "insert into [DDdatabase].[dbo].[Tab_KocelApp_Kmr_PostSapRecord]([ID],[ifSap] ,[pickType],[pickID],[pickInfo],[pickItemType],[pickDate],[pickPersonId]) ";
            strSql += " VALUES('" + System.Guid.NewGuid().ToString() + "','1','" + sucType + "','" + recordId + "','" + sucMessage + "','" + sucItemType + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + perCode + "')";
            SqlCommand cmdsuc = new SqlCommand(strSql, con);
            cmdsuc.ExecuteNonQuery();

            con.Close();

        }

    }
}