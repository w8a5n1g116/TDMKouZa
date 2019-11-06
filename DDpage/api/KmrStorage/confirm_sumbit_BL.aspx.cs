using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using DDpage.KmrAppComPick;
//using DDpage.KmrAppMatCreateRsNo;
//using DDpage.Get_KmrStorage_PlanList;
using DDpage.PlanIn;
using DDpage.PlanOut;
using DDpage.CoastCenter;
using DDpage.InnerOrder;
using System.Net;

namespace DDpage.api.KmrStorage
{
    public partial class confirm_sumbit_BL : System.Web.UI.Page
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

            string errorString = "";

            SqlConnection con = new SqlConnection("Data Source=192.168.197.101;User ID=sa;Password=2018Sql101");
            con.Open();
            SqlConnection conNew = new SqlConnection("Data Source=192.168.197.101;User ID=sa;Password=2018Sql101");
            conNew.Open();

            JArray jsonObj = JArray.Parse(dataString);
            foreach (JObject jObject in jsonObj)
            {

                string strPdid = jObject["pdid"].ToString();

                string strSql2 = " select [MStockCode],[MStock] FROM [DDdatabase].[dbo].[Stock] where 1=1 and [MStock] = '" + jObject["MStock"].ToString() + "'";

                SqlCommand cmd2 = new SqlCommand(strSql2, con);
                SqlDataReader sdr2 = cmd2.ExecuteReader();

                string scode = "";
                string sname = "";
                while (sdr2.Read())
                {

                    scode = sdr2[0].ToString();
                    sname = sdr2[1].ToString();

                }

                sdr2.Close();

                strSql2 = " update [DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComPickDetail] set [MStock] = '" + sname + "',[MStockCode] = '" + scode + "',[PickInventory] = " + jObject["Pnum"].ToString() + " where 1=1 and PDID='" + strPdid + "'";

                SqlCommand cmd3 = new SqlCommand(strSql2, con);
                int rowaffected = cmd3.ExecuteNonQuery();


                //更新领料确认数据表

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
                strSql = "select PType,oddNo,CostCenter,SapVenture,MFactoryCode,MStockCode,MCode,PickInventory,MUnit,MRsNo,MRsPos,Vornor,MBatch  from [DDdatabase].[dbo].[View_KocelApp_Kmr_ComConfDetail] where pdid='" + strPdid + "'";
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
                        
                        ZPP_PLAN planin = new ZPP_PLAN();
                        PlanIn.ZPP_PLAN_HEAD head = new PlanIn.ZPP_PLAN_HEAD();
                        head.AUFNR = strOddNo;
                        head.SGTXT = strVornor;
                        PlanIn.ZPP_PLAN_MSG[] msg = new PlanIn.ZPP_PLAN_MSG[] { };
                        PlanIn.BAPIRET2[] ret2 = new PlanIn.BAPIRET2[] { };
                        PlanIn.ZPP_PLAN_ITEM[] item = new PlanIn.ZPP_PLAN_ITEM[1];
                        
                        PlanIn.ZPP_PLAN_ITEM sap_itemtmp = new PlanIn.ZPP_PLAN_ITEM();
                        sap_itemtmp.MATNR = strMCode;
                        sap_itemtmp.LGORT = strMStockCode;
                        sap_itemtmp.MENGE = Convert.ToDecimal(strMInventory);
                        sap_itemtmp.WERKS = strMFactoryCode;
                        sap_itemtmp.SERIALNO = strMRsNo;

                        item[0] = sap_itemtmp;

                        planin.ET_MSG = msg;
                        planin.ET_RETURN = ret2;
                        planin.IT_ITEM = item;
                        planin.IS_HEAD = head;


                        ZZXPLAN zzxplan = new ZZXPLAN();
                        zzxplan.Credentials = new NetworkCredential("RFC_USER", "pfseR91iC20an");

                        ZPP_PLANResponse zppplanret = zzxplan.ZPP_PLAN(planin);

                        string ifPost = zppplanret.E_MSGTY;
                        string ifMessage = "";
                        string ifNumber = zppplanret.E_MSGTX;
                        string ifYear = DateTime.Now.Year.ToString();

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
                            errorString = zppplanret.ET_RETURN.FirstOrDefault().MESSAGE;
                            GetErrRecord(strPdid, errorString, "计划内领料", strPersonCode);
                            stat = false;
                        }

                        //sap 接口传递并接受返回值


                    }
                    else if (strType == "计划外领料")
                    {
                        ZPP_NO_PLAN planout = new ZPP_NO_PLAN();
                        PlanOut.ZPP_PLAN_HEAD head = new PlanOut.ZPP_PLAN_HEAD();
                        head.AUFNR = strOddNo;
                        head.SGTXT = strVornor;
                        PlanOut.ZPP_PLAN_MSG[] msg = new PlanOut.ZPP_PLAN_MSG[] { };
                        PlanOut.BAPIRET2[] ret2 = new PlanOut.BAPIRET2[] { };
                        PlanOut.ZPP_PLAN_ITEM[] item = new PlanOut.ZPP_PLAN_ITEM[1];

                        PlanOut.ZPP_PLAN_ITEM sap_itemtmp = new PlanOut.ZPP_PLAN_ITEM();
                        sap_itemtmp.MATNR = strMCode;
                        sap_itemtmp.LGORT = strMStockCode;
                        sap_itemtmp.MENGE = Convert.ToDecimal(strMInventory);
                        sap_itemtmp.WERKS = strMFactoryCode;

                        item[0] = sap_itemtmp;

                        planout.ET_MSG = msg;
                        planout.ET_RETURN = ret2;
                        planout.IT_ITEM = item;
                        planout.IS_HEAD = head;


                        ZZXNOPLAN zzxplan = new ZZXNOPLAN();
                        zzxplan.Credentials = new NetworkCredential("RFC_USER", "pfseR91iC20an");

                        ZPP_NO_PLANResponse zppplanret = zzxplan.ZPP_NO_PLAN(planout);



                        string ifPost = zppplanret.E_MSGTY;
                        string ifMessage = "";
                        string ifNumber = zppplanret.E_MSGTX;
                        string ifYear = DateTime.Now.Year.ToString();

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

                        ZMM_GOODSMOVE_201 zmm201 = new ZMM_GOODSMOVE_201();
                        CoastCenter.ZZXGOODSMOVE[] items = new CoastCenter.ZZXGOODSMOVE[1];

                        CoastCenter.ZZXGOODSMOVE item = new CoastCenter.ZZXGOODSMOVE();
                        item.MATNR = strMCode;
                        item.ERFMG = Convert.ToDecimal(strMInventory);

                        items[0] = item;

                        zmm201.ZGOODSMOVE = items;
                        zmm201.ZKOSTL = strCoseCenter;
                        zmm201.ZLGORT = strMStockCode;
                        zmm201.ZWERKS = strMFactoryCode;

                        ZZXKOSTL201 sap_server = new ZZXKOSTL201();
                        sap_server.Credentials = new NetworkCredential("RFC_USER", "pfseR91iC20an");
                        ZMM_GOODSMOVE_201Response res_return = sap_server.ZMM_GOODSMOVE_201(zmm201);


                        string ifPost = res_return.E_MSGTY;
                        string ifMessage = "";
                        string ifNumber = res_return.E_MSGTX;
                        string ifYear = DateTime.Now.Year.ToString();

                        if (ifPost == "S")
                        {
                            //接口调用成功
                            string strTmp = ifMessage + "||凭证号：" + ifNumber + "||凭证年度:" + ifYear;
                            GetSucRecord(strPdid, strTmp, "成本中心领料", strPersonCode);
                            stat = true;
                            //接口调用成功
                        }
                        else
                        {
                            GetErrRecord(strPdid, ifMessage, "成本中心领料", strPersonCode);
                            stat = false;
                        }

                        ////sap 生成预留
                        ////判断strResRsNo是不是数字，如果是数字，预留生成，如果不是保留错误代码
                        //if (IsNumeric(strResRsNo))
                        //{
                        //    //依据预留获取预留信息,201领料接口调用
                        //    stat = PostRsNo(strPdid, strResRsNo, "1", strPersonCode);
                        //    //依据预留获取预留信息,201领料接口调用
                        //}
                        //else
                        //{
                        //    GetErrRecord(strPdid, strResRsNo, "成本中心领料", strPersonCode);
                        //    stat = false;
                        //}


                    }
                    else if (strType == "工程类领料" || strType == "研发类领料")
                    {

                        //创建预留获取预留编号

                        ZMM_MOVE_IORDER zmm201 = new ZMM_MOVE_IORDER();
                        InnerOrder.ZZXGOODSMOVE[] items = new InnerOrder.ZZXGOODSMOVE[1];

                        InnerOrder.ZZXGOODSMOVE item = new InnerOrder.ZZXGOODSMOVE();
                        item.MATNR = strMCode;
                        item.ERFMG = Convert.ToDecimal(strMInventory);

                        items[0] = item;

                        zmm201.ZGOODSMOVE = items;
                        zmm201.ZAUFNR = strOddNo;
                        zmm201.ZLGORT = strMStockCode;
                        zmm201.ZWERKS = strMFactoryCode;
                        if (strType == "工程类领料")
                            zmm201.ZBWART = "Z61";
                        else if (strType == "研发类领料")
                            zmm201.ZBWART = "Z41";

                        ZZXMOVEORDER sap_server = new ZZXMOVEORDER();
                        sap_server.Credentials = new NetworkCredential("RFC_USER", "pfseR91iC20an");
                        ZMM_MOVE_IORDERResponse res_return = sap_server.ZMM_MOVE_IORDER(zmm201);


                        string ifPost = res_return.E_MSGTY;
                        string ifMessage = "";
                        string ifNumber = res_return.E_MSGTX;
                        string ifYear = DateTime.Now.Year.ToString();

                        if (ifPost == "S")
                        {
                            //接口调用成功
                            string strTmp = ifMessage + "||凭证号：" + ifNumber + "||凭证年度:" + ifYear;
                            GetSucRecord(strPdid, strTmp, strType, strPersonCode);
                            stat = true;
                            //接口调用成功
                        }
                        else
                        {
                            GetErrRecord(strPdid, ifMessage, strType, strPersonCode);
                            stat = false;
                        }

                        ////sap 生成预留
                        ////判断strResRsNo是不是数字，如果是数字，预留生成，如果不是保留错误代码
                        //if (IsNumeric(strResRsNo))
                        //{
                        //    //依据预留获取预留信息,201领料接口调用
                        //    stat = PostRsNo(strPdid, strResRsNo, "1", strPersonCode);
                        //    //依据预留获取预留信息,201领料接口调用
                        //}
                        //else
                        //{
                        //    GetErrRecord(strPdid, strResRsNo, "成本中心领料", strPersonCode);
                        //    stat = false;
                        //}


                    }

                   
                }
                sdrJsonDetail.Close();
            }

            con.Close();
            conNew.Close();


            if (stat)
            {
                Response.Write("数据已保存！");
            }
            else
            {
                Response.Write("领料数据已保存,SAP数据接口调用不成功！"+ errorString);
            }

        }

        //public Boolean PostRsNo(string strPid, string rsNo, string thNum, string perCode)
        //{
        //    //获取预留信息
        //    ZPP_RESB sap_resb = new ZPP_RESB();
        //    ZSDATA_RESB[] re_data;
        //    sap_resb.Z_AUFNR = "";
        //    sap_resb.Z_RSNUM = rsNo;
        //    ZPP_RESBResponse sap_resbRes = new ZPP_RESBResponse();

        //    SI_ZPP_RESB_SenderService sap_serv = new SI_ZPP_RESB_SenderService();
        //    sap_serv.Credentials = new NetworkCredential("prd_pisuper", "handhand0");
        //    sap_resbRes = sap_serv.SI_ZPP_RESB_Sender(sap_resb);
        //    re_data = sap_resbRes.Z_RESBINFO;
        //    //获取预留信息
        //    //创建201领料信息
        //    int x = 0;
        //    ZPP_GOODSMVT sap_goodsmvt = new ZPP_GOODSMVT();
        //    ZSD_GOODSMVTHEADER[] sap_head = new ZSD_GOODSMVTHEADER[1];
        //    ZSD_GOODSMVTITEM[] sap_item = new ZSD_GOODSMVTITEM[Convert.ToInt16(thNum)];
        //    ZSD_GOODSMVTRETURN[] sap_return = new ZSD_GOODSMVTRETURN[1];

        //    ZSD_GOODSMVTHEADER sap_headItem = new ZSD_GOODSMVTHEADER();
        //    sap_headItem.BUDAT = DateTime.Now.ToString("yyyy-MM-dd");

        //    sap_head[0] = sap_headItem;

        //    sap_goodsmvt.ZSD_GOODSMVTHEADER = sap_head;

        //    //创建201领料信息
        //    foreach (ZSDATA_RESB ob in re_data)
        //    {
        //        ZSD_GOODSMVTITEM sap_itemtmp = new ZSD_GOODSMVTITEM();
        //        sap_itemtmp.STGE_LOC = ob.LGORT;
        //        sap_itemtmp.BATCH = "";
        //        sap_itemtmp.KZBEW = "X";
        //        sap_itemtmp.RESERVATION_NUMBER = rsNo;
        //        sap_itemtmp.ERFMG = ob.MENGE;
        //        sap_itemtmp.PLANT = ob.WERKS;
        //        sap_itemtmp.MATERIAL = ob.MATNR;
        //        sap_itemtmp.ENTRY_UOM = ob.GMEIN.ToUpper();
        //        sap_itemtmp.MOVE_TYPE = "201";
        //        sap_itemtmp.RES_ITEM = ob.RSPOS;

        //        sap_item[x] = sap_itemtmp;
        //        x += 1;
        //    }
        //    //sap 接口传递并接受返回值
        //    sap_goodsmvt.ZSD_GOODSMVTITEM = sap_item;
        //    SI_ZPP_GOODSMVTService sap_server = new SI_ZPP_GOODSMVTService();
        //    sap_server.Credentials = new NetworkCredential("prd_pisuper", "handhand0");

        //    ZPP_GOODSMVTResponse res_return = sap_server.SI_ZPP_GOODSMVT(sap_goodsmvt);

        //    string ifPost = res_return.MESSG;
        //    string ifMessage = res_return.INFO;
        //    string ifNumber = res_return.MBLNR;
        //    string ifYear = res_return.MJAHR;

        //    if (ifPost == "S")
        //    {
        //        //接口调用成功
        //        string strTmp = ifMessage + "||凭证号：" + ifNumber + "||凭证年度:" + ifYear;
        //        GetSucRecord(strPid, strTmp, "成本中心领料", perCode);
        //        return true;
        //        //接口调用成功
        //    }
        //    else
        //    {
        //        GetErrRecord(strPid, ifMessage, "成本中心领料", perCode);
        //        return false;
        //    }
        //    //sap 接口传递并接受返回值

        //}


        public void GetErrRecord(string recordId, string errMessage, string errType, string perCode)
        {
            //记录错误信息,错误数据表，ID，错误信息，错误类型
            SqlConnection con = new SqlConnection("Data Source=192.168.197.101;User ID=sa;Password=2018Sql101");
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
            SqlConnection con = new SqlConnection("Data Source=192.168.197.101;User ID=sa;Password=2018Sql101");
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