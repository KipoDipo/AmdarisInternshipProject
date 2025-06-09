using HiFive.Application.AwardSystem;
using HiFive.Application.DTOs.Artist;
using HiFive.Application.DTOs.Distributor;
using HiFive.Application.DTOs.Listener;
using HiFive.Application.UnitOfWork;
using HiFive.Domain.Models.Join;
using HiFive.Domain.Models.Misc;
using HiFive.Domain.Models.Music;
using HiFive.Domain.Models.Throphies;
using HiFive.Domain.Models.Users;

namespace HiFive.Infrastructure;

public class DbSeeder
{
	public static async Task Seed(IUnitOfWork unit)
	{
		Artist Reol, LotusJuice, DuaLipa, LynyrdSkynyrd, FleetwoodMac, PinkFloyd, Sabaton;

		Album ReolAlbum, LotusJuiceAlbum, DuaLipaAlbum, LynyrdSkynyrdAlbum, FleetwoodMacAlbum, PinkFloydAlbum, SabatonAlbum;

		ImageFile reolPfp, lotusPfp, duaPfp, lynyrdPfp, fleetwoodPfp, pinkPfp, sabatonPfp;

		ImageFile reolCover, lotusCover, duaCover, lynyrdCover, fleetwoodCover, pinkCover, sabatonCover;

		ImageFile user1Pfp, user2Pfp, user3Pfp, user4Pfp;

		ImageFile goldHiFive, silverHiFive, bronzeHiFive, platHiFive;


		// Seed the database with initial data
		if (!unit.Images.GetAllNoTracking().Any())
		{
			reolPfp			= FileHelper.CreateImageFile("Images/pfp/reol.jpg");
			lotusPfp		= FileHelper.CreateImageFile("Images/pfp/lotus.jpg");
			duaPfp			= FileHelper.CreateImageFile("Images/pfp/dua.jpg");
			lynyrdPfp		= FileHelper.CreateImageFile("Images/pfp/lynyrd.jpg");
			fleetwoodPfp	= FileHelper.CreateImageFile("Images/pfp/fleetwood.jpg");
			pinkPfp			= FileHelper.CreateImageFile("Images/pfp/pink.jpg");
			sabatonPfp		= FileHelper.CreateImageFile("Images/pfp/sabaton.jpg");

			reolCover		= FileHelper.CreateImageFile("Images/cover/reol.png");
			lotusCover		= FileHelper.CreateImageFile("Images/cover/lotus.jpg");
			duaCover		= FileHelper.CreateImageFile("Images/cover/dua.png");
			lynyrdCover		= FileHelper.CreateImageFile("Images/cover/lynyrd.jpg");
			fleetwoodCover	= FileHelper.CreateImageFile("Images/cover/fleetwood.jpg");
			pinkCover		= FileHelper.CreateImageFile("Images/cover/pink.png");
			sabatonCover	= FileHelper.CreateImageFile("Images/cover/sabaton.jpg");

			user1Pfp		= FileHelper.CreateImageFile("Images/pfp/user1.png");
			user2Pfp		= FileHelper.CreateImageFile("Images/pfp/user2.png");
			user3Pfp		= FileHelper.CreateImageFile("Images/pfp/user3.png");
			user4Pfp		= FileHelper.CreateImageFile("Images/pfp/user4.png");

			platHiFive		= FileHelper.CreateImageFile("Images/badge/plat-hifive.png"); 
			goldHiFive		= FileHelper.CreateImageFile("Images/badge/gold-hifive.png");
			silverHiFive	= FileHelper.CreateImageFile("Images/badge/silver-hifive.png");
			bronzeHiFive	= FileHelper.CreateImageFile("Images/badge/bronze-hifive.png");

			await unit.BeginTransactionAsync();

			await unit.Images.AddAsync(reolPfp);
			await unit.Images.AddAsync(lotusPfp);
			await unit.Images.AddAsync(duaPfp);
			await unit.Images.AddAsync(lynyrdPfp);
			await unit.Images.AddAsync(fleetwoodPfp);
			await unit.Images.AddAsync(pinkPfp);
			await unit.Images.AddAsync(sabatonPfp);

			await unit.Images.AddAsync(reolCover);
			await unit.Images.AddAsync(lotusCover);
			await unit.Images.AddAsync(duaCover);
			await unit.Images.AddAsync(lynyrdCover);
			await unit.Images.AddAsync(fleetwoodCover);
			await unit.Images.AddAsync(pinkCover);
			await unit.Images.AddAsync(sabatonCover);

			await unit.Images.AddAsync(user1Pfp);
			await unit.Images.AddAsync(user2Pfp);
			await unit.Images.AddAsync(user3Pfp);
			await unit.Images.AddAsync(user4Pfp);

			await unit.Images.AddAsync(platHiFive);
			await unit.Images.AddAsync(goldHiFive);
			await unit.Images.AddAsync(silverHiFive);
			await unit.Images.AddAsync(bronzeHiFive);


			await unit.CommitTransactionAsync();
		}
		else
		{
			// !!very bad!!
			var all = unit.Images.GetAllNoTracking().ToList();
			int ctr = 0;
			reolPfp			= all[ctr++];
			lotusPfp		= all[ctr++];
			duaPfp			= all[ctr++];
			lynyrdPfp		= all[ctr++];
			fleetwoodPfp	= all[ctr++];
			pinkPfp			= all[ctr++];
			sabatonPfp		= all[ctr++];

			reolCover		= all[ctr++];
			lotusCover		= all[ctr++];
			duaCover		= all[ctr++];
			lynyrdCover		= all[ctr++];
			fleetwoodCover	= all[ctr++];
			pinkCover		= all[ctr++];
			sabatonCover	= all[ctr++];

			user1Pfp		= all[ctr++];
			user2Pfp		= all[ctr++];
			user3Pfp		= all[ctr++];
			user4Pfp		= all[ctr++];

			platHiFive		= all[ctr++];
			goldHiFive		= all[ctr++];
			silverHiFive	= all[ctr++];
			bronzeHiFive	= all[ctr++];
		}
		if (!unit.Conditions.GetAllNoTracking().Any())
		{
			var reg		= new Condition() { Key = Conditions.Profile.Registered };
			var upPfp	= new Condition() { Key = Conditions.Profile.UploadedPfp };
			var md3		= new Condition() { Key = Conditions.Playlists.Created3 };
			var f5		= new Condition() { Key = Conditions.Artists.Followed5 };

			await unit.BeginTransactionAsync();

			await unit.Conditions.AddAsync(reg);
			await unit.Conditions.AddAsync(upPfp);
			await unit.Conditions.AddAsync(md3);
			await unit.Conditions.AddAsync(f5);

			await unit.Badges.AddAsync(new Badge() { Name = "A part of the family",		Description = "You registered for Hi-Five!",	Condition = reg,	ImageId = bronzeHiFive.Id});
			await unit.Badges.AddAsync(new Badge() { Name = "Identified!",				Description = "You uploaded a Profile Picture",	Condition = upPfp,	ImageId = silverHiFive.Id });
			await unit.Badges.AddAsync(new Badge() { Name = "Because 1 isn't enough",	Description = "You created 3 Playlists!",		Condition = md3,	ImageId = goldHiFive.Id });
			await unit.Badges.AddAsync(new Badge() { Name = "Active listener",			Description = "You followed 5 artists!",		Condition = f5,		ImageId = platHiFive.Id });

			await unit.CommitTransactionAsync();
		}
		if (!unit.Genres.GetAllNoTracking().Any())
		{
			await unit.BeginTransactionAsync();
			List<string> genres = ["pop", "rock", "jazz", "classical", "rap", "hip-hop", "country", "electronic", "reggae", "metal"];

			foreach (var genre in genres)
				await unit.Genres.AddAsync(new Genre() { Name = genre });

			await unit.CommitTransactionAsync();
		}
		if (!unit.Artists.GetAllNoTracking().Any())
		{
			var ReolDto			= new ArtistCreateDto()	{ UserName = "artist5",		Email = "reol@gmail.com",		Password = "ReolPower123!",		FirstName = "Reol",			LastName = "",			Bio = "Genre-defying Japanese vocalist and producer.",							DisplayName = "Reol",			ProfilePictureId = reolPfp.Id };
			var LotusJuiceDto	= new ArtistCreateDto()	{ UserName = "artist6",		Email = "lotus@gmail.com",		Password = "LotusPersona123!",	FirstName = "Lotus",		LastName = "Juice",		Bio = "Japanese rapper known for Persona series music.",						DisplayName = "Lotus Juice",	ProfilePictureId = lotusPfp.Id };
			var DuaLipaDto		= new ArtistCreateDto()	{ UserName = "artist7",		Email = "dua@gmail.com",		Password = "DuaLipaPop123!",	FirstName = "Dua",			LastName = "Lipa",		Bio = "British-Albanian pop superstar and Grammy winner.",						DisplayName = "Dua Lipa",		ProfilePictureId = duaPfp.Id };
			var LynyrdDto		= new ArtistCreateDto()	{ UserName = "artist8",		Email = "skynyrd@gmail.com",	Password = "FreeBird1973!",		FirstName = "Lynyrd",		LastName = "Skynyrd",	Bio = "Southern rock legends known for 'Free Bird' and 'Sweet Home Alabama'.",	DisplayName = "Lynyrd Skynyrd",	ProfilePictureId = lynyrdPfp.Id };
			var FleetwoodDto	= new ArtistCreateDto()	{ UserName = "artist9",		Email = "fleetwood@gmail.com",	Password = "DreamsGoOn123!",	FirstName = "Fleetwood",	LastName = "Mac",		Bio = "British-American rock band with iconic harmonies.",						DisplayName = "Fleetwood Mac",	ProfilePictureId = fleetwoodPfp.Id };
			var PinkFloydDto	= new ArtistCreateDto()	{ UserName = "artist10",	Email = "floyd@gmail.com",		Password = "Wall1979!",			FirstName = "Pink",			LastName = "Floyd",		Bio = "Pioneers of progressive and psychedelic rock.",							DisplayName = "Pink Floyd",		ProfilePictureId = pinkPfp.Id };
			var SabatonDto		= new ArtistCreateDto()	{ UserName = "artist11",	Email = "sabaton@gmail.com",	Password = "SabatonWar123!",	FirstName = "Sabaton",		LastName = "",			Bio = "Swedish heavy metal band inspired by historical battles.",				DisplayName = "Sabaton",		ProfilePictureId = sabatonPfp.Id };

			await unit.BeginTransactionAsync();

			Reol			= await unit.Artists.Register(ReolDto);
			LotusJuice		= await unit.Artists.Register(LotusJuiceDto);
			DuaLipa			= await unit.Artists.Register(DuaLipaDto);
			LynyrdSkynyrd	= await unit.Artists.Register(LynyrdDto);
			FleetwoodMac	= await unit.Artists.Register(FleetwoodDto);
			PinkFloyd		= await unit.Artists.Register(PinkFloydDto);
			Sabaton			= await unit.Artists.Register(SabatonDto);

			await unit.CommitTransactionAsync();
		}
		else
		{
			Reol			= unit.Artists.GetAll().First(a => a.DisplayName == "Reol");
			LotusJuice		= unit.Artists.GetAll().First(a => a.DisplayName == "Lotus Juice");
			DuaLipa			= unit.Artists.GetAll().First(a => a.DisplayName == "Dua Lipa");
			LynyrdSkynyrd	= unit.Artists.GetAll().First(a => a.DisplayName == "Lynyrd Skynyrd");
			FleetwoodMac	= unit.Artists.GetAll().First(a => a.DisplayName == "Fleetwood Mac");
			PinkFloyd		= unit.Artists.GetAll().First(a => a.DisplayName == "Pink Floyd");
			Sabaton			= unit.Artists.GetAll().First(a => a.DisplayName == "Sabaton");
		}
		if (!unit.Albums.GetAllNoTracking().Any())
		{
			await unit.BeginTransactionAsync();

			ReolAlbum			= new Album() { Title = "Sigma",								ReleaseDate = new DateTime(2016, 10, 19),	ArtistId = Reol.Id,				CoverImageId = reolCover.Id };
			LotusJuiceAlbum		= new Album() { Title = "Only a Test",							ReleaseDate = new DateTime(2009, 8, 26),	ArtistId = LotusJuice.Id,		CoverImageId = lotusCover.Id };
			DuaLipaAlbum		= new Album() { Title = "Future Nostalgia",						ReleaseDate = new DateTime(2020, 3, 27),	ArtistId = DuaLipa.Id,			CoverImageId = duaCover.Id };
			LynyrdSkynyrdAlbum	= new Album() { Title = "(Pronounced 'Lĕh-'nérd 'Skin-'nérd)",	ReleaseDate = new DateTime(1973, 8, 13),	ArtistId = LynyrdSkynyrd.Id,	CoverImageId = lynyrdCover.Id };
			FleetwoodMacAlbum	= new Album() { Title = "Rumours",								ReleaseDate = new DateTime(1977, 2, 4),		ArtistId = FleetwoodMac.Id,		CoverImageId = fleetwoodCover.Id };
			PinkFloydAlbum		= new Album() { Title = "The Dark Side of the Moon",			ReleaseDate = new DateTime(1973, 3, 1),		ArtistId = PinkFloyd.Id,		CoverImageId = pinkCover.Id };
			SabatonAlbum		= new Album() { Title = "The Art of War",						ReleaseDate = new DateTime(2008, 5, 30),	ArtistId = Sabaton.Id,			CoverImageId = sabatonCover.Id };

			await unit.Albums.AddAsync(ReolAlbum);
			await unit.Albums.AddAsync(LotusJuiceAlbum);
			await unit.Albums.AddAsync(DuaLipaAlbum);
			await unit.Albums.AddAsync(LynyrdSkynyrdAlbum);
			await unit.Albums.AddAsync(FleetwoodMacAlbum);
			await unit.Albums.AddAsync(PinkFloydAlbum);
			await unit.Albums.AddAsync(SabatonAlbum);

			await unit.CommitTransactionAsync();
		}
		else
		{
			ReolAlbum			= unit.Albums.GetAll().First(a => a.Title == "Sigma");
			LotusJuiceAlbum		= unit.Albums.GetAll().First(a => a.Title == "Only a Test");
			DuaLipaAlbum		= unit.Albums.GetAll().First(a => a.Title == "Future Nostalgia");
			LynyrdSkynyrdAlbum	= unit.Albums.GetAll().First(a => a.Title == "(Pronounced 'Lĕh-'nérd 'Skin-'nérd)");
			FleetwoodMacAlbum	= unit.Albums.GetAll().First(a => a.Title == "Rumours");
			PinkFloydAlbum		= unit.Albums.GetAll().First(a => a.Title == "The Dark Side of the Moon");
			SabatonAlbum		= unit.Albums.GetAll().First(a => a.Title == "The Art of War");
		}
		if (!unit.Songs.GetAllNoTracking().Any())
		{
			var pop			= unit.Genres.GetAll().First(g => g.Name == "pop");
			var rock		= unit.Genres.GetAll().First(g => g.Name == "rock");
			var jazz		= unit.Genres.GetAll().First(g => g.Name == "jazz");
			var classical	= unit.Genres.GetAll().First(g => g.Name == "classical");
			var electronic	= unit.Genres.GetAll().First(g => g.Name == "electronic");
			var rap			= unit.Genres.GetAll().First(g => g.Name == "rap");
			var metal		= unit.Genres.GetAll().First(g => g.Name == "metal");


			var ReolSong1		= new Song() { Title = "Give Me a Break Stop Now",	Duration = (uint)TimeSpan.ParseExact("3:47", @"m\:ss", null).TotalSeconds,	Genres = [pop, electronic],	Data = "REOL - Give me a break stop now.ogg",		Artist = Reol,			CoverImage = reolCover };
			var ReolSong2		= new Song() { Title = "ChiruChiru",				Duration = (uint)TimeSpan.ParseExact("3:17", @"m\:ss", null).TotalSeconds,	Genres = [electronic],		Data = "REOL - ChiruChiru .ogg",					Artist = Reol,			CoverImage = reolCover };

			var LotusSong1		= new Song() { Title = "Mass Destruction",			Duration = (uint)TimeSpan.ParseExact("3:29", @"m\:ss", null).TotalSeconds,	Genres = [rap, rock],		Data = "Lotus Juice - Mass Destruction.ogg",		Artist = LotusJuice,	CoverImage = lotusCover };
			var LotusSong2		= new Song() { Title = "Burn My Dread",				Duration = (uint)TimeSpan.ParseExact("4:38", @"m\:ss", null).TotalSeconds,	Genres = [rap],				Data = "Lotus Juice - Burn My Dread.ogg",			Artist = LotusJuice,	CoverImage = lotusCover };

			var DuaSong1		= new Song() { Title = "Levitating",				Duration = (uint)TimeSpan.ParseExact("3:41", @"m\:ss", null).TotalSeconds,	Genres = [pop],				Data = "Dua Lipa - Levitating.ogg",					Artist = DuaLipa,		CoverImage = duaCover };
			var DuaSong2		= new Song() { Title = "Don't Start Now",			Duration = (uint)TimeSpan.ParseExact("3:23", @"m\:ss", null).TotalSeconds,	Genres = [pop],				Data = "Dua Lipa - Dont Start Now.ogg",				Artist = DuaLipa,		CoverImage = duaCover };

			var SkynyrdSong1	= new Song() { Title = "Free Bird",					Duration = (uint)TimeSpan.ParseExact("9:10", @"m\:ss", null).TotalSeconds,	Genres = [rock],			Data = "Lynyrd Skynyrd - Free Bird.ogg",			Artist = LynyrdSkynyrd,	CoverImage = lynyrdCover };
			var SkynyrdSong2	= new Song() { Title = "Sweet Home Alabama",		Duration = (uint)TimeSpan.ParseExact("4:44", @"m\:ss", null).TotalSeconds,	Genres = [rock],			Data = "Lynyrd Skynyrd - Sweet Home Alabama.ogg",	Artist = LynyrdSkynyrd,	CoverImage = lynyrdCover };

			var FleetwoodSong1	= new Song() { Title = "Dreams",					Duration = (uint)TimeSpan.ParseExact("4:18", @"m\:ss", null).TotalSeconds,	Genres = [rock, pop],		Data = "Fleetwood Mac - Dreams.ogg",				Artist = FleetwoodMac,	CoverImage = fleetwoodCover };
			var FleetwoodSong2	= new Song() { Title = "Go Your Own Way",			Duration = (uint)TimeSpan.ParseExact("3:38", @"m\:ss", null).TotalSeconds,	Genres = [rock],			Data = "Fleetwood Mac - Go Your Own Way .ogg",		Artist = FleetwoodMac,	CoverImage = fleetwoodCover };

			var FloydSong1		= new Song() { Title = "Time",						Duration = (uint)TimeSpan.ParseExact("6:56", @"m\:ss", null).TotalSeconds,	Genres = [rock],			Data = "Pink Floyd Time.ogg",						Artist = PinkFloyd,		CoverImage = pinkCover };
			var FloydSong2		= new Song() { Title = "Money",						Duration = (uint)TimeSpan.ParseExact("6:22", @"m\:ss", null).TotalSeconds,	Genres = [rock],			Data = "Pink Floyd - Money.ogg",					Artist = PinkFloyd,		CoverImage = pinkCover };

			var SabatonSong1	= new Song() { Title = "Ghost Division",			Duration = (uint)TimeSpan.ParseExact("3:51", @"m\:ss", null).TotalSeconds,	Genres = [metal],			Data = "SABATON - Ghost Division.ogg",				Artist = Sabaton,		CoverImage = sabatonCover };
			var SabatonSong2	= new Song() { Title = "The Art of War",			Duration = (uint)TimeSpan.ParseExact("4:59", @"m\:ss", null).TotalSeconds,	Genres = [metal],			Data = "SABATON - The Art Of War.ogg",				Artist = Sabaton,		CoverImage = sabatonCover };


			ReolSong1.AlbumSong			= new AlbumSong() { Album = ReolAlbum,			Song = ReolSong1,		OrderIndex = 1 };
			ReolSong2.AlbumSong			= new AlbumSong() { Album = ReolAlbum,			Song = ReolSong2,		OrderIndex = 2 };
			LotusSong1.AlbumSong		= new AlbumSong() { Album = LotusJuiceAlbum,	Song = LotusSong1,		OrderIndex = 1 };
			LotusSong2.AlbumSong		= new AlbumSong() { Album = LotusJuiceAlbum,	Song = LotusSong2,		OrderIndex = 2 };
			DuaSong1.AlbumSong			= new AlbumSong() { Album = DuaLipaAlbum,		Song = DuaSong1,		OrderIndex = 1 };
			DuaSong2.AlbumSong			= new AlbumSong() { Album = DuaLipaAlbum,		Song = DuaSong2,		OrderIndex = 2 };
			SkynyrdSong1.AlbumSong		= new AlbumSong() { Album = LynyrdSkynyrdAlbum,	Song = SkynyrdSong1,	OrderIndex = 1 };
			SkynyrdSong2.AlbumSong		= new AlbumSong() { Album = LynyrdSkynyrdAlbum,	Song = SkynyrdSong2,	OrderIndex = 2 };
			FleetwoodSong1.AlbumSong	= new AlbumSong() { Album = FleetwoodMacAlbum,	Song = FleetwoodSong1,	OrderIndex = 1 };
			FleetwoodSong2.AlbumSong	= new AlbumSong() { Album = FleetwoodMacAlbum,	Song = FleetwoodSong2,	OrderIndex = 2 };
			FloydSong1.AlbumSong		= new AlbumSong() { Album = PinkFloydAlbum,		Song = FloydSong1,		OrderIndex = 1 };
			FloydSong2.AlbumSong		= new AlbumSong() { Album = PinkFloydAlbum,		Song = FloydSong2,		OrderIndex = 2 };
			SabatonSong1.AlbumSong		= new AlbumSong() { Album = SabatonAlbum,		Song = SabatonSong1,	OrderIndex = 1 };
			SabatonSong2.AlbumSong		= new AlbumSong() { Album = SabatonAlbum,		Song = SabatonSong2,	OrderIndex = 2 };


			await unit.BeginTransactionAsync();

			await unit.Songs.AddAsync(ReolSong1);
			await unit.Songs.AddAsync(ReolSong2);
			await unit.Songs.AddAsync(LotusSong1);
			await unit.Songs.AddAsync(LotusSong2);
			await unit.Songs.AddAsync(DuaSong1);
			await unit.Songs.AddAsync(DuaSong2);
			await unit.Songs.AddAsync(SkynyrdSong1);
			await unit.Songs.AddAsync(SkynyrdSong2);
			await unit.Songs.AddAsync(FleetwoodSong1);
			await unit.Songs.AddAsync(FleetwoodSong2);
			await unit.Songs.AddAsync(FloydSong1);
			await unit.Songs.AddAsync(FloydSong2);
			await unit.Songs.AddAsync(SabatonSong1);
			await unit.Songs.AddAsync(SabatonSong2);

			await unit.CommitTransactionAsync();
		}
		if (!unit.Listeners.GetAll().Any())
		{
			var destroyer99		= new ListenerCreateDto() { UserName = "destroyer99",	Email = "destroyer99@gmail.com",	Password = "Password123!",		FirstName = "Fred",		LastName = "Stone",		Bio = "I love music",			DisplayName = "Fred the Destroyer",	ProfilePictureId = user1Pfp.Id };
			var rockstar		= new ListenerCreateDto() { UserName = "rockstar",		Email = "rockstar@gmail.com",		Password = "Passw@ord123!",		FirstName = "John",		LastName = "Doe",		Bio = "I love rock music",		DisplayName = "Rockstar",			ProfilePictureId = user2Pfp.Id };
			var jazzlover		= new ListenerCreateDto() { UserName = "jazzlover",		Email = "jazzlover@gmail.com",		Password = "PASDassword123!",	FirstName = "Jane",		LastName = "Smith",		Bio = "I love jazz music",		DisplayName = "Jazz Lover",			ProfilePictureId = user3Pfp.Id };
			var classicalfan	= new ListenerCreateDto() { UserName = "classicalfan",	Email = "classicalfan@gmail.com",	Password = "Pass++_+_word123!",	FirstName = "Alice",	LastName = "Johnson",	Bio = "I love classical music",	DisplayName = "Classical Fan",		ProfilePictureId = user4Pfp.Id };

			await unit.BeginTransactionAsync();

			await unit.Listeners.Register(destroyer99);
			await unit.Listeners.Register(rockstar);
			await unit.Listeners.Register(jazzlover);
			await unit.Listeners.Register(classicalfan);

			await unit.CommitTransactionAsync();
		}
		if(!unit.Distributors.GetAll().Any())
		{
			var dto = new DistributorCreateDto() { UserName = "deestreebyutorr", Email = "distri@utor", Password = "DistroBistro123!", DisplayName = "Distributorotery" };
			await unit.BeginTransactionAsync();
			var distributor = await unit.Distributors.Register(dto);
			distributor.IsApproved = true;
			distributor.Artists.AddRange([Reol, LotusJuice, DuaLipa, LynyrdSkynyrd, FleetwoodMac, PinkFloyd, Sabaton]);
			await unit.CommitTransactionAsync();
		}
		if (!unit.Admins.GetAll().Any())
		{
			//TODO: Seed an admin
		}
	}
}

public class FileHelper
{
	public static ImageFile CreateImageFile(string fileName)
	{
		var stream = File.OpenRead(fileName);
		var contentType = Path.GetExtension(fileName) switch
		{
			".png" => "image/png",
			".jpeg" => "image/jpeg",
			".jpg" => "image/jpeg",
			_ => throw new Exception("can't read file extension")
		};
		using (MemoryStream ms = new MemoryStream())
		{
			stream.CopyTo(ms);

			return new ImageFile()
			{
				Data = ms.ToArray(),
				ContentType = contentType
			};
		}
	}
}