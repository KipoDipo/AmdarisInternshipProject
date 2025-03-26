namespace Assignment
{
    public class Unit : ComputerComponent
    {
        public uint CoreCount { get; init; }

        public Unit(string name, string manufacturer, uint coreCount, decimal price)
            : base(name, manufacturer, price)
        {
            CoreCount = coreCount;
        }

        public override void DoWork()
        {
            Console.WriteLine($"Generic unit {Manufacturer} {Name} is processing with all {CoreCount} cores!");
        }
    }
}
