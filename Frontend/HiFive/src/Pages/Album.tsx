import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Song } from "../Models/Song";
import { baseURL, fetcher } from "../Fetcher";
import { Avatar, Fab, Paper, Stack, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import { useSetSong } from "../Contexts/UseSetSong";
import { TimeFormat } from "../Utils/TimeFormat";
import PlayArrowRoundedIcon from '@mui/icons-material/PlayArrowRounded';
import { Album } from "../Models/Album";

export default function Page() {
    const { id } = useParams();

    const [album, setAlbum] = useState<Album>()
    const [songs, setSongs] = useState<Song[]>()

    const setSong = useSetSong();

    useEffect(() => {
        if (!id)
            return;

        fetcher.get(`/Album/details/${id}`)
            .then((response) => setAlbum(response.data));

        fetcher.get(`/Song/album/${id}`)
            .then((response) => setSongs(response.data));
    }, [id])

    return (
        <Stack gap={3} margin={3} direction='row' justifyContent='space-evenly' alignItems='center' height='80%'>
            <Stack gap={2} alignItems='center'>
                {
                    album &&
                    <Avatar src={`${baseURL}Image/${album?.coverImageId}`} variant='rounded' sx={{ width: 200, height: 200 }} />
                }
                <Typography variant='h5'>{album?.title}</Typography>
                <Typography variant='body1' width='200px' textAlign='center'>{album?.description}</Typography>
                <Fab centerRipple>
                    {<PlayArrowRoundedIcon fontSize='large'/>}
                </Fab>
            </Stack>

            <Stack gap={2} alignItems='center'>
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
                                            <TableCell>{TimeFormat(+song.duration)}</TableCell>
                                        </TableRow>
                                    )
                                })
                            }
                        </TableBody>
                    </Table>
                </TableContainer>
            </Stack>
        </Stack>
    )
}