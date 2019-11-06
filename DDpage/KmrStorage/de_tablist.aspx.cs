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
    public partial class De_tablist : System.Web.UI.Page
    {
        public String strPhone = string.Empty;
        public String user_info = string.Empty;
        //钉钉授权引用
        public string appId = string.Empty;
        public string corpId = string.Empty;
        public string timestamp = string.Empty;
        public string nonceStr = string.Empty;
        public string signature = string.Empty;
        public string title = string.Empty;
        //钉钉授权引用
        protected void Page_Load(object sender, EventArgs e)
        {
            strPhone = Request.QueryString["phone"];
            yan y = new yan();
            user_info = y.user_code(strPhone);
            this.GetConfig();
        }

        private void GetConfig()
        {
            appId = Request.QueryString["appid"];
            corpId = Config.ECorpId;
            string corpSecret = Config.ECorpSecret;
            nonceStr = Helper.randNonce();
            timestamp = Helper.timeStamp();
            string url = Request.Url.ToString();
            //这里重新实现
            //string accessToken = EnterpriseBusiness.GetToken(corpId, corpSecret);
            string accessToken = EnterpriseBusiness.GetToken2(Config.AppKey, Config.AppSecret);
            string jsApiTicket = EnterpriseBusiness.GetTickets(accessToken);
            string string1 = "jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}";
            string1 = string.Format(string1, jsApiTicket, nonceStr, timestamp, url);
            signature = FormsAuthentication.HashPasswordForStoringInConfigFile(string1, "SHA1").ToLower();

        }
    }
}