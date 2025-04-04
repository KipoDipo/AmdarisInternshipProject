using Assignment.Coffee;

namespace Assignment.Factories;

class FlatWhiteFactory : CoffeeFactory
{
	public override IDrink CreateCoffee()
	{
		try
		{
			var coffee = new FlatWhite();
			Logger.LogSuccess($"{nameof(FlatWhiteFactory)} - {nameof(CreateCoffee)}");
			return coffee;
		}
		catch (Exception)
		{
			Logger.LogFailure($"{nameof(FlatWhiteFactory)} - {nameof(CreateCoffee)}");
			throw;
		}
	}
}
