using Assignment.NotificationType;
using Assignment.Users.Interfaces;

namespace Assignment
{
    interface INotificationSystem
    {
		NotificationTypes Type { get; }

		void SendNotification(IUser from, INotifyableUser to, string content);
	}
}
