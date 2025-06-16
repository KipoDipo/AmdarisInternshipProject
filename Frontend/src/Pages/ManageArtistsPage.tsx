import { Avatar, Box, Button, Dialog, Paper, Stack, Table, TableBody, TableCell, TableHead, TableRow, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { Artist } from "../Models/Artist";
import { fetcher, fetchPaged } from "../Fetcher";
import { theme } from "../Styling/Theme";
import FetchImage from "../Utils/FetchImage";
import { AddArtistToDistributor } from "./AddArtistToDistributor";
import { Song } from "../Models/Song";
import { Album } from "../Models/Album";
import { useForm } from "react-hook-form";
import { OptionalTextField } from "../Components/TextFields";
import { useNotification } from "../Contexts/Snackbar/UseNotification";
import AddSongPage from "./AddSong";
import AddAlbumPage from "./AddAlbum";

export default function Page() {
    const [artists, setArtists] = useState<Artist[]>();
    const [isAddingArtist, setIsAddingArtist] = useState(false);
    const [managedArtist, setManagedArtist] = useState<Artist | null>(null)

    useEffect(() => {
        fetcher.get('Distributor/get-artists')
            .then(response => setArtists(response.data))
    }, [isAddingArtist])

    function handleRemove(id: string) {
        fetcher.post(`Distributor/remove-artist/${id}`)
            .then(() => setArtists(artists!.filter(artist => artist.id !== id)))
    }

    return (
        <Box width='100%' display='flex' justifyContent='center' alignItems='center'>
            <Paper sx={{ width: '50%', borderRadius: theme.shape.borderRadius / 2, overflow: 'hidden' }}>
                <Table>
                    <TableHead>
                        <TableCell>Artist</TableCell>
                        <TableCell align='right'>Actions</TableCell>
                    </TableHead>

                    <TableBody>
                        <TableRow>
                            <TableCell />
                            <TableCell align='right' width='100%'>
                                <Button variant='contained' onClick={() => setIsAddingArtist(true)}>Add an artist</Button>
                            </TableCell>
                        </TableRow>
                        {
                            artists?.map(artist => {
                                return (
                                    <TableRow>
                                        <TableCell width='100%'>
                                            <Stack alignItems='center' gap={2} direction='row'>
                                                <Avatar src={FetchImage(artist.profilePictureId)} />
                                                <Typography>{artist.displayName}</Typography>
                                            </Stack>
                                        </TableCell>
                                        <TableCell>
                                            <Stack direction='row' gap={2} justifyContent='flex-end'>
                                                <Button variant='contained' onClick={() => setManagedArtist(artist)}>Manage</Button>
                                                <Button variant='outlined' onClick={() => handleRemove(artist.id)}>Remove</Button>
                                            </Stack>
                                        </TableCell>
                                    </TableRow>
                                )
                            })
                        }
                    </TableBody>
                </Table>
            </Paper>
            <Dialog open={isAddingArtist} onClose={() => setIsAddingArtist(false)} fullWidth>
                <Stack padding={5} gap={3}>
                    <AddArtistToDistributor />
                </Stack>
            </Dialog>
            <Dialog open={managedArtist !== null} onClose={() => setManagedArtist(null)} fullWidth>
                <ManageArtist artist={managedArtist} />
            </Dialog>
        </Box>
    )
}

function ManageArtist({ artist }: { artist: Artist | null }) {
    const [isManagingSongs, setIsManagingSongs] = useState(false);
    const [isManagingAlbums, setIsManagingAlbums] = useState(false);

    const [songs, setSongs] = useState<Song[]>()
    const [albums, setAlbums] = useState<Album[]>()

    useEffect(() => {
        if (!artist)
            return;

        fetchPaged(`Song/artist/${artist.id}`, 1, 100)
            .then(response => setSongs(response));

        fetcher.get(`Album/artist/${artist.id}`)
            .then(response => setAlbums(response.data));
    }, [artist, isManagingAlbums, isManagingSongs])

    if (!artist)
        return;

    return (
        <Stack gap={3} padding={5}>
            <Stack alignItems='center' gap={2}>
                <Avatar src={FetchImage(artist.profilePictureId)} sx={{ width: 100, height: 100 }} />
                <Typography variant='h4'>{artist.displayName}</Typography>
            </Stack>

            <Stack direction='row' justifyContent='center' gap={3}>
                <Button onClick={() => setIsManagingSongs(true)} variant='outlined' fullWidth>Songs</Button>
                <Button onClick={() => setIsManagingAlbums(true)} variant='outlined' fullWidth>Albums</Button>
            </Stack>

            <Dialog open={isManagingSongs} onClose={() => setIsManagingSongs(false)} fullWidth maxWidth='lg'>
                <ManageSongs songs={songs!} artist={artist} />
            </Dialog>
            <Dialog open={isManagingAlbums} onClose={() => setIsManagingAlbums(false)} fullWidth maxWidth='md'>
                <ManageAlbums albums={albums!} artist={artist} />
            </Dialog>
        </Stack>
    )
}

function ManageAlbums({ albums, artist }: { albums: Album[], artist: Artist }) {
    const [isAddingNewAlbum, setIsAddingNewAlbum] = useState(false)
    const [albumsLocal, setAlbumsLocal] = useState<Album[]>(albums);

    const notify = useNotification();

    async function handleDelete(album: Album) {
        try {
            await fetcher.delete(`Album/${album.id}`);
            setAlbumsLocal(albumsLocal.filter(a => a.id !== album.id))
            notify({ message: 'Album deleted', severity: 'success' })
        }
        catch (error) {
            notify({ message: error, severity: 'error' })
        }
    }

    let formData: FormData | undefined = undefined;

    function setFormData(data: FormData) {
        formData = data;
    }

    async function handleUpload() {
        try {
            formData?.append('artistId', artist.id);
            await fetcher.post("Album", formData);
            notify({ message: 'Album added', severity: 'success' })
            setIsAddingNewAlbum(false);
        }
        catch (error) {
            notify({ message: error, severity: 'error' })
        }
    }

    return (
        <Box padding={5}>
            {
                !isAddingNewAlbum ?

                    <Paper elevation={3}>
                        <Table>
                            <TableHead>
                                <TableCell>Title</TableCell>
                                <TableCell align="right">Action</TableCell>
                            </TableHead>

                            <TableBody>
                                <TableRow>
                                    <TableCell />
                                    <TableCell align='right'>
                                        <Button variant='contained' onClick={() => setIsAddingNewAlbum(true)}>Add a new album</Button>
                                    </TableCell>
                                </TableRow>
                                {
                                    albumsLocal?.map((album) => {
                                        return (
                                            <TableRow>
                                                <TableCell>
                                                    <Stack direction='row' alignItems='center' gap={2}>
                                                        <Avatar variant='rounded' src={FetchImage(album.coverImageId)} />
                                                        <Typography>{album.title}</Typography>
                                                    </Stack>
                                                </TableCell>
                                                <TableCell align='right'>
                                                    <Stack direction='row' justifyContent='flex-end' gap={2}>
                                                        <Button variant='contained' disabled>Edit</Button>
                                                        <Button variant='outlined' onClick={() => handleDelete(album)}>Remove</Button>
                                                    </Stack>
                                                </TableCell>
                                            </TableRow>
                                        )
                                    })
                                }
                            </TableBody>
                        </Table>
                    </Paper>
                    :
                    <Stack gap={3} alignItems='center'>
                        <AddAlbumPage noArtist onUpload={handleUpload} setFormData={setFormData} />
                        <Button variant='outlined' onClick={() => setIsAddingNewAlbum(false)}>Cancel</Button>
                    </Stack>
            }
        </Box>
    )
}

function ManageSongs({ songs, artist }: { songs: Song[], artist: Artist }) {
    const [editingSong, setEditingSong] = useState<Song>()
    const [songsLocal, setSongsLocal] = useState<Song[]>(songs);
    const [isAddingSong, setIsAddingSong] = useState(false)
    const notify = useNotification();

    let uploadSongData: FormData | undefined = undefined;

    function setUploadSongData(data: FormData) {
        uploadSongData = data;
    }

    async function handleDelete(song: Song) {
        try {
            await fetcher.delete(`Song/${song.id}`)
            setSongsLocal(songsLocal?.filter(s => s.id !== song.id));
            notify({ message: 'Song deleted', severity: 'success' })
        }
        catch (error) {
            notify({ message: error, severity: 'error' })
        }
    }

    async function handleUpload() {
        try {
            uploadSongData?.append('artistId', artist.id);
            await fetcher.post("Song", uploadSongData);
            notify({ message: 'Song uploaded', severity: 'success' })
            setIsAddingSong(false);
        }
        catch (error) {
            notify({ message: error, severity: 'error' })
        }
    }

    return (
        <Box padding={5}>
            {
                !isAddingSong ?

                    !editingSong ?
                        <Paper elevation={3}>
                            <Table>
                                <TableHead>
                                    <TableCell>Title</TableCell>
                                    <TableCell>Album</TableCell>
                                    <TableCell align='right'>Action</TableCell>
                                </TableHead>

                                <TableBody>
                                    <TableRow>
                                        <TableCell />
                                        <TableCell />
                                        <TableCell align='right'>
                                            <Button variant='contained' onClick={() => setIsAddingSong(true)}>Add a new song</Button>
                                        </TableCell>
                                    </TableRow>
                                    {
                                        songsLocal?.map((song) => {
                                            return (
                                                <TableRow>
                                                    <TableCell>
                                                        <Stack direction='row' alignItems='center' gap={2}>
                                                            <Avatar variant='rounded' src={FetchImage(song.coverImageId)} />
                                                            <Typography>{song.title}</Typography>
                                                        </Stack>
                                                    </TableCell>
                                                    <TableCell>{song.album}</TableCell>
                                                    <TableCell align='right'>
                                                        <Stack direction='row' justifyContent='flex-end' gap={2}>
                                                            <Button variant='contained' onClick={() => setEditingSong(song)}>Edit</Button>
                                                            <Button variant='outlined' onClick={() => handleDelete(song)}>Remove</Button>
                                                        </Stack>
                                                    </TableCell>
                                                </TableRow>
                                            )
                                        })
                                    }
                                </TableBody>
                            </Table>
                        </Paper>
                        :
                        <Stack>
                            <UpdateSongForm song={editingSong} onClose={() => setEditingSong(undefined)} />
                        </Stack>
                    :
                    <Stack alignItems='center'>
                        <AddSongPage noArtist setFormData={setUploadSongData} onUpload={handleUpload} />
                        <Button variant='outlined' onClick={() => setIsAddingSong(false)}>Cancel</Button>
                    </Stack>
            }
        </Box>
    )
}

type UpdateSong = {
    id: string,
    title: string,
    albumId: string,
    data: File,
    coverImage: File,
}

function UpdateSongForm({ song, onClose }: { song: Song, onClose: () => void }) {
    const { register, handleSubmit } = useForm<UpdateSong>({
        defaultValues: {
            title: song.title,
        }
    });

    const notify = useNotification();

    function handleUpdate(update: UpdateSong) {
        const formData = new FormData();
        formData.append('id', song.id);
        formData.append('title', update.title);

        fetcher.put('Song', formData)
            .then(() => onClose())
            .catch(error => notify({ message: error, severity: 'error' }))
    }

    return (
        <Stack gap={3} alignItems={'flex-start'}>
            <OptionalTextField label='Title' {...register('title')} />

            <Stack gap={3} direction='row'>

                <Button variant='contained' onClick={handleSubmit(handleUpdate)}>Update</Button>
                <Button variant='outlined' onClick={onClose}>Cancel</Button>
            </Stack>
        </Stack>
    )

}