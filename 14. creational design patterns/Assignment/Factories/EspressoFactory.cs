using Assignment.Coffee;

namespace Assignment.Factories;

class EspressoFactory : CoffeeFactory
{
	public override IDrink CreateCoffee()
	{
		return new Espresso();
	}
}
