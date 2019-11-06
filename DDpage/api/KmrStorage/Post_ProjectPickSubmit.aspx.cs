using DDpage.Get_KmrStorage_PlanList;
using DDpage.KmrAppComPick;
using Newtonsoft.Json;
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
    public partial class Post_ProjectPickSubmit : System.Web.UI.Page
    {
        string strKeeperInfo = "";


        protected void Page_Load(object sender, EventArgs e)
        {

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
            string dataStringFour = Request.Form["dataListFour"].ToString();
            string fourNum = Request.Form["FourNum"].ToString();
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


            string strSql = "insert into [DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComPickMaterial] VALUES('" + strPID + "','" + strCompany + "','" + strDept + "','" + strVen + "','" + strSapVenCode + "','" + strName + "','" + strPersonCode + "','" + strTime + "','" + strOrder + "','','" + strProOrder + "','" + strCostCode + "','APP','" + strStat + "','" + strGID + "')";
            SqlCommand cmdhead = new SqlCommand(strSql, con);
            t1 = cmdhead.ExecuteNonQuery();
            if (t1 != 0)
            {
                stat = true;
            }


            if (dataStringFour.Length > 0)
            {

                JArray jsonObjFour = JArray.Parse(dataStringFour);
                foreach (JObject jObject in jsonObjFour)
                {
                    string strPdid = System.Guid.NewGuid().ToString();
                    if (jObject["PType"].ToString() == "工程类领料")
                    {
                        strSql = "insert into [DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComPickDetail] VALUES('" + strPdid + "','" + strGID + "','" + jObject["MFactory"] + "','" + jObject["MFactoryCode"] + "','" + jObject["MStock"] + "','" + jObject["MStockCode"] + "','" + jObject["MGroup"] + "','" + jObject["MGroupCode"] + "','','','','" + jObject["Material"] + "','" + jObject["MCode"] + "','" + jObject["MBatch"] + "','" + jObject["MUnit"] + "','" + jObject["MInventory"] + "','" + jObject["PickInventory"] + "','" + jObject["PType"] + "','1')";

                        SqlCommand cmd = new SqlCommand(strSql, con);
                        t1 = cmd.ExecuteNonQuery();
                        if (t1 != 0)
                        {
                            stat = true;
                        }

                        //检索是否存在库管员
                        strSql = "select top(1) [FKeeper] ,[FKeeperCode],[FKeeperPhone] from  [DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComFacBaseData] where 1=1";
                        strSql += " and FCompany ='" + strCompany + "'";
                        strSql += " and FFacCode = '" + jObject["MFactoryCode"] + "'";
                        strSql += " and FStockCode = '" + jObject["MStockCode"] + "'";
                        //strSql += " and FGroupCode = '" + jObject["MGroupCode"] + "'";

                        SqlCommand cmdTh = new SqlCommand(strSql, con);
                        SqlDataReader sdrTh = cmdTh.ExecuteReader();

                        if (sdrTh.HasRows)
                        {
                            string strKeeper = "";
                            string strKeeperCode = "";
                            string strKeeperPhone = "";
                            while (sdrTh.Read())
                            {
                                strKeeper = sdrTh[0].ToString();
                                strKeeperCode = sdrTh[1].ToString();
                                strKeeperPhone = sdrTh[2].ToString();
                            }
                            string strConfId = System.Guid.NewGuid().ToString();

                            strSql = "insert into [DDdatabase].[dbo].[Tab_KocelApp_Kmr_ComConfDetail]( [ConfirmID] ,[PDID],[ConfInventory] ,[CName],[CCode] ,[CSTime],[CStat])";
                            strSql += " values('" + strConfId + "','" + strPdid + "','" + jObject["PickInventory"] + "','" + strKeeper + "','" + strKeeperCode + "','" + DateTime.Now.ToString() + "','0')";

                            SqlCommand cmdConf = new SqlCommand(strSql, conNew);
                            t1 = cmdConf.ExecuteNonQuery();
                            if (t1 != 0)
                            {
                                stat = true;
                            }

                            //库管员数据
                            strKeeperInfo += "{\"ddPhone\":\"" + strKeeperPhone + "\"},";

                            //钉消息发送
                            string strDPhone = strKeeperPhone;
                            //string strDPhone = "15109509909";
                            string strDName = "掌上仓储";
                            string strDSubject = "领料确认通知";
                            string strDContent = "您有新的领料信息需要确认!";
                            string strDUrl = "http://122.112.213.22/KmrStorage/KmrStorage/confirm_tablist.aspx?phone=" + strDPhone + "";
                            //string strUrl = "http://www.baidu.com";
                            string strReturn = MessgeToDing_Statistics(strDPhone, strDName, strDSubject, strDContent, strDUrl);


                        }
                        else
                        {
                            strSql = "update [DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComPickDetail] set [PDStat]=9 where 1=1";
                            strSql += " and [PDID]='" + strPdid + "'";

                            SqlCommand cmderr = new SqlCommand(strSql, conNew);
                            t1 = cmderr.ExecuteNonQuery();
                            if (t1 != 0)
                            {
                                stat = true;
                            }

                        }
                        sdrTh.Close();


                    }
                }



            }



            //判断该领料单下是否全部异常，如是则改变领料单状态1--》2

            strSql = "select * from [DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComPickDetail] where 1=1 ";
            strSql += " and PLinkID='" + strGID + "'";
            strSql += " and [PDStat]=1 ";

            SqlCommand cmdCheck = new SqlCommand(strSql, con);
            SqlDataReader sdrCheck = cmdCheck.ExecuteReader();
            if (!sdrCheck.HasRows)
            {
                strSql = " update [DDdatabase].[dbo].[Tab_kocelApp_Kmr_ComPickMaterial] set [PickStat] = 2  where 1=1";
                strSql += " and PLinkID='" + strGID + "'";
                SqlCommand cmdCheckErr = new SqlCommand(strSql, conNew);
                t1 = cmdCheckErr.ExecuteNonQuery();
                if (t1 != 0)
                {
                    stat = true;
                }

            }

            sdrCheck.Close();



            con.Close();
            conNew.Close();
            //库管员数据
            strKeeperInfo = strKeeperInfo.Substring(0, strKeeperInfo.Length - 1);
            string strJson = "";

            if (stat)
            {
                strJson = "{\"info\":\"数据已保存！\",\"ddInfo\":[" + strKeeperInfo + "]}";
                Response.Write(strJson);
            }
            else
            {
                strJson = "{\"info\":\"领料数据异常！\",\"ddInfo\":[" + strKeeperInfo + "]}";
                Response.Write(strJson);
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


        public static String MessgeToDing_Statistics(string phone, string strName, string strTempSubject, string strTempContent, string strUrl)
        {
            #region 消息通知
            string re = "none";
            //发送请求的数据
            string POSTURL = "http://122.112.213.22/AppMessage/api.aspx";
            Dictionary<string, string> myDictionary = new Dictionary<string, string>();
            myDictionary.Add("appid", "150465644");
            myDictionary.Add("users", phone);
            myDictionary.Add("title", strTempSubject);
            myDictionary.Add("text", strTempContent + DateTime.Now);
            myDictionary.Add("url", strUrl);
            myDictionary.Add("name", strName);
            if (Post(POSTURL, myDictionary))
            {
                re = "true";
            }
            return re;
            #endregion
        }

        /// <summary>  
        /// 指定Post地址使用Get 方式获取全部字符串  
        /// </summary>  
        /// <param name="url">请求后台地址</param>  
        /// <param name="content">Post提交数据内容(utf-8编码的)</param>  
        /// <returns></returns>  
        public static bool Post(string POSTURL, Dictionary<string, string> dic)
        {
            #region 指定Post地址使用Get 方式获取全部字符串
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(POSTURL);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            #region 添加Post 参数
            StringBuilder builder = new StringBuilder();
            int i = 0;
            foreach (var item in dic)
            {
                if (i > 0)
                    builder.Append("&");
                builder.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
            }
            byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容  
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }

            string json = JsonConvert.SerializeObject(result);
            // if("{"errcode":0,"errmsg":"ok","messageId":"decd701487b933a9801352bae77491dc","invalidparty":"","invaliduser":""}"==result)
            //{

            //}

            bool send_result = false;
            if (result.Contains("ok") || result.Contains("success"))
            {
                send_result = true;
            }

            return send_result;
            #endregion
        }


    }
}