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
        <Stack margin={3} display='flex' justifyContent='center' gap={3}>
            <Typography variant='h2' alignSelf='flex-start' textAlign='center'>Following Artists</Typography>
            <Box gap={8} display='flex' flexWrap='wrap' justifyContent='center'>
                {
                    artists.map(artist => {
                        return <ArtistComponent key={artist.id} artist={artist} />
                    })
                }
            </Box>
        </Stack>
    )
}