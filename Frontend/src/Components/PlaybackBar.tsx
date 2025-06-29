import { useEffect, useRef, useState } from 'react'

import { Slider, Stack, Typography, Fab, TypographyOwnProps, IconButton, Box, Avatar, Dialog, MenuItem, Select, FormControl, InputLabel, Button } from '@mui/material'
import PlayArrowRoundedIcon from '@mui/icons-material/PlayArrowRounded';
import PauseRoundedIcon from '@mui/icons-material/PauseRounded';
import SkipNextRoundedIcon from '@mui/icons-material/SkipNextRounded';
import SkipPreviousRoundedIcon from '@mui/icons-material/SkipPreviousRounded';
import LinkRoundedIcon from '@mui/icons-material/LinkRounded';
import RepeatRoundedIcon from '@mui/icons-material/RepeatRounded';
import RepeatOneRoundedIcon from '@mui/icons-material/RepeatOneRounded';
import ShuffleRoundedIcon from '@mui/icons-material/ShuffleRounded';
import ThumbUpRoundedIcon from '@mui/icons-material/ThumbUpRounded';
import QueueMusicRoundedIcon from '@mui/icons-material/QueueMusicRounded';
import PlaylistAddRoundedIcon from '@mui/icons-material/PlaylistAddRounded';

import { Song } from '../Models/Song';
import { Link } from 'react-router-dom';
import { textWidth, theme } from '../Styling/Theme';
import { fetcher } from '../Fetcher';
import { Playlist } from '../Models/Playlist';
import { useQueue } from '../Contexts/Queue/UseQueue';
import { useSetQueue } from '../Contexts/Queue/UseSetQueue';
import { TimeFormat } from '../Utils/TimeFormat';
import { useNotification } from '../Contexts/Snackbar/UseNotification';
import FetchImage from '../Utils/FetchImage';
import { Shuffled } from '../Utils/Shuffle';
import { CreateQueue } from '../Utils/QueueUtils';

function Controls({ size, onPlayToggle, isPlaying, duration, currentTime }: { size?: number, onPlayToggle: () => void, isPlaying: boolean, duration: number, currentTime: number }) {
    const [isAddingToPlaylist, setIsAddingToPlaylist] = useState(false);
    const [hasLikedSong, setHasLikedSong] = useState(false);

    if (!size)
        size = 32;

    const iconSize = size;
    const smallFabSx = { width: iconSize, height: iconSize, minWidth: 0, minHeight: 0, color: theme.palette.secondary.dark, background: theme.palette.primary.main, '&:hover': { background: theme.palette.primary.light } };
    const bigFabSx = { width: iconSize * 2, height: iconSize * 2, minWidth: 0, minHeight: 0, color: theme.palette.secondary.dark, background: theme.palette.primary.main, '&:hover': { background: theme.palette.primary.light } };
    const smallFabSxAlt = { ...smallFabSx, background: "transparent", color: theme.palette.secondary.light, '&:hover': { transition: 'color 0.2s ease', color: theme.palette.secondary.dark, background: theme.palette.primary.light }, boxShadow: "none" }
    const smallFontSx = { fontSize: iconSize };
    const bigFontSx = { fontSize: iconSize * 2 };

    const queue = useQueue();
    const setQueue = useSetQueue();

    const notify = useNotification();

    async function handleLike() {
        if (!queue || !queue.songs.length)
            return;

        if (!hasLikedSong) {
            setHasLikedSong(true);
            notify({ message: "Song has been liked" });
            await fetcher.post(`/Listener/like/${queue.songs[queue.current].id}`)
        }
        else {
            setHasLikedSong(false);
            notify({ message: "Song has been unliked" });
            await fetcher.post(`/Listener/unlike/${queue.songs[queue.current].id}`)
        }
    }

    useEffect(() => {
        if (!queue || !queue.songs.length)
            return;

        fetcher.get('/Song/my-liked')
            .then((response) => {
                const songs: Song[] = response.data;
                setHasLikedSong(songs.some(s => s.id === queue.songs[queue.current].id));
            })
            .catch(error => notify({ message: error, severity: 'error' }))
    }, [queue, notify])

    function skipPrevious() {
        if (queue?.current === 0)
            return;

        setQueue(prev => ({ ...prev, current: prev.current - 1 }))
    }

    function skipNext() {
        if (queue && queue?.current >= queue?.songs.length - 1)
            return;

        setQueue(prev => ({ ...prev, current: prev.current + 1 }))
    }

    return (
        <Stack alignItems='center'>
            <Stack
                direction='row'
                justifyContent='center'
                alignItems='center'
                spacing={3}
                flex={0}
                marginBottom={1}
                marginTop={1}
            >

                <Fab sx={{
                    ...smallFabSxAlt,
                    color: hasLikedSong ? theme.palette.primary.main : theme.palette.secondary.light,
                }}
                    onClick={handleLike} centerRipple>
                    <ThumbUpRoundedIcon />
                </Fab>
                <Fab sx={smallFabSx} centerRipple onClick={skipPrevious}>
                    <SkipPreviousRoundedIcon sx={smallFontSx} />
                </Fab>
                <Fab sx={bigFabSx} onClick={onPlayToggle} centerRipple>
                    {isPlaying ? <PauseRoundedIcon sx={bigFontSx} /> : <PlayArrowRoundedIcon sx={bigFontSx} />}
                </Fab>
                <Fab sx={smallFabSx} centerRipple onClick={skipNext}>
                    <SkipNextRoundedIcon sx={smallFontSx} />
                </Fab>
                <Fab sx={smallFabSxAlt} onClick={() => setIsAddingToPlaylist(true)} centerRipple>
                    <PlaylistAddRoundedIcon />
                </Fab>
            </Stack>

            <Typography align='center' variant='subtitle1' sx={{ position: 'absolute', top: '80%' }}>{TimeFormat(currentTime)}/{TimeFormat(duration)}</Typography>

            <AddToPlaylistDialog open={isAddingToPlaylist} setIsOpen={setIsAddingToPlaylist} />
        </Stack>
    )
}

function AddToPlaylistDialog({ open, setIsOpen }: { open: boolean, setIsOpen: (p: boolean) => void }) {
    const [playlists, setPlaylists] = useState<Playlist[]>()
    const [selectedPlaylist, setSelectedPlaylist] = useState("")

    const queue = useQueue();

    const notify = useNotification();

    async function addToPlaylist() {
        if (!queue || !queue.songs.length)
            return;

        try {
            await fetcher.post(`/Playlist/add/${selectedPlaylist}/song/${queue.songs[queue.current].id}`)
            setIsOpen(false);
        }
        catch (error) {
            notify({ message: error, severity: 'error' })
        }
    }
    useEffect(() => {
        if (!open)
            return;

        fetcher.get('/Playlist/my-playlists')
            .then((response) => setPlaylists(response.data))
            .catch(error => notify({ message: error, severity: 'error' }))
    }, [open, notify])


    return (
        <Dialog open={open} onClose={() => setIsOpen(false)} fullWidth maxWidth="sm">
            <Stack margin={3} gap={3} alignItems='center'>
                <Stack gap={3} alignItems='center' direction='row'>
                    <Avatar
                        src={queue && queue?.songs[queue.current] && queue?.songs[queue.current].coverImageId ? FetchImage(queue.songs[queue.current].coverImageId) : ''}
                        variant='rounded'
                        sx={{ width: 80, height: 80 }} />
                    <Stack>
                        <Typography variant='h5'>{queue?.songs[queue.current]?.title}</Typography>
                        <Typography>{queue?.songs[queue.current]?.artistName}</Typography>
                    </Stack>
                </Stack>
                <FormControl fullWidth>
                    <InputLabel>Select Playlist</InputLabel>
                    <Select
                        label="Select Playlist"
                        value={selectedPlaylist}
                        onChange={(event) => setSelectedPlaylist(event.target.value)}
                    >
                        {
                            playlists?.map((p) =>
                                <MenuItem key={p.id} value={p.id}>
                                    <Stack direction='row' gap={3} alignItems='center'>
                                        <Avatar src={FetchImage(p.thumbnailId)} variant='rounded' />
                                        <Typography>{p.title}</Typography>
                                    </Stack>
                                </MenuItem>
                            )
                        }
                    </Select>
                </FormControl>
                <Button variant='contained' sx={{ width: textWidth }} onClick={addToPlaylist}>Add</Button>
            </Stack>
        </Dialog>
    )
}

function Info({ song }: { song?: Song }) {
    const typographySx: TypographyOwnProps = {
        whiteSpace: 'nowrap',
        overflow: 'hidden',
        textOverflow: 'ellipsis',
    }

    return (
        <Stack direction='row' alignItems='center' spacing={3} flex={1}>
            {
                song?.coverImageId &&
                <Avatar sx={{ width: 64, height: 64 }} variant='rounded' src={FetchImage(song?.coverImageId)} alt={song?.title} />
            }
            <Stack>
                <Typography variant='h5' sx={typographySx}>{song?.title}</Typography>
                <Stack direction='row' gap={1}>
                    <Typography variant='subtitle1' sx={typographySx} component={Link} to={`/artist/${song?.artistId}`} >{song?.artistName}</Typography>
                    {
                        song?.albumId &&
                        <>
                            <Typography variant='subtitle1' sx={typographySx} >{`•`}</Typography>
                            <Typography variant='subtitle1' sx={typographySx} component={Link} to={`/album/${song?.albumId}`} >{song?.album}</Typography>
                        </>
                    }
                </Stack>
            </Stack>
        </Stack>
    )
}

// horrible nightmare
function Actions({ volume, onVolumeChange, repeatState, setRepeatState, size }: { volume: number, onVolumeChange: (newVolume: number) => void, size?: number, isShuffling: boolean, setIsShuffling: (newVal: boolean) => void, repeatState: number, setRepeatState: (newVal: number) => void }) {
    if (!size)
        size = 32;

    const iconSize = size;
    const buttonSx = { width: iconSize * 1.2, height: iconSize * 1.2, minWidth: 0, minHeight: 0 };
    const fontSx = { fontSize: iconSize }

    const notify = useNotification();
    const setQueue = useSetQueue();

    function handleRepeat() {
        const newState = (repeatState + 1) % 3;
        switch (newState) {
            case 0:
                notify({ message: "No Repeat" });
                break;
            case 1:
                notify({ message: "Repeat Queue" });
                break;
            case 2:
                notify({ message: "Repeat Single" });
                break;
        }
        setRepeatState(newState);
    }

    function handleShuffle() {
        setQueue(last => CreateQueue(Shuffled(last.songs)))
        notify({ message: 'Shuffling...' })
    }

    return (
        <Stack direction='row' alignItems='center' justifyContent='flex-end' flex={1} gap={1}>
            <Slider
                min={0}
                max={1}
                step={0.01}
                value={volume}
                onChange={(_e, val) => onVolumeChange(Array.isArray(val) ? val[0] : val)}
                sx={{ width: 100, marginRight: 2 }}
            />
            <IconButton component={Link} to='/queue'>
                <QueueMusicRoundedIcon sx={{ ...fontSx, color: theme.palette.secondary.light }} />
            </IconButton>

            <IconButton sx={buttonSx} onClick={handleRepeat}>
                {repeatState == 0 && <RepeatRoundedIcon sx={{ ...fontSx, color: theme.palette.secondary.light }} />}
                {repeatState == 1 && <RepeatRoundedIcon sx={{ ...fontSx, color: theme.palette.primary.main }} />}
                {repeatState == 2 && <RepeatOneRoundedIcon sx={{ ...fontSx, color: theme.palette.primary.main }} />}
            </IconButton>

            <IconButton sx={buttonSx} onClick={handleShuffle}>
                <ShuffleRoundedIcon sx={{ ...fontSx, color: theme.palette.secondary.light }} />
            </IconButton>

            <IconButton>
                <LinkRoundedIcon sx={{ ...fontSx, color: theme.palette.secondary.light }} />
            </IconButton>


        </Stack>
    )
}

type PlayBarProps = {
    volume: number;
    onVolumeChange: (newVolume: number) => void;
    duration: number;
    currentTime: number;
    onPlayToggle: () => void;
    isPlaying: boolean;
    isShuffling: boolean;
    setIsShuffling: (newVal: boolean) => void;
    repeatState: number;
    setRepeatState: (newVal: number) => void;
};

function PlayBar({ volume, onVolumeChange, duration, currentTime, onPlayToggle, isPlaying, isShuffling, setIsShuffling, repeatState, setRepeatState }: PlayBarProps) {
    const queue = useQueue();

    const controlsSize = 32;

    return (
        <Box width='100vw' sx={{ background: theme.palette.secondary.main }}>
            <Stack direction='row' justifyContent='space-between' sx={{ margin: 2 }}>
                <Info song={queue?.songs[queue.current]} />
                <Controls duration={duration} currentTime={currentTime} onPlayToggle={onPlayToggle} isPlaying={isPlaying} size={controlsSize} />
                <Actions isShuffling={isShuffling} setIsShuffling={setIsShuffling} repeatState={repeatState} setRepeatState={setRepeatState} volume={volume} onVolumeChange={onVolumeChange} size={24} />
            </Stack>
        </Box>
    )
}

function MySlider({ length, value, onSeek }: { length: number, value: number, onSeek: (time: number) => void }) {
    return (
        <Slider
            sx={{ padding: 0 }}
            max={length}
            color={'primary'}
            value={value}
            onChange={(_e, value) => onSeek(Array.isArray(value) ? value[0] : value)}
            onChangeCommitted={() => (document.activeElement as HTMLElement).blur()} />
    )
}

function PlayerWrapper({ audioUrl }: { audioUrl: string }) {
    const audioRef = useRef<HTMLAudioElement>(null);
    const [duration, setDuration] = useState(0);
    const [currentTime, setCurrentTime] = useState(0);
    const [volume, setVolume] = useState(1)
    const [isPlaying, setIsPlaying] = useState(false);

    const [isShuffling, setIsShuffling] = useState(false)
    const [repeatState, setRepeatState] = useState(0)

    const setQueue = useSetQueue();
    const queue = useQueue();

    const handlePlay = () => {
        setIsPlaying(true);
    }
    const handlePause = () => {
        setIsPlaying(false);
    }

    const handleLoadedMetadata = () => {
        if (audioRef.current) {
            setDuration(audioRef.current.duration);
        }
    };

    const handleTimeUpdate = () => {
        if (audioRef.current) {
            setCurrentTime(audioRef.current.currentTime);
        }
    };

    const handleSeek = (time: number) => {
        if (audioRef.current) {
            audioRef.current.currentTime = time;
        }
    };

    const handlePlayToggle = () => {
        const audio = audioRef.current;
        if (!audio)
            return;

        if (audio.paused) {
            audio.play();
        } else {
            audio.pause();
        }
    };

    const handleVolumeChange = (newVolume: number) => {
        setVolume(newVolume);
        if (audioRef.current) {
            audioRef.current.volume = newVolume;
        }
    };

    const notify = useNotification();

    const handleEnded = () => {
        if (!queue || !queue.songs.length)
            return;

        switch (repeatState) {
            case 0: // no repeat
                if (queue.current != queue.songs.length - 1)
                    setQueue({ ...queue, current: queue.current + 1 });
                else
                    notify({ message: "Reached end of queue...", duration: 5000 })
                break;
            case 1: // repeat queue
                setQueue({ ...queue, current: (queue.current + 1) % queue.songs.length });
                break;
            case 2: // single repeat
                setQueue({ ...queue });
                break;
        }
    }

    return (
        <Stack>
            <MySlider value={currentTime} onSeek={handleSeek} length={duration} />
            <PlayBar
                duration={duration}
                currentTime={currentTime}
                onPlayToggle={handlePlayToggle}
                isPlaying={isPlaying}
                volume={volume}
                onVolumeChange={handleVolumeChange}
                isShuffling={isShuffling}
                setIsShuffling={setIsShuffling}
                repeatState={repeatState}
                setRepeatState={setRepeatState}
            />

            <audio
                ref={audioRef}
                autoPlay
                src={audioUrl ? audioUrl : undefined}
                onLoadedMetadata={handleLoadedMetadata}
                onTimeUpdate={handleTimeUpdate}
                onPause={handlePause}
                onPlay={handlePlay}
                onEnded={handleEnded}
            />
        </Stack>
    );
}

function PlaybackBar() {
    const queue = useQueue();
    const [audioUrl, setAudioUrl] = useState("")

    const notify = useNotification();

    useEffect(() => {
        if (!queue || !queue.songs || !queue.songs[queue.current])
            return;

        fetcher.get(`Song/download/${queue.songs[queue.current].id}`)
            .then((response) => {
                const url = response.data.url;

                setAudioUrl(url);
            })
            .catch(error => notify({ message: error, severity: 'error' }))

    }, [queue, notify])

    return (
        <Stack position='fixed' bottom={0} left={0} component='footer'>
            <PlayerWrapper audioUrl={audioUrl} />
        </Stack>
    )
}

export default PlaybackBar
