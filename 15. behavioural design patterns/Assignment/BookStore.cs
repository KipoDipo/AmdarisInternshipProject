using Assignment.Models;
using Assignment.Models.Contracts;
using Assignment.Services.Contracts;

namespace Assignment;

class BookStore
{
	private IPersonService<ICustomer> _customerService;
	private IPersonService<IEmployee> _employeeService;
	private IOrderService _orderService;

	public BookStore(IPersonService<ICustomer> customerService, IPersonService<IEmployee> employeeService, IOrderService orderService)
	{
		_customerService = customerService;
		_employeeService = employeeService;
		_orderService = orderService;
	}

	public void AddEmployee(IEmployee employee)
	{
		_employeeService.Add(employee);
		_employeeService.Subscribe(employee);
	}

	public void Order(ICustomer customer, IBook book)
	{
		foreach (var employee in _employeeService.GetAllSubscribed())
		{
			employee.Notify($"{customer.Name} has ordered {book.Name} ({book.Author})");
		}

		_customerService.Add(customer);
		_customerService.Subscribe(customer);

		customer.Notify($"Your order ({book.Name}) has been placed.\n" +
			"\tYou can unsubscribe from further notifications with .UnsubscribeCustomer(ICustomer)");

		_orderService.Add(new Order() { Notified = customer, Book = book });
	}

	public void ProcessOrders()
	{
		var subscribedEmployees = _employeeService.GetAllSubscribed();

		foreach (var order in _orderService.GetAll())
		{
			foreach (var employee in subscribedEmployees)
			{
				employee.Notify($"Processing {order.Notified.Name}'s order for {order.Book.Name}");
			}
		}
		foreach (var order in GetSubscribedCustomersOrders())
		{
			order.Notified.Notify($"Hey {order.Notified.Name}, we are processing your order for {order.Book.Name}");
		}
	}

	public void PackageOrders()
	{
		var subscribedEmployees = _employeeService.GetAllSubscribed();
		
		foreach (var order in _orderService.GetAll())
		{
			foreach (var employee in subscribedEmployees)
			{
				employee.Notify($"Processing {order.Notified.Name}'s order for {order.Book.Name}");
			}
		}
		foreach (var order in GetSubscribedCustomersOrders())
		{
			order.Notified.Notify($"Hey {order.Notified.Name}, your order is being packaged");
		}
	}

	public void SendOrders()
	{
		var subscribedEmployees = _employeeService.GetAllSubscribed();

		foreach (var order in _orderService.GetAll())
		{
			foreach (var employee in subscribedEmployees)
			{
				employee.Notify($"Processing {order.Notified.Name}'s order for {order.Book.Name}");
			}
		}
		foreach (var order in GetSubscribedCustomersOrders())
		{
			order.Notified.Notify($"Hey {order.Notified.Name}, your order ({order.Book.Name}) was sent!");
		}
	}

	public void UnsubscribeCustomer(ICustomer customer)
	{
		customer.Notify($"You {customer.Name} have unsubscribed from notifications");
		_customerService.Unsubscribe(customer);
	}

	public void UnsubscribeEmployee(IEmployee employee)
	{
		employee.Notify($"You {employee.Name} have unsubscribed from notifications");
		_employeeService.Unsubscribe(employee);
	}

	private IEnumerable<IOrder> GetSubscribedCustomersOrders()
	{
		return _orderService.GetAll().Where(order => _customerService.GetAllSubscribed().Contains(order.Notified));
	}
}
