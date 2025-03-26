namespace Assignment
{
    public class Person
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }

    public class Program
    {
        delegate void PersonAction(Person person);

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

            var query = people.Select(people => new Person() { FirstName = people.FirstName.ToUpper(), LastName = people.LastName.ToUpper() });

            foreach (var person in query)
            {
                Console.WriteLine($"{person.FirstName} {person.LastName}");
            }
        }
    }
}
