namespace Assignment
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<ComputerComponent> configuration =
            [
                new GraphicsCard("RTX 3080", "NVidia", 3000, 1800),
                new Processor("Ryzen 5600x", "AMD", 6, 400),
                new Ram("Fury", "HyperX", 8, 3200, Ram.MemoryType.DDR4, 100),
                new Ram("Fury", "HyperX", 8, 3200, Ram.MemoryType.DDR4, 100),
                new ComputerComponent("500W", "SeaSonic", 100),
                new ComputerComponent("123ABC", "ASRock", 150)
            ];

            PersonalComputer computer = new PersonalComputer();

            foreach (var component in configuration)
            {
                computer.AddComponent(component);
            }

            decimal priceOfPC = 0;
            foreach (var component in computer)
            {
                priceOfPC += component.Price;
            }

            Console.WriteLine($"The PC that costs {priceOfPC}BGN is going to attempt a boot seqence...");

            if (computer.IsValidConfiguration())
            {
                computer.PowerOn(shutDownAfter: true, delaySeconds: 2);
            }
            else
            {
                Console.WriteLine("This configuration is missing parts...");
            }

        }
    }
}
