using Assignment.Coffee;

namespace Assignment.Factories;

class CappuccinoFactory : CoffeeFactory
{
	public override IDrink CreateCoffee()
	{
		try
		{
			var coffee = new Cappuccino();
			Logger.LogSuccess($"{nameof(CappuccinoFactory)} - {nameof(CreateCoffee)}");
			return coffee;
		}
		catch (Exception)
		{
			Logger.LogFailure($"{nameof(CappuccinoFactory)} - {nameof(CreateCoffee)}");
			throw;
		}
	}
}
