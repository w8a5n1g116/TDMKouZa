using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.KmrStorage
{
    public partial class deList_sumbit : System.Web.UI.Page
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
            string strGID = System.Guid.NewGuid().ToString();
            int t1 = 0;
            Boolean stat = false;

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            con.Open();
            SqlConnection conNew = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=DataFromSap;User ID=tide;Password=lan@2mail");
            conNew.Open();

            JArray jsonObj = JArray.Parse(dataString);
            foreach (JObject jObject in jsonObj)
            {
                string strSql = "update [DDdatabase].[dbo].[Tab_KocelApp_Kmr_DeDetail] set DETime='" + strTime + "',DePhoto='"+ jObject["photo"] + "',DeStat='" + jObject["sstat"] + "' where 1=1";
                strSql += " and DEID='" + jObject["deid"] + "'";
                SqlCommand cmdStock = new SqlCommand(strSql, con);
                t1 = cmdStock.ExecuteNonQuery();
                if (t1 != 0)
                {
                    stat = true;
                }


                strSql = "update [DDdatabase].[dbo].[Tab_kocelApp_Kmr_PickDetail] set PDStat = 3 where PDID='" + jObject["pdid"] + "'";
                SqlCommand cmdPick = new SqlCommand(strSql, con);
                t1 = cmdPick.ExecuteNonQuery();
                if (t1 != 0)
                {
                    stat = true;
                }

                //验收信息表数据写入

                strSql = "select  [PName],[PCode] from [DDdatabase].[dbo].[Tab_kocelApp_Kmr_PickMaterial] where [PLinkID]='" + jObject["linkid"] + "'";

                SqlCommand cmdde = new SqlCommand(strSql, con);
                SqlDataReader sdrde = cmdde.ExecuteReader();
                while (sdrde.Read())
                {
                    var strPickMan = sdrde[0].ToString();
                    var strPickManCode = sdrde[1].ToString();

                    strSql = "insert into [DDdatabase].[dbo].[Tab_KocelApp_Kmr_CheckDetail]([CheckID],[DEID] ,[CSTime] ,[CName],[CCode],[CStat]) ";
                    strSql += " Values('" + System.Guid.NewGuid().ToString() + "','" + jObject["deid"] + "','" + strTime + "','" + strPickMan + "','" + strPickManCode + "','0')";

                    SqlCommand cmddeList = new SqlCommand(strSql, conNew);
                    t1 = cmddeList.ExecuteNonQuery();
                    if (t1 != 0)
                    {
                        stat = true;
                    }

                    strSql = "select 联系电话 from [kocelbasedata].[dbo].[tdm_public_personinfo] where 身份证号='" + strPickManCode + "'";
                    SqlCommand cmdPMan = new SqlCommand(strSql, conNew);
                    SqlDataReader sdrPMan = cmdPMan.ExecuteReader();
                    if (!sdrPMan.HasRows)
                    {
                        string strDPhone = sdrPMan[0].ToString();
                        string strDName = "掌上仓储";
                        string strDSubject = "工厂验收通知";
                        string strDContent = "您有新的工厂验收信息需要处理!";
                        string strDUrl = "http://122.112.213.22/KmrStorage/KmrStorage/de_tablist.aspx?phone=" + strDPhone + "";
                        string strReturn = MessgeToDing_Statistics(strDPhone, strDName, strDSubject, strDContent, strDUrl);
                    }
                    sdrPMan.Close();
                }
                sdrde.Close();

                //验收信息表数据写入


                strSql = "select * from [DDdatabase].[dbo].[Tab_kocelApp_Kmr_PickDetail] where PDStat <= 2  and PLinkID='" + jObject["linkid"] + "'";

                SqlCommand cmdPickDetail = new SqlCommand(strSql, con);
                SqlDataReader sdrPickDetail = cmdPickDetail.ExecuteReader();
                if (!sdrPickDetail.HasRows)
                {
                    strSql = "update [DDdatabase].[dbo].[Tab_kocelApp_Kmr_PickMaterial] set PickStat=3 where PLinkID='" + jObject["linkid"] + "' ";
                    SqlCommand cmdPickHead = new SqlCommand(strSql, conNew);
                    t1 = cmdPickHead.ExecuteNonQuery();
                    if (t1 != 0)
                    {
                        stat = true;
                    }

                }
                sdrPickDetail.Close();
            }
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