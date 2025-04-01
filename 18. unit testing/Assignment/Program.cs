//using Assignment;

//ComputerComponentsRepository repository = new ComputerComponentsRepository();

//void PrintComponent(ComputerComponent component)
//{
//    Console.WriteLine($"Id: {component.Id}, Manufacturer: {component.Manufacturer}, Name: {component.Name}, Price: {component.Price}");
//}

//repository.Create(new ComputerComponent() { Id = 1, Manufacturer = "Intel", Name = "Core i9-11900K", Price = 599.99m });
//repository.Create(new ComputerComponent() { Id = 2, Manufacturer = "AMD", Name = "Ryzen 9 5950X", Price = 799.99m });
//repository.Create(new ComputerComponent() { Id = 3, Manufacturer = "Nvidia", Name = "RTX 3090", Price = 1499.99m });
//repository.Create(new ComputerComponent() { Id = 4, Manufacturer = "Corsair", Name = "Vengeance RGB Pro", Price = 99.99m });
//repository.Create(new ComputerComponent() { Id = 5, Manufacturer = "Samsung", Name = "970 EVO Plus", Price = 149.99m });

//foreach (var component in repository.Read())
//{
//    PrintComponent(component);
//}

//Console.WriteLine("=======================================");

//uint id = 3;
//Console.WriteLine($"Searching for component by ID {id}...");
//var componentById = repository.ReadById(id);
//if (componentById != null)
//{
//    Console.WriteLine("Found!");
//    PrintComponent(componentById);
//}
//else
//{
//    Console.WriteLine("No such component, aborting program...");
//    return;
//}

//Console.WriteLine("=======================================");

//Console.WriteLine($"Deleting component with ID {id}...");
//repository.Delete(componentById);

//foreach (var component in repository.Read())
//{
//    PrintComponent(component);
//}

//Console.WriteLine("=======================================");

//id = 1;
//Console.WriteLine($"Generating a new identity for component with ID {id}...");

//var componentToUpdate = new ComputerComponent() { Id = id, Manufacturer = "Intel", Name = "Core i5-4460", Price = 99.99m };

//repository.Update(componentToUpdate);

//foreach (var component in repository.Read())
//{
//    PrintComponent(component);
//}

//Console.WriteLine("=======================================");

