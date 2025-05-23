namespace HiFive.Application.AwardSystem;
public static class Conditions
{
	public static class Profile
	{
		public const string Registered = "registered";
		public const string UploadedPfp = "uploaded_pfp";
	}
	
	public static class Playlists
	{
		public const string Created3 = "created_3_playlists";
		public const string Created10 = "created_10_playlists";
		public const string Created15 = "created_15_playlists";
	}

	public static class Artists
	{
		public const string Followed5 = "followed_5_artists";
		public const string Followed10 = "followed_10_artists";
		public const string Followed20 = "followed_20_artists";
	}

	public static class Artist
	{
		public const string Listened5Songs = "listened_artist_5_songs";
		public const string Listened10Songs = "listened_artist_10_songs";
		public const string ListenedAllSongs = "listened_artist_all_songs";
	}

	public static class Listener
	{
		public const string Added1Listener = "added_1_listener";
		public const string Added5Listeners = "added_5_listeners";
	}

	public static class Application
	{
		public const string DailyStreak5Days = "daily_streak_5_days";
		public const string DailyStreak30Days = "daily_streak_30_days";
	}
}
