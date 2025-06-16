import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Song } from "../Models/Song";
import { fetcher } from "../Fetcher";
import { Avatar, Box, Fab, Paper, Stack, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import { TimeFormat } from "../Utils/TimeFormat";
import { Album } from "../Models/Album";
import { useSetQueue } from "../Contexts/Queue/UseSetQueue";
import { CreateQueue } from "../Utils/QueueUtils";
import { useNotification } from "../Contexts/Snackbar/UseNotification";
import FetchImage from "../Utils/FetchImage";
import { Shuffled } from "../Utils/Shuffle";
import { useListener } from "../Contexts/Listener/UseListener";
import { Artist } from "../Models/Artist";

import PlayArrowRoundedIcon from '@mui/icons-material/PlayArrowRounded';
import ShuffleRoundedIcon from '@mui/icons-material/ShuffleRounded';
import LinkRoundedIcon from '@mui/icons-material/LinkRounded';

export default function Page() {
    const { id } = useParams();

    const [album, setAlbum] = useState<Album>()
    const [artist, setArtist] = useState<Artist>()
    const [songs, setSongs] = useState<Song[]>()

    const listener = useListener();

    const setQueue = useSetQueue();

    const notify = useNotification();

    const navigate = useNavigate();

    useEffect(() => {
        if (!id)
            return;

        fetcher.get(`/Album/details/${id}`)
            .then((response) => {
                fetcher.get(`/Artist/id/${response.data.artistId}`)
                    .then((response) => setArtist(response.data))
                setAlbum(response.data)
            })
            .catch(error => notify({ message: error, severity: 'error' }))


        fetcher.get(`/Song/album/${id}`)
            .then((response) => setSongs(response.data))
            .catch(error => notify({ message: error, severity: 'error' }))

    }, [id, notify])


    function play() {
        if (!songs)
            return;

        notify({ message: "Queuing Album..." });
        setQueue(CreateQueue(listener?.isSubscribed ? songs : Shuffled(songs)));
        navigate('/queue')
    }

    function shuffle() {
        if (!songs)
            return;

        notify({ message: "Shuffling Album..." });
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
        <Stack gap={3} margin={3} direction='row' justifyContent='space-evenly' alignItems='center' height='80%'>
            <Stack gap={2} alignItems='center'>
                {
                    album &&
                    <Avatar src={FetchImage(album.coverImageId)} variant='rounded' sx={{ width: 200, height: 200 }} />
                }
                <Typography variant='h5'>{album?.title}</Typography>
                <Typography variant='h6' marginTop={-2}>{artist?.displayName}</Typography>
                <Typography variant='body1' width='200px' textAlign='center'>{album?.description}</Typography>
                <Stack direction='row' gap={3} alignItems='center'>
                    <Fab centerRipple onClick={shuffle} sx={{width: 40, height: 40}}>
                        {<ShuffleRoundedIcon fontSize='medium' />}
                    </Fab>
                    <Fab centerRipple onClick={play} disabled={!listener?.isSubscribed}>
                        {<PlayArrowRoundedIcon fontSize='large' />}
                    </Fab>
                    <Fab centerRipple onClick={copy} sx={{width: 40, height: 40}}>
                        {<LinkRoundedIcon fontSize='medium' />}
                    </Fab>
                </Stack>
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