namespace Assignment.Models.Contracts;

interface IEmployee : IPerson, INotifyable, IPayable
{
	int ID { get; init; }
}
