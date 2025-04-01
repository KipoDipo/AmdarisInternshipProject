using System.ComponentModel;

namespace Assignment.Tests;

public class UnitTest1
{
	private ComputerComponentsRepository repository = new();

	[Fact]
	public void Creation()
	{
		int count = repository.Read().Count();
		repository.Create(new ComputerComponent() { Id = 1, Manufacturer = "", Name = "", Price = 0 });
		Assert.Equal(repository.Read().Count(), count + 1);
	}

	[Fact]
	public void Deletion()
	{
		ComputerComponent component = new ComputerComponent() { Id = 1, Manufacturer = "", Name = "", Price = 0 };

		repository.Create(component);
		
		int count = repository.Read().Count();
		
		repository.Delete(component);

		Assert.Equal(repository.Read().Count(), count - 1);
	}

	[Fact]
	public void Update()
	{
		ComputerComponent component1 = new ComputerComponent() { Id = 1, Manufacturer = "", Name = "", Price = 0 };

		repository.Create(component1);

		int count = repository.Read().Count();

		ComputerComponent component2 = new ComputerComponent() { Id = 1, Manufacturer = "Man", Name = "Nam", Price = 2.0m };
		
		repository.Update(component2);

		Assert.Equal(repository.Read().Count(), count);

		ComputerComponent? foundComponent = repository.ReadById(1);

		Assert.NotNull(foundComponent);
		Assert.Equal(component2.Id, foundComponent.Id);
		Assert.Equal(component2.Manufacturer, foundComponent.Manufacturer);
		Assert.Equal(component2.Name, foundComponent.Name);
		Assert.Equal(component2.Price, foundComponent.Price);
	}



	[Fact]
	public void GetsCorrectComponent()
	{
		ComputerComponent component = new ComputerComponent() { Id = 1, Manufacturer = "", Name = "", Price = 0 };
		repository.Create(component);
		ComputerComponent? foundComponent = repository.ReadById(1);

		Assert.NotNull(foundComponent);
		Assert.Equal(component.Id, foundComponent.Id);
		Assert.Equal(component.Manufacturer, foundComponent.Manufacturer);
		Assert.Equal(component.Name, foundComponent.Name);
		Assert.Equal(component.Price, foundComponent.Price);
	}
}
