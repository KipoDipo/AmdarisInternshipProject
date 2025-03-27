namespace Assignment
{
    interface INotification
    {
        IUser Sender { get; set; }
        List<INotifyableUser> Receivers { get; set; }
        string Content { get; set; }

        void Send();
	}
}
