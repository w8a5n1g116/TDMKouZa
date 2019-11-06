using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using TDM;

namespace DDpage.KmrStorage
{
    public partial class pickInCom_CostCenterlist : System.Web.UI.Page
    {
        public String strPhone = string.Empty;
        public String user_info = string.Empty;

        public string appId = string.Empty;
        public string appkey = string.Empty;
        public string appsecret = string.Empty;
        public string corpId = string.Empty;
        public string timestamp = string.Empty;
        public string nonceStr = string.Empty;
        public string signature = string.Empty;
        public string title = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            strPhone = Request.QueryString["phone"];
            yan y = new yan();
            user_info = y.user_code(strPhone);
            this.GetConfig();

            strPhone = user_info.Split(new char[] { ',' })[8];
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
            string url = Request.Url.ToString();
            //这里重新实现
            string accessToken = EnterpriseBusiness.GetToken2(appkey, appsecret);
            string jsApiTicket = EnterpriseBusiness.GetTickets(accessToken);
            string string1 = "jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}";
            string1 = string.Format(string1, jsApiTicket, nonceStr, timestamp, url);
            signature = FormsAuthentication.HashPasswordForStoringInConfigFile(string1, "SHA1").ToLower();

        }
    }
}