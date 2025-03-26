namespace Assignment
{
    public class GraphicsCard : Unit
    {
        public GraphicsCard(string name, string manufacturer, uint coreCount, decimal price)
            : base(name, manufacturer, coreCount, price)
        {
        }

        public override void DoWork()
        {
            Console.WriteLine($"GPU {Manufacturer} {Name} is processing the graphics with all {CoreCount} cores!");
        }
    }
}
