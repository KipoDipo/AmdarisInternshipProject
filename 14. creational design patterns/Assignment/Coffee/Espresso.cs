using Assignment.Ingredients;

namespace Assignment.Coffee;

class Espresso : IDrink
{
	public List<IConsumable> Ingredients { get; set; }

	public Espresso()
	{
		Ingredients = [new BlackCoffee()];
	}
}
