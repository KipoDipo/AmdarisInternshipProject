import { useEffect, useState } from "react";
import { Song } from "../Models/Song";
import { fetcher } from "../Fetcher";
import { Avatar, Box, Fab, Paper, Stack, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import { TimeFormat } from "../Utils/TimeFormat";
import { ThumbUpRounded } from "@mui/icons-material";
import { useSetQueue } from "../Contexts/Queue/UseSetQueue";
import { CreateQueue } from "../Utils/QueueUtils";
import { useNotification } from "../Contexts/Snackbar/UseNotification";
import FetchImage from "../Utils/FetchImage";
import { useListener } from "../Contexts/Listener/UseListener";
import { Shuffled } from "../Utils/Shuffle";
import { useNavigate } from "react-router-dom";

import PlayArrowRoundedIcon from '@mui/icons-material/PlayArrowRounded';
import ShuffleRoundedIcon from '@mui/icons-material/ShuffleRounded';


export default function Page() {

    const [songs, setSongs] = useState<Song[]>()

    const listener = useListener();

    const setQueue = useSetQueue();

    const notify = useNotification();

    const navigate = useNavigate();

    useEffect(() => {
        fetcher.get(`/Song/my-liked`)
            .then((response) => setSongs(response.data.reverse()))
            .catch((error) => notify({ message: error, severity: 'error', duration: 10000 }))
    }, [notify])

    function play() {
        if (!songs)
            return;

        notify({ message: "Queuing Liked Songs..." });
        setQueue(CreateQueue(listener?.isSubscribed ? songs : Shuffled(songs)));
        navigate('/queue')
    }

    function shuffle() {
        if (!songs)
            return;

        notify({ message: "Shuffling Liked Songs..." });
        setQueue(CreateQueue(Shuffled(songs)));
        navigate('/queue')
    }

    return (
        songs &&
        <Stack gap={3} margin={3} direction='row' justifyContent='space-evenly' alignItems='center' height='80%' >
            <Stack gap={2} alignItems='center'>
                <Avatar variant='rounded' sx={{ width: 200, height: 200 }}>
                    <ThumbUpRounded sx={{ fontSize: 80 }} />
                </Avatar>
                <Typography variant='h5'>{'Your liked songs'}</Typography>
                <Typography variant='body1' width='200px' textAlign='center'>{'Songs that you like will appear here'}</Typography>
                <Stack direction='row' gap={3} alignItems='center'>
                    <Fab centerRipple onClick={shuffle} sx={{width: 40, height: 40}}>
                        {<ShuffleRoundedIcon fontSize='medium' />}
                    </Fab>
                    <Fab centerRipple onClick={play} disabled={!listener?.isSubscribed}>
                        {<PlayArrowRoundedIcon fontSize='large' />}
                    </Fab>
                </Stack>
            </Stack>

            <Stack gap={2} alignItems='center'>
                {
                    songs && songs.length > 0 ?
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

                        :

                        <Stack alignItems='center' gap={3}>
                            <Typography variant='h3'>{"Nothing here :("}</Typography>
                            <Typography variant='h4'>{"Like some songs first"}</Typography>
                        </Stack>
                }
            </Stack>
        </Stack>
    )
}