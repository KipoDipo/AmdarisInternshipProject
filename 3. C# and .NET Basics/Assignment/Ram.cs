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

        public string Capacity { get; }
        public string Speed { get; }
        public MemoryType Type { get; }

        public Ram(string name, string manufacturer, string capacity, string speed, MemoryType type, decimal price)
            : base(name, manufacturer, price)
        {
            Capacity = capacity;
            Speed = speed;
            Type = type;
        }
    }
}
