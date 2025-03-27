namespace Assignment
{
	public class Car
	{
		public string Name { get; set; }
		public int Speed { get; set; }
		public string Color { get; set; }
	}

	public class CarOwner
	{
		public string Name { get; set; }
		public string[] Cars { get; set; }
	}

	internal class Program
	{
		static void Main(string[] args)
		{
			Car[] cars =
			{
				new() { Name = "Toyota", Speed = 200, Color = "Red" },
				new() { Name = "Nissan", Speed = 220, Color = "Blue" },
				new() { Name = "Ford", Speed = 180, Color = "Red" },
				new() { Name = "Volkswagen", Speed = 190, Color = "Yellow" },
				new() { Name = "Chevrolet", Speed = 210, Color = "Red" },
				new() { Name = "BMW", Speed = 250, Color = "White" },
				new() { Name = "Mercedes", Speed = 240, Color = "Yellow" },
			};

			CarOwner[] owners = {
				new() {Name = "Nasko", Cars = [ "Toyota", "Nissan" ]},
				new() {Name = "Pesho", Cars = [ "Toyota", "Chevrolet" ]},
				new() {Name = "Vasko", Cars = [ "Ford", "Volkswagen", "Chevrolet" ]},
				new() {Name = "Gesho", Cars = [ "Nissan", "Volkswagen" ]},
			};

			var carsToOwnersQuery = from owner in owners
									from car in owner.Cars
									select $"{owner.Name} - {car}";

			foreach (var item in carsToOwnersQuery)
			{
				Console.WriteLine(item);
			}

			// =============================================================

			var ownerToColorsQuery1 = from owner in owners
									  select new
									  {
										  Owner = owner.Name,
										  Colors = (from carName in owner.Cars
													join car in cars on carName equals car.Name
													select car.Color).Distinct()
									  };

			var ownerToColorsQuery2 = owners
				.Select(owner => new
				{
					Owner = owner.Name,
					Colors = owner.Cars.Join(cars, ownedCar => ownedCar, car => car.Name, (ownedCar, car) => car.Color).Distinct()
				});

			foreach (var item in ownerToColorsQuery2)
			{
				Console.WriteLine($"Owner: {item.Owner}, owned car colors: {string.Join(", ", item.Colors)}");
			}


			// =============================================================

			var groupCarsByColors = from car in cars
									group car by car.Color;

			foreach (var group in groupCarsByColors)
			{
				Console.WriteLine(group.Key);
				foreach (var car in group)
				{
					Console.WriteLine($"  {car.Name}");
				}
			}
		}
	}
}
