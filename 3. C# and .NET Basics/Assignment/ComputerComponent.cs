namespace Assignment
{
	public class ComputerComponent
	{
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public decimal Price { get; set; }

        public virtual void DoWork()
        {
            Console.WriteLine($"Generic component {Manufacturer} {Name}, doing work!");
        }

        public ComputerComponent(string name, string manufacturer, decimal price)
        {
            Name = name;
            Manufacturer = manufacturer;
            Price = price;
        }
    }
}
