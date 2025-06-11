import { Autocomplete, Box, Button, Stack, TextField } from "@mui/material";
import { Controller, useForm } from "react-hook-form";
import { useState } from "react";
import { Artist } from "../Models/Artist";
import { fetcher } from "../Fetcher";
import { textWidth, theme } from "../Styling/Theme";
import { useNotification } from "../Contexts/Snackbar/UseNotification";

type FormType = {
    artist: Artist | null,
}

export function AddArtistToDistributor() {
    const { control, handleSubmit } = useForm<FormType>();
    const [artists, setArtists] = useState<Artist[]>([]);

    const notify = useNotification();

    async function getByName(value: string | undefined) {
        if (!value)
            return;

        const response = await fetcher.get(`Artist/name/${value}`)
        setArtists(response.data);
    }

    const onSubmit = (data: FormType) => {
        if (!data.artist)
            return;

        fetcher.post(`Distributor/add-artist/${data.artist.id}`)
            .then(() => notify({ message: 'Artist added to your artist group', severity: 'success' }))
            .catch(error => notify({ message: error, severity: 'error' }))
    };


    return (
        <>
            <Controller
                name="artist"
                control={control}
                render={({ field }) => (
                    <Autocomplete
                        onInputChange={async (_, value) => await getByName(value)}
                        options={artists}
                        value={field.value}
                        onChange={(_, newValue) => field.onChange(newValue)}
                        getOptionLabel={(option) => option.displayName}
                        renderInput={(params) => (
                            <TextField {...params} label="Search for an artist" />
                        )}
                    />
                )}
            />
            <Button variant='contained' onClick={handleSubmit(onSubmit)}>Add</Button>
        </>
    )
}

export default function Page() {
    return (
        <Box width='100%' display='flex' justifyContent='center' alignItems='center'>
            <Stack padding={5} gap={3} width={textWidth} bgcolor={theme.palette.secondary.dark} borderRadius={theme.shape.borderRadius}>
                <AddArtistToDistributor/>
            </Stack>
        </Box>
    )
}