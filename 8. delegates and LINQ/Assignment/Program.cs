namespace Assignment
{
    public class Person
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }

    public static class PersonListExtention
    {
        public static void CapitalizeNames(this List<Person> people)
        {
            foreach (var person in people)
            {
                person.FirstName = person.FirstName.ToUpper();
                person.LastName = person.LastName.ToUpper();
            }
        }

        public static void PrintNames(this List<Person> people)
        {
            foreach (var person in people)
            {
                Console.WriteLine($"{person.FirstName} {person.LastName}");
            }
        }
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

            people.CapitalizeNames();
            people.PrintNames();
        }
    }
}
