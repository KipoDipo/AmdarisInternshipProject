using Assignment.Factories;
using Assignment.Ingredients;

namespace Assignment;

public class Program
{
	static void Main(string[] args)
	{

		CoffeeFactory espressoFactory = new EspressoFactory();
		CoffeeFactory cappuccinoFactory = new CappuccinoFactory();
		CoffeeFactory flatWhiteFactory = new FlatWhiteFactory();

		var espresso = espressoFactory.CreateCoffee();
		var espressoCustom = espressoFactory.CreateCoffeeWithExtra([new SoyMilk(), new Sugar()]);

		var cappuccino = cappuccinoFactory.CreateCoffee();
		var cappuccinoCustom = cappuccinoFactory.CreateCoffeeWithExtra([new Sugar(), new Sugar()]);

		var flatWhite = flatWhiteFactory.CreateCoffee();
		var flatWhiteCustom = flatWhiteFactory.CreateCoffeeWithExtra([new Sugar(), new Sugar()]);

	}
}
