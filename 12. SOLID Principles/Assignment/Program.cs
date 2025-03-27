namespace Assignment
{
	public class Program
	{
		static void Main(string[] args)
		{
			User Adam = new() { FirstName = "Adam", LastName = "Adamov" };
			User Eve = new() { FirstName = "Eva", LastName = "Evova" };

			MyNotificationSystem system = new MyNotificationSystem();

			system.SendNotification(
				NotificationTypes.Email | NotificationTypes.SMS,
				Adam,
				Eve,
				"Hello!!!"
			);
		}
	}
}
