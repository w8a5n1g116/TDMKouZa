using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.KmrStorage
{
    public partial class test_index : System.Web.UI.Page
    {
        public string appId = string.Empty;
        public string appkey = string.Empty;
        public string appsecret = string.Empty;
        public string corpId = string.Empty;
        public string timestamp = string.Empty;
        public string nonceStr = string.Empty;
        public string signature = string.Empty;
        public string title = string.Empty;
        public string AccessToken = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.GetConfig();
            title = "掌上仓储";
        }
        private void GetConfig()
        {
            appId = Config.EAgentID;//Request.QueryString["appid"];
            corpId = Config.ECorpId;
            appkey = Config.AppKey;
            appsecret = Config.AppSecret;
            string corpSecret = Config.ECorpSecret;
            nonceStr = Helper.randNonce();
            timestamp = Helper.timeStamp();
            string url = "http://25347aq284.qicp.vip/KmrStorage/index.aspx?agentid=276461520";//Request.Url.ToString();
            //这里重新实现
            string accessToken = EnterpriseBusiness.GetToken2(appkey, appsecret);
            AccessToken = accessToken;
            string jsApiTicket = EnterpriseBusiness.GetTickets(accessToken);
            string string1 = "jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}";
            string1 = string.Format(string1, jsApiTicket, nonceStr, timestamp, url);
            signature = FormsAuthentication.HashPasswordForStoringInConfigFile(string1, "SHA1").ToLower();

        }
    }
}