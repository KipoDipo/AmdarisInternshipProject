import { ReactNode, useEffect, useState } from "react"
import { baseURL, fetcher } from "../Fetcher"
import { Avatar, AvatarProps, Button, Dialog, DialogContent, DialogTitle, Stack, TextField, Typography } from "@mui/material"
import ClickableBase from "../Components/ClickableBase"
import { FieldValues, useForm } from "react-hook-form";
import { textWidth } from "../Styling/Theme";
import { Playlist } from "../Models/Playlist";
import { Link } from "react-router-dom";

export default function Page() {
    const [playlists, setPlaylists] = useState<Playlist[]>();
    const [openDialog, setOpenDialog] = useState(false);


    useEffect(() => {
        fetcher.get('Playlist/my-playlists')
            .then((response) => { console.log(response.data); setPlaylists(response.data) })
    }, [])

    return (
        <Stack margin={3} direction='row' flexWrap='wrap' gap={3}>
            <Tile label="Create New" props={{ onClick: () => setOpenDialog(true) }}>
                <Typography variant="h1">+</Typography>
            </Tile>
            {
                playlists?.map(p => <PlaylistComponent data={p} />)
            }
            <Dialog open={openDialog} onClose={() => setOpenDialog(false)} fullWidth maxWidth="sm">
                <DialogTitle>Create a new Playlist</DialogTitle>
                <DialogContent>
                    <AddPlaylist onSubmit={() => setOpenDialog(false)} />
                </DialogContent>
            </Dialog>

        </Stack>
    )
}

function Tile({ label, props, children }: { label: string, props?: AvatarProps & { to?: string }, children?: ReactNode }) {
    return (
        <Stack gap={2} alignItems='center'>
            <ClickableBase>
                <Avatar sx={{ width: 200, height: 200, boxShadow: 10 }} variant='rounded' {...props}>
                    {children}
                </Avatar>
            </ClickableBase>
            <Typography variant='h5'>{label}</Typography>
        </Stack>
    )
}

function PlaylistComponent({ data }: { data: Playlist }) {
    return (
        <Tile 
        label={data.title} 
        props={{ 
            src: `${baseURL}Image/${data.thumbnailId}`,
            component: Link,
            to: `/playlist/${data.id}`
        }} />
    )
}

function AddPlaylist({ onSubmit }: { onSubmit: () => void }) {
    const { register, handleSubmit } = useForm();

    async function submit(data: FieldValues) {
        const form = new FormData();
        form.append('title', data.title);
        form.append('description', data.description);
        form.append('thumbnail', data.thumbnail[0] as Blob);

        try {
            const response = await fetcher.post('/Playlist/create-my-playlist', form);
            console.log(response);
            onSubmit();
        }
        catch (error) {
            console.error(error);
        }
    }

    return (
        <Stack gap={3} alignItems='center' margin={1}>
            <TextField
                label="Title"
                sx={{ width: textWidth }}
                {...register("title")}
            />
            <TextField
                label="Description"
                sx={{ width: textWidth }}
                {...register("description")}
            />
            <TextField
                label="Thumbnail"
                type='file'
                sx={{ width: textWidth }}
                slotProps={{ inputLabel: { shrink: true } }}
                {...register("thumbnail")}
            />
            <Button
                variant='contained'
                onClick={handleSubmit(submit)}
                sx={{ width: textWidth }}>
                Create
            </Button>
        </Stack>
    )
}
