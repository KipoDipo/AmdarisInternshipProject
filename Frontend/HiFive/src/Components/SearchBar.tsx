import { Autocomplete, Avatar, Stack, TextField, Typography } from "@mui/material";
import { useEffect, useRef, useState } from "react";
import { baseURL, fetcher } from "../Fetcher";
import { Song } from "../Models/Song";
import { textWidth, theme } from "../Styling/Theme";
import { useSetSong } from "../Contexts/UseSetSong";
import { Artist } from "../Models/Artist";
import { Link } from "react-router-dom";

type SongExt = Song & { type: 'song' }
type ArtistExt = Artist & { type: 'artist' }

type OptionType = SongExt | ArtistExt

function renderOptionComponent(option: OptionType) {
    if (option.type === 'song')
        return (
            <Stack direction='row' alignItems='center' gap={3}>
                <Avatar src={`${baseURL}Image/${option.coverImageId}`} variant='rounded' />
                <Stack>
                    <Typography>{option.title}</Typography>
                    <Typography variant='body2' color={theme.palette.secondary.light}>{option.artistName}</Typography>
                </Stack>
            </Stack>
        )
    if (option.type === 'artist')
        return (
            <Stack direction='row' alignItems='center' gap={3} component={Link} to={`/artist/${option?.id}`} width='100%' sx={{textDecoration:'none'}}>
                <Avatar src={`${baseURL}Image/${option.profilePictureId}`} variant='circular' />
                <Typography>{option.displayName}</Typography>
            </Stack>
        )

    return (
        <>
            <Typography>ERROR</Typography>
        </>
    )
}


export default function SearchBar() {
    const [options, setOptions] = useState<OptionType[]>([]);
    const [input, setInput] = useState("");
    const inputRef = useRef<HTMLInputElement>(null);

    const [debouncedInput, setDebouncedInput] = useState(input);

    useEffect(() => {
        const handler = setTimeout(() => {
            setDebouncedInput(input)
        }, 300);

        return () => {
            clearTimeout(handler);
        }
    }, [input]);

    useEffect(() => {
        if (debouncedInput)
            updateOptions(debouncedInput);
        else
            setOptions([]);
    }, [debouncedInput]);

    const setSong = useSetSong();

    const updateOptions = async (query: string) => {
        if (!query) {
            setOptions([])
            return;
        }

        const songs = await fetcher.get(`/Song/name/${query}`);
        const artists = await fetcher.get(`/Artist/name/${query}`);
        const songsType = songs.data.map((s:Song) => ({ ...s, type: 'song' }))
        const artistsType = artists.data.map((a:Artist) => ({ ...a, type: 'artist' }))

        const result = [...songsType, ...artistsType];
        setOptions(result);
    }


    return (
        <Stack margin={3} width={textWidth * 2}>
            <Autocomplete
                freeSolo
                disableCloseOnSelect
                options={options || []}
                getOptionLabel={(option) => {
                    if (typeof option === 'string')
                        return option;
                    if (option.type === 'song')
                        return option.title;
                    if (option.type === 'artist')
                        return option.displayName;
                    return '';
                }}
                isOptionEqualToValue={(option, value) => option.id === value.id}
                renderInput={(params) => <TextField inputRef={inputRef} {...params} label="Search..." />}
                renderOption={(props, option) => (
                    <li {...props}>
                        {renderOptionComponent(option)}
                    </li>
                )}
                inputValue={input}
                onInputChange={(_event, newValue, reason) => {
                    if (reason !== 'reset') {
                        setInput(newValue);
                    }
                }}
                onChange={(_event, newValue) => {
                    if (typeof newValue == 'string' || !newValue)
                        return;

                    if (newValue.type === 'song')
                        setSong(newValue);

                    inputRef.current?.blur();
                }}
                onBlur={() => {
                    setInput("");
                    setTimeout(() => setOptions([]), 100); // very hacky
                }}
            />
        </Stack>
    )
}