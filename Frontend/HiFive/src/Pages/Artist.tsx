import { useParams } from "react-router-dom"
import { ArtistDetails } from "../Models/ArtistDetails";
import { useEffect, useState } from "react";
import { baseURL, fetcher } from "../Fetcher";
import { Avatar, Box, Paper, Stack, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import { Song } from "../Models/Song";
import { Album } from "../Models/Album";

import { useSetSong } from "../Contexts/UseSetSong";
import AlbumCategory from "../Components/AlbumCategory";

function timeFormat(seconds: number) {
    const min = Math.floor(seconds / 60);
    const sec = seconds % 60;

    return `${min}:${String(sec).padStart(2, '0')}`
}

export default function Page() {
    const { id } = useParams();

    const [artist, setArtist] = useState<ArtistDetails>();
    const [albums, setAlbums] = useState<Album[]>();
    const [songs, setSongs] = useState<Song[]>();

    useEffect(() => {
        if (!id)
            return;

        fetcher.get(`/Artist/details/${id}`)
            .then((response) => setArtist(response.data));
    }, [id])

    useEffect(() => {
        if (!artist)
            return;

        fetcher.get(`/Album/artist/${artist?.id}`)
            .then((response) => setAlbums(response.data));

        fetcher.get(`/Song/artist/${artist?.id}`)
            .then((response) => setSongs(response.data))
    }, [artist])

    const setSong = useSetSong();

    return (
        <Stack margin={3}>
            <Stack direction='row' justifyContent='space-between' width='80vw' gap={3}>
                <Stack gap={3}>
                    <Stack direction='row' alignItems='center' gap={3}>
                        {
                            artist ?
                                <Avatar src={`${baseURL}Image/${artist?.profilePictureId}`} sx={{ width: '400px', height: `400px` }}></Avatar>
                                :
                                <Box sx={{ width: '400px', height: `400px` }}></Box>
                        }
                        <Stack>
                            <Typography variant='h2'>{artist?.displayName}</Typography>
                        </Stack>
                    </Stack>
                    <Stack>
                        <Typography variant='h3'>Bio</Typography>
                        <Typography variant='body1'>{artist?.bio}</Typography>
                    </Stack>
                    <Stack gap={3}>
                        <AlbumCategory albums={albums} name="Albums" />
                    </Stack>
                    <Stack gap={3}>
                        <Typography variant='h3'>Discography</Typography>
                        <TableContainer component={Paper}>
                            <Table sx={{ width: '80vw' }}>
                                <TableHead>
                                    <TableRow>
                                        <TableCell>Title</TableCell>
                                        <TableCell>Album</TableCell>
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
                                                    <TableCell>{timeFormat(+song.duration)}</TableCell>
                                                </TableRow>
                                            )
                                        })
                                    }
                                </TableBody>
                            </Table>
                        </TableContainer>
                    </Stack>
                </Stack>
            </Stack>
        </Stack>

    )
}