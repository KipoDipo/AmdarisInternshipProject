namespace Assignment.Models.Contracts;

interface IOrder
{
	ICustomer Notified { get; init; }
	IBook Book { get; set; }
}