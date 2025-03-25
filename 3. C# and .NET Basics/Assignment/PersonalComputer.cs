using System.Collections;

namespace Assignment
{
    public class PersonalComputer : IEnumerable<ComputerComponent>
    {
        private List<ComputerComponent> components;

        public PersonalComputer()
        {
            components = new List<ComputerComponent>();
        }

        public PersonalComputer(PersonalComputer other)
            : this()
        {
            foreach (var otherComponent in other.components)
            {
                components.Add((ComputerComponent)otherComponent.Clone());
            }
        }

        public void AddComponent(ComputerComponent component)
        {
            components.Add(component);
        }

        public void PowerOn(float delaySeconds = 0.0f, bool shutDownAfter = false)
        {
            if (delaySeconds != 0.0f)
            {
                Console.WriteLine($"Waiting {delaySeconds} seconds before power on...");
                Thread.Sleep((int)(delaySeconds * 1000));
            }
            foreach (var component in components)
            {
                component.DoWork();
            }
            if (shutDownAfter)
            {
                PowerOff();
            }
        }

        public void PowerOff()
        {
            Console.WriteLine("Shutting down...");
        }

        public static bool IsValidConfiguration(List<ComputerComponent> components)
        {
            bool hasGPU = false;
            bool hasCPU = false;
            bool hasRam = false;

            foreach (var component in components)
            {
                if (component is GraphicsCard)
                    hasGPU = true;
                else if (component is Processor)
                    hasCPU = true;
                else if (component is Ram)
                    hasRam = true;
            }

            return hasGPU && hasCPU && hasRam;
        }

        public bool IsValidConfiguration()
        {
            return IsValidConfiguration(components);
        }

        public IEnumerator<ComputerComponent> GetEnumerator()
        {
            return components.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
