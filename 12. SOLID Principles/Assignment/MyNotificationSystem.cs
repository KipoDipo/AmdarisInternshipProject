
namespace Assignment
{
	[Flags]
    enum NotificationTypes : uint
    {
        SMS		= 1 << 0,
        Email	= 1 << 1,
        Push	= 1 << 2,

		All		= ~0u
    }
	
	// TODO: Implement a possibly better solution
	class MyNotificationSystem : INotificationSystem<NotificationTypes>
	{
		public void SendNotification(NotificationTypes type, IUser from, INotifyableUser to, string content)
		{
			if ((type & NotificationTypes.SMS) != 0)
			{
				Console.WriteLine($"Sending SMS from {from.FirstName} to {to.FirstName}");
				to.Notify(content);
			}
			if ((type & NotificationTypes.Email) != 0)
			{
				Console.WriteLine($"Sending Email from {from.FirstName} to {to.FirstName}");
				to.Notify(content);
			}
			if ((type & NotificationTypes.Push) != 0)
			{
				Console.WriteLine($"Sending Push from {from.FirstName} to {to.FirstName}");
				to.Notify(content);
			}
		}

		public void SendNotification(IUser from, INotifyableUser to, string content)
		{
			SendNotification(NotificationTypes.All, from, to, content);
		}
	}
}
