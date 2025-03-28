using Assignment.NotificationType;
using Assignment.Users.Interfaces;

namespace Assignment.NotificationSystems
{
	class EmailNotificationSystem : INotificationSystem
	{
		public NotificationTypes Type { get; } = NotificationTypes.Email;

		public void SendNotification(IUser from, INotifyableUser to, string content)
		{
			Console.WriteLine($"Sending email to {to.FirstName}");
			to.Notify(content);
		}
	}
}
