using Assignment.NotificationSystems;
using Assignment.NotificationType;

namespace Assignment
{
	public class Program
	{
		static void Main(string[] args)
		{
			User Adam = new() { FirstName = "Adam", LastName = "Adamov" };
			User Eve = new() { FirstName = "Eva", LastName = "Evova" };

			MessageService service = new MessageService([new SmsNotificationSystem(), new EmailNotificationSystem(), new PushNotificationSystem()]);

			service.SendNotifications(NotificationTypes.SMS | NotificationTypes.Email, Adam, Eve, "Hello");
		}
	}
}
