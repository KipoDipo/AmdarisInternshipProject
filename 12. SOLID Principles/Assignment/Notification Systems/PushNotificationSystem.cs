using Assignment.NotificationType;

namespace Assignment.NotificationSystems
{
	class PushNotificationSystem : INotificationSystem
	{
		public NotificationTypes Type { get; } = NotificationTypes.Push;

		public void SendNotification(IUser from, INotifyableUser to, string content)
		{
			Console.WriteLine($"Sending push notification to {to.FirstName}");
			to.Notify(content);
		}
	}
}
