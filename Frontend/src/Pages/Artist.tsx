import { useParams } from "react-router-dom"
import { ArtistDetails } from "../Models/ArtistDetails";
import { useEffect, useState } from "react";
import { fetcher, fetchPaged } from "../Fetcher";
import { Avatar, Box, Button, Stack, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
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
import { useListener } from "../Contexts/Listener/UseListener";



export default function Page() {
    const { id } = useParams();

    const [artist, setArtist] = useState<ArtistDetails>();
    const [albums, setAlbums] = useState<Album[] | undefined>();
    const [songs, setSongs] = useState<Song[]>([]);
    const [isFollowing, setIsFollowing] = useState<boolean | null>(null);

    const [user, setUser] = useState<ListenerDetails>();

    const [page, setPage] = useState(1)
    const [hasMore, setHasMore] = useState(true)

    const listener = useListener();

    useEffect(() => {
        setSongs([]);
    }, [id])

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

        fetcher.get(`Listener/following-artists/${listener?.id}`)
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
    }, [user, id, listener, notify])

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
    }, [artist, notify])

    useEffect(() => {
        if (!artist)
            return;

        fetchPaged(`/Song/artist/${artist?.id}`, page, 5)
            .then((response) => {
                fetchPaged(`/Song/artist/${artist?.id}`, page + 1, 5)
                    .then(response => setHasMore(response.length != 0))
                setSongs(last => [...last, ...response])
            })
            .catch(error => notify({ message: error, severity: 'error' }))
    }, [artist, notify, page])

    const setQueue = useSetQueue();

    return (
        artist && albums && songs ?
            <Stack margin={3} gap={6}>
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
                <Stack padding={3} gap={1} marginTop={-6}>
                    <Typography variant='h4'>Info</Typography>
                    <Typography variant='body1'>{artist?.bio}</Typography>
                </Stack>
                <AlbumCategory albums={albums} name="Albums" />
                <Stack gap={3}>
                    <Typography variant='h3'>Discography</Typography>
                    <TableContainer>
                        <Table>
                            <TableHead>
                                <TableRow>
                                    <TableCell align='left'>Title</TableCell>
                                    <TableCell align='left'>Album</TableCell>
                                    <TableCell align='right'>Length</TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {
                                    songs?.map((song, index) => {
                                        return (
                                            <TableRow key={index} onClick={() => setQueue(CreateQueue([song]))} sx={{ cursor: 'pointer' }}>
                                                <TableCell align='left'>
                                                    <Stack direction='row' alignItems='center' gap={3} >
                                                        <Avatar variant='rounded' src={FetchImage(song.coverImageId)}></Avatar>
                                                        <Typography variant='body2'>
                                                            {song.title}
                                                        </Typography>
                                                    </Stack>
                                                </TableCell>
                                                <TableCell align='left'>{song.album}</TableCell>
                                                <TableCell align='right'>{TimeFormat(+song.duration)}</TableCell>
                                            </TableRow>
                                        )
                                    })
                                }
                            </TableBody>
                        </Table>
                    </TableContainer>
                </Stack>
                {
                    hasMore &&
                    <Button variant='contained' onClick={() => setPage(last => last + 1)}>Load more</Button>
                }
            </Stack>
            :
            <ArtistSkeleton />

    )
}