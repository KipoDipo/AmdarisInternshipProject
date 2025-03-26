using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace Assignment
{
	public class Program
	{
		static IConfiguration config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

		static string sender = config["gmailAddr"];
		static string password = config["gmailPass"];


		static void SendEmail(string receiver)
		{
			using SmtpClient client = new SmtpClient("smtp.gmail.com")
			{
				EnableSsl = true,
				Credentials = new NetworkCredential(sender, password)
			};

			using MailMessage message = new MailMessage(new MailAddress(sender), new MailAddress(receiver))
			{
				Body = "Thank you for subscribing to our newsletter!",
				Subject = "Thank you!"
			};

			client.Send(message);

			Console.WriteLine($"Message sent to {receiver}!");
		}

		static void Main(string[] args)
		{
			Console.Write("Enter your email to subscribe to our newsletter: ");
			string input = Console.ReadLine()!;

			SendEmail(input);
		}
	}
}
