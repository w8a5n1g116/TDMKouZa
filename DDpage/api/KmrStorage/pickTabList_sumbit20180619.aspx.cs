using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDpage.KmrAppComPick;
using DDpage.KmrAppMatCreateRsNo;
using DDpage.Get_KmrStorage_PlanList;
using System.Net;
using System.Text.RegularExpressions;

namespace DDpage.api.KmrStorage
{
    public partial class pickTabList_sumbit20180619 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string data = Request.Form["data"];

            //JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            ////string s = jsonSerializer.Serialize(data);
            //Object s = jsonSerializer.Deserialize<object>(data);

            string strCompany = Request.Form["PCompany"].ToString();
            string strDept = Request.Form["PDept"].ToString();
            string strVen = Request.Form["PVenture"].ToString();
            string strSapVenCode = Request.Form["SapVenId"].ToString();
            string strName = Request.Form["PName"].ToString();
            string strPersonCode = Request.Form["PCode"].ToString();
            string strCostCode = Request.Form["PCostCode"].ToString();
            string strTime = DateTime.Now.ToString();
            string strOrder = Request.Form["OddNo"].ToString();
            string strProOrder = Request.Form["ProNo"].ToString();
            string strStat = "1";
            string dataStringOne = Request.Form["dataListOne"].ToString();
            string dataStringTwo = Request.Form["dataListTwo"].ToString();
            string dataStringTh = Request.Form["dataListTh"].ToString();
            string oneNum = Request.Form["OneNum"].ToString();
            string twoNum = Request.Form["TwoNum"].ToString();
            string thNum = Request.Form["ThNum"].ToString();
            string strGID = System.Guid.NewGuid().ToString();

            Random r = new Random();
            int i = r.Next(1000, 9999);
            Boolean stat = false;
            int t1 = 0;
            string strPID = System.DateTime.Now.ToFileTime().ToString() + i.ToString();


            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();

            SqlConnection conNew = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            conNew.Open();


            string strSql = "insert into [DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComPickMaterial] VALUES('" + strPID + "','" + strCompany + "','" + strDept + "','" + strVen + "','" + strSapVenCode + "','" + strName + "','" + strPersonCode + "','" + strTime + "','" + strOrder + "','','" + strProOrder + "','APP','" + strStat + "','" + strGID + "')";
            SqlCommand cmdhead = new SqlCommand(strSql, con);
            t1 = cmdhead.ExecuteNonQuery();
            if (t1 != 0)
            {
                stat = true;
            }


            if (dataStringOne.Length > 0) {

                //sap接口定义
                int x = 0;
                ZPP_GOODSMVT sap_goodsmvt = new ZPP_GOODSMVT();
                ZSD_GOODSMVTHEADER[] sap_head = new ZSD_GOODSMVTHEADER[1];
                ZSD_GOODSMVTITEM[] sap_item = new ZSD_GOODSMVTITEM[Convert.ToInt16(oneNum)];
                ZSD_GOODSMVTRETURN[] sap_return = new ZSD_GOODSMVTRETURN[1];

                ZSD_GOODSMVTHEADER sap_headItemOne = new ZSD_GOODSMVTHEADER();
                sap_headItemOne.BUDAT = DateTime.Now.ToString("yyyy-MM-dd");

                sap_head[0] = sap_headItemOne;

                sap_goodsmvt.ZSD_GOODSMVTHEADER = sap_head;

                //sap接口定义

                JArray jsonObjOne = JArray.Parse(dataStringOne);
                foreach (JObject jObject in jsonObjOne)
                {
                    string strPdid = System.Guid.NewGuid().ToString();
                    if (jObject["PType"].ToString() == "计划内领料")
                    {
                        //计划内领料数据补全
                        string MFactoryOne = "";
                        string strSqlOne1 = "SELECT [NAME1] FROM [DataFromSap].[dbo].[T_SAP_DM_Factory] where WERKS='" + jObject["MFactoryCode"] + "'";

                        SqlCommand cmdone1 = new SqlCommand(strSqlOne1, con);
                        SqlDataReader sdrOne1 = cmdone1.ExecuteReader();
                        while (sdrOne1.Read())
                        {
                            MFactoryOne = sdrOne1[0].ToString();
                        }
                        sdrOne1.Close();


                        string MStockOne = "";
                        string strSqlOne2 = "SELECT [LGOBE] FROM [DataFromSap].[dbo].[T_SAP_DM_Stock] where WERKS='" + jObject["MFactoryCode"] + "' and LGORT='" + jObject["MStockCode"] + "'";

                        SqlCommand cmdone2 = new SqlCommand(strSqlOne2, con);
                        SqlDataReader sdrOne2 = cmdone2.ExecuteReader();
                        while (sdrOne2.Read())
                        {
                            MStockOne = sdrOne2[0].ToString();
                        }
                        sdrOne2.Close();

                        string MGroupOne = "";
                        string strSqlOne3 = "SELECT [WGBEZ] FROM [DataFromSap].[dbo].[T_SAP_DM_MeasureGroup] where [MATKL]='" + jObject["MGroupCode"] + "' ";

                        SqlCommand cmdone3 = new SqlCommand(strSqlOne3, con);
                        SqlDataReader sdrOne3 = cmdone3.ExecuteReader();
                        while (sdrOne3.Read())
                        {
                            MGroupOne = sdrOne3[0].ToString();
                        }
                        sdrOne3.Close();

                        //计划内领料数据补全


                        strSql = "insert into [DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComPickDetail] VALUES('" + strPdid + "','" + strGID + "','" + MFactoryOne + "','" + jObject["MFactoryCode"] + "','" + MStockOne + "','" + jObject["MStockCode"] + "','" + MGroupOne + "','" + jObject["MGroupCode"] + "','" + jObject["MatName"] + "','" + jObject["MCode"] + "','','" + jObject["MUnit"] + "','" + jObject["MInventory"] + "','" + jObject["MInventory"] + "','" + jObject["PType"] + "','1')";


                        SqlCommand cmd = new SqlCommand(strSql, con);
                        t1 = cmd.ExecuteNonQuery();
                        if (t1 != 0)
                        {
                            stat = true;
                        }

                        //sap接口参数赋值
                        ZSD_GOODSMVTITEM sap_itemtmp = new ZSD_GOODSMVTITEM();
                        sap_itemtmp.STGE_LOC = jObject["MStockCode"].ToString();
                        sap_itemtmp.BATCH = "";
                        sap_itemtmp.KZBEW = "X";
                        sap_itemtmp.RESERVATION_NUMBER = jObject["MRsNo"].ToString();
                        sap_itemtmp.ERFMG = Convert.ToDecimal(jObject["MInventory"]);
                        sap_itemtmp.PLANT = jObject["MFactoryCode"].ToString();
                        sap_itemtmp.MATERIAL = jObject["MCode"].ToString();
                        sap_itemtmp.ENTRY_UOM = jObject["MUnit"].ToString();
                        sap_itemtmp.MOVE_TYPE = "261";
                        sap_itemtmp.RES_ITEM = jObject["MRsPos"].ToString();
                        sap_itemtmp.ORDERID = strOrder;
                        sap_itemtmp.PARGB = strSapVenCode;
                       
                        sap_item[x] = sap_itemtmp;
                        x += 1;
                        //sap接口参数赋值
                        //异常处理，及时库存中有，sap接口调用失败写入异常信息表


                        //异常处理，及时库存中有，sap接口调用失败写入异常信息表

                    }
                }


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
                    GetSucRecord(strPID, strTmp, "计划内领料", strPersonCode);
                    stat = true;
                    //接口调用成功
                }
                else
                {
                    GetErrRecord(strPID, ifMessage, "计划内领料", strPersonCode);
                    stat = false;
                }

                //sap 接口传递并接受返回值
            }

            if (dataStringTwo.Length>0) {

                //sap计划外接口定义
                int x = 0;
                ZPP_GOODSMVT sap_goodsmvt = new ZPP_GOODSMVT();
                ZSD_GOODSMVTHEADER[] sap_head = new ZSD_GOODSMVTHEADER[1];
                ZSD_GOODSMVTITEM[] sap_item = new ZSD_GOODSMVTITEM[Convert.ToInt16(twoNum)];
                ZSD_GOODSMVTRETURN[] sap_return = new ZSD_GOODSMVTRETURN[1];

                ZSD_GOODSMVTHEADER sap_headItemTwo = new ZSD_GOODSMVTHEADER();
                sap_headItemTwo.BUDAT = DateTime.Now.ToString("yyyy-MM-dd");

                sap_head[0] = sap_headItemTwo;

                sap_goodsmvt.ZSD_GOODSMVTHEADER = sap_head;

                //sap计划外接口定义

                JArray jsonObjTwo = JArray.Parse(dataStringTwo);
                foreach (JObject jObject in jsonObjTwo)
                {
                    string strPdid = System.Guid.NewGuid().ToString();
                    if (jObject["PType"].ToString() == "计划外领料")
                    {
                        strSql = "insert into [DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComPickDetail] VALUES('" + strPdid + "','" + strGID + "','" + jObject["MFactory"] + "','" + jObject["MFactoryCode"] + "','" + jObject["MStock"] + "','" + jObject["MStockCode"] + "','" + jObject["MGroup"] + "','" + jObject["MGroupCode"] + "','" + jObject["Material"] + "','" + jObject["MCode"] + "','" + jObject["MBatch"] + "','" + jObject["MUnit"] + "','" + jObject["MInventory"] + "','" + jObject["PickInventory"] + "','" + jObject["PType"] + "','1')";

                        SqlCommand cmd = new SqlCommand(strSql, con);
                        t1 = cmd.ExecuteNonQuery();
                        if (t1 != 0)
                        {
                            stat = true;
                        }

                        //sap接口参数赋值

                        ZSD_GOODSMVTITEM sap_itemtmp = new ZSD_GOODSMVTITEM();
                        sap_itemtmp.STGE_LOC = jObject["MStockCode"].ToString();
                        sap_itemtmp.BATCH = "";
                        sap_itemtmp.KZBEW = "X";
                        //sap_itemtmp.RESERVATION_NUMBER = jObject["MRsNo"].ToString();
                        sap_itemtmp.ERFMG = Convert.ToDecimal(jObject["MInventory"]);
                        sap_itemtmp.PLANT = jObject["MFactoryCode"].ToString();
                        sap_itemtmp.MATERIAL = jObject["MCode"].ToString();
                        sap_itemtmp.ENTRY_UOM = jObject["MUnit"].ToString();
                        sap_itemtmp.MOVE_TYPE = "261";
                        //sap_itemtmp.RES_ITEM = jObject["MRsPos"].ToString();
                        sap_itemtmp.ORDERID = strOrder;
                        sap_itemtmp.PARGB = strSapVenCode;
                        sap_itemtmp.VORNR = strProOrder;
                        sap_item[x] = sap_itemtmp;
                        x += 1;
                        //sap接口参数赋值


                        //异常处理，及时库存中有，sap接口调用失败写入异常信息表


                        //异常处理，及时库存中有，sap接口调用失败写入异常信息表
                    }
                }

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
                    GetSucRecord(strPID, strTmp, "计划外领料", strPersonCode);
                    stat = true;
                    //接口调用成功
                }
                else
                {
                    GetErrRecord(strPID, ifMessage, "计划外领料", strPersonCode);
                    stat = false;
                }



                //sap 接口传递并接受返回值
            }

            if (dataStringTh.Length > 0) {
                //创建预留获取预留编号
                int x = 0;
                ZPP_RESERVATION_CREATE res_create = new ZPP_RESERVATION_CREATE();
                ZSD_RESERVATION_HEADER[] res_head = new ZSD_RESERVATION_HEADER[1];
                ZSD_RESERVATION_LINE[] res_line = new ZSD_RESERVATION_LINE[Convert.ToInt16(thNum)];
                ZPP_RESERVATION_CREATEResponse res_return = new ZPP_RESERVATION_CREATEResponse();

                ZSD_RESERVATION_HEADER sap_headItem = new ZSD_RESERVATION_HEADER();

                sap_headItem.RSDAT = DateTime.Now.ToString("yyyy-MM-dd");
                sap_headItem.BWART = "201";
                sap_headItem.KOSTL = strCostCode.ToUpper();
                sap_headItem.GSBER = strSapVenCode;
                res_head[0] = sap_headItem;

                res_create.ZSD_RESERVATION_HEADER = res_head;

                //sap201领料
                JArray jsonObjTh = JArray.Parse(dataStringTh);
                foreach (JObject jObject in jsonObjTh)
                {
                    string strPdid = System.Guid.NewGuid().ToString();
                    if (jObject["PType"].ToString() == "成本中心领料")
                    {
                        strSql = "insert into [DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComPickDetail] VALUES('" + strPdid + "','" + strGID + "','" + jObject["MFactory"] + "','" + jObject["MFactoryCode"] + "','" + jObject["MStock"] + "','" + jObject["MStockCode"] + "','" + jObject["MGroup"] + "','" + jObject["MGroupCode"] + "','" + jObject["Material"] + "','" + jObject["MCode"] + "','" + jObject["MBatch"] + "','" + jObject["MUnit"] + "','" + jObject["MInventory"] + "','" + jObject["PickInventory"] + "','" + jObject["PType"] + "','1')";

                        SqlCommand cmd = new SqlCommand(strSql, con);
                        t1 = cmd.ExecuteNonQuery();
                        if (t1 != 0)
                        {
                            stat = true;
                        }

                        //sap接口调用
                        ZSD_RESERVATION_LINE res_itemtmp = new ZSD_RESERVATION_LINE();
                        res_itemtmp.MATNR = jObject["MCode"].ToString();
                        res_itemtmp.ERFMG = Convert.ToDecimal(jObject["PickInventory"]);
                        res_itemtmp.ERFME = jObject["MUnit"].ToString();
                        res_itemtmp.LGORT = jObject["MStockCode"].ToString();
                        res_itemtmp.CHARG = jObject["MBatch"].ToString();
                        res_itemtmp.WERKS = jObject["MFactoryCode"].ToString();

                        res_line[x] = res_itemtmp;
                        x += 1;


                        //sap接口调用

                        //异常处理，及时库存中有，sap接口调用失败写入异常信息表


                        //异常处理，及时库存中有，sap接口调用失败写入异常信息表
                    }
                }

                //sap 生成预留

                res_create.ZSD_RESERVATION_LINE = res_line;
                SI_ZPP_RESERVATION_CREATE_SenderService  sap_server = new SI_ZPP_RESERVATION_CREATE_SenderService();              
                sap_server.Credentials = new NetworkCredential("prd_pisuper", "handhand0");
                res_return = sap_server.SI_ZPP_RESERVATION_CREATE_Sender(res_create);
                
                string strResRsNo = res_return.MESSAGE;

                //sap 生成预留
                //判断strResRsNo是不是数字，如果是数字，预留生成，如果不是保留错误代码
                if (IsNumeric(strResRsNo))
                {
                    //依据预留获取预留信息,201领料接口调用
                    stat = PostRsNo(strPID,strResRsNo,thNum,strPersonCode);
                    //依据预留获取预留信息,201领料接口调用
                }
                else
                {
                    GetErrRecord(strPID,strResRsNo,"成本中心领料", strPersonCode);
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

        
        

        public Boolean PostRsNo(string strPid,string rsNo,string thNum,string perCode)
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

            if(ifPost == "S")
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

        public void GetSucRecord(string recordId, string sucMessage, string sucType,string perCode)
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