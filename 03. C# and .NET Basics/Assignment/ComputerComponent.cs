namespace Assignment
{
	public class ComputerComponent : ICloneable
	{
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public decimal Price { get; set; }

        public ComputerComponent(string name, string manufacturer, decimal price)
        {
            Name = name;
            Manufacturer = manufacturer;
            Price = price;
        }

        public virtual void DoWork()
        {
            Console.WriteLine($"Generic component {Manufacturer} {Name}, doing work!");
        }

        // if a new component class intruduces a mutable type, it should override this method and properly create a deep copy
        public virtual object Clone()
        {
            return MemberwiseClone();
        }
    }
}
