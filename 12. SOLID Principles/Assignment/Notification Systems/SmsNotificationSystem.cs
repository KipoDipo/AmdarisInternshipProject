using Assignment.NotificationType;
using Assignment.Users.Interfaces;

namespace Assignment.NotificationSystems
{
	class SmsNotificationSystem : INotificationSystem
	{
		public NotificationTypes Type { get; } = NotificationTypes.SMS;

		public void SendNotification(IUser from, INotifyableUser to, string content)
		{
			Console.WriteLine($"Sending SMS to {to.FirstName}");
			to.Notify(content);
		}
	}
}
