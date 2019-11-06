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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DDpage.api.KocelWageApp
{
    public partial class KWA_AuditOneReject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strSql = "";
            string strFlowCompany = Request.Form["flowCompany"];
            string strCompany = Request.Form["company"];
            string strDept = Request.Form["dept"];
            string strName = Request.Form["name"];
            string strYear = Request.Form["year"];
            string strMonth = Request.Form["month"];
            string strId = Request.Form["id"];
            string strOid = Request.Form["oid"];
            string strPhone = Request.Form["phone"];
            string strOpinion = Request.Form["opinion"];
            string strThCompany = "";
            string strThDept = "";
            string strThName = "";
            string strThPhone = "";
            string strJson = "";
            string strMsg = "";
            Boolean stat = false;
            int t1 = 0;


            SqlConnection conOne = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=TDM;User ID=tide;Password=lan@2mail");
            conOne.Open();

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=TDM;User ID=tide;Password=lan@2mail");
            con.Open();



            //审批流程查询，没有审批流程则返回

            strSql = "  SELECT [FCompany],[FDept],[FPerson],[FPhone]   ";
            strSql += " FROM [DDdatabase].[dbo].[Tab_KWA_Flow] where 1=1 ";
            strSql += " and [flowCompany] = '" + strFlowCompany + "' ";
            strSql += " and [Role] = '1' ";

            SqlCommand cmd01 = new SqlCommand(strSql, conOne);
            SqlDataReader sdr01 = cmd01.ExecuteReader();
            if (sdr01.HasRows)
            {
                while (sdr01.Read())
                {

                    strThCompany = sdr01[0].ToString();
                    strThDept = sdr01[1].ToString();
                    strThName = sdr01[2].ToString();
                    strThPhone = sdr01[3].ToString();

                }
                string strID = System.Guid.NewGuid().ToString();
                //写入审批表
                strSql = " update [DDdatabase].[dbo].[Tab_KWA_AuditList] set TwoRemark='" + strOpinion + "',TwoTime='" + DateTime.Now.ToString() + "',stat='4' ";
                strSql += " where [PID]='" + strId + "'";


                SqlCommand cmd02 = new SqlCommand(strSql, con);
                t1 = cmd02.ExecuteNonQuery();
                if (t1 != 0)
                {
                    stat = true;
                }

                //写入审批表

                //返回json数据
                strJson = "{\"stat\":\"" + stat + "\",\"err\":\"" + strMsg + "\",\"id\":\"" + strId + "\",\"phone\":\"" + strThPhone + "\"}";

            }
            else
            {
                strMsg = "" + strCompany + "未设置审批流程，请联系管理人员进行设置！";
                strJson = "{\"stat\":\"" + stat + "\",\"err\":\"" + strMsg + "\",\"id\":\"" + strId + "\",\"phone\":\"" + strThPhone + "\"}";
            }

            sdr01.Close();
            conOne.Close();
            con.Close();

            string strTodoId = strOid + "1";
            string strTodoUrl = "http://122.112.213.22/KmrStorage/KocelWageApp/KWA_AuditDetail.aspx?id=" + strId + "&phone=" + strPhone + "";
            UpToDo(strTodoId, strTodoUrl);

            Response.Write(strJson);


        }

        public static String StringToJson(String s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\"");
                        break;
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '/':
                        sb.Append("\\/");
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString();
        }


        public static bool UpToDo(string uid, string url)
        {
            bool isok = false;

            string strdata = GetFunction(uid);
            try
            {
                JObject application = (JObject)JsonConvert.DeserializeObject(strdata);
                string id = application["ID"].ToString();
                string companyID = application["CompanyID"].ToString();
                string companyName = application["CompanyName"].ToString();
                string appID = application["AppID"].ToString();
                string appName = application["AppName"].ToString();
                string operateUrl = url;
                string operateName = application["OperateName"].ToString();
                string operateNameID = application["OperateNameID"].ToString();
                string operateTime = DateTime.Now.ToString();
                string entryTime = application["EntryTime"].ToString();
                string isConfirm = "是";
                string entryName = application["EntryName"].ToString();
                string operateStep = application["OperateStep"].ToString();
                string messageID = application["MessageID"].ToString();

                string body = "{\"ID\":\"" + id + "\",";
                body += "\"CompanyID\":\"" + companyID + "\",";
                body += "\"CompanyName\":\"" + companyName + "\",";
                body += "\"AppID\":" + appID + ",";
                body += "\"AppName\":\"" + appName + "\",";
                body += "\"OperateUrl\":\"" + operateUrl + "\",";
                body += "\"OperateName\":\"" + operateName + "\",";
                body += "\"OperateNameID\":\"" + operateNameID + "\",";
                body += "\"OperateTime\":\"" + operateTime + "\",";
                body += "\"EntryTime\":\"" + entryTime + "\",";
                body += "\"IsConfirm\":\"" + isConfirm + "\",";
                body += "\"EntryName\":\"" + entryName + "\",";
                body += "\"OperateStep\":\"" + operateStep + "\",";
                body += "\"MessageID\":\"" + messageID + "\"}";

                Update(id, body);
                isok = true;
            }
            catch (Exception e)
            {
                throw e;
            }


            return isok;
        }

        public static string Update(string id, string jsonParas)
        {
            string strURL = "http://122.112.213.22/WebAPI/api/DingToDos/" + id;
            //创建一个HTTP请求  
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURL);
            request.KeepAlive = false;
            request.ProtocolVersion = HttpVersion.Version10;
            //Post请求方式  
            request.Method = "PUT";
            //内容类型
            request.ContentType = "application/json;charset=UTF-8";

            //设置参数，并进行URL编码 
            string paraUrlCoded = jsonParas;//System.Web.HttpUtility.UrlEncode(jsonParas);   

            byte[] payload;
            //将Json字符串转化为字节  
            payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
            //设置请求的ContentLength   
            request.ContentLength = payload.Length;
            //发送请求，获得请求流 
            Stream writer;
            try
            {
                writer = request.GetRequestStream();//获取用于写入请求数据的Stream对象
            }
            catch (Exception)
            {
                writer = null;
                Console.Write("连接服务器失败!");
            }
            //将请求参数写入流
            writer.Write(payload, 0, payload.Length);
            writer.Close();//关闭请求流

            HttpWebResponse response;
            try
            {
                //获得响应流
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("url：'{0}',错误信息：'{1}'", strURL, ex.Message);
                response = ex.Response as HttpWebResponse;
            }

            Stream s = response.GetResponseStream();
            StreamReader sRead = new StreamReader(s);
            string postContent = sRead.ReadToEnd();
            sRead.Close();
            return postContent;//返回Json数据
        }



        public static string GetFunction(string id)
        {
            string serviceAddress = "http://122.112.213.22/WebAPI/api/DingToDos/Link?messageID=" + id + "&s=0";
            try
            {
                if (!serviceAddress.StartsWith("http://"))
                    return "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
                request.Method = "GET";
                request.ContentType = "application/json;charset=UTF-8";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                return retString;
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("GetAccessToken获取认证失败，错误信息：'{0}'", ex.Message);
                // lh.WriteLog(sb.ToString());
                return ex.Message;
            }

        }

    }
}

