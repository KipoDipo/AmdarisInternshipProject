namespace Assignment
{
    public class Processor : Unit
    {
        public Processor(string name, string manufacturer, uint coreCount, decimal price)
            : base(name, manufacturer, coreCount, price)
        {
        }

        public override void DoWork()
        {
            Console.WriteLine($"CPU {Manufacturer} {Name} is processing the logic with all {CoreCount} cores!");
        }
    }
}
