import { Box, Button, Stack, TextField } from "@mui/material";
import { useState } from "react";
import { textWidth, theme } from "../Styling/Theme";
import { fetcher } from "../Fetcher";

function AddGenrePage() {
    const [inputGenreText, setInputGenreText] = useState("")

    const handleUpload = async () => {
        const response = await fetcher.post("Genre", { name: inputGenreText })
        console.log(response)
    }

    return (
        <Box width='100%' display='flex' justifyContent='center' alignItems='center'>
            <Stack margin={3} gap={3} padding={5} bgcolor={theme.palette.secondary.dark} borderRadius={theme.shape.borderRadius} width={textWidth}>
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