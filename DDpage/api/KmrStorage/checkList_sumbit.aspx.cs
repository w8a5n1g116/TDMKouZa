using DDpage.Get_KmrStorage_PlanList;
using DDpage.Post_KmrApp_OutComCostPick;
//using DDpage.KmrAppComPick;
using DDpage.KmrAppMatCreateRsNo;
using DDpage.KmrAppMatPick;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.KmrStorage
{
    public partial class checkList_sumbit : System.Web.UI.Page
    {
        public string PCostCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCompany = Request.Form["PCompany"].ToString();
            string strDept = Request.Form["PDept"].ToString();
            string strVen = Request.Form["PVenture"].ToString();
            string strName = Request.Form["PName"].ToString();
            string strPersonCode = Request.Form["PCode"].ToString();
            string strTime = Request.Form["PTime"].ToString();
            string strSapVenCode = Request.Form["sapCode"].ToString();
            string strCostCode = Request.Form["costCode"].ToString();
            PCostCode = Request.Form["costCode"].ToString();
            string dataString = Request.Form["dataList"].ToString();
            string strGID = System.Guid.NewGuid().ToString();
            
            int t1 = 0;
            string strJson = "[";
            Boolean stat = false;

           

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();
            SqlConnection conNew = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            conNew.Open();

            //获取公司代码
            string strComCode = "";
            string strComSql = "select distinct(公司代码) as mx  from [DataFromSap].[dbo].[View_SAP_SapEhrComStock] where 公司简称='" + strCompany + "'";
            SqlCommand cmdCom = new SqlCommand(strComSql, conNew);
            SqlDataReader sdrCom = cmdCom.ExecuteReader();
            while (sdrCom.Read())
            {
                strComCode = sdrCom[0].ToString();
            }
            sdrCom.Close();



            JArray jsonObj = JArray.Parse(dataString);
            foreach (JObject jObject in jsonObj)
            {
                int oneNum = 0;
                int twoNum = 0;
                int thNum = 0;
                string strCheckId = "";
                //sap中checkID赋值
                strCheckId = jObject["checkid"].ToString();
                string strSql = "update [DDdatabase].[dbo].[Tab_KocelApp_Kmr_CheckDetail] set CETime='" + strTime + "',CStat='" + jObject["cstat"] + "' where 1=1";
                strSql += " and CheckID='" + jObject["checkid"] + "'";
                string strSqlPrice = "";
                SqlCommand cmdStock = new SqlCommand(strSql, con);
                t1 = cmdStock.ExecuteNonQuery();
                if (t1 != 0)
                {
                    stat = true;
                }


                strSql = "update [DDdatabase].[dbo].[Tab_kocelApp_Kmr_PickDetail] set PDStat = 4 where PDID='" + jObject["pdid"] + "'";
                SqlCommand cmdPick = new SqlCommand(strSql, con);
                t1 = cmdPick.ExecuteNonQuery();
                if (t1 != 0)
                {
                    stat = true;
                }

               


                strSql = "select * from [DDdatabase].[dbo].[Tab_kocelApp_Kmr_PickDetail] where PDStat <= 3  and PLinkID='" + jObject["linkid"] + "'";

                SqlCommand cmdPickDetail = new SqlCommand(strSql, con);
                SqlDataReader sdrPickDetail = cmdPickDetail.ExecuteReader();
                if (!sdrPickDetail.HasRows)
                {
                    strSql = "update [DDdatabase].[dbo].[Tab_kocelApp_Kmr_PickMaterial] set PickStat=4 where PLinkID='" + jObject["linkid"] + "' ";
                    SqlCommand cmdPickHead = new SqlCommand(strSql, conNew);
                    t1 = cmdPickHead.ExecuteNonQuery();
                    if (t1 != 0)
                    {
                        stat = true;
                    }

                }

                sdrPickDetail.Close();
                //conNew.Close();

                //IM接口传数据拼接json数据
                string strJCode = "";
                string strJName = "";
                string strJPName = "";
                string strJProcess = "";
                string strJPBatch = "";
                string strJStore = "";
                string strJPrice = "";
                string strLimsNo = "";
                string strPType = "";
                strSql = "select [MCode],[Material],[Vornor],[MBatch],[PickInventory],[PName],[PType]  From [DDdatabase].[dbo].[View_KocelApp_Kmr_CheckListDetail] where CheckID='" + jObject["checkid"] + "'";
                SqlCommand cmdJsonDetail = new SqlCommand(strSql, con);
                SqlDataReader sdrJsonDetail = cmdJsonDetail.ExecuteReader();
                if (sdrJsonDetail.HasRows)
                {
                    while (sdrJsonDetail.Read())
                    {
                        strSql = "select [REPORTNO] from [DataFromSap].[dbo].[View_SAP_LimsReport] where [Matnr]='" + sdrJsonDetail[0].ToString() + "'";
                        strSqlPrice = "select [VERPR] from [DataFromSap].[dbo].[MBEW] where [Matnr]='" + sdrJsonDetail[0].ToString() + "' and [BWKEY]='" + strComCode + "'";
                        strJCode = sdrJsonDetail[0].ToString();
                        strJName = sdrJsonDetail[1].ToString();
                        strJProcess = sdrJsonDetail[2].ToString();
                        strJPBatch = sdrJsonDetail[3].ToString();
                        strJStore = sdrJsonDetail[4].ToString();
                        strJPName = sdrJsonDetail[5].ToString();
                        strPType = sdrJsonDetail[6].ToString();
                    }

                    if (strPType== "计划内领料") {
                        oneNum += 1;
                    }
                    else if(strPType == "计划外领料")
                    {
                        twoNum += 1;
                    }
                    else if (strPType == "成本中心领料")
                    {
                        thNum += 1;
                    }
                    //strSql = "select [REPORTNO] from [DataFromSap].[dbo].[View_SAP_LimsReport] where [Matnr]='" + sdrJsonDetail[1].ToString() + "'";
                    SqlCommand cmdlims = new SqlCommand(strSql, conNew);
                    SqlDataReader sdrlims = cmdlims.ExecuteReader();
                    if (sdrlims.HasRows)
                    {
                        while (sdrlims.Read())
                        {
                            strLimsNo = sdrlims[0].ToString();
                        }
                           
                    }
                    else
                    {
                        strLimsNo = "0000000000";
                    }
                    sdrlims.Close();

                   

                    //依据物料代码获取周期价格

                    SqlCommand cmdprice = new SqlCommand(strSqlPrice, conNew);
                    SqlDataReader sdrprice = cmdprice.ExecuteReader();
                    if (sdrprice.HasRows)
                    {
                        while (sdrprice.Read())
                        {
                            strJPrice = sdrprice[0].ToString();
                        }

                    }
                    else
                    {
                        strJPrice = "0";
                    }
                    sdrprice.Close();

                    strJson += "{";
                    strJson += "\"company\":\"" + strCompany + "\",";
                    strJson += "\"dept\":\"" + strDept + "\",";
                    strJson += "\"ven\":\"" + strVen + "\",";
                    strJson += "\"linelibCode\":\""+ strJCode + "\",";
                    strJson += "\"linelibName\":\"" + StringToJson(strJName.Replace("\\","|")) + "\",";
                    strJson += "\"linelibProcess\":\"" + strJProcess + "\",";
                    strJson += "\"linelibBatch\":\"" + strJPBatch + "\",";
                    strJson += "\"linelibCheckNumber\":\"" + strLimsNo + "\",";
                    strJson += "\"linelibStore\":\"" + Math.Round(Convert.ToDecimal(strJStore),3) + "\",";
                    strJson += "\"linelibLocation\":\"" + jObject["kh"] + "\",";
                    strJson += "\"linelibAvgPrice\":\"" + strJPrice + "\",";
                    strJson += "\"userName\":\"" + strJPName + "\"},";

                }
                sdrJsonDetail.Close();
                //IM接口传数据拼接json数据

                //计划内领料函数调用
                if (oneNum > 0) { 
                InsidePlanSap(oneNum,strCheckId, strCostCode,strSapVenCode, strPersonCode);
                }
                //计划外领料函数调用
                if (twoNum > 0) { 
                OutsidePlanSap(twoNum,strCheckId,strCostCode, strSapVenCode, strPersonCode);
                }
                //成本中心领料函数调用
                if (thNum > 0) { 
                CostCenterSap(thNum,strCheckId,strCostCode, strSapVenCode,strPersonCode);
                }
            }

            strJson = strJson.Substring(0, strJson.Length - 1);
            strJson = strJson + "]";

            //调取192.168.0.234上IM接口
            KmrAppMatSendToLineLib.KmrSendMatService lineObject = new KmrAppMatSendToLineLib.KmrSendMatService();
            string re = lineObject.SendToLineLib(strJson);
            //调取192.168.0.234上IM接口

            conNew.Close();
            con.Close();

            if (re == "True")
            {
                stat = true;
            }
            else
            {
                stat = false;
            }

            if (stat)
            {
                Response.Write("数据已提交！");
            }
            else
            {
                Response.Write("数据未保存，请联系系统管理员并反馈相关问题！");
            }

        }

        public void InsidePlanSap(int strNum, string strCheckId, string strCostCode, string strVenCode,string strPersonCode)
        {
            ZSPP_MAREQ_HEAD sap_head = new ZSPP_MAREQ_HEAD();
            ZSPP_MAREQ_ITEM[] sap_item = new ZSPP_MAREQ_ITEM[1];
            ZSPP_MAREQ_MSG[] sap_msg = new ZSPP_MAREQ_MSG[1];
            KmrAppMatPick.BAPIRET2[] sap_return = new KmrAppMatPick.BAPIRET2[1];

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();
            string strSql = "select OddNo,SapVenture,Vornor,MCode,MStockCode,StockInventory ";
            strSql += " FROM [DDdatabase].[dbo].[View_KocelApp_Kmr_CheckListDetail]";
            strSql += " where  CheckID='" + strCheckId + "'";
            SqlCommand cmdJsonDetail = new SqlCommand(strSql, con);
            SqlDataReader sdrJsonDetail = cmdJsonDetail.ExecuteReader();

            if (sdrJsonDetail.HasRows)
            {
                while (sdrJsonDetail.Read())
                {
                    //跨公司计划内领料接口调用
                   
                    sap_head.AUFNR = sdrJsonDetail[0].ToString(); 
                    sap_head.GSBER = sdrJsonDetail[1].ToString();
                    sap_head.SGTXT = sdrJsonDetail[2].ToString();

                    ZSPP_MAREQ_ITEM sap_lineItem = new ZSPP_MAREQ_ITEM();
                    sap_lineItem.MATNR = sdrJsonDetail[3].ToString(); 
                    //sap_lineItem.WERKS = "1100";
                    //sap_lineItem.CHARG = "000000213";
                    sap_lineItem.LGORT = sdrJsonDetail[4].ToString();
                    sap_lineItem.MENGE = Convert.ToDecimal(sdrJsonDetail[5].ToString());
                    sap_lineItem.MENGESpecified = true;
                    //sap_lineItem.AUFNR = "3100001031";
                    //sap_lineItem.GSBER = "0001";

                    sap_item[0] = sap_lineItem;
                    //跨公司计划内领料接口调用
                }
            }

            con.Close();

            SI_ZPP_MATERIAL_REQService sap_serv = new SI_ZPP_MATERIAL_REQService();
            sap_serv.Credentials = new NetworkCredential("prd_pisuper", "handhand0");

            string e_msgty = "";

            //string userName = "helb";
            //string password = "init1234";

            string rt_msg = sap_serv.SI_ZPP_MATERIAL_REQ(sap_head, ref sap_msg, ref sap_return, ref sap_item, out e_msgty);
            //接口返回详细信息处理


            if (e_msgty == "S")
            {

                GetSucRecord(strCheckId, rt_msg, "计划内领料", strPersonCode);
            }
            else
            {
                GetErrRecord(strCheckId, rt_msg, "计划内领料", strPersonCode);
            }

            //foreach (KmrAppMatPick.BAPIRET2 rt_return in sap_return)
            //{
            //    if(rt_return.TYPE=="S"){

            //        GetSucRecord(strCheckId, rt_return.MESSAGE, "计划内领料", strPersonCode);
            //    }
            //    else
            //    {
            //        GetErrRecord(strCheckId, rt_return.MESSAGE, "计划内领料", strPersonCode);
            //    }
            //}
            //接口返回详细信息处理
            //Response.Write(rt_msg);

        }

        public void OutsidePlanSap(int strNum, string strCheckId,string strCostCode,string strVenCode,string strPersonCode)
        {
            ZSPP_MAREQ_HEAD sap_head = new ZSPP_MAREQ_HEAD();
            ZSPP_MAREQ_ITEM[] sap_item = new ZSPP_MAREQ_ITEM[1];
            ZSPP_MAREQ_MSG[] sap_msg = new ZSPP_MAREQ_MSG[1];
            KmrAppMatPick.BAPIRET2[] sap_return = new KmrAppMatPick.BAPIRET2[1];

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();
            SqlConnection conNew = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            conNew.Open();

            string strSql = "select OddNo,SapVenture,Vornor,MCode,PStCode,StockInventory,PCompany ";
            strSql += " FROM [DDdatabase].[dbo].[View_KocelApp_Kmr_CheckListDetail]";
            strSql += " where  CheckID='" + strCheckId + "'";
            SqlCommand cmdJsonDetail = new SqlCommand(strSql, con);
            SqlDataReader sdrJsonDetail = cmdJsonDetail.ExecuteReader();

            if (sdrJsonDetail.HasRows)
            {
                while (sdrJsonDetail.Read())
                {
                    //跨公司计划内领料接口调用

                    string strComCode = "";
                    strSql = "select distinct(公司代码) as mx  from [DataFromSap].[dbo].[View_SAP_SapEhrComStock] where 公司简称='"+ sdrJsonDetail[6].ToString() +"'";
                    SqlCommand cmdCom = new SqlCommand(strSql, conNew);
                    SqlDataReader sdrCom = cmdCom.ExecuteReader();
                    while (sdrCom.Read())
                    {
                        strComCode = sdrCom[0].ToString();
                    }
                    sdrCom.Close();
                    
                        //sap_head.AUFNR = sdrJsonDetail[0].ToString();
                        //sap_head.GSBER = sdrJsonDetail[1].ToString();
                    sap_head.SGTXT = sdrJsonDetail[2].ToString();

                    ZSPP_MAREQ_ITEM sap_lineItem = new ZSPP_MAREQ_ITEM();
                    sap_lineItem.MATNR = sdrJsonDetail[3].ToString();
                    sap_lineItem.WERKS = strComCode;
                    //sap_lineItem.CHARG = "000000213";
                    sap_lineItem.LGORT = sdrJsonDetail[4].ToString();
                    sap_lineItem.MENGE = Convert.ToDecimal(sdrJsonDetail[5].ToString());
                    sap_lineItem.MENGESpecified = true;
                    sap_lineItem.AUFNR = sdrJsonDetail[0].ToString();
                    sap_lineItem.GSBER = sdrJsonDetail[1].ToString();

                    sap_item[0] = sap_lineItem;
                    //跨公司计划内领料接口调用
                }
            }

            con.Close();
            conNew.Close();

            SI_ZPP_MATERIAL_REQService sap_serv = new SI_ZPP_MATERIAL_REQService();
            sap_serv.Credentials = new NetworkCredential("prd_pisuper", "handhand0");

            string e_msgty = "";

            //string userName = "helb";
            //string password = "init1234";

            string rt_msg = sap_serv.SI_ZPP_MATERIAL_REQ(sap_head, ref sap_msg, ref sap_return, ref sap_item, out e_msgty);
            //接口返回详细信息处理

            if (e_msgty == "S")
            {

                GetSucRecord(strCheckId, rt_msg, "计划外领料", strPersonCode);
            }
            else
            {
                GetErrRecord(strCheckId, rt_msg, "计划外领料", strPersonCode);
            }

            //foreach (KmrAppMatPick.BAPIRET2 rt_return in sap_return)
            //{
            //    if (rt_return.TYPE == "S")
            //    {

            //        GetSucRecord(strCheckId, rt_return.MESSAGE, "计划外领料", strPersonCode);
            //    }
            //    else
            //    {
            //        GetErrRecord(strCheckId, rt_return.MESSAGE, "计划外领料", strPersonCode);
            //    }
            //}
            //接口返回详细信息处理
            //Response.Write(rt_msg);
        }

        public void CostCenterSap(int strNum,string strCheckId, string strCostCode, string strVenCode,string strPersonCode)
        {
            //SAP预留接口调用
            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();
            SqlConnection conNew = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            conNew.Open();

            int x = 0;
            ZPP_RESERVATION_CREATE res_create = new ZPP_RESERVATION_CREATE();
            ZSD_RESERVATION_HEADER[] res_head = new ZSD_RESERVATION_HEADER[1];
            ZSD_RESERVATION_LINE[] res_line = new ZSD_RESERVATION_LINE[Convert.ToInt16(strNum)];
            ZPP_RESERVATION_CREATEResponse res_return = new ZPP_RESERVATION_CREATEResponse();

            ZSD_RESERVATION_HEADER sap_headItem = new ZSD_RESERVATION_HEADER();

            sap_headItem.RSDAT = DateTime.Now.ToString("yyyy-MM-dd");
            sap_headItem.BWART = "201";
            sap_headItem.KOSTL = strCostCode.ToUpper();
            sap_headItem.GSBER = strVenCode;
            res_head[0] = sap_headItem;

            res_create.ZSD_RESERVATION_HEADER = res_head;

            string strSql = "select [MCode],[PickInventory],[MUnit],[MStockCode],[MBatch],[MFactoryCode],[PCompany],[PStCode]  From [DDdatabase].[dbo].[View_KocelApp_Kmr_CheckListDetail] where CheckID='" + strCheckId + "'";
            SqlCommand cmdJsonDetail = new SqlCommand(strSql, con);
            SqlDataReader sdrJsonDetail = cmdJsonDetail.ExecuteReader();

            if (sdrJsonDetail.HasRows)
            {
                while (sdrJsonDetail.Read())
                {
                    string werksCode = "";

                    string strWerkSql = "select top(1) 工厂代码  from [DataFromSap].[dbo].[View_SAP_SapEhrComStock] where 1=1 ";
                    strWerkSql += " and 公司简称='" + sdrJsonDetail[6].ToString() + "'";
                    strWerkSql += " and 库房代码='" + sdrJsonDetail[7].ToString() + "'";
                    SqlCommand cmdwerk = new SqlCommand(strWerkSql, con);
                    SqlDataReader sdrwerk = cmdwerk.ExecuteReader();
                    while (sdrwerk.Read())
                    {
                        werksCode = sdrwerk[0].ToString();
                    }
                    sdrwerk.Close();
                    //sap预留接口调用
                    ZSD_RESERVATION_LINE res_itemtmp = new ZSD_RESERVATION_LINE();
                    res_itemtmp.MATNR = sdrJsonDetail[0].ToString();
                    res_itemtmp.ERFMG = Convert.ToDecimal(sdrJsonDetail[1].ToString());
                    res_itemtmp.ERFME = sdrJsonDetail[2].ToString();
                    res_itemtmp.LGORT = sdrJsonDetail[7].ToString();
                    res_itemtmp.CHARG = sdrJsonDetail[4].ToString();
                    res_itemtmp.WERKS = werksCode;

                    res_line[x] = res_itemtmp;
                    x += 1;


                    //sap预留接口调用
                }
            }
            con.Close();
            conNew.Close();

            res_create.ZSD_RESERVATION_LINE = res_line;
            SI_ZPP_RESERVATION_CREATE_SenderService sap_server = new SI_ZPP_RESERVATION_CREATE_SenderService();
            sap_server.Credentials = new NetworkCredential("prd_pisuper", "handhand0");
            res_return = sap_server.SI_ZPP_RESERVATION_CREATE_Sender(res_create);

            string strResRsNo = res_return.MESSAGE;

            //sap 生成预留

            if (IsNumeric(strResRsNo))
            {
                //依据预留获取预留信息,201领料接口调用
                PostRsNo(strCheckId, strResRsNo, strNum, strPersonCode);
                //依据预留获取预留信息,201领料接口调用
            }
            else
            {
                GetErrRecord(strCheckId, strResRsNo, "成本中心领料", strPersonCode);
            }
            //SAP预留接口调用
        }

        public void PostRsNo(string strPid, string rsNo, int thNum, string perCode)
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
            //创建201领料信息
            int x = 0;
            ZPP_GOODSMVT_201 sap_goodsmvt = new ZPP_GOODSMVT_201();
            ZSD_GOODSMVTITEM_201[] sap_item = new ZSD_GOODSMVTITEM_201[Convert.ToInt16(thNum)]; 
            ZSD_GOODSMVTHEADER_201 sap_headItem = new ZSD_GOODSMVTHEADER_201();
       
            sap_goodsmvt.IS_HEAD = sap_headItem;

            //创建201领料信息
            foreach (ZSDATA_RESB ob in re_data)
            {
                ZSD_GOODSMVTITEM_201 sap_itemtmp = new ZSD_GOODSMVTITEM_201();
                sap_itemtmp.LGORT = ob.LGORT;
                sap_itemtmp.BATCH = "";
                sap_itemtmp.RSNUM = rsNo;
                sap_itemtmp.RSPOS = ob.RSPOS;
                sap_itemtmp.MENGE = ob.MENGE;
                sap_itemtmp.ENTRY_UOM = ob.GMEIN;
                sap_itemtmp.PLANT = ob.WERKS;
                sap_itemtmp.MATNR = ob.MATNR;
                sap_itemtmp.COSTCENTER = PCostCode.ToUpper();

                sap_item[x] = sap_itemtmp;
                x += 1;
            }
            //sap 接口传递并接受返回值
            sap_goodsmvt.IT_ITEM = sap_item;
            SI_ZPP_GOODSMVT_201Service sap_server = new SI_ZPP_GOODSMVT_201Service();
            sap_server.Credentials = new NetworkCredential("prd_pisuper", "handhand0");

            ZPP_GOODSMVT_201Response res_return = sap_server.SI_ZPP_GOODSMVT_201(sap_goodsmvt);

            string ifPost = res_return.E_MSGTY;
            string ifMessage = res_return.E_MSGTX;
            //string ifNumber = res_return.MBLNR;
            //string ifYear = res_return.MJAHR;

            if (ifPost == "S")
            {
                //接口调用成功
                string strTmp = ifMessage ;
                GetSucRecord(strPid, strTmp, "成本中心领料", perCode);
                //接口调用成功
            }
            else
            {
                GetErrRecord(strPid, ifMessage, "成本中心领料", perCode);
            }
            //sap 接口传递并接受返回值

        }

        static bool IsNumeric(string s)
        {
            // 用正则表达式判断是否为数字
            return Regex.IsMatch(s, @"^[+-]?\d*[.]?\d*$");
        }


        public void GetErrRecord(string recordId, string errMessage, string errType, string perCode)
        {
            //记录错误信息,错误数据表，ID，错误信息，错误类型
            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();

            string strSql = "insert into [DDdatabase].[dbo].[Tab_KocelApp_Kmr_PostSapRecord]([ID],[ifSap] ,[pickType],[pickID],[pickInfo],[pickItemType],[pickDate],[pickPersonId]) ";
            strSql += " VALUES('" + System.Guid.NewGuid().ToString() + "','0','跨公司领料','" + recordId + "','" + errMessage + "','" + errType + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + perCode + "')";
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
            strSql += " VALUES('" + System.Guid.NewGuid().ToString() + "','1','跨公司领料','" + recordId + "','" + sucMessage + "','" + sucType + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + perCode + "')";
            SqlCommand cmdsuc = new SqlCommand(strSql, con);
            cmdsuc.ExecuteNonQuery();

            con.Close();

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



        public string httpPost(string Url, string jsonParas)
        {
            string strURL = Url;

            //创建一个HTTP请求  
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURL);
            //Post请求方式  
            request.Method = "POST";
            //内容类型
            request.ContentType = "application/json";

            //设置参数，并进行URL编码 

            string paraUrlCoded = jsonParas;//System.Web.HttpUtility.UrlEncode(jsonParas);   

            byte[] payload;
            //将Json字符串转化为字节  
            payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
            //设置请求的ContentLength   
            request.ContentLength = payload.Length;
            //发送请求，获得请求流 

            Stream writer;
            try
            {
                writer = request.GetRequestStream();//获取用于写入请求数据的Stream对象
            }
            catch (Exception)
            {
                writer = null;
                Console.Write("连接服务器失败!");
            }
            //将请求参数写入流
            writer.Write(payload, 0, payload.Length);
            writer.Close();//关闭请求流

            String strValue = "";//strValue为http响应所返回的字符流
            HttpWebResponse response;
            try
            {
                //获得响应流
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;
            }

            Stream s = response.GetResponseStream();


            Stream postData = Request.InputStream;
            StreamReader sRead = new StreamReader(s);
            string postContent = sRead.ReadToEnd();
            sRead.Close();


            return postContent;//返回Json数据
        }

    }
}