using Assignment.Ingredients;

namespace Assignment.Coffee;

class Cappuccino : IDrink
{
	public List<IConsumable> Ingredients { get; set; }

	public Cappuccino()
	{
		Ingredients = [new BlackCoffee(), new RegularMilk()];
	}
}