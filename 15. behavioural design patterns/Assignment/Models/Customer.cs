using Assignment.Models.Contracts;

namespace Assignment.Models;

class Customer : ICustomer
{
	public string Name { get; init; }
	public NotificationType Preference { get; set; }

	public void Notify(string content)
	{
		Console.WriteLine($"{Preference} notification: {content}");
	}
}