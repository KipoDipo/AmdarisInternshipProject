using Assignment.Coffee;

namespace Assignment.Factories;

class CappuccinoFactory : CoffeeFactory
{
	public override IDrink CreateCoffee()
	{
		return new Cappuccino();
	}
}
