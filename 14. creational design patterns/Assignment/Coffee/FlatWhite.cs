using Assignment.Ingredients;

namespace Assignment.Coffee;

class FlatWhite : IDrink
{
	public List<IConsumable> Ingredients { get; set; }
	
	public FlatWhite()
	{
		Ingredients = [new BlackCoffee(), new BlackCoffee(), new RegularMilk()];
	}
}