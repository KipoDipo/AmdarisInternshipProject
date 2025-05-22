import { Stack } from "@mui/material";

import { useEffect, useState } from "react";

import { Song } from "../Models/Song";
import { fetcher } from "../Fetcher";
import SongCategory from "../Components/SongCategory";
import { useNotification } from "../Contexts/Snackbar/UseNotification";

function Home() {
    const [songs, setSongs] = useState<Song[]>([]);

    const notify = useNotification();

    useEffect(() => {
        fetcher.get('Song')
            .then((response) => {
                setSongs(response.data);
            })
            .catch(error => notify({ message: error, severity: 'error' }))
    }, [notify])

    return (
        <Stack margin={3}>
            <SongCategory name="Songs in the DB" songs={songs} />
        </Stack>
    );
}

export default Home;