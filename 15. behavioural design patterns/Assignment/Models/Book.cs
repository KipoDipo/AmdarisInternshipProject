using Assignment.Models.Contracts;

namespace Assignment.Models;

class Book : IBook
{
	public required string Name { get; init; }
	public required string Author { get; init; }
}
