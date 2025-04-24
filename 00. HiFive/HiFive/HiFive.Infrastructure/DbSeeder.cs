using HiFive.Application.DTOs.Artist;
using HiFive.Application.DTOs.Listener;
using HiFive.Application.UnitOfWork;
using HiFive.Domain.Models.Music;
using HiFive.Domain.Models.Users;

namespace HiFive.Infrastructure;

public class DbSeeder
{
	public static async Task Seed(IUnitOfWork unit)
	{
		Artist Michael;
		Artist Freddie;
		Artist Ella;
		Artist Ludwig;

		Album MichaelAlbum;
		Album FreddieAlbum;
		Album EllaAlbum;
		Album LudwigAlbum;

		// Seed the database with initial data
		if (!unit.Genres.GetAllNoTracking().Any())
		{
			await unit.BeginTransactionAsync();
			List<string> genres = ["pop", "rock", "jazz", "classical", "hip-hop", "country", "electronic", "reggae"];

			foreach (var genre in genres)
				await unit.Genres.AddAsync(new Genre() { Name = genre });

			await unit.CommitTransactionAsync();
		}
		if (!unit.Artists.GetAllNoTracking().Any())
		{
			var MichaelDto = new ArtistCreateDto() { UserName = "artist1", Email = "michael@gmail.com", Password = "michaelistheBEst123!", FirstName = "Michael", LastName = "Jackson", Bio = "I am the king of pop", DisplayName = "Michael Jackson" };
			var FreddieDto = new ArtistCreateDto() { UserName = "artist2", Email = "freddie@gmail.com", Password = "FreddieRocks123!", FirstName = "Freddie", LastName = "Mercury", Bio = "I am the king of rock", DisplayName = "Freddie Mercury" };
			var EllaDto = new ArtistCreateDto() { UserName = "artist3", Email = "ella@gmail.com", Password = "EllaJazz123!", FirstName = "Ella", LastName = "Fitzgerald", Bio = "I am the queen of jazz", DisplayName = "Ella Fitzgerald" };
			var LudwigDto = new ArtistCreateDto() { UserName = "artist4", Email = "ludwig@gmail.com", Password = "LudwigClassical123!", FirstName = "Ludwig", LastName = "Beethoven", Bio = "I am the master of classical music", DisplayName = "Ludwig van Beethoven" };

			await unit.BeginTransactionAsync();

			Michael = await unit.Artists.Register(MichaelDto);
			Freddie = await unit.Artists.Register(FreddieDto);
			Ella = await unit.Artists.Register(EllaDto);
			Ludwig = await unit.Artists.Register(LudwigDto);

			await unit.CommitTransactionAsync();
		}
		else
		{
			Michael = unit.Artists.GetAll().First(a => a.DisplayName == "Michael Jackson");
			Freddie = unit.Artists.GetAll().First(a => a.DisplayName == "Freddie Mercury");
			Ella = unit.Artists.GetAll().First(a => a.DisplayName == "Ella Fitzgerald");
			Ludwig = unit.Artists.GetAll().First(a => a.DisplayName == "Ludwig van Beethoven");
		}
		if (!unit.Albums.GetAllNoTracking().Any())
		{
			await unit.BeginTransactionAsync();

			MichaelAlbum = new Album() { Title = "Thriller", ReleaseDate = new DateTime(1982, 11, 30), ArtistId = Michael.Id };
			FreddieAlbum = new Album() { Title = "A Night at the Opera", ReleaseDate = new DateTime(1975, 11, 21), ArtistId = Freddie.Id };
			EllaAlbum = new Album() { Title = "Ella and Friends", ReleaseDate = new DateTime(1960, 1, 1), ArtistId = Ella.Id };
			LudwigAlbum = new Album() { Title = "Symphony No. 9", ReleaseDate = new DateTime(1824, 5, 7), ArtistId = Ludwig.Id };

			await unit.Albums.AddAsync(MichaelAlbum);
			await unit.Albums.AddAsync(FreddieAlbum);
			await unit.Albums.AddAsync(EllaAlbum);
			await unit.Albums.AddAsync(LudwigAlbum);

			await unit.CommitTransactionAsync();
		}
		else
		{
			MichaelAlbum = unit.Albums.GetAll().First(a => a.Title == "Thriller");
			FreddieAlbum = unit.Albums.GetAll().First(a => a.Title == "A Night at the Opera");
			EllaAlbum = unit.Albums.GetAll().First(a => a.Title == "Ella and Friends");
			LudwigAlbum = unit.Albums.GetAll().First(a => a.Title == "Symphony No. 9");
		}
		if (!unit.Songs.GetAllNoTracking().Any())
		{
			var pop = unit.Genres.GetAll().First(g => g.Name == "pop");
			var rock = unit.Genres.GetAll().First(g => g.Name == "rock");
			var jazz = unit.Genres.GetAll().First(g => g.Name == "jazz");
			var classical = unit.Genres.GetAll().First(g => g.Name == "classical");

			var MichaelSong1 = new Song() { Title = "Billie Jean", Duration = (uint)TimeSpan.FromMinutes(4.54).TotalSeconds, Genres = [pop], Data = "blob", Album = MichaelAlbum, Artist = Michael };
			var MichaelSong2 = new Song() { Title = "Beat It", Duration = (uint)TimeSpan.FromMinutes(4.18).TotalSeconds, Genres = [pop], Data = "blob", Album = MichaelAlbum, Artist = Michael };
			var FreddieSong1 = new Song() { Title = "Bohemian Rhapsody", Duration = (uint)TimeSpan.FromMinutes(5.55).TotalSeconds, Genres = [rock], Data = "blob", Album = FreddieAlbum, Artist = Freddie };
			var FreddieSong2 = new Song() { Title = "Somebody to Love", Duration = (uint)TimeSpan.FromMinutes(4.55).TotalSeconds, Genres = [rock], Data = "blob", Album = FreddieAlbum, Artist = Freddie };
			var EllaSong1 = new Song() { Title = "Summertime", Duration = (uint)TimeSpan.FromMinutes(4.00).TotalSeconds, Genres = [jazz], Data = "blob", Album = EllaAlbum, Artist = Ella };
			var EllaSong2 = new Song() { Title = "Dream a Little Dream of Me", Duration = (uint)TimeSpan.FromMinutes(3.10).TotalSeconds, Genres = [jazz], Data = "blob", Album = EllaAlbum, Artist = Ella };
			var LudwigSong1 = new Song() { Title = "Symphony No. 5", Duration = (uint)TimeSpan.FromMinutes(7.00).TotalSeconds, Genres = [classical], Data = "blob", Album = LudwigAlbum, Artist = Ludwig };
			var LudwigSong2 = new Song() { Title = "Piano Sonata No. 14", Duration = (uint)TimeSpan.FromMinutes(5.00).TotalSeconds, Genres = [classical], Data = "blob", Album = LudwigAlbum, Artist = Ludwig };

			await unit.BeginTransactionAsync();

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
		if (!unit.Listeners.GetAll().Any())
		{
			var destroyer99 = new ListenerCreateDto() { UserName = "destroyer99", Email = "destroyer99@gmail.com", Password = "Password123!", FirstName = "Fred", LastName = "Stone", Bio = "I love music", DisplayName = "Fred the Destroyer" };
			var rockstar = new ListenerCreateDto() { UserName = "rockstar", Email = "rockstar@gmail.com", Password = "Passw@ord123!", FirstName = "John", LastName = "Doe", Bio = "I love rock music", DisplayName = "Rockstar" };
			var jazzlover = new ListenerCreateDto() { UserName = "jazzlover", Email = "jazzlover@gmail.com", Password = "PASDassword123!", FirstName = "Jane", LastName = "Smith", Bio = "I love jazz music", DisplayName = "Jazz Lover" };
			var classicalfan = new ListenerCreateDto() { UserName = "classicalfan", Email = "classicalfan@gmail.com", Password = "Pass++_+_word123!", FirstName = "Alice", LastName = "Johnson", Bio = "I love classical music", DisplayName = "Classical Fan" };

			await unit.BeginTransactionAsync();
			
			await unit.Listeners.Register(destroyer99);
			await unit.Listeners.Register(rockstar);
			await unit.Listeners.Register(jazzlover);
			await unit.Listeners.Register(classicalfan);

			await unit.CommitTransactionAsync();
		}

	}
}
