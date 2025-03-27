namespace Assignment
{
	class User : INotifyableUser
	{
		public required string FirstName { get; set; }
		public required string LastName { get; set; }

		public void Notify(string notificationContent)
		{
			Console.WriteLine($"You ({FirstName}) have a new message!");
			Console.WriteLine($"  {{ {notificationContent} }}");
		}
	}
}
