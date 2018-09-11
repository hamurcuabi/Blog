using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataLayer
{
    public class DALMailQueue
    {
        private static readonly string TableName = "MAILQUEUE";
        public static void Insert(DALMailQueue item)
        {
            Functions.InsertEntity(TableName, item);
        }

        public static void Update(DALMailQueue item)
        {
            Functions.UpdateEntity(TableName, item);
        }

        public static void SendMail(string body, string subject, string toAddress, string ccAddress)
        {
            MailMessage mail = new MailMessage();
            SmtpClient client = new SmtpClient();
            string smtpServer = DALMailParameter.GetByCode("SMTPServer").VALUE;
            int smptpPort = Convert.ToInt32(DALMailParameter.GetByCode("SMTPPort").VALUE);
            string smtpUser = DALMailParameter.GetByCode("SMTPUser").VALUE;
            string smtpUserAlias = DALMailParameter.GetByCode("SMTPUserAlias").VALUE;
            string smtpPassword = DALMailParameter.GetByCode("SMTPPassword").VALUE;
            bool smtpSSL = DALMailParameter.GetByCode("SMTPSSL").VALUE == "0";
            int smtpTimeOut = Convert.ToInt32(DALMailParameter.GetByCode("SMTPTimeout").VALUE);

            if (smtpPassword != string.Empty)
            {
                mail = new MailMessage();
                mail.IsBodyHtml = true;
                mail.From = new MailAddress(smtpUser, smtpUserAlias);
                mail.Body = body;
                mail.Subject = subject;
                mail.HeadersEncoding = Encoding.GetEncoding("ISO-8859-9");
                mail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-9");
                mail.BodyEncoding = Encoding.GetEncoding("ISO-8859-9");
                foreach (string to in toAddress.Split(';'))
                {
                    if (!string.IsNullOrEmpty(to))
                    {
                        mail.To.Add(new MailAddress(to));
                    }
                }

                foreach (string cc in ccAddress.Split(';'))
                {
                    if (!string.IsNullOrEmpty(cc))
                    {
                        mail.CC.Add(new MailAddress(cc));
                    }
                }


                try
                {
                    client = new SmtpClient(smtpServer, smptpPort);
                    client.Credentials = new NetworkCredential(smtpUser, smtpPassword);
                    client.EnableSsl = smtpSSL;
                    client.Timeout = smtpTimeOut;
                    client.Send(mail);
                    MailQueueEntty(toAddress, ccAddress, string.Empty, subject, body, true);
                }
                catch (Exception ex)
                {
                    MailQueueEntty(toAddress, ccAddress, string.Empty, subject, body, false);
                    throw ex;
                }
            }
        }
        public static void SendContactMail(string email, string name, string subject, string message)
        {

            string body = DALMail.GetByCode("General").BODY;
            string content = string.Empty;
            content = "<div><h1 style='text-align: center;'> <b> Contact US </b>  </h1></div> <br />";
            content += "<div class='well'><div class='row-fluid'></b></div><div class='row-fluid'> <b> Adı Soyadı:</b> " + name + "  <br /> <b> E-Mail:</b> " + email + "  <br /> <b> Konu:</b> " + subject + "  <br /> <b> Mesaj:</b> " + message + " </div></div>";
            body = body.Replace("#CONTENT#", content);
            SendMail(body, "Soner Kadıköylü - Bize Yazın", "bilgi@sonerkadikoylu.com", string.Empty);
        }


        public static void SendPasswordMail(string email, string name, string password)
        {

            string[] mails = email.Split(';');
            string body = DALMail.GetByCode("General").BODY;
            string content = string.Empty;
            body = body.Replace("#CONTENTNAME#", name);
            content = "<div><h1 style='text-align: center;'> <b> Soner Kadıköylü Admin Panel Şifre Bilgileri </b>  </h1></div> <br />";
            content += "<div class='well'><div class='row-fluid'></b></div><div class='row-fluid'> <b> Kullanıcı Adınız:</b> " + mails[0].ToString() + "  <br /> <b> Şifreniz:</b> " + password + " </div></div>";
            body = body.Replace("#CONTENT#", content);
            SendMail(body, "Soner Kadıköylü Şifre Bilgileri", email, string.Empty);
        }

        public static void SendNewPasswordMail(string email, string name, string password)
        {
            string body = DALMail.GetByCode("General").BODY;
            string content = string.Empty;
            body = body.Replace("#CONTENTNAME#", name);
            content = "<div><h1 style='text-align: center;'> <b> Soner Kadıköylü Admin Panel Şifre Yenileme </b>  </h1></div> <br />";
            content += GetP("Şifrenizi değiştirmek için aşağıdaki linke tıklayınız veya kopyalayıp tarayıcınıza yapıştırınız");
            content += GetP(" <div class='well'><div class='row-fluid'></b></div><div class='row-fluid'> <b> Şifre Kurtarma Linki:</b>  " + GetLink("http://www.sonerkadikoylu.com/AdminPasswordReset?Code=" + password + "") + "  </ div ></ div >");
            body = body.Replace("#CONTENT#", content);
            SendMail(body, "Soner Kadıköylü - Admin Panel Şifre Kurtarma", email, string.Empty);
        }


        public static string GetP(string value)
        {
            return "<p style='font-size: 13px; margin-left:10px;margin-top:0px;margin-right:10px;margin-bottom:0px;font-family: Arial, sans-serif; font-weight: normal; line-height: 24px; color: #444444; padding-bottom: 10px; padding-top: 10px;'>" + value + "</p>";
        }
        public static string GetLink(string value)
        {
            return "<a href='" + value + "' style='color: #478fca; text-decoration: none; '>" + value + "</a>";
        }
        public static void MailQueueEntty(string to, string cc, string bcc, string subject, string body, bool sent)
        {
            MAILQUEUE mailq = new MAILQUEUE();
            mailq.ID = Guid.NewGuid();
            mailq.MAILTO = to;
            mailq.CC = cc;
            mailq.BCC = bcc;
            mailq.SUBJECT = subject;
            mailq.BODY = body;
            mailq.SENDDATE = DateTime.Now;
            mailq.ISSENT = sent;
            mailq.ERRORCOUNT = 6;
            mailq.SENTDATE = DateTime.Now;
            Functions.InsertEntity(TableName, mailq);
        }
    }
}
