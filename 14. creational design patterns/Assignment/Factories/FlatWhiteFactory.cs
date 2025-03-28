using Assignment.Coffee;

namespace Assignment.Factories;

class FlatWhiteFactory : CoffeeFactory
{
	public override IDrink CreateCoffee()
	{
		return new FlatWhite();
	}
}
