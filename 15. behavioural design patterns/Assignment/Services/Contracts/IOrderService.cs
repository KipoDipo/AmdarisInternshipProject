using Assignment.Models.Contracts;

namespace Assignment.Services.Contracts;

interface IOrderService
{
	IEnumerable<IOrder> GetAll();

	void Add(IOrder order);
}