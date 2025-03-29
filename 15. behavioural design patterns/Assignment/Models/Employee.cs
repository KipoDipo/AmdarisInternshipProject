using Assignment.Models.Contracts;

namespace Assignment.Models;

class Employee : IEmployee
{
	public required int ID { get; init; }
	public required string Name { get; init; }
	public required NotificationType Preference { get; set; }
	public required double Salary { get; set; }

	public void Notify(string content)
	{
		Console.WriteLine($"Employee #{ID}, {Preference} Notification: {content}");
	}
}
