import { Autocomplete, Avatar, Box, Button, Dialog, DialogContent, DialogTitle, Stack, TextField, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { Artist } from "../Models/Artist";
import { Song } from "../Models/Song";
import AddSongPage from "./AddSong";
import { fetcher } from "../Fetcher";
import { theme } from "../Styling/Theme";

function AddAlbumPage() {
    const [artists, setArtists] = useState<Artist[]>([]);
    const [inputArtistText, setInputArtistText] = useState<string>();
    const [inputArtist, setInputArtist] = useState<Artist | null>(null);

    const [inputAlbumTitle, setInputAlbumTitle] = useState("");
    const [inputReleaseDate, setInputReleaseDate] = useState("");

    const [inputDescriptionText, setInputDescriptionText] = useState("")

    const [inputImageFile, setInputImageFile] = useState<File | null>(null);

    const [open, setOpen] = useState(false);

    const [formDatas, setFormDatas] = useState<FormData[]>([]);

    const addFormData = (formData: FormData) => {
        setFormDatas((prev) => [...prev, formData]);
    }

    useEffect(() => {
        fetcher.get("Artist/get-artists-from-distributor-me")
            .then(response => setArtists(response.data));
    }, []);

    const handleImageFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files?.[0];
        if (file) {
            setInputImageFile(file);
        }
    }


    const upload = async () => {
        if (!inputArtist || !inputAlbumTitle || !inputReleaseDate) {
            alert("Please fill all fields");
            return;
        }

        const formData = new FormData();
        formData.append("title", inputAlbumTitle);
        formData.append("releaseDate", inputReleaseDate);
        formData.append("artistId", inputArtist.id);
        formData.append("coverImage", inputImageFile as Blob);
        formData.append("description", inputDescriptionText)
        formDatas.forEach((fd, index) => {
            formData.append(`songs[${index}].title`, fd.get('title') ?? "ERROR");
            formData.append(`songs[${index}].genreIds`, fd.get('genreIds') ?? "ERROR");
            formData.append(`songs[${index}].coverImage`, fd.get('coverImage') ?? "ERROR");
            formData.append(`songs[${index}].data`, fd.get('data') ?? "ERROR");
            formData.append(`songs[${index}].artistId`, inputArtist.id);
        });

        try {
            const response = await fetcher.post("Album", formData);
            console.log(response);
        }
        catch (error) {
            console.log(error);
        }
    }

    const remove = (index: number) => {
        setFormDatas((prev) => {
            const newFormDatas = [...prev];
            newFormDatas.splice(index, 1);
            return newFormDatas;
        });
    }

    const newSong = () => {
        setOpen(true);
    }

    const handleClose = () => {
        setOpen(false);
    }

    return (
        <Box width='100%' display='flex' justifyContent='center' alignItems='center'>
            <Stack direction='row' gap={6} padding={5} bgcolor={theme.palette.secondary.dark} borderRadius={theme.shape.borderRadius} sx={{ overflowX: 'auto', whiteSpace: 'nowrap' }} >
                <Stack gap={3}>
                    <Autocomplete
                        disablePortal
                        options={artists || []}
                        getOptionLabel={(option) => option.displayName || "ERROR"}
                        isOptionEqualToValue={(option, value) => option.id === value.id}
                        inputValue={inputArtistText}
                        renderInput={(params) => <TextField {...params} label="Enter artist" />}
                        onInputChange={async (_event, newValue) => {
                            setInputArtistText(newValue);
                        }}
                        onChange={(_event, newValue) => {
                            setInputArtist(newValue);
                        }}
                    />
                    <TextField
                        label="Album title"
                        value={inputAlbumTitle}
                        onChange={(event) => setInputAlbumTitle(event.target.value)}
                    />
                    <TextField
                        label="Release date"
                        type="date"
                        value={inputReleaseDate}
                        onChange={(event) => setInputReleaseDate(event.target.value)}
                        slotProps={{
                            inputLabel: { shrink: true },
                        }}
                    />
                    <TextField
                        label="Description"
                        type="text"
                        multiline
                        rows={5}
                        onChange={(event) => setInputDescriptionText(event.target.value)}
                    />
                    <TextField
                        label="Upload image"
                        variant="outlined"
                        type="file"
                        onChange={handleImageFileChange}
                        slotProps={{
                            inputLabel: { shrink: true }
                        }}
                    />
                    <Button variant="contained" onClick={newSong}>Add Song</Button>
                    <Button variant="contained" onClick={upload}>Upload</Button>

                </Stack>
                <Stack gap={3} width={600}>
                    <Typography variant="h5">Songs for the album</Typography>
                    {formDatas.map((fd, index) => {
                        const song = Object.fromEntries(fd.entries()) as unknown as Song & {coverImage: File};
                        const url = URL.createObjectURL(song.coverImage);
                        return (
                            <Stack key={song?.id} direction='row' bgcolor={theme.palette.secondary.main} borderRadius={theme.shape.borderRadius} padding={3} gap={3} justifyContent={'space-between'} alignItems='center'>
                                <Stack direction='row' gap={3} alignItems='center'>
                                    <Avatar variant='rounded' src={url}></Avatar>
                                    <Typography>{song?.title || "ERROR"}</Typography>
                                </Stack>
                                <Button variant="contained" onClick={() => remove(index)}>Remove</Button>
                            </Stack>
                        )
                    }
                    )}
                </Stack>
                <Dialog open={open} onClose={handleClose} fullWidth maxWidth="sm">
                    <DialogTitle>Upload new song</DialogTitle>
                    <DialogContent>
                        <AddSongPage noArtist noBackground onUpload={handleClose} setFormData={addFormData} />
                    </DialogContent>
                </Dialog>
            </Stack>
        </Box>
    )
}

export default AddAlbumPage;