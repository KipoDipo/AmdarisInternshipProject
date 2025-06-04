import { Box, Button, Stack, Typography } from "@mui/material";
import { theme } from "../Styling/Theme";
import { Link } from "react-router-dom";

export default function Page() {
    return (
        <Box width='100%' height='70%' display='flex' justifyContent='center' alignItems='center'>
            <Stack alignItems='center' padding={5} gap={6} bgcolor={theme.palette.secondary.dark} borderRadius={theme.shape.borderRadius}>
                <Typography variant='h3'>Subscription failed...</Typography>
                <Typography variant='h5'>Sorry, there was error trying to process your payment, please try again.</Typography>
                <Stack gap={3}>
                    <Button variant='contained' component={Link} to='/subscribe'>Try again</Button>
                    <Button variant='outlined' component={Link} to='/'>Go Home</Button>
                </Stack>
            </Stack>
        </Box>
    )
}