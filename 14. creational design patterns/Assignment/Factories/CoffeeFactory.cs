using Assignment.Coffee;

namespace Assignment.Factories;

abstract class CoffeeFactory
{
	public abstract IDrink CreateCoffee();

	public IDrink CreateCoffeeWithExtra(List<IConsumable> extraIngredients)
	{
		var coffee = CreateCoffee();
		coffee.Ingredients.AddRange(extraIngredients);
		return coffee;
	}
}
