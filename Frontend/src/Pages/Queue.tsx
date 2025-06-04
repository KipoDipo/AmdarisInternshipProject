import { Avatar, Box, Paper, Stack, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import { useQueue } from "../Contexts/Queue/UseQueue"
import { TimeFormat } from "../Utils/TimeFormat";
import { useSetQueue } from "../Contexts/Queue/UseSetQueue";
import { theme } from "../Styling/Theme";
import FetchImage from "../Utils/FetchImage";

export default function Page() {
    const queue = useQueue();
    const setQueue = useSetQueue();

    return (
        <Stack gap={3} margin={6} alignItems='center'>
            {
                queue && queue.songs.length > 0 ?
                    <>
                        <Typography variant='h2'>Current queue</Typography>
                        <Stack width='40vw'>
                            <TableContainer component={Paper}>
                                <Table>
                                    <TableHead>
                                        <TableRow>
                                            <TableCell align='left'>Title</TableCell>
                                            <TableCell align='right'>Length</TableCell>
                                        </TableRow>
                                    </TableHead>
                                </Table>
                            </TableContainer>
                            <Box sx={{ maxHeight: '60vh', overflowY: 'auto' }}>
                                <TableContainer component={Paper}>
                                    <Table>
                                        <TableBody>
                                            {
                                                queue?.songs.map((song, index) => {
                                                    return (
                                                        <TableRow key={index} onClick={() => setQueue({ ...queue, current: index })} sx={{
                                                            cursor: 'pointer',
                                                            background: index == queue.current ? theme.palette.primary.dark : theme.palette.secondary.main
                                                        }}>
                                                            <TableCell align='left'>
                                                                <Stack direction='row' alignItems='center' gap={3} >
                                                                    <Avatar variant='rounded' src={FetchImage(song.coverImageId)}></Avatar>
                                                                    <Stack>
                                                                        <Typography variant='body1'>{song.title}</Typography>
                                                                        <Typography variant='body2'>{song.artistName}</Typography>
                                                                    </Stack>
                                                                </Stack>
                                                            </TableCell>
                                                            <TableCell align='right'>{TimeFormat(+song.duration)}</TableCell>
                                                        </TableRow>
                                                    )
                                                })
                                            }
                                        </TableBody>
                                    </Table>
                                </TableContainer>
                            </Box>
                        </Stack>
                    </>
                    :
                    <>
                        <Typography variant='h2'>Nothing is playing currently</Typography>
                        <Typography variant='h4'>Go listen to some tunes</Typography>
                    </>
            }
        </Stack>

    )
}