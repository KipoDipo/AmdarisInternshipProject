import { useEffect, useState } from "react";
import { useNotification } from "../Contexts/Snackbar/UseNotification";
import { fetcher } from "../Fetcher";
import { Box, Stack, Typography } from "@mui/material";
import { Listener } from "../Models/Listener";
import { useParams } from "react-router-dom";
import ListenerComponent from "../Components/ListenerComponent";

export default function Page() {
    const { id } = useParams();

    const [listeners, setListeners] = useState<Listener[]>([]);

    const notify = useNotification();

    useEffect(() => {
        fetcher.get(`/Listener/following-listeners/${id}`)
            .then(response => setListeners(response.data))
            .catch(error => notify({ message: error, severity: 'error' }))
    }, [notify, id]);

    return (
        <Stack margin={3} display='flex' justifyContent='center' gap={3}>
            <Typography variant='h2' alignSelf='flex-start' textAlign='center'>Following Users</Typography>
            <Box gap={8} display='flex' flexWrap='wrap' justifyContent='center'>
                {
                    listeners.map(listener => {
                        return <ListenerComponent key={listener.id} listener={listener} />
                    })
                }
            </Box>
        </Stack>
    )
}