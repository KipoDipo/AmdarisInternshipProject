using Assignment.Coffee;

namespace Assignment.Factories;

abstract class CoffeeFactory
{
	public abstract IDrink CreateCoffee();

	public IDrink CreateCoffeeWithExtra(List<IConsumable> extraIngredients)
	{
		try
		{
			var coffee = CreateCoffee();
			coffee.Ingredients.AddRange(extraIngredients);
			Logger.LogSuccess($"{nameof(CoffeeFactory)} - {nameof(CreateCoffeeWithExtra)} - {string.Join(", ", extraIngredients)}");
			return coffee;
		}
		catch (Exception)
		{
			Logger.LogFailure($"{nameof(CoffeeFactory)} - {nameof(CreateCoffeeWithExtra)} - {string.Join(", ", extraIngredients)}");
			throw;
		}
	}
}
