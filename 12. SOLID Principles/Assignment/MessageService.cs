using Assignment.NotificationType;

namespace Assignment
{
    class MessageService
    {
        private List<INotificationSystem> systems;

		public MessageService(List<INotificationSystem> systems)
		{
			this.systems = systems;
		}

		public void SendNotifications(NotificationTypes type, IUser from, INotifyableUser to, string content)
		{
			foreach (var system in systems)
			{
				if ((system.Type & type) != 0)
				{
					system.SendNotification(from, to, content);
				}
			}
		}
	}
}
