using Assignment.Services;
using Assignment.Models;
using Assignment.Models.Contracts;

namespace Assignment;

class Program
{
	public static void Main()
	{
		var bookstore = new BookStore(new PersonService<ICustomer>(), new PersonService<IEmployee>(), new OrderService());

		var employee1 = new Employee() { ID = 1, Name = "John Doe", Preference = NotificationType.Email, Salary = 1000 };
		var employee2 = new Employee() { ID = 2, Name = "Jane Doe", Preference = NotificationType.SMS, Salary = 2000 };

		bookstore.AddEmployee(employee1);
		bookstore.AddEmployee(employee2);

		var customer = new Customer() { Name = "Ivan", Preference = NotificationType.Email };

		bookstore.Order(customer, new Book() { Name = "TLOTR", Author = "J.R.R. Tolkien" });
		Console.WriteLine();
		bookstore.ProcessOrders();
		Console.WriteLine();
		bookstore.PackageOrders();
		Console.WriteLine();
		bookstore.SendOrders();
	}
}
