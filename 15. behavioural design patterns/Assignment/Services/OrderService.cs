using Assignment.Models.Contracts;
using Assignment.Services.Contracts;

namespace Assignment.Services;

class OrderService : IOrderService
{
	private List<IOrder> _orders;
	
	public OrderService()
	{
		_orders = [];	
	}

	public IEnumerable<IOrder> GetAll()
	{
		return _orders;
	}

	public void Add(IOrder order)
	{
		_orders.Add(order);
	}
}
