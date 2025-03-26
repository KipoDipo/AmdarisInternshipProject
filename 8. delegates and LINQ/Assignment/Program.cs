namespace Assignment
{
    class Person
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
    
    public class Program
    {
        static void Main(string[] args)
        {
            List<Person> people =
            [
                new() { FirstName = "Atanas", LastName = "Kambitov" },
                new() { FirstName = "Georgi", LastName = "Georgiev" },
                new() { FirstName = "Ivan", LastName = "Ivanov" },
                new() { FirstName = "Petar", LastName = "Samoilov" },
                new() { FirstName = "Stefan", LastName = "Stambolov" },
                new() { FirstName = "Vasil", LastName = "Karaivanov" },
                new() { FirstName = "Vladimir", LastName = "Randomirov" },
            ];
        }
    }
}
