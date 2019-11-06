using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TDM.pages.userinfo
{
    public partial class index : System.Web.UI.Page
    {
        public string appId = string.Empty;
        public string corpId = string.Empty;
        public string timestamp = string.Empty;
        public string nonceStr = string.Empty;
        public string signature = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.GetConfig();
        }

        private void GetConfig()
        {
            appId = Config.EAgentID;
            corpId = Config.ECorpId;
            string corpSecret = Config.ECorpSecret;
            nonceStr = Helper.randNonce();
            timestamp = Helper.timeStamp();
            string url = Request.Url.ToString();
            //这里重新实现
            string accessToken = EnterpriseBusiness.GetToken(corpId, corpSecret);
            string jsApiTicket = EnterpriseBusiness.GetTickets(accessToken);
            string string1 = "jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}";
            string1 = string.Format(string1, jsApiTicket, nonceStr, timestamp, url);
            signature = FormsAuthentication.HashPasswordForStoringInConfigFile(string1, "SHA1").ToLower();

        }
    }
}