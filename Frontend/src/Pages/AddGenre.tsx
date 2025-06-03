import { Box, Button, Stack, TextField } from "@mui/material";
import { useState } from "react";
import { textWidth, theme } from "../Styling/Theme";
import { fetcher } from "../Fetcher";
import { useNotification } from "../Contexts/Snackbar/UseNotification";

function AddGenrePage({ onAdd, noBackground}: { onAdd?: () => void, noBackground?: boolean }) {
    const [inputGenreText, setInputGenreText] = useState("")
    const notify = useNotification();

    const handleUpload = async () => {
        try {
            await fetcher.post("Genre", { name: inputGenreText })
            notify({ message: 'Genre uploaded successfully', severity: 'success' });
            if (onAdd)
                onAdd();
        }
        catch (error) {
            notify({message: error, severity: 'error'});
        }
    }

    return (
        <Box width='100%' display='flex' justifyContent='center' alignItems='center'>
            <Stack margin={3} gap={3} padding={5} bgcolor={noBackground ? 'transparent' : theme.palette.secondary.dark} borderRadius={theme.shape.borderRadius} width={textWidth}>
                <TextField
                    label="Name"
                    value={inputGenreText}
                    onChange={(event) => { setInputGenreText(event.target.value) }}
                />
                <Button
                    variant='contained'
                    onClick={handleUpload}
                >
                    Upload
                </Button>
            </Stack>
        </Box>
    )
}

export default AddGenrePage;