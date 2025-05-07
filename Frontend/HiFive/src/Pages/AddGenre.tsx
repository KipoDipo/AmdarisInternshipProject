import { Button, Stack, TextField } from "@mui/material";
import axios from "axios";
import { useState } from "react";

function AddGenrePage() {
    const [inputGenreText, setInputGenreText] = useState("")

    const handleUpload = async () => {
        var response = await axios.post("https://localhost:7214/Genre", {name: inputGenreText})
        console.log(response)
    }

    return(
        <Stack margin={3} gap={3}>
            <TextField
                label="Name"
                value={inputGenreText}
                onChange={(event) => {setInputGenreText(event.target.value)}}
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

export default AddGenrePage;