import { Stack, Autocomplete, TextField, Button } from "@mui/material";
import axios from "axios";
import { useEffect, useState } from "react";
import { Artist } from "../Models/Artist";
import { Genre } from "../Models/Genre";

function AddSongPage({noArtist, onUpload, setFormData} : {noArtist?: boolean, onUpload?: () => void, setFormData?: (formData: FormData) => void}) {
    const [artists, setArtists] = useState<Artist[]>([]);
    const [inputArtistText, setInputArtistText] = useState<string>();
    const [inputArtist, setInputArtist] = useState<Artist | null>(null);

    const [inputSongTitle, setInputSongTitle] = useState("");

    const [genres, setGenres] = useState<Genre[]>([]);
    const [inputGenreText, setInputGenreText] = useState<string>();
    const [inputGenre, setInputGenre] = useState<Genre | null>(null);

    const [inputImageFile, setInputImageFile] = useState<File | null>(null);
    const [inputSongFile, setInputSongFile] = useState<File | null>(null);

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
        const formData = new FormData();
        formData.append("title", inputSongTitle);
        formData.append("duration", "0");
        formData.append("genreIds", inputGenre?.id || "");
        formData.append("coverImage", inputImageFile as Blob);
        formData.append("data", "AZURE_BLOB_STORAGE_URL");
        formData.append("artistId", inputArtist?.id || "");
        formData.append("albumId", "");
        //TODO implement azure blob storage upload
        try {
            setFormData?.(formData);
            if (onUpload) {
                onUpload();
            }
            else {
                var response = await axios.post("https://localhost:7214/Song", formData)
                console.log(response.data);   
            }
        }
        catch (error) {
            console.error("There was an error uploading the data!", error);
        }
    }


    useEffect(() => {
        axios.get("https://localhost:7214/Genre")
        .then((response) => {
            setGenres(response.data);
        })
        .catch((error) => {
            console.error("There was an error fetching the data!", error);
        });
    }, []);

    return (
    <Stack margin={3} gap={3} width={300}>
        {!noArtist && <Autocomplete
            disablePortal
            options={artists || []}
            getOptionLabel={(option) => option.displayName || ""}
            isOptionEqualToValue={(option, value) => option.id === value.id}
            inputValue={inputArtistText}
            renderInput={(params) => <TextField {...params} label="Enter artist" />}
            onInputChange={async (_event, newValue) => {
                setInputArtistText(newValue);
                try {
                    const response = await axios.get(`https://localhost:7214/Artist/name/${newValue}`);
                    setArtists(response.data);
                }
                catch (error) {
                    console.error("There was an error fetching the data!", error);
                }
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
        <Autocomplete
            disablePortal
            options={genres || []}
            getOptionLabel={(option) => option.name || ""}
            isOptionEqualToValue={(option, value) => option.id === value.id}
            renderInput={(params) => <TextField {...params} label="Enter genre" />}
            inputValue={inputGenreText}
            onInputChange={(_event, newValue) => {
                setInputGenreText(newValue);
            }}
            onChange={(_event, newValue) => {
                setInputGenre(newValue);
            }}
        />
        <TextField
            label="Upload image"
            variant="outlined"
            type="file"
            onChange={handleImageFileChange}
            slotProps = {{ inputLabel: {shrink: true}}}
        />
        <TextField
            label="Upload song"
            variant="outlined"
            type="file"
            onChange={handleSongFileChange}
            slotProps = {{ inputLabel: {shrink: true}}}
        />
        <Button 
            variant='contained'
            onClick={handleUpload}
            >
            Upload
        </Button>
    </Stack>
    )
}

export default AddSongPage;