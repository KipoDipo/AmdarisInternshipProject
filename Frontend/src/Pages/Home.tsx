import { Stack } from "@mui/material";

import { useEffect, useState } from "react";

import { Song } from "../Models/Song";
import { fetcher } from "../Fetcher";
import SongCategory from "../Components/SongCategory";
import { useNotification } from "../Contexts/Snackbar/UseNotification";
import { Artist } from "../Models/Artist";
import FetchImage from "../Utils/FetchImage";
import SongCategorySkeleton from "../Components/Skeletons/SongCategorySkeleton";

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
        <Stack margin={3} gap={3}>
            {
                history.length > 0 ?
                <SongCategory name="Listen again" songs={history} />
                :
                <SongCategorySkeleton />
            }
            {
                artists.length > 0 ?
                artists.map((artist) => {
                   return <SongCategory name={artist.displayName} imageUrl={FetchImage(artist.profilePictureId)} to={`Artist/${artist.id}`} songs={artist.songs}/>
                }) :
                new Array(3).fill(0).map((_, index) => <SongCategorySkeleton key={index} />)
            }

        </Stack>
    );
}

export default Home;