create table Users (
	id int not null primary key,
	created_at datetime,
	updated_at datetime,
	is_deleted bit,
	deleted_at datetime,

	email varchar(64) not null,
	password_hash varchar(256) not null,
	username varchar(16) not null,
	nickname nvarchar(32),
	phone_number varchar(18),
);

create table Songs (
	id int not null primary key,
	title nvarchar(128) not null,
	duration time not null,
	release_date datetime,
	content varbinary(max) not null
);

create table Playlists (
	id int not null primary key,
	created_by int foreign key references Users(id)
);

create table PlaylistsSongs (
	playlist_id int,
	song_id int,

	primary key(song_id, playlist_id),
	foreign key (playlist_id) references Playlists(id),
	foreign key (song_id) references Songs(id),
);

create table Albums (
	id int not null primary key,
	title nvarchar(128),
	created_by int foreign key references Users(id), -- "Users" should be "Artists"; there is no such table at this moment
													 -- "Artists" and other tables will be made with TBH/TBT from EF 
	release_date datetime,
);

create table AlbumsSongs (
	album_id int,
	song_id int,

	primary key(song_id, album_id),
	foreign key (album_id) references Albums(id),
	foreign key (song_id) references Songs(id),
);
