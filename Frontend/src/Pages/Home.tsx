import { Stack } from "@mui/material";

import { useEffect, useState } from "react";

import { Song } from "../Models/Song";
import { baseURL, fetcher } from "../Fetcher";
import SongCategory from "../Components/SongCategory";
import { useNotification } from "../Contexts/Snackbar/UseNotification";
import { Artist } from "../Models/Artist";

function Home() {
    const [history, setHistory] = useState<Song[]>([]);
    const [artists, setArtists] = useState<(Artist & {songs: Song[]})[]>([]);

    const notify = useNotification();

    useEffect(() => {
        fetcher.get(`Song/my-history/?count=${24}`)
            .then((response) => {
                const songs: Song[] = response.data;
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
    }, [notify])

    return (
        <Stack margin={3}>
            {/* <SongCategory name="Songs in the DB" songs={songs} /> */}
            {
                history.length > 0 &&
                <SongCategory name="Listen again" songs={history} />
            }
            {
                artists.map((artist) => {
                    return <SongCategory name={artist.displayName} imageUrl={`${baseURL}Image/${artist.profilePictureId}`} to={`Artist/${artist.id}`} songs={artist.songs}/>
                })
            }

        </Stack>
    );
}

export default Home;