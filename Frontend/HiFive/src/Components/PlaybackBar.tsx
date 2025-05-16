import { useEffect, useState } from 'react'

import { Slider, Stack, Typography, Fab, TypographyOwnProps, IconButton, useTheme, Box, Avatar, Dialog, MenuItem, Select, FormControl, InputLabel, Button } from '@mui/material'
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

import { Song } from '../Models/Song';
import { useSong } from '../Contexts/UseSong';
import { Link } from 'react-router-dom';
import { textWidth } from '../Styling/Theme';
import { baseURL, fetcher } from '../Fetcher';
import { Playlist } from '../Models/Playlist';

function Controls({size}: {size?:number}) {
  const theme = useTheme();
  const [isPlayingMusic, setPlayingMusic] = useState(false);
  const [isAddingToPlaylist, setIsAddingToPlaylist] = useState(false);
  const [hasLikedSong, setHasLikedSong] = useState(false);

  if (!size)
    size = 32;

  const iconSize = size;
  const smallFabSx = {width: iconSize, height: iconSize, minWidth: 0, minHeight: 0, color: theme.palette.secondary.dark, background: theme.palette.primary.main, '&:hover': {background: theme.palette.primary.light}};
  const bigFabSx = {width: iconSize * 2, height: iconSize * 2, minWidth: 0, minHeight: 0, color: theme.palette.secondary.dark, background: theme.palette.primary.main, '&:hover': {background: theme.palette.primary.light}};
  const smallFabSxAlt = {...smallFabSx, background: "transparent", color: theme.palette.secondary.light ,'&:hover': {transition:'color 0.2s ease', color:theme.palette.secondary.dark, background: theme.palette.primary.light}, boxShadow: "none"}
  const smallFontSx = {fontSize: iconSize};
  const bigFontSx = {fontSize: iconSize * 2};

  function handlePlay() {
    setPlayingMusic(!isPlayingMusic);
  }

  
  const song = useSong();

  async function likeSong() {
    if (!song)
      return;

    await fetcher.post(`/Listener/like/${song.id}`)
    setHasLikedSong(true);
  }
  
  useEffect(() => {
    if (!song)
      return;

    fetcher.get('/Song/my-liked')
    .then((response) => {
      const songs: Song[] = response.data;
      setHasLikedSong(songs.some(s => s.id === song.id));
    })
  }, [song])

  return (
    <Stack
    direction='row'
    justifyContent='center'
    alignItems='center'
    spacing={3}
    flex={0}
  >
      <Fab sx={{
        ...smallFabSxAlt, 
        color: hasLikedSong ? theme.palette.primary.main : theme.palette.secondary.light,
        }} 
        onClick={likeSong} centerRipple>
        <ThumbUpRoundedIcon/>
      </Fab>
      <Fab sx={smallFabSx} centerRipple>
        <SkipPreviousRoundedIcon sx={smallFontSx}/>
      </Fab>
      <Fab sx={bigFabSx} onClick={handlePlay} centerRipple>
        {isPlayingMusic ? <PauseRoundedIcon sx={bigFontSx}/> : <PlayArrowRoundedIcon sx={bigFontSx}/>}
      </Fab>
      <Fab sx={smallFabSx} centerRipple>
        <SkipNextRoundedIcon sx={smallFontSx}/>  
      </Fab>
      <Fab sx={smallFabSxAlt} onClick={() => setIsAddingToPlaylist(true)} centerRipple>
        <QueueMusicRoundedIcon/>  
      </Fab>

      <AddToPlaylistDialog open={isAddingToPlaylist} setIsOpen={setIsAddingToPlaylist}/>
  </Stack>
  )
}

function AddToPlaylistDialog({open, setIsOpen}: {open: boolean, setIsOpen: (p: boolean) => void}) {
  const [playlists, setPlaylists] = useState<Playlist[]>()
  const [selectedPlaylist, setSelectedPlaylist] = useState("")

  const song = useSong();

  async function addToPlaylist() {
    if (!song)
      return;

    try {
      await fetcher.post(`/Playlist/add/${selectedPlaylist}/song/${song.id}`)
      setIsOpen(false);
    }
    catch(error) {
      console.error(error)
    }
  }
  useEffect(() => {
    if (!open)
      return;

    fetcher.get('/Playlist/my-playlists')
    .then((response) => setPlaylists(response.data))
  }, [open])


  return (
    <Dialog open={open} onClose={() => setIsOpen(false)} fullWidth maxWidth="sm">
        <Stack margin={3} gap={3} alignItems='center'>
          <Stack gap={3} alignItems='center' direction='row'>
            <Avatar src={`${baseURL}Image/${song?.coverImageId}`} variant='rounded' sx={{width: 80, height: 80}}/>
            <Stack>
              <Typography variant='h5'>{song?.title}</Typography>
              <Typography>{song?.artistName}</Typography>
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
                      <Avatar src={`${baseURL}Image/${p.thumbnailId}`} variant='rounded'/>
                      <Typography>{p.title}</Typography>
                    </Stack>
                  </MenuItem>
                )
              }
            </Select>
          </FormControl>
          <Button variant='contained' sx={{width:textWidth}} onClick={addToPlaylist}>Add</Button>
        </Stack>
      </Dialog>
  )
}


function Info({song}: {song?: Song}) {
  const typographySx: TypographyOwnProps = {
    whiteSpace: 'nowrap',
    overflow: 'hidden',
    textOverflow: 'ellipsis',
    width: '100%',
  }

  return (
    <Stack direction='row' alignItems='center' spacing={3} flex={1} minWidth={0}>
      <Avatar sx={{width: 64, height: 64}} variant='rounded' src={song?.coverImageId ? `https://localhost:7214/Image/${song?.coverImageId}` : ''} alt={song?.title}/>
      <Stack minWidth={0}>
      <Typography variant='h5' sx={typographySx}>{song?.title}</Typography>
      <Typography variant='subtitle1' sx={typographySx} component={Link} to={`/artist/${song?.artistId}`} >{song?.artistName}</Typography>
      </Stack>
    </Stack>
    )
}

function Actions({size}: {size?:number}) {
  const theme = useTheme();
  
  if (!size)
    size = 32;

  const iconSize = size;
  const buttonSx = {width: iconSize * 1.2, height: iconSize * 1.2, minWidth: 0, minHeight: 0};
  const fontSx = {fontSize: iconSize}

  const [isShuffling, setShuffle] = useState(false)
  const [repeatState, setRepeat] = useState(0)

  function handleRepeat() {
    setRepeat((repeatState + 1) % 3);
  }

  function handleShuffle() {
    setShuffle(!isShuffling);
  }

  return (
    <Stack direction='row' alignItems='center' justifyContent='flex-end' flex={1}>
      <IconButton sx={buttonSx} onClick={handleRepeat}>
        {repeatState == 0 && <RepeatRoundedIcon sx={{...fontSx, color: theme.palette.secondary.light}}/>}
        {repeatState == 1 && <RepeatRoundedIcon sx={{...fontSx, color: theme.palette.primary.main}}/>}
        {repeatState == 2 && <RepeatOneRoundedIcon sx={{...fontSx, color: theme.palette.primary.main}}/>}
      </IconButton>

      <IconButton sx={buttonSx} onClick={handleShuffle}>
        <ShuffleRoundedIcon sx={{...fontSx, color:isShuffling ? theme.palette.primary.main : theme.palette.secondary.light}}/>
      </IconButton>

      <IconButton>
        <LinkRoundedIcon sx={{...fontSx, color: theme.palette.secondary.light}}/>
      </IconButton>
    </Stack>
  )
}

function PlayBar() {
  const theme = useTheme();
  const song = useSong();

  const controlsSize = 32;

  return (
    <Box width='100vw' sx={{background: theme.palette.secondary.main}}>
      <Stack direction='row' justifyContent='space-between' sx={{margin: 2}}>
        <Info song={song}/>
        <Controls size={controlsSize}/>
        <Actions size={24}/>
      </Stack>
    </Box>
  )
}

function MySlider({length}: {length: number}) {
  return (
    <Slider sx={{padding:0}} max={length} color={'primary'} onChangeCommitted={() => (document.activeElement as HTMLElement).blur()}/>
  )
}

function PlaybackBar() {
  return (
        <Stack position='fixed' bottom={0} left={0} component='footer'>
          <MySlider length={120} />
          <PlayBar/>
        </Stack>
  )
}

export default PlaybackBar
