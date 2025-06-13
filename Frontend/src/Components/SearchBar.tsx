import { Autocomplete, Avatar, ButtonBase, Divider, Menu, MenuItem, Stack, TextField, Typography } from "@mui/material";
import { useEffect, useRef, useState } from "react";
import { fetcher } from "../Fetcher";
import { Song } from "../Models/Song";
import { textWidth, theme } from "../Styling/Theme";
import { Artist } from "../Models/Artist";
import { Link } from "react-router-dom";
import { useSetQueue } from "../Contexts/Queue/UseSetQueue";
import { CreateQueue } from "../Utils/QueueUtils";
import FetchImage from "../Utils/FetchImage";
import { useListener } from "../Contexts/Listener/UseListener";
import AppBadge from "./AppBadge";

import SettingsRoundedIcon from '@mui/icons-material/SettingsRounded';
import PersonRoundedIcon from '@mui/icons-material/PersonRounded';
import ExitToAppRoundedIcon from '@mui/icons-material/ExitToAppRounded';
import { Listener } from "../Models/Listener";

type SongExt = Song & { type: 'song' }
type ArtistExt = Artist & { type: 'artist' }
type ListenerExt = Listener & { type: 'listener' }

type OptionType = SongExt | ArtistExt | ListenerExt

function renderOptionComponent(option: OptionType) {
    if (option.type === 'song')
        return (
            <Stack direction='row' alignItems='center' gap={3}>
                <Avatar src={FetchImage(option.coverImageId)} variant='rounded' />
                <Stack>
                    <Typography>{option.title}</Typography>
                    <Typography variant='body2' color={theme.palette.secondary.light}>{option.artistName}</Typography>
                </Stack>
            </Stack>
        )
    if (option.type === 'artist')
        return (
            <Stack direction='row' alignItems='center' gap={3} component={Link} to={`/artist/${option?.id}`} width='100%' sx={{ textDecoration: 'none' }}>
                <Avatar src={FetchImage(option.profilePictureId)} variant='circular' />
                <Stack>
                    <Typography>{option.displayName}</Typography>
                    <Typography marginTop={-0.5} variant='body2'>Artist</Typography>
                </Stack>
            </Stack>
        )
    if (option.type === 'listener')
        return (
            <Stack direction='row' alignItems='center' gap={3} component={Link} to={`/account/${option?.id}`} width='100%' sx={{ textDecoration: 'none' }}>
                <Avatar src={FetchImage(option.profilePictureId)} variant='circular' />
                <Stack>
                    <Typography>{option.displayName}</Typography>
                    <Typography marginTop={-0.5} variant='body2'>User</Typography>
                </Stack>
            </Stack>
        )


    return (
        <>
            <Typography>ERROR</Typography>
        </>
    )
}

export default function Bar() {
    return (
        <Stack direction='row' margin={3} alignItems='center' justifyContent='space-between'>
            <SearchBar />
            <AccountHub />
        </Stack>
    )
}

function AccountHub() {
    const [anchorEl, setAnchorEl] = useState<HTMLElement | null>(null);

    const open = Boolean(anchorEl);

    const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
        setAnchorEl(event.currentTarget);
    };
    const handleClose = () => {
        setAnchorEl(null);
    };

    const handleExit = () => {
        delete localStorage['token'];
        delete localStorage['role']
        location.reload();
    }

    const listener = useListener();

    return (
        <Stack direction='row' alignItems='center' >
            <AppBadge badgeId={listener?.equippedBadgeId}>
                <ButtonBase onClick={handleClick} sx={{ borderRadius: '50%', overflow: 'hidden', boxShadow: 10 }}>
                    <Avatar src={FetchImage(listener?.profilePictureId)} sx={{ width: 80, height: 80 }} />
                </ButtonBase>
            </AppBadge>
            <Menu anchorEl={anchorEl} open={open} onClose={handleClose}>
                <MenuItem component={Link} to={`/account/${listener?.id}`} onClick={handleClose}>
                    <Stack direction='row' gap={1}>
                        <PersonRoundedIcon />
                        <Typography>Account</Typography>
                    </Stack>
                </MenuItem>
                <MenuItem component={Link} to='/account-edit' onClick={handleClose}>
                    <Stack direction='row' gap={1}>
                        <SettingsRoundedIcon />
                        <Typography>Settings</Typography>
                    </Stack>
                </MenuItem>
                <Divider />
                <MenuItem onClick={() => { handleClose(); handleExit(); }}>
                    <Stack direction='row' gap={1}>
                        <ExitToAppRoundedIcon />
                        <Typography>Log out</Typography>
                    </Stack>
                </MenuItem>
            </Menu>
        </Stack>
    )
}

function SearchBar() {
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

    const setQueue = useSetQueue();

    const updateOptions = async (query: string) => {
        if (!query) {
            setOptions([])
            return;
        }

        const songs = await fetcher.get(`/Song/name/${query}`);
        const artists = await fetcher.get(`/Artist/name/${query}`);
        const listeners = await fetcher.get(`/Listener/name/${query}`);
        const songsType = songs.data.map((s: Song) => ({ ...s, type: 'song' }))
        const artistsType = artists.data.map((a: Artist) => ({ ...a, type: 'artist' }))
        const listenersType = listeners.data.map((a: Listener) => ({ ...a, type: 'listener' }))

        const result = [...songsType, ...artistsType, ...listenersType];
        setOptions(result);
    }


    return (
        <Stack width={textWidth * 2}>
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
                    if (option.type === 'listener')
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
                        setQueue(CreateQueue([newValue]));

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