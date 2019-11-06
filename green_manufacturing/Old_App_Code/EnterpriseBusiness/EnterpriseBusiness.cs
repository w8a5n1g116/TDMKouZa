using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

/// <summary>
/// EnterpriseBusiness 的摘要说明
/// </summary>
public class EnterpriseBusiness
{
    public EnterpriseBusiness()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 拿到JSAPI 的access_token
    /// </summary>
    /// <param name="CorpId"></param>
    /// <param name="CorpSecret"></param>
    /// <returns></returns>
    public static string GetToken(string CorpId, string CorpSecret)
    {
        string tagUrl = "https://oapi.dingtalk.com/gettoken?corpid=" + CorpId + "&corpsecret=" + CorpSecret;
        string result = HttpHelper.Get(tagUrl);
        JObject jo = (JObject)JsonConvert.DeserializeObject(result);
        string tokenModel = jo["access_token"].ToString();
        try
        {
            string[] er;
            string tr = "";
            tr = tokenModel;
            er = Regex.Split(tr, "\"", RegexOptions.IgnoreCase);
            tr = er[1];
            er = Regex.Split(tr, "\"", RegexOptions.IgnoreCase);
            tr = er[0];
            tokenModel = tr;
        }
        catch (Exception ex) { }
        return tokenModel;
    }

    /// <summary>
    /// 拿到当前登录的用户
    /// </summary>
    /// <param name="access_token"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    public static string GetCurrentUser(string access_token, string code)
    {
        string tagUrl = "https://oapi.dingtalk.com/user/get?access_token=" + access_token + "&userid=" + code;
        string result = HttpHelper.Get(tagUrl);
        JObject jo = (JObject)JsonConvert.DeserializeObject(result);
        string mobile = jo["mobile"].ToString();
        string userModel = mobile;
        return userModel;
    }

    /// <summary>
    /// 拿到当前用户的公司名称
    /// </summary>
    /// <param name="access_token"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    public static string GetCurrentname(string access_token, string code)
    {
        string tagUrl = "https://oapi.dingtalk.com/department/get?access_token=" + access_token + "&id=" + code;
        string result = HttpHelper.Get(tagUrl);
        JObject jo = (JObject)JsonConvert.DeserializeObject(result);
        string department_name = jo["name"].ToString();
        string userModel = department_name;
        return userModel;
    }

    /// <summary>
    /// 拿到Tickets
    /// </summary>
    /// <param name="CorpId"></param>
    /// <param name="CorpSecret"></param>
    /// <returns></returns>
    public static string GetTickets(string access_token)
    {
        string url = "https://oapi.dingtalk.com/get_jsapi_ticket?access_token={0}&type=jsapi";
        url = string.Format(url, access_token);
        string result = HttpHelper.Get(url);
        JObject jo = (JObject)JsonConvert.DeserializeObject(result);
        string jsApiTicket = jo["ticket"].ToString();
        try
        {
            string[] er;
            string tr = "";
            tr = jsApiTicket;
            er = Regex.Split(tr, "\"", RegexOptions.IgnoreCase);
            tr = er[1];
            er = Regex.Split(tr, "\"", RegexOptions.IgnoreCase);
            tr = er[0];
            jsApiTicket = tr;
        }
        catch (Exception ex) { }
        return jsApiTicket;
    }

    /// <summary>
    /// 发送oa消息
    /// </summary>
    /// <param name="access_token"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    public static string sendMSG(string access_token, string title, string text, string name, string emplid,string form,string url)
    {
        string tagUrl = "https://oapi.dingtalk.com/message/send?access_token=" + access_token;
        string agentid = Config.EAgentID;
        string post = "{\"touser\":\"" + emplid + "\",\"agentid\":\"" + agentid + "\",\"msgtype\":\"oa\",\"oa\":{\"message_url\":\""+url+"\",\"head\":{\"bgcolor\":\"FFBBBBBB\",\"text\":\"绿色制造问题审核\"},\"body\":{\"form\":["+form+"],\"content\":\""+text+"\",\"author\": \""+name+"\"}}}";
        string result = HttpHelper.Post(tagUrl,post);
        return "ok";
    }

}