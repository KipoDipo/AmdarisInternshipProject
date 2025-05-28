import { useEffect, useState } from "react";
import { useNotification } from "../Contexts/Snackbar/UseNotification";
import { Artist } from "../Models/Artist";
import { fetcher } from "../Fetcher";
import { Box, Stack, Typography } from "@mui/material";
import ArtistComponent from "../Components/ArtistComponent";

export default function Page() {
    const [artists, setArtists] = useState<Artist[]>([]);

    const notify = useNotification();

    useEffect(() => {
        fetcher.get("/Listener/following-artists")
            .then(response => setArtists(response.data))
            .catch(error => notify({ message: error, severity: 'error' }))
    }, [notify]);

    return (
        <Stack width='100%' display='flex' justifyContent='center'>
            <Typography variant='h2' alignSelf='flex-start' textAlign='center' margin={3}>Following Artists</Typography>
            <Box margin={3} gap={8} display='flex' flexWrap='wrap' justifyContent='center'>
                {
                    artists.map(artist => {
                        return <ArtistComponent key={artist.id} artist={artist} />
                    })
                }
            </Box>
        </Stack>
    )
}