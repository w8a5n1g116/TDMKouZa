using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.KmrStorage
{
    public partial class ddInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string strPhone = "15109509909";
            string strName = "掌上仓储";
            string strSubject = "领料确认通知";
            string strContent = "您有新的领料信息需要处理!";
            string strUrl = "http://122.112.213.22/KmrStorage/KmrStorage/confirm_tablist.aspx?phone=" + strPhone + "";
            //string strUrl = "http://www.baidu.com";
            string strReturn = MessgeToDing_Statistics(strPhone, strName, strSubject, strContent, strUrl);
            Response.Write(strReturn);
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