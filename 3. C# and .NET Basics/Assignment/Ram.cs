namespace Assignment
{
    public class Ram : ComputerComponent
    {
        public enum MemoryType
        {
            DDR2,
            DDR3,
            DDR4,
            DDR5
        }

        public uint Capacity { get; }
        public uint Speed { get; }
        public MemoryType Type { get; }

        public Ram(string name, string manufacturer, uint capacity, uint speed, MemoryType type, decimal price)
            : base(name, manufacturer, price)
        {
            Capacity = capacity;
            Speed = speed;
            Type = type;
        }

        public override void DoWork()
        {
            Console.WriteLine($"RAM {Manufacturer} {Name} ({Type}) is swiftly storing up to {Capacity}GB of data at {Speed}MHz");
        }
    }
}
