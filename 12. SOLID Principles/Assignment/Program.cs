using Assignment.NotificationSystems;
using Assignment.NotificationType;
using Assignment.Users;

namespace Assignment
{
	public class Program
	{
		static void Main(string[] args)
		{
			User Adam = new() { FirstName = "Adam", LastName = "Adamov" };
			User Eve = new() { FirstName = "Eva", LastName = "Evova" };

			MessageService service = new MessageService([new EmailNotificationSystem(), new PushNotificationSystem(), new SmsNotificationSystem()]);

			service.SendNotifications(NotificationTypes.Push | NotificationTypes.SMS, Adam, Eve, "Hello!");
		}
	}
}
