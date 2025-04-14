namespace HiFive.Application.DTOs.Genre;

public class GenreDto
{
	public Guid Id { get; set; }
	public string Name { get; set; } = null!;

	public static GenreDto FromEntity(Domain.Models.Music.Genre genre)
	{
		return new GenreDto
		{
			Id = genre.Id,
			Name = genre.Name
		};
	}
}
