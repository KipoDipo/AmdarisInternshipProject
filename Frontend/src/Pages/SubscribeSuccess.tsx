import { Box, Button, Stack, Typography } from "@mui/material";
import { theme } from "../Styling/Theme";
import { Link } from "react-router-dom";
import { useEffect } from "react";
import { fetcher } from "../Fetcher";
import { useSetListener } from "../Contexts/Listener/UseSetListener";

export default function Page() {

    const setListener = useSetListener();

    useEffect(() => { // TODO: This should be done by a webhook
        fetcher.post('Payment/subscribe')
            .then(() => setListener!(prev => ({ ...prev!, isSubscribed: true })));
    }, [setListener])
    
    return (
        <Box width='100%' height='70%' display='flex' justifyContent='center' alignItems='center'>
            <Stack alignItems='center' padding={5} gap={6} bgcolor={theme.palette.secondary.dark} borderRadius={theme.shape.borderRadius}>
                <Typography variant='h3'>Subscribed successfully!</Typography>
                <Typography variant='h5'>{`Enjoy the premium perks Hi-Five has to offer :)`}</Typography>
                <Stack gap={3}>
                    <Button variant='contained' component={Link} to='/'>Go Home</Button>
                </Stack>
            </Stack>
        </Box>
    )
}