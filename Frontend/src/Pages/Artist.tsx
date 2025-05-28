import { useParams } from "react-router-dom"
import { ArtistDetails } from "../Models/ArtistDetails";
import { useEffect, useState } from "react";
import { fetcher } from "../Fetcher";
import { Avatar, Box, Button, Paper, Stack, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import { Song } from "../Models/Song";
import { Album } from "../Models/Album";

import AlbumCategory from "../Components/AlbumCategory";
import { TimeFormat } from "../Utils/TimeFormat";
import { textWidth } from "../Styling/Theme";
import { ListenerDetails } from "../Models/ListenerDetails";
import { Artist } from "../Models/Artist";
import { useSetQueue } from "../Contexts/Queue/UseSetQueue";
import { CreateQueue } from "../Utils/QueueUtils";
import { useNotification } from "../Contexts/Snackbar/UseNotification";
import FetchImage from "../Utils/FetchImage";
import ArtistSkeleton from "../Components/Skeletons/ArtistSkeleton";



export default function Page() {
    const { id } = useParams();

    const [artist, setArtist] = useState<ArtistDetails>();
    const [albums, setAlbums] = useState<Album[] | undefined>();
    const [songs, setSongs] = useState<Song[] | undefined>();
    const [isFollowing, setIsFollowing] = useState<boolean | null>(null);

    const [user, setUser] = useState<ListenerDetails>();

    async function handleFollow() {
        if (isFollowing)
            unfollow();
        else
            follow();
    }

    async function follow() {
        if (!artist)
            return;

        fetcher.post(`/Listener/follow-artist/${artist.id}`);
        setIsFollowing(true);
    }
    async function unfollow() {
        if (!artist)
            return;

        fetcher.post(`/Listener/unfollow-artist/${artist.id}`);
        setIsFollowing(false);
    }

    const notify = useNotification();

    useEffect(() => {
        fetcher.get('/Listener')
            .then((response) => setUser(response.data))
            .catch(error => notify({ message: error, severity: 'error' }))
    }, [notify])

    useEffect(() => {
        if (!user || !id)
            return;

        fetcher.get(`Listener/following-artists`)
            .then((response) => {
                const following: Artist[] = response.data;
                let found = false;
                following.forEach(a => {
                    if (a.id == id) {
                        found = true;
                        return;
                    }
                })
                setIsFollowing(found);
            })
            .catch(error => notify({ message: error, severity: 'error' }))
    }, [user, id, notify])

    useEffect(() => {
        if (!id)
            return;

        fetcher.get(`/Artist/details/${id}`)
            .then((response) => setArtist(response.data))
            .catch(error => notify({ message: error, severity: 'error' }))
    }, [id, notify])

    useEffect(() => {
        if (!artist)
            return;

        fetcher.get(`/Album/artist/${artist?.id}`)
            .then((response) => setAlbums(response.data ?? []))
            .catch(error => notify({ message: error, severity: 'error' }))


        fetcher.get(`/Song/artist/${artist?.id}`)
            .then((response) => setSongs(response.data ?? []))
            .catch(error => notify({ message: error, severity: 'error' }))

    }, [artist, notify])

    const setQueue = useSetQueue();

    return (
        artist && albums && songs ?
        <Stack margin={3}>
            <Stack direction='row' justifyContent='space-between' width='80vw' gap={3}>
                <Stack gap={3}>
                    <Stack direction='row' alignItems='center' gap={3}>
                        {
                            artist ?
                                <Avatar src={FetchImage(artist.profilePictureId)} sx={{ width: '400px', height: `400px` }}></Avatar>
                                :
                                <Box sx={{ width: '400px', height: `400px` }}></Box>
                        }
                        <Stack gap={3}>
                            <Typography variant='h2'>{artist?.displayName}</Typography>
                            <Button variant='contained' onClick={handleFollow} sx={{ width: textWidth }}>{isFollowing == null ? "..." : (isFollowing ? "Unfollow" : "Follow")}</Button>
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
                                                <TableRow key={index} onClick={() => setQueue(CreateQueue([song]))} sx={{ cursor: 'pointer' }}>
                                                    <TableCell>
                                                        <Stack direction='row' alignItems='center' gap={3} >
                                                            <Avatar variant='rounded' src={FetchImage(song.coverImageId)}></Avatar>
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
                    </Stack>
                </Stack>
            </Stack>
        </Stack>
        :
        <ArtistSkeleton />

    )
}