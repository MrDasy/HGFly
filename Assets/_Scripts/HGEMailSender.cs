using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using UnityEngine;

public class HGEMailSender : MonoBehaviour {
	//
	private static string host = "smtp-mail.outlook.com";// 邮件服务器smtp.163.com表示网易邮箱服务器    
	private static string userName = "hgeek_hgfly@outlook.com";// 发送端账号   
	private static string password = "HGeek!@#2018";// 发送端密码(这个客户端重置后的密码)
	//
	public static void SendMail(string data) {
		SmtpClient client = new SmtpClient();
		client.UseDefaultCredentials = false;
		client.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式    
		client.Host = host;//邮件服务器
		client.Port = 587;
		//client.UseDefaultCredentials = true;
		client.Credentials = new NetworkCredential(userName, password) as ICredentialsByHost;//用户名、密码

		//////////////////////////////////////
		System.DateTime now = System.DateTime.Now;
		string strfrom = userName;
		string strto = "hgeek_hgfly@outlook.com";
		string strcc = "";//抄送

		string subject = string.Format("HGFly bug反馈 {0} {1}",now.ToShortDateString(),now.ToLongTimeString());//邮件的主题             
		string body = data;//发送的邮件正文  

		MailMessage msg = new MailMessage();
		msg.From = new MailAddress(strfrom, "HGFly开发组");
		msg.Sender = new MailAddress(strfrom, "HGFly开发组");
		msg.To.Add(strto);
		msg.CC.Add(strcc);

		msg.Subject = subject;//邮件标题   
		msg.Body = body;//邮件内容   
		msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码   
		msg.IsBodyHtml = true;//是否是HTML邮件   
		msg.Priority = MailPriority.High;//邮件优先级   


		try {
			client.Send(msg);
			print("发送成功");
		} catch (SmtpException ex) {
			print(ex.Message+"发送邮件出错");
		}
	}
}
