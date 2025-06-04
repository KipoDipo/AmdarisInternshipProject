import { Box, Button, Paper, Stack, Table, TableBody, TableCell, TableHead, TableRow } from "@mui/material";
import { loadStripe } from "@stripe/stripe-js";
import { fetcher } from "../Fetcher";
import { theme } from "../Styling/Theme";
import CancelIcon from '@mui/icons-material/Cancel';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';

const stripePromise = loadStripe('pk_test_51QbojsFzFkTiMdFL54Mm5S1JUjs7HqsHFB5Cs3NlPmO1KsRNuTFybhJZFQsIl4Wb2QsN6Pai7vuuVa4IVwCP3g3X00EcUna1w2');

const perks = [
    "Hi-Fi Audio Quality",
    "Ad-Free Music Listening",
    "Play songs in any order",
    "Download to listen offline",
    "Unlimited Titles",
    "Unlimited Badges"
]

export default function Page() {
    async function handleClick() {
        const stripe = await stripePromise;

        const response = await fetcher.post(`payment`);
        const session = response.data.id;

        await stripe?.redirectToCheckout({ sessionId: session });
    }

    return (
        <Box width='100%' display='flex' justifyContent='center' alignItems='center'>
            <Stack gap={12} width='50%' alignItems='center' padding={5}
            sx={{
                background: `linear-gradient(${theme.palette.primary.main},rgba(0, 0, 0, 0))`, 
                borderRadius: `${theme.shape.borderRadius * 8}px ${theme.shape.borderRadius * 8}px 0 0`
                }}>
                <Paper elevation={3} sx={{ width: '100%', borderRadius: theme.shape.borderRadius / 2, overflow: 'hidden' }}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>Perks</TableCell>
                                <TableCell align='center'>Free Plan</TableCell>
                                <TableCell align='center'>Premium</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {
                                perks.map(p => {
                                    return <TableRow>
                                        <TableCell sx={{ color: theme.palette.primary.main }}>{p}</TableCell>
                                        <TableCell align='center'><CancelIcon /></TableCell>
                                        <TableCell align='center' sx={{ color: theme.palette.primary.main }}><CheckCircleIcon /></TableCell>
                                    </TableRow>
                                })
                            }
                        </TableBody>
                    </Table>
                </Paper>
                <Button variant='contained' onClick={handleClick}>
                    Upgrade to Premium
                </Button>
            </Stack>
        </Box>
    )
}