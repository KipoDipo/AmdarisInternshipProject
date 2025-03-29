using Assignment.Models.Contracts;

namespace Assignment.Models;

class Order : IOrder
{
	public required ICustomer Notified { get; init; }
	public required IBook Book { get; set; }
}
