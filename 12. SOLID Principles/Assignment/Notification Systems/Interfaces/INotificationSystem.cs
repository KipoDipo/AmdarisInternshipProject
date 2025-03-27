using Assignment.NotificationType;

namespace Assignment
{
    interface INotificationSystem
    {
		NotificationTypes Type { get; }

		void SendNotification(IUser from, INotifyableUser to, string content);
	}
}
