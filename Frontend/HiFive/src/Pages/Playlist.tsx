import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Playlist } from "../Models/Playlist";
import { Song } from "../Models/Song";
import { baseURL, fetcher } from "../Fetcher";
import { Avatar, Fab, Paper, Stack, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import { useSetSong } from "../Contexts/UseSetSong";
import { TimeFormat } from "../Utils/TimeFormat";
import PlayArrowRoundedIcon from '@mui/icons-material/PlayArrowRounded';

export default function Page() {
    const { id } = useParams();

    const [playlist, setPlaylist] = useState<Playlist>()
    const [songs, setSongs] = useState<Song[]>()

    const setSong = useSetSong();

    useEffect(() => {
        if (!id)
            return;

        fetcher.get(`/Playlist/details/${id}`)
            .then((response) => setPlaylist(response.data));

        fetcher.get(`/Song/playlist/${id}`)
            .then((response) => setSongs(response.data));
    }, [id])

    return (
        <Stack gap={3} margin={3} direction='row' justifyContent='space-evenly' alignItems='center' height='80%'>
            <Stack gap={2} alignItems='center'>
                {
                    playlist &&
                    <Avatar src={`${baseURL}Image/${playlist?.thumbnailId}`} variant='rounded' sx={{ width: 200, height: 200 }} />
                }
                <Typography variant='h5'>{playlist?.title}</Typography>
                <Typography variant='body1' width='200px' textAlign='center'>{playlist?.description}</Typography>
                <Fab centerRipple disabled={!(songs && songs.length > 0)}>
                    {<PlayArrowRoundedIcon fontSize='large'/>}
                </Fab>
            </Stack>

            <Stack gap={2} alignItems='center'>
                {
                    songs && songs.length > 0 ?
                    <TableContainer component={Paper}>
                        <Table sx={{ width: '40vw' }}>
                            <TableHead>
                                <TableRow>
                                    <TableCell>Title</TableCell>
                                    <TableCell>Length</TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {
                                    songs?.map((song, index) => {
                                        return (
                                            <TableRow key={index} onClick={() => setSong(song)} sx={{ cursor: 'pointer' }}>
                                                <TableCell>
                                                    <Stack direction='row' alignItems='center' gap={3} >
                                                        <Avatar variant='rounded' src={`${baseURL}Image/${song.coverImageId}`}></Avatar>
                                                        <Typography variant='body2'>
                                                            {song.title}
                                                        </Typography>
                                                    </Stack>
                                                </TableCell>
                                                <TableCell>{song.album}</TableCell>
                                                <TableCell>{TimeFormat(+song.duration)}</TableCell>
                                            </TableRow>
                                        )
                                    })
                                }
                            </TableBody>
                        </Table>
                    </TableContainer>

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