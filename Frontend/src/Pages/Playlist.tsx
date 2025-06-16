import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Playlist } from "../Models/Playlist";
import { Song } from "../Models/Song";
import { fetcher } from "../Fetcher";
import { Avatar, Box, Fab, Paper, Stack, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import { TimeFormat } from "../Utils/TimeFormat";
import { useSetQueue } from "../Contexts/Queue/UseSetQueue";
import { CreateQueue } from "../Utils/QueueUtils";
import { useNotification } from "../Contexts/Snackbar/UseNotification";
import FetchImage from "../Utils/FetchImage";
import { Shuffled } from "../Utils/Shuffle";
import { useListener } from "../Contexts/Listener/UseListener";

import PlayArrowRoundedIcon from '@mui/icons-material/PlayArrowRounded';
import ShuffleRoundedIcon from '@mui/icons-material/ShuffleRounded';
import LinkRoundedIcon from '@mui/icons-material/LinkRounded';


export default function Page() {
    const { id } = useParams();

    const [playlist, setPlaylist] = useState<Playlist>()
    const [songs, setSongs] = useState<Song[]>()

    const setQueue = useSetQueue();
    const listener = useListener();
    const notify = useNotification();
    const navigate = useNavigate();

    useEffect(() => {
        if (!id)
            return;

        fetcher.get(`/Playlist/details/${id}`)
            .then((response) => setPlaylist(response.data))
            .catch(error => notify({ message: error, severity: 'error' }))

        fetcher.get(`/Song/playlist/${id}`)
            .then((response) => setSongs(response.data))
            .catch(error => notify({ message: error, severity: 'error' }))
    }, [id, notify])

    function play() {
        if (!songs)
            return;

        notify({ message: "Queuing Playlist..." });
        setQueue(CreateQueue(listener?.isSubscribed ? songs : Shuffled(songs)));
        navigate('/queue')
    }

    function shuffle() {
        if (!songs)
            return;

        notify({ message: "Shuffling Playlist..." });
        setQueue(CreateQueue(Shuffled(songs)));
        navigate('/queue')
    }

    function copy() {
        if (!songs)
            return;

        notify({ message: "Copied link..." });
        navigator.clipboard.writeText(window.location.href)
    }

    return (
        playlist &&
        <Stack gap={3} margin={3} direction='row' justifyContent='space-evenly' alignItems='center' height='80%'>

            <Stack gap={2} alignItems='center'>
                <Avatar src={FetchImage(playlist.thumbnailId)} variant='rounded' sx={{ width: 200, height: 200 }} />
                <Typography variant='h5'>{playlist.title}</Typography>
                <Typography variant='body1' width='200px' textAlign='center'>{playlist.description}</Typography>
                <Stack direction='row' gap={3} alignItems='center'>
                    <Fab centerRipple onClick={shuffle} sx={{ width: 40, height: 40 }}>
                        {<ShuffleRoundedIcon fontSize='medium' />}
                    </Fab>
                    <Fab centerRipple onClick={play} disabled={!(songs && songs.length > 0) && !listener?.isSubscribed}>
                        {<PlayArrowRoundedIcon fontSize='large' />}
                    </Fab>
                    <Fab centerRipple onClick={copy} sx={{ width: 40, height: 40 }}>
                        {<LinkRoundedIcon fontSize='medium' />}
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
                            <Typography variant='h4'>{"Add some songs first"}</Typography>
                        </Stack>
                }
            </Stack>
        </Stack>
    )
}