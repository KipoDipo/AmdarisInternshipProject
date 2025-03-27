namespace Assignment
{
    interface INotificationSystem<T> where T : Enum
    {
        void SendNotification(T type, IUser from, INotifyableUser to, string content);
	}
}
