import { Stack } from "@mui/material";

import { useEffect, useState } from "react";

import { Song } from "../Models/Song";
import { fetcher, fetchPaged } from "../Fetcher";
import SongCategory from "../Components/SongCategory";
import { useNotification } from "../Contexts/Snackbar/UseNotification";
import { Artist } from "../Models/Artist";
import FetchImage from "../Utils/FetchImage";
import SongCategorySkeleton from "../Components/Skeletons/SongCategorySkeleton";
import { Shuffle } from "../Utils/Shuffle";

type ArtistWithSongs = Artist & {
    songs: Song[]
}

async function createArtistsWithSongs(artists: Artist[]): Promise<ArtistWithSongs[]> {
    const artistResponses = await Promise.all(
        artists.map(artist => fetcher.get(`Artist/id/${artist.id}`))
    );
    const fullArtists = artistResponses.map(res => res.data);

    const songsResponses = await Promise.all(
        fullArtists.map(artist => fetcher.get(`Song/artist/${artist.id}`))
    );

    const result: ArtistWithSongs[] = fullArtists.map((artist, index) => ({
        ...artist,
        songs: songsResponses[index].data
    }));

    return result;
}

function Home() {
    const [history, setHistory] = useState<Song[]>([]);
    const [curatedSongs, setCuratedSongs] = useState<Song[]>([]);
    const [curatedArtists, setCuratedArtists] = useState<ArtistWithSongs[]>([]);
    const [artists, setArtists] = useState<ArtistWithSongs[]>([]);

    const notify = useNotification();

    useEffect(() => {
        fetchPaged(`Song/my-history`, 1, 12)
            .then((response) => {
                const songs: Song[] = response;
                setHistory(songs.slice(0, 8));

                const artists = songs.map(song => song.artistId);
                const artistCount: { [key: string]: number } = {};
                for (const artist of artists) {
                    if (artistCount[artist]) {
                        artistCount[artist]++;
                    } else {
                        artistCount[artist] = 1;
                    }
                }

                for (const artist in artistCount) {
                    if (artistCount[artist] < 2) {
                        delete artistCount[artist];
                    }
                }

                const requests = Object.keys(artistCount).map(id => fetcher.get(`Artist/id/${id}`));
                Promise.all(requests)
                    .then(responses => {
                        const artists = responses.map(res => res.data);

                        const artistSongsRequests = artists.map(artist => fetcher.get(`Song/artist/${artist.id}`));
                        Promise.all(artistSongsRequests)
                            .then(songsResponses => {
                                const artistsWithSongs = artists.map((artist, index) => ({
                                    ...artist,
                                    songs: songsResponses[index].data
                                }));
                                setArtists(artistsWithSongs);
                            });
                    })
            })
            .catch(error => notify({ message: error, severity: 'error' }))

        fetcher.get(`Song/curated-songs`)
            .then(response => {
                const result: Song[] = response.data;
                Shuffle(result);
                setCuratedSongs(result);
            })
            .catch(error => notify({ message: error, severity: 'error' }))

        fetcher.get(`Artist/curated-artists`)
            .then(response => {
                const result: Artist[] = response.data;
                Shuffle(result);
                createArtistsWithSongs(result)
                    .then(artists => setCuratedArtists(artists))
            })
    }, [notify])

    return (
        <Stack margin={3} gap={3}>
            {
                history.length >= 5 ?
                    <SongCategory name="Listen again" songs={history} />
                    :
                    curatedSongs.length != 0 ?
                        <SongCategory name="Curated picks" songs={curatedSongs} />
                        :
                        <SongCategorySkeleton />
            }
            {
                artists.length >= 2 ?
                    artists.map((artist) => {
                        return <SongCategory name={artist.displayName} imageUrl={FetchImage(artist.profilePictureId)} to={`Artist/${artist.id}`} songs={artist.songs} />
                    }) :
                    curatedArtists.length != 0 ?
                        curatedArtists.map((artist) => {
                            return <SongCategory name={artist.displayName} imageUrl={FetchImage(artist.profilePictureId)} to={`Artist/${artist.id}`} songs={artist.songs} />
                        })
                        :
                        new Array(3).fill(0).map((_, index) => <SongCategorySkeleton key={index} />)

            }

        </Stack>
    );
}

export default Home;