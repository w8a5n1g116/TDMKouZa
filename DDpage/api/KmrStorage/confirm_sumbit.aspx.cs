using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDpage.KmrAppComPick;
using DDpage.KmrAppMatCreateRsNo;
using DDpage.Get_KmrStorage_PlanList;
using System.Net;

namespace DDpage.api.KmrStorage
{
    public partial class confirm_sumbit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCompany = Request.Form["PCompany"].ToString();
            string strDept = Request.Form["PDept"].ToString();
            string strVen = Request.Form["PVenture"].ToString();
            string strName = Request.Form["PName"].ToString();
            string strPersonCode = Request.Form["PCode"].ToString();
            string strSapVenCode = Request.Form["sapCode"].ToString();

            string dataString = Request.Form["dataList"].ToString();
            string strGID = System.Guid.NewGuid().ToString();

            int t1 = 0;
            string strJson = "[";
            Boolean stat = false;
            string strPlinkId = "";



            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();
            SqlConnection conNew = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            conNew.Open();

            JArray jsonObj = JArray.Parse(dataString);
            foreach (JObject jObject in jsonObj)
            {
                //更新领料确认数据表
                string strPdid = jObject["pdid"].ToString();
                string strSql = " update [DDdatabase].[dbo].[Tab_KocelApp_Kmr_ComConfDetail] set CETime='" + DateTime.Now.ToString() +"',CStat=1 where 1=1";
                strSql += " and PDID='" + strPdid + "'";

                SqlCommand cmd01 = new SqlCommand(strSql, con);
                t1 = cmd01.ExecuteNonQuery();
                if (t1 != 0)
                {
                    stat = true;
                }

                //修改[DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComPickDetail]表[PDStat]=2

                strSql = "update [DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComPickDetail] set [PDStat]=4 where PDID='"+ strPdid + "'";
                SqlCommand cmd02 = new SqlCommand(strSql, con);
                t1 = cmd02.ExecuteNonQuery();
                if (t1 != 0)
                {
                    stat = true;
                }

                strSql = "select [PLinkID]  from [DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComPickDetail] where PDID='" + strPdid + "'";
                SqlCommand cmdPlinkID = new SqlCommand(strSql, con);
                SqlDataReader sdrPlinkID = cmdPlinkID.ExecuteReader();
                while (sdrPlinkID.Read())
                {
                    strPlinkId = sdrPlinkID[0].ToString();
                }
                sdrPlinkID.Close();




                strSql = "select * from [DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComPickDetail] where PLinkID = '" + strPlinkId + "'  and [PDStat] < 4";

                SqlCommand cmdPlinkDetail = new SqlCommand(strSql, con);
                SqlDataReader sdrPlinkDetail= cmdPlinkDetail.ExecuteReader();
                if (!sdrPlinkDetail.HasRows)
                {
                    strSql = "update [DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComPickMaterial] set PickStat=2 where PLinkID='" + strPlinkId + "' ";
                    SqlCommand cmdPickHead = new SqlCommand(strSql, conNew);
                    t1 = cmdPickHead.ExecuteNonQuery();
                    if (t1 != 0)
                    {
                        stat = true;
                    }

                }
                sdrPlinkDetail.Close();

                //更新领料确认数据表

                //依据pdid查询获取基础数据，依据领料类型调用sap接口
                strSql = "select PType,oddNo,CostCenter,SapVenture,MFactoryCode,MStockCode,MCode,ConfInventory,MUnit,MRsNo,MRsPos,Vornor,MBatch  from [DDdatabase].[dbo].[View_KocelApp_Kmr_ComConfDetail] where pdid='" + strPdid + "'";
                SqlCommand cmdJsonDetail = new SqlCommand(strSql, con);
                SqlDataReader sdrJsonDetail = cmdJsonDetail.ExecuteReader();
                while (sdrJsonDetail.Read())
                {
                    string strType = sdrJsonDetail[0].ToString();
                    string strOddNo = sdrJsonDetail[1].ToString();
                    string strCoseCenter = sdrJsonDetail[2].ToString();
                    string strSapVenture = sdrJsonDetail[3].ToString();
                    string strMFactoryCode = sdrJsonDetail[4].ToString();
                    string strMStockCode = sdrJsonDetail[5].ToString();
                    string strMCode = sdrJsonDetail[6].ToString();
                    string strMInventory = sdrJsonDetail[7].ToString();
                    string strMUnit = sdrJsonDetail[8].ToString();
                    string strMRsNo = sdrJsonDetail[9].ToString();
                    string strMRsPos = sdrJsonDetail[10].ToString();
                    string strVornor = sdrJsonDetail[11].ToString();
                    string strMBatch = sdrJsonDetail[12].ToString(); 

                    if (strType== "计划内领料")
                    {
                        //sap接口定义
                        int x = 0;
                        ZPP_GOODSMVT sap_goodsmvt = new ZPP_GOODSMVT();
                        ZSD_GOODSMVTHEADER[] sap_head = new ZSD_GOODSMVTHEADER[1];
                        ZSD_GOODSMVTITEM[] sap_item = new ZSD_GOODSMVTITEM[1];
                        ZSD_GOODSMVTRETURN[] sap_return = new ZSD_GOODSMVTRETURN[1];

                        ZSD_GOODSMVTHEADER sap_headItemOne = new ZSD_GOODSMVTHEADER();
                        sap_headItemOne.BUDAT = DateTime.Now.ToString("yyyy-MM-dd");

                        sap_head[0] = sap_headItemOne;

                        sap_goodsmvt.ZSD_GOODSMVTHEADER = sap_head;

                        //sap接口定义
                        //sap接口参数赋值
                        ZSD_GOODSMVTITEM sap_itemtmp = new ZSD_GOODSMVTITEM();
                        sap_itemtmp.STGE_LOC = strMStockCode;
                        sap_itemtmp.BATCH = "";
                        sap_itemtmp.KZBEW = "X";
                        sap_itemtmp.RESERVATION_NUMBER = strMRsNo;
                        sap_itemtmp.ERFMG = Convert.ToDecimal(strMInventory);
                        sap_itemtmp.PLANT = strMFactoryCode;
                        sap_itemtmp.MATERIAL = strMCode;
                        sap_itemtmp.ENTRY_UOM = strMUnit;
                        sap_itemtmp.MOVE_TYPE = "261";
                        sap_itemtmp.RES_ITEM = strMRsPos;
                        sap_itemtmp.ORDERID = strOddNo;
                        sap_itemtmp.PARGB = strSapVenture;

                        sap_item[0] = sap_itemtmp;
                        //sap接口参数赋值
                        //sap 接口传递并接受返回值
                        sap_goodsmvt.ZSD_GOODSMVTITEM = sap_item;
                        SI_ZPP_GOODSMVTService sap_server = new SI_ZPP_GOODSMVTService();
                        sap_server.Credentials = new NetworkCredential("prd_pisuper", "handhand0");

                        ZPP_GOODSMVTResponse sap_returnOne = sap_server.SI_ZPP_GOODSMVT(sap_goodsmvt);

                        string ifPost = sap_returnOne.MESSG;
                        string ifMessage = sap_returnOne.INFO;
                        string ifNumber = sap_returnOne.MBLNR;
                        string ifYear = sap_returnOne.MJAHR;

                        if (ifPost == "S")
                        {
                            //接口调用成功
                            string strTmp = ifMessage + "||凭证号：" + ifNumber + "||凭证年度:" + ifYear;
                            GetSucRecord(strPdid, strTmp, "计划内领料", strPersonCode);
                            stat = true;
                            //接口调用成功
                        }
                        else
                        {
                            GetErrRecord(strPdid, ifMessage, "计划内领料", strPersonCode);
                            stat = false;
                        }

                        //sap 接口传递并接受返回值


                    }
                    else if (strType == "计划外领料")
                    {
                        //sap计划外接口定义
                        ZPP_GOODSMVT sap_goodsmvt = new ZPP_GOODSMVT();
                        ZSD_GOODSMVTHEADER[] sap_head = new ZSD_GOODSMVTHEADER[1];
                        ZSD_GOODSMVTITEM[] sap_item = new ZSD_GOODSMVTITEM[1];
                        ZSD_GOODSMVTRETURN[] sap_return = new ZSD_GOODSMVTRETURN[1];

                        ZSD_GOODSMVTHEADER sap_headItemTwo = new ZSD_GOODSMVTHEADER();
                        sap_headItemTwo.BUDAT = DateTime.Now.ToString("yyyy-MM-dd");

                        sap_head[0] = sap_headItemTwo;

                        sap_goodsmvt.ZSD_GOODSMVTHEADER = sap_head;

                        //sap计划外接口定义
                        //sap接口参数赋值

                        ZSD_GOODSMVTITEM sap_itemtmp = new ZSD_GOODSMVTITEM();
                        sap_itemtmp.STGE_LOC = strMStockCode;
                        sap_itemtmp.BATCH = "";
                        sap_itemtmp.KZBEW = "X";
                        //sap_itemtmp.RESERVATION_NUMBER = jObject["MRsNo"].ToString();
                        sap_itemtmp.ERFMG = Convert.ToDecimal(strMInventory);
                        sap_itemtmp.PLANT = strMFactoryCode;
                        sap_itemtmp.MATERIAL = strMCode;
                        sap_itemtmp.ENTRY_UOM = strMUnit;
                        sap_itemtmp.MOVE_TYPE = "261";
                        //sap_itemtmp.RES_ITEM = jObject["MRsPos"].ToString();
                        sap_itemtmp.ORDERID = strOddNo;
                        sap_itemtmp.PARGB = strSapVenture;
                        sap_itemtmp.VORNR = strVornor;
                        sap_item[0] = sap_itemtmp;

                        //sap接口参数赋值
                        //sap 接口传递并接受返回值
                        sap_goodsmvt.ZSD_GOODSMVTITEM = sap_item;
                        SI_ZPP_GOODSMVTService sap_server = new SI_ZPP_GOODSMVTService();
                        sap_server.Credentials = new NetworkCredential("prd_pisuper", "handhand0");

                        ZPP_GOODSMVTResponse sap_returnTwo = sap_server.SI_ZPP_GOODSMVT(sap_goodsmvt);

                        string ifPost = sap_returnTwo.MESSG;
                        string ifMessage = sap_returnTwo.INFO;
                        string ifNumber = sap_returnTwo.MBLNR;
                        string ifYear = sap_returnTwo.MJAHR;

                        if (ifPost == "S")
                        {
                            //接口调用成功
                            string strTmp = ifMessage + "||凭证号：" + ifNumber + "||凭证年度:" + ifYear;
                            GetSucRecord(strPdid, strTmp, "计划外领料", strPersonCode);
                            stat = true;
                            //接口调用成功
                        }
                        else
                        {
                            GetErrRecord(strPdid, ifMessage, "计划外领料", strPersonCode);
                            stat = false;
                        }



                        //sap 接口传递并接受返回值


                    }
                    else if (strType == "成本中心领料")
                    {

                        //创建预留获取预留编号

                        ZPP_RESERVATION_CREATE res_create = new ZPP_RESERVATION_CREATE();
                        ZSD_RESERVATION_HEADER[] res_head = new ZSD_RESERVATION_HEADER[1];
                        ZSD_RESERVATION_LINE[] res_line = new ZSD_RESERVATION_LINE[1];
                        ZPP_RESERVATION_CREATEResponse res_return = new ZPP_RESERVATION_CREATEResponse();

                        ZSD_RESERVATION_HEADER sap_headItem = new ZSD_RESERVATION_HEADER();

                        sap_headItem.RSDAT = DateTime.Now.ToString("yyyy-MM-dd");
                        sap_headItem.BWART = "201";
                        sap_headItem.KOSTL = strCoseCenter.ToUpper();
                        sap_headItem.GSBER = strSapVenture;
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
                            stat = PostRsNo(strPdid, strResRsNo, "1", strPersonCode);
                            //依据预留获取预留信息,201领料接口调用
                        }
                        else
                        {
                            GetErrRecord(strPdid, strResRsNo, "成本中心领料", strPersonCode);
                            stat = false;
                        }


                    }
                }
            }

            con.Close();
            conNew.Close();


            if (stat)
            {
                Response.Write("数据已保存！");
            }
            else
            {
                Response.Write("领料数据已保存,SAP数据接口调用不成功！");
            }

        }

        public Boolean PostRsNo(string strPid, string rsNo, string thNum, string perCode)
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
            ZPP_GOODSMVT sap_goodsmvt = new ZPP_GOODSMVT();
            ZSD_GOODSMVTHEADER[] sap_head = new ZSD_GOODSMVTHEADER[1];
            ZSD_GOODSMVTITEM[] sap_item = new ZSD_GOODSMVTITEM[Convert.ToInt16(thNum)];
            ZSD_GOODSMVTRETURN[] sap_return = new ZSD_GOODSMVTRETURN[1];

            ZSD_GOODSMVTHEADER sap_headItem = new ZSD_GOODSMVTHEADER();
            sap_headItem.BUDAT = DateTime.Now.ToString("yyyy-MM-dd");

            sap_head[0] = sap_headItem;

            sap_goodsmvt.ZSD_GOODSMVTHEADER = sap_head;

            //创建201领料信息
            foreach (ZSDATA_RESB ob in re_data)
            {
                ZSD_GOODSMVTITEM sap_itemtmp = new ZSD_GOODSMVTITEM();
                sap_itemtmp.STGE_LOC = ob.LGORT;
                sap_itemtmp.BATCH = "";
                sap_itemtmp.KZBEW = "X";
                sap_itemtmp.RESERVATION_NUMBER = rsNo;
                sap_itemtmp.ERFMG = ob.MENGE;
                sap_itemtmp.PLANT = ob.WERKS;
                sap_itemtmp.MATERIAL = ob.MATNR;
                sap_itemtmp.ENTRY_UOM = ob.GMEIN.ToUpper();
                sap_itemtmp.MOVE_TYPE = "201";
                sap_itemtmp.RES_ITEM = ob.RSPOS;

                sap_item[x] = sap_itemtmp;
                x += 1;
            }
            //sap 接口传递并接受返回值
            sap_goodsmvt.ZSD_GOODSMVTITEM = sap_item;
            SI_ZPP_GOODSMVTService sap_server = new SI_ZPP_GOODSMVTService();
            sap_server.Credentials = new NetworkCredential("prd_pisuper", "handhand0");

            ZPP_GOODSMVTResponse res_return = sap_server.SI_ZPP_GOODSMVT(sap_goodsmvt);

            string ifPost = res_return.MESSG;
            string ifMessage = res_return.INFO;
            string ifNumber = res_return.MBLNR;
            string ifYear = res_return.MJAHR;

            if (ifPost == "S")
            {
                //接口调用成功
                string strTmp = ifMessage + "||凭证号：" + ifNumber + "||凭证年度:" + ifYear;
                GetSucRecord(strPid, strTmp, "成本中心领料", perCode);
                return true;
                //接口调用成功
            }
            else
            {
                GetErrRecord(strPid, ifMessage, "成本中心领料", perCode);
                return false;
            }
            //sap 接口传递并接受返回值

        }


        public void GetErrRecord(string recordId, string errMessage, string errType, string perCode)
        {
            //记录错误信息,错误数据表，ID，错误信息，错误类型
            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();

            string strSql = "insert into [DDdatabase].[dbo].[Tab_KocelApp_Kmr_PostSapRecord]([ID],[ifSap] ,[pickType],[pickID],[pickInfo],[pickItemType],[pickDate],[pickPersonId]) ";
            strSql += " VALUES('" + System.Guid.NewGuid().ToString() + "','0','公司内领料','" + recordId + "','" + errMessage + "','" + errType + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + perCode + "')";
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
            strSql += " VALUES('" + System.Guid.NewGuid().ToString() + "','1','公司内领料','" + recordId + "','" + sucMessage + "','" + sucType + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + perCode + "')";
            SqlCommand cmdsuc = new SqlCommand(strSql, con);
            cmdsuc.ExecuteNonQuery();

            con.Close();

        }

        static bool IsNumeric(string s)
        {
            // 用正则表达式判断是否为数字
            return Regex.IsMatch(s, @"^[+-]?\d*[.]?\d*$");
        }

    }
}