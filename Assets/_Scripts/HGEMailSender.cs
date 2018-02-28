using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using UnityEngine;

public class HGEMailSender : MonoBehaviour {
	//
	private static string host = "smtp-mail.outlook.com";
	private static int port = 587;
	private static string userName = "hgeek_hgfly@outlook.com";// 发送端账号   
	private static string password = "HGeek!@#2018";// 发送端密码(这个客户端重置后的密码)
	//
	public static void SendMail(string data) {
		System.DateTime now = System.DateTime.Now;
		string strfrom = userName;
		string strto = "hgfly@hgeek.club";

		string subject = string.Format("HGFly bug反馈 {0} {1}",now.ToShortDateString(),now.ToLongTimeString());//邮件的主题             
		string body = data;//发送的邮件正文  

		MailMessage msg = new MailMessage();
		msg.From = new MailAddress(strfrom);
		//msg.Sender = new MailAddress(strfrom, "HGFly开发组");
		msg.To.Add(new MailAddress(strto));

		msg.Subject = subject;//邮件标题   
		msg.Body = body;//邮件内容   
		msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码   
		msg.IsBodyHtml = true;//是否是HTML邮件   
		msg.Priority = MailPriority.High;//邮件优先级   
		System.Object userState = msg;
		//////////////////////////////////////
		SmtpClient client = new SmtpClient(host, port);
		client.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式    
		client.EnableSsl = false;
		client.UseDefaultCredentials = false;
		client.Credentials = new NetworkCredential(msg.From.Address, password) as ICredentialsByHost;//用户名、密码
		try {
			//client.Send(msg);
			//client.SendAsync(msg, userState);
			client.Send(msg);
			print("操作完成");
		} catch (SmtpException ex) {
			print(ex.Message+"发送邮件出错");
		}
	}

	/*public static void SendMail_chalk(string data) {
		//  The mailman object is used for sending and receiving email.
		MailMan mailman = new MailMan();

		//  Any string argument automatically begins the 30-day trial.
		bool success = mailman.UnlockComponent("30-day trial");
		if (success != true) {
			print(mailman.LastErrorText);
			return;
		}

		//  Set the SMTP server.
		mailman.SmtpHost = host;

		mailman.SmtpUsername = userName;
		mailman.SmtpPassword = password;

		mailman.StartTLS = true;

		//  Create a new email object
		Email email = new Email();
		System.DateTime now = System.DateTime.Now;
		email.Subject = string.Format("HGFly bug反馈 {0} {1}", now.ToShortDateString(), now.ToLongTimeString());
		email.Body = data;
		email.From = "HGFly BUG Reporter <hgeek_hgfly@outlook.com>";
		success = email.AddTo("HGFly Developer", "hgfly@hgeek.club");

		success = mailman.SendEmail(email);
		if (success != true) {
			print(mailman.LastErrorText);
			return;
		}

		success = mailman.CloseSmtpConnection();
		if (success != true) {
			print("Connection to SMTP server not closed cleanly.");
		}

		print("Mail Sent!");
	}*/
}
