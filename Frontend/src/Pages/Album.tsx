import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Song } from "../Models/Song";
import { fetcher } from "../Fetcher";
import { Avatar, Box, Fab, Paper, Stack, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import { TimeFormat } from "../Utils/TimeFormat";
import PlayArrowRoundedIcon from '@mui/icons-material/PlayArrowRounded';
import { Album } from "../Models/Album";
import { useSetQueue } from "../Contexts/Queue/UseSetQueue";
import { CreateQueue } from "../Utils/QueueUtils";
import { useNotification } from "../Contexts/Snackbar/UseNotification";
import FetchImage from "../Utils/FetchImage";
import { Shuffled } from "../Utils/Shuffle";
import { useListener } from "../Contexts/Listener/UseListener";

export default function Page() {
    const { id } = useParams();

    const [album, setAlbum] = useState<Album>()
    const [songs, setSongs] = useState<Song[]>()

    const listener = useListener();

    const setQueue = useSetQueue();

    const notify = useNotification();

    useEffect(() => {
        if (!id)
            return;

        fetcher.get(`/Album/details/${id}`)
            .then((response) => setAlbum(response.data))
            .catch(error => notify({ message: error, severity: 'error' }))


        fetcher.get(`/Song/album/${id}`)
            .then((response) => setSongs(response.data))
            .catch(error => notify({ message: error, severity: 'error' }))

    }, [id, notify])


    function play() {
        if (!songs)
            return;

        notify({message: "Queuing Album..."});
        setQueue(CreateQueue(listener?.isSubscribed ? songs : Shuffled(songs)));
    }

    return (
        <Stack gap={3} margin={3} direction='row' justifyContent='space-evenly' alignItems='center' height='80%'>
            <Stack gap={2} alignItems='center'>
                {
                    album &&
                    <Avatar src={FetchImage(album.coverImageId)} variant='rounded' sx={{ width: 200, height: 200 }} />
                }
                <Typography variant='h5'>{album?.title}</Typography>
                <Typography variant='body1' width='200px' textAlign='center'>{album?.description}</Typography>
                <Fab centerRipple onClick={play}>
                    {<PlayArrowRoundedIcon fontSize='large' />}
                </Fab>
            </Stack>

            <Stack gap={2} alignItems='center'>
                <Stack>
                    <TableContainer component={Paper}>
                        <Table sx={{ width: '40vw' }}>
                            <TableHead>
                                <TableRow>
                                    <TableCell align='left'>Title</TableCell>
                                    <TableCell align='right'>Length</TableCell>
                                </TableRow>
                            </TableHead>
                        </Table>
                    </TableContainer>
                    <Box sx={{ maxHeight: '70vh', overflowY: 'auto' }}>
                        <TableContainer component={Paper}>
                            <Table sx={{ width: '40vw' }}>
                                <TableBody>
                                    {
                                        songs?.map((song, index) => {
                                            return (
                                                <TableRow key={index} onClick={() => setQueue(CreateQueue([song]))} sx={{ cursor: 'pointer' }}>
                                                    <TableCell align='left'>
                                                        <Stack direction='row' alignItems='center' gap={3} >
                                                            <Avatar variant='rounded' src={FetchImage(song.coverImageId)}></Avatar>
                                                            <Stack>
                                                                <Typography variant='body1'>{song.title}</Typography>
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
            </Stack>
        </Stack>
    )
}