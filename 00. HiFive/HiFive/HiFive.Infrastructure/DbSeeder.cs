using HiFive.Application.Contracts;
using HiFive.Domain.Models.Music;
using HiFive.Domain.Models.Users;

namespace HiFive.Infrastructure;

public class DbSeeder
{
	public static async Task Seed(IUnitOfWork unit, BaseUserManager<Listener> listenerManager, BaseUserManager<Artist> artistManager)
	{
		// Seed the database with initial data
		if (!unit.Genres.GetAll().Any())
		{
			await unit.BeginTransactionAsync();
			List<string> genres = ["pop", "rock", "jazz", "classical", "hip-hop", "country", "electronic", "reggae"];

			foreach (var genre in genres)
				await unit.Genres.AddAsync(new Genre() { Name = genre });

			await unit.CommitTransactionAsync();
		}
		if (!artistManager.Users.Any())
		{
			await unit.BeginTransactionAsync();
			var Michael = new Artist() { UserName = "artist1", FirstName = "Michael", LastName = "Jackson", Bio = "I am the king of pop", DisplayName = "Michael Jackson" };
			var Freddie = new Artist() { UserName = "artist2", FirstName = "Freddie", LastName = "Mercury", Bio = "I am the king of rock", DisplayName = "Freddie Mercury" };
			var Ella = new Artist() { UserName = "artist3", FirstName = "Ella", LastName = "Fitzgerald", Bio = "I am the queen of jazz", DisplayName = "Ella Fitzgerald" };
			var Ludwig = new Artist() { UserName = "artist4", FirstName = "Ludwig", LastName = "Beethoven", Bio = "I am the master of classical music", DisplayName = "Ludwig van Beethoven" };

			await artistManager.CreateAsync(Michael, "Password123!");
			await artistManager.CreateAsync(Freddie, "Password123!");
			await artistManager.CreateAsync(Ella, "Password123!");
			await artistManager.CreateAsync(Ludwig, "Password123!");

			await unit.CommitTransactionAsync();
		}
		if (!unit.Albums.GetAll().Any())
		{
			await unit.BeginTransactionAsync();

			var Michael = artistManager.Users.First(a => a.UserName == "artist1");
			var Freddie = artistManager.Users.First(a => a.UserName == "artist2");
			var Ella = artistManager.Users.First(a => a.UserName == "artist3");
			var Ludwig = artistManager.Users.First(a => a.UserName == "artist4");

			var MichaelAlbum = new Album() { Title = "Thriller", ReleaseDate = new DateTime(1982, 11, 30), ArtistId = Michael.Id };
			var FreddieAlbum = new Album() { Title = "A Night at the Opera", ReleaseDate = new DateTime(1975, 11, 21), ArtistId = Freddie.Id };
			var EllaAlbum = new Album() { Title = "Ella and Friends", ReleaseDate = new DateTime(1960, 1, 1), ArtistId = Ella.Id };
			var LudwigAlbum = new Album() { Title = "Symphony No. 9", ReleaseDate = new DateTime(1824, 5, 7), ArtistId = Ludwig.Id };

			await unit.Albums.AddAsync(MichaelAlbum);
			await unit.Albums.AddAsync(FreddieAlbum);
			await unit.Albums.AddAsync(EllaAlbum);
			await unit.Albums.AddAsync(LudwigAlbum);
			await unit.CommitTransactionAsync();

		}
		if (!unit.Songs.GetAll().Any())
		{ 
			await unit.BeginTransactionAsync();

			var pop = unit.Genres.GetAll().First(g => g.Name == "pop");
			var rock = unit.Genres.GetAll().First(g => g.Name == "rock");
			var jazz = unit.Genres.GetAll().First(g => g.Name == "jazz");
			var classical = unit.Genres.GetAll().First(g => g.Name == "classical");

			var Michael = artistManager.Users.First(a => a.UserName == "artist1");
			var Freddie = artistManager.Users.First(a => a.UserName == "artist2");
			var Ella = artistManager.Users.First(a => a.UserName == "artist3");
			var Ludwig = artistManager.Users.First(a => a.UserName == "artist4");

			var MichaelAlbum = unit.Albums.GetAll().First(a => a.Title == "Thriller");
			var FreddieAlbum = unit.Albums.GetAll().First(a => a.Title == "A Night at the Opera");
			var EllaAlbum = unit.Albums.GetAll().First(a => a.Title == "Ella and Friends");
			var LudwigAlbum = unit.Albums.GetAll().First(a => a.Title == "Symphony No. 9");

			var MichaelSong1 = new Song() { Title = "Billie Jean", Duration = (uint)TimeSpan.FromMinutes(4.54).TotalSeconds, Genres = [pop], Data = "blob", Album = MichaelAlbum, Artist = Michael};
			var MichaelSong2 = new Song() { Title = "Beat It", Duration = (uint)TimeSpan.FromMinutes(4.18).TotalSeconds, Genres = [pop], Data = "blob", Album = MichaelAlbum, Artist = Michael };
			var FreddieSong1 = new Song() { Title = "Bohemian Rhapsody", Duration = (uint)TimeSpan.FromMinutes(5.55).TotalSeconds, Genres = [rock], Data = "blob", Album = FreddieAlbum, Artist = Freddie };
			var FreddieSong2 = new Song() { Title = "Somebody to Love", Duration = (uint)TimeSpan.FromMinutes(4.55).TotalSeconds, Genres = [rock], Data = "blob", Album = FreddieAlbum, Artist = Freddie };
			var EllaSong1 = new Song() { Title = "Summertime", Duration = (uint)TimeSpan.FromMinutes(4.00).TotalSeconds, Genres = [jazz], Data = "blob", Album = EllaAlbum, Artist = Ella };
			var EllaSong2 = new Song() { Title = "Dream a Little Dream of Me", Duration = (uint)TimeSpan.FromMinutes(3.10).TotalSeconds, Genres = [jazz], Data = "blob", Album = EllaAlbum, Artist = Ella };
			var LudwigSong1 = new Song() { Title = "Symphony No. 5", Duration = (uint)TimeSpan.FromMinutes(7.00).TotalSeconds, Genres = [classical], Data = "blob", Album = LudwigAlbum, Artist = Ludwig };
			var LudwigSong2 = new Song() { Title = "Piano Sonata No. 14", Duration = (uint)TimeSpan.FromMinutes(5.00).TotalSeconds, Genres = [classical], Data = "blob", Album = LudwigAlbum, Artist = Ludwig };

			await unit.Songs.AddAsync(MichaelSong1);
			await unit.Songs.AddAsync(MichaelSong2);
			await unit.Songs.AddAsync(FreddieSong1);
			await unit.Songs.AddAsync(FreddieSong2);
			await unit.Songs.AddAsync(EllaSong1);
			await unit.Songs.AddAsync(EllaSong2);
			await unit.Songs.AddAsync(LudwigSong1);
			await unit.Songs.AddAsync(LudwigSong2);
			await unit.CommitTransactionAsync();
		}
		if (!listenerManager.Users.Any())
		{
			await unit.BeginTransactionAsync();
			await listenerManager.CreateAsync(new Listener() { UserName = "destroyer99", FirstName = "Fred", LastName = "Stone", Bio = "I love music", DisplayName = "Fred the Destroyer" }, "Password123!");
			await listenerManager.CreateAsync(new Listener() { UserName = "rockstar", FirstName = "John", LastName = "Doe", Bio = "I love rock music", DisplayName = "Rockstar" }, "Passw@ord123!");
			await listenerManager.CreateAsync(new Listener() { UserName = "jazzlover", FirstName = "Jane", LastName = "Smith", Bio = "I love jazz music", DisplayName = "Jazz Lover" }, "PASDassword123!");
			await listenerManager.CreateAsync(new Listener() { UserName = "classicalfan", FirstName = "Alice", LastName = "Johnson", Bio = "I love classical music", DisplayName = "Classical Fan" }, "Pass++_+_word123!");
			await unit.CommitTransactionAsync();
		}

	}
}
