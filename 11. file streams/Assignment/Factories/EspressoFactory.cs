using Assignment.Coffee;

namespace Assignment.Factories;

class EspressoFactory : CoffeeFactory
{
	public override IDrink CreateCoffee()
	{
		try
		{
			var coffee = new Espresso();
			Logger.LogSuccess($"{nameof(EspressoFactory)} - {nameof(CreateCoffee)}");
			return coffee;
		}
		catch (Exception)
		{
			Logger.LogFailure($"{nameof(EspressoFactory)} - {nameof(CreateCoffee)}");
			throw;
		}
	}
}
