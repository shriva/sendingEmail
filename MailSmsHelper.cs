using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net;
//using System.Net.Mail;
using System.Web.UI.WebControls;
using CypherUtility;
using System.Net.Mail;
using System.IO;

public class MailSmsHelper
{
  
    public static bool sendAuthenticatedMail(string To, string Subject, string MailBodyInHTML,string AttacFilePath)
    {
        try
        {
           // string s=Cypher.Encrypt("abc");
                if (To != "")
                {
                    if (Utility.GetAppSetting("IsEmailActive").Equals("0"))
                    {
                        MailMessage mail = new MailMessage();
                        mail.To.Add(To);
                        mail.CC.Add(Utility.GetAppSetting("Mail_CC"));
                        mail.From = new MailAddress(Utility.GetAppSetting("SendId"));
                        mail.Subject = Subject;
                        mail.Body = MailBodyInHTML;
                        mail.IsBodyHtml = true;
                        mail.Priority = MailPriority.High;
                        SmtpClient smtp = new SmtpClient();
                        if (AttacFilePath != "")
                        {
                            string fullPath =  AttacFilePath;
                            Attachment attFile = new Attachment(HttpContext.Current.Server.MapPath(fullPath));
                            mail.Attachments.Add(attFile);
                        }
                        smtp.Host = Utility.GetAppSetting("SmtpServer");
                        smtp.Port = Convert.ToInt32(Utility.GetAppSetting("port"));
                        smtp.Credentials = new System.Net.NetworkCredential(Utility.GetAppSetting("SendId"), Cypher.Decrypt(Utility.GetAppSetting("SendPassword")));
                        smtp.EnableSsl = Convert.ToBoolean(Utility.GetAppSetting("ssl"));
                        smtp.Send(mail);
                        mail.Attachments.Dispose();                                          

                        return true;
                    }
                    else return false;
                }
                else return false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static string SendSMS(string numbers, string msg)
    {
        try
        {
            string s = "SMS service disebal!";
            if (Utility.GetAppSetting("IsSmsActive") == "1")
            {
                s = "";
                WebClient Client = new WebClient();
                string baseurl = "";
                //string baseurl = "http://bulksms.smsintro4u.com/sendhttp.php?user=" + GetAppSetting("SmsUserName") + "&password=" + GetAppSetting("ApiPassword") + "&sender=" + GetAppSetting("SenderId") + "&mobiles=" + numbers + "&message=" + msg + "&route=4";

                //baseurl = "http://bulksms.smsintro4u.com/api/sendhttp.php?authkey=" + Utility.GetAppSetting("SMSApiPassword") + "&mobiles=" + numbers + "&message=" + msg + "&sender=" + Utility.GetAppSetting("SMSSenderId") + "&route=4";
                baseurl = "http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=70219960668c25b140896857922913d8&message=" + msg + "&senderId=RAMAB&routeId=1&mobileNos=" + numbers + "&smsContentType=english";
                
                Stream data = Client.OpenRead(baseurl);
                StreamReader reader = new StreamReader(data);
                s = s + reader.ReadToEnd();
                data.Close();
                reader.Close();

                if (s.Contains("alert_"))
                    s = "SMS Successfully send! " + s;
                else
                    s = "SMS not send! " + s;
            }
            return s;
        }
        catch (Exception ex)
        {
            return "Error on SMS sending. Error detail is " + ex.Message;
        }
    }

    //public static void SendAuthenticatedMail(string subject, string )
    //{
    //    MailMessage mail = new MailMessage();

    //    mail.From = new MailAddress("me@mycompany.com");
    //    mail.To.Add("you@yourcompany.com");

    //    mail.Subject = "This is an email";
    //    mail.Body = "this is the body content of the email.";

    //    SmtpClient smtp = new SmtpClient("127.0.0.1");

    //    //to authenticate we set the username and password properites on the SmtpClient
    //    smtp.Credentials = new NetworkCredential("username", "secret");
    //    smtp.Send(mail);
    //}


    public static bool sendAuthenticatedMail(string To, string Subject, System.Text.StringBuilder MailBodyInHTML, string AttacFilePath)
    {
        try
        {
             string s=Cypher.Encrypt("k");
            if (To != "")
            {
                if (Utility.GetAppSetting("IsEmailActive").Equals("0"))
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(To);
                    mail.CC.Add(Utility.GetAppSetting("AdminMail"));                    
                    mail.From = new MailAddress(Utility.GetAppSetting("SendId"));
                    mail.Subject = Subject;
                    mail.Body = MailBodyInHTML.ToString();
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;
                    SmtpClient smtp = new SmtpClient();
                    if (AttacFilePath != "")
                    {
                        string fullPath = AttacFilePath;
                        Attachment attFile = new Attachment(HttpContext.Current.Server.MapPath(fullPath));
                        mail.Attachments.Add(attFile);
                    }
                    smtp.Host = Utility.GetAppSetting("SmtpServer");
                    smtp.Port = Convert.ToInt32(Utility.GetAppSetting("port"));
                    smtp.Credentials = new System.Net.NetworkCredential(Utility.GetAppSetting("SendId"), Cypher.Decrypt(Utility.GetAppSetting("SendPassword")));
                    smtp.EnableSsl = Convert.ToBoolean(Utility.GetAppSetting("ssl"));
                    smtp.Send(mail);
                    mail.Attachments.Dispose();

                    return true;
                }
                else return false;
            }
            else return false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}