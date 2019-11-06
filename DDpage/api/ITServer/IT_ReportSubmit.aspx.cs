using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDpage.api.ITServer
{
    public partial class IT_ReportSubmit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strid = Request.Form["id"].ToString();
            string strGzxx = Request.Form["gzyy"].ToString();
            string strJjcd = Request.Form["jjcd"].ToString();
            string strWhhb = Request.Form["whhb"].ToString();
            string startTime = Request.Form["stime"].ToString();
            string strPhone = "";
            int t1 = 0;
            Boolean stat = false;

            //pc端代码
            double score = 0.0f;
            DateTime dt = System.DateTime.Now;
            DateTime starTime = Convert.ToDateTime(startTime);
            string hbDate = dt.ToString();
            string strkhdate = string.Empty;

            if ("1" == strJjcd)
            {
                int year = 0;
                int month = 0;
                int day = 0;
                int hour = 0;
                int minutes = 0;
                int seconds = 0;
                if (starTime.Hour >= 8 && starTime.Hour < 13)
                {
                    //维护时间4小时+1小时午休
                    year = starTime.Year;
                    month = starTime.Month;
                    day = starTime.Day;
                    hour = starTime.Hour + 5;
                    minutes = starTime.Minute;
                    seconds = starTime.Second;
                }
                else if (starTime.Hour >= 13 && starTime.Hour < 17)
                {
                    //13:00-17:00区间，维护截止时间自动延长至第二天
                    if ((starTime.Day + 1) > DateTime.DaysInMonth(starTime.Year, starTime.Month))
                    {
                        day = 1;
                        month = starTime.Month + 1;
                    }
                    else
                    {
                        day = starTime.Day + 1;
                        month = starTime.Month;
                    }
                    if (month > 12)
                    {
                        month = 1;
                        year = starTime.Year + 1;
                    }
                    else
                    {
                        year = starTime.Year;
                    }
                    hour = starTime.Hour + 19 - 24;
                    minutes = starTime.Minute;
                    seconds = starTime.Second;

                }
                else
                {
                    //下班后的维护内容自动计算到地2天上班开始
                    if ((starTime.Day + 1) > DateTime.DaysInMonth(starTime.Year, starTime.Month))
                    {
                        day = 1;
                        month = starTime.Month + 1;
                    }
                    else
                    {
                        day = starTime.Day + 1;
                        month = starTime.Month;
                    }
                    if (month > 12)
                    {
                        month = 1;
                        year = starTime.Year + 1;
                    }
                    else
                    {
                        year = starTime.Year;
                    }
                    hour = 13;
                    //  minutes = dt.Minute;
                    //  seconds = dt.Second;
                }

                strkhdate = year.ToString() + "-" + month.ToString() + "-" + day.ToString() + " " + hour.ToString() + ":" + minutes.ToString() + ":" + seconds.ToString();
                DateTime khdate = Convert.ToDateTime(strkhdate);
                int d = (int)khdate.DayOfWeek;
                if (d == 0)
                {
                    //  strkhdate = khdate.AddDays(1).ToString("yyyy-MM-dd hh:mm:ss");
                    strkhdate = khdate.AddDays(1).ToString();
                }
                else if (d == 6)
                {
                    //  strkhdate = khdate.AddDays(2).ToString("yyyy-MM-dd hh:mm:ss");
                    strkhdate = khdate.AddDays(2).ToString();
                }
                DateTime skhdate = Convert.ToDateTime(strkhdate);
                if (dt > skhdate)
                {
                    // double ddate = (dt - khdate).Hours;
                    int ddate = (int)(dt.Subtract(skhdate).TotalHours + 0.5);
                    score = 0.5 * ddate;
                    if (score > 10)
                    {
                        score = 10;
                    }
                }

            }
            else if ("2" == strJjcd)
            {
                if (dt > starTime)
                {
                    int ddate = (int)(dt.Subtract(starTime).TotalHours + 0.5);
                    if (ddate > 240)
                    {
                        score = 0.5 * (ddate - 240);
                    }
                    if (score > 10)
                    {
                        score = 10;
                    }
                }
                // strkhdate = starTime.AddDays(10).ToString("yyyy-MM-dd hh:mm:ss");
                strkhdate = starTime.AddDays(10).ToString();
            }

            SqlConnection con = new SqlConnection("Data Source=192.168.0.186;Initial Catalog=BPMS;User ID=tide;Password=lan@2mail");
            con.Open();

            string strSql01 = "update [TDM].[dbo].[Maintenance_Plan] set whhb='" + strWhhb + "',whgzyy='" + strGzxx + "',zrrHbTime='" + dt.ToString() + "',jjcd='" + strJjcd + "',gongshi='" + score.ToString() + "',whjzTime='" + strkhdate + "',bxrKH='0'   where primID=" + strid + "";
            SqlCommand cmd01 = new SqlCommand(strSql01, con);
            t1 = cmd01.ExecuteNonQuery();
            if (t1 != 0)
            {
                stat = true;
            }

            //获取报修人电话
            string strSql02 = "select phone from [TDM].[dbo].[Maintenance_Plan] where primID=" + strid + "";
            SqlCommand cmd02 = new SqlCommand(strSql02, con);
            SqlDataReader sdr02 = cmd02.ExecuteReader();
            while (sdr02.Read())
            {

                strPhone = sdr02[0].ToString();

            }
            sdr02.Close();
            //获取报修人电话
            //维护信息查询
            string strbxrName = "";
            string strbxrMail = "";
            string strwhZrr = "";
            string strbxrGzxx = "";
            string strSql03 = "select [bxrName],[bxrMail],[gzxx],[whzrr]  from [TDM].[dbo].[Maintenance_Plan] where primID=" + strid + "";
            SqlCommand cmd03 = new SqlCommand(strSql03, con);
            SqlDataReader sdr03 = cmd03.ExecuteReader();
            while (sdr03.Read())
            {

                strbxrName = sdr03[0].ToString();
                strbxrMail = sdr03[1].ToString();
                strbxrGzxx = sdr03[2].ToString();
                strwhZrr = sdr03[3].ToString();

            }
            sdr03.Close();

            //维护信息查询
            con.Close();

            //邮件通知

            bool sendMailSuccess = false;
            sendMailSuccess = MailClass.SendMaintenancePlanToBxrEmail(strbxrName, strbxrMail, strid, strbxrGzxx, strwhZrr, startTime, dt.ToString(), strGzxx,strJjcd, strWhhb);
            //邮件通知

            //pc端代码
            string strJson = "{\"stat\":\"" + stat + "\",\"phone\":\"" + strPhone + "\",\"time\":\"" + startTime + "\",\"id\":\"" + strid + "\"}";
            

            Response.Write(strJson);
        }
    }
}