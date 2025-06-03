import { Stack, Autocomplete, TextField, Button, Box, Dialog, Divider, Typography, DialogTitle, DialogContent } from "@mui/material";
import { useEffect, useState } from "react";
import { Artist } from "../Models/Artist";
import { Genre } from "../Models/Genre";
import { fetcher } from "../Fetcher";
import { theme } from "../Styling/Theme";
import { useNotification } from "../Contexts/Snackbar/UseNotification";
import AddGenrePage from "./AddGenre";

type GenreSelect = Genre & {
    selected: boolean
}

function AddSongPage({ noArtist, noBackground, onUpload, setFormData }: { noArtist?: boolean, noBackground?: boolean, onUpload?: () => void, setFormData?: (formData: FormData) => void }) {
    const [artists, setArtists] = useState<Artist[]>([]);
    const [inputArtistText, setInputArtistText] = useState<string>();
    const [inputArtist, setInputArtist] = useState<Artist | null>(null);

    const [inputSongTitle, setInputSongTitle] = useState("");

    const [genres, setGenres] = useState<GenreSelect[]>([]);

    const [inputImageFile, setInputImageFile] = useState<File | null>(null);
    const [inputSongFile, setInputSongFile] = useState<File | null>(null);

    const notify = useNotification();

    const handleImageFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files?.[0];
        if (file) {
            setInputImageFile(file);
        }
    };

    const handleSongFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files?.[0];
        if (file) {
            setInputSongFile(file);
        }
    };

    const handleUpload = async () => {
        if (!inputArtist) {
            notify({ message: "An artist is required", severity: 'error' });
            return;
        }

        const formData = new FormData();
        formData.append("title", inputSongTitle);
        genres.forEach(g => {
            if (g.selected)
                formData.append("genreIds", g.id);
        })
        formData.append("coverImage", inputImageFile as Blob);
        formData.append("artistId", inputArtist.id);
        formData.append("data", inputSongFile as Blob);
        try {
            setFormData?.(formData);
            if (onUpload) {
                onUpload();
            }
            else {
                await fetcher.post("Song", formData)
                notify({ message: 'Song uploaded successfully', severity: 'success' })
            }
        }
        catch (error) {
            notify({ message: error, severity: 'error' })
        }
    }


    function loadGenres() {
        fetcher.get("Genre")
            .then((response) => {
                setGenres(response.data);
            })
            .catch((error) => {
                console.error("There was an error fetching the data!", error);
            });
    }

    useEffect(() => {
        fetcher.get("Artist/get-artists-from-distributor-me")
            .then(response => setArtists(response.data));

        loadGenres();
    }, []);

    const [isSelectingGenres, setIsSelectingGenres] = useState(false)
    const [isAddingNewGenre, setIsAddingNewGenre] = useState(false)

    const toggleGenre = (id: string) => {
        setGenres(prev =>
            prev.map(g =>
                g.id === id ? { ...g, selected: !g.selected } : g
            )
        );
    };

    return (
        <Box width='100%' display='flex' justifyContent='center' alignItems='center'>
            <Stack margin={3} gap={3} padding={5} bgcolor={noBackground ? 'transparent' : theme.palette.secondary.dark} borderRadius={theme.shape.borderRadius} width={300}>
                {
                    !noArtist &&
                    <Autocomplete
                        disablePortal
                        options={artists || []}
                        getOptionLabel={(option) => option.displayName || ""}
                        isOptionEqualToValue={(option, value) => option.id === value.id}
                        inputValue={inputArtistText}
                        renderInput={(params) => <TextField {...params} label="Enter artist" />}
                        onInputChange={async (_event, newValue) => {
                            setInputArtistText(newValue);
                        }}
                        onChange={(_event, newValue) => {
                            setInputArtist(newValue);
                        }}
                    />}
                <TextField
                    label="Enter song name"
                    variant="outlined"
                    value={inputSongTitle}
                    onChange={(event) => setInputSongTitle(event.target.value)}
                />
                <Button variant='outlined' color='primary' onClick={() => setIsSelectingGenres(true)} sx={{
                    height: '55px',
                    borderColor: '#777',
                    color: theme.palette.secondary.light,
                    backgroundColor: theme.palette.secondary.dark,
                    '&:hover': {
                        backgroundColor: theme.palette.secondary.main,
                    },
                }}>Select Genres</Button>
                {
                    (() => {
                        const selectedGenres = genres.filter(g => g.selected).map(g => g.name)

                        return <Typography>{"Genres: " + (selectedGenres.length > 0 ? selectedGenres : "none")}</Typography>
                    })()
                }
                <TextField
                    label="Upload image"
                    variant="outlined"
                    type="file"
                    onChange={handleImageFileChange}
                    slotProps={{ inputLabel: { shrink: true } }}
                />
                <TextField
                    label="Upload song"
                    variant="outlined"
                    type="file"
                    onChange={handleSongFileChange}
                    slotProps={{ inputLabel: { shrink: true } }}
                />
                <Button
                    variant='contained'
                    onClick={handleUpload}
                >
                    Upload
                </Button>
            </Stack>
            <Dialog open={isSelectingGenres} fullWidth onClose={() => setIsSelectingGenres(false)}>
                <DialogTitle>Select genres</DialogTitle>
                <DialogContent>

                    <Stack alignItems='center' gap={3} padding={3}>
                        <Stack alignItems='center' direction='row' flexWrap='wrap' gap={3} padding={5}>
                            {
                                genres.map(g => {
                                    return <Button variant={g.selected ? 'contained' : 'outlined'} onClick={() => toggleGenre(g.id)}>{g.name}</Button>
                                })
                            }
                        </Stack>
                        <Divider sx={{ bgcolor: theme.palette.secondary.dark, width: '75%' }} />
                        <Button variant='outlined' onClick={() => setIsAddingNewGenre(true)}>Add new Genre</Button>
                        <Button variant='contained' onClick={() => setIsSelectingGenres(false)} fullWidth>Done</Button>
                    </Stack>
                </DialogContent>

            </Dialog>
            <Dialog open={isAddingNewGenre} fullWidth onClose={() => setIsAddingNewGenre(false)}>
                <DialogTitle>Add a new genre</DialogTitle>
                <DialogContent>
                    <AddGenrePage noBackground onAdd={() => {
                        loadGenres();
                        setIsAddingNewGenre(false)
                    }} />
                </DialogContent>
            </Dialog>
        </Box>

    )
}

export default AddSongPage;