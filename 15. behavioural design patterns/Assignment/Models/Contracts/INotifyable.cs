namespace Assignment.Models.Contracts;

[Flags]
enum NotificationType
{
	Email,
	SMS
}

interface INotifyable
{
	NotificationType Preference { get; set; }

	void Notify(string content);
}
