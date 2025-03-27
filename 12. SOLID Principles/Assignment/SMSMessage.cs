namespace Assignment
{
	class SMSMessage : INotification
	{
		public required IUser Sender { get; set; }
		public required List<INotifyableUser> Receivers { get; set; }
		public required string Content { get; set; }

		public void Send()
		{
			Console.WriteLine($"Sending SMS from {Sender.FirstName} {Sender.LastName}");

			foreach (var receiver in Receivers)
			{
				receiver.Notify(Content);
			}
		}
	}
}
