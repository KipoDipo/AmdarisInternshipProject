import { Stack, Typography } from "@mui/material";
import { baseURL } from "../Fetcher";
import { Song } from "../Models/Song";
import ClickableAvatar from "./ClickableAvatar";
import { useSetQueue } from "../Contexts/Queue/UseSetQueue";
import { CreateQueue } from "../Utils/QueueUtils";

export default function SongComponent({song} : {song: Song}) {
  const setQueue = useSetQueue();

  const handleClick = () => {
    setQueue(CreateQueue([song]));
  }
  
  return (
    <Stack alignItems='center' sx={{ cursor: 'pointer', maxWidth: 200, textAlign: 'center', whiteSpace: 'normal'}} onClick={handleClick}>
      <ClickableAvatar src={song.coverImageId ? `${baseURL}Image/${song.coverImageId}` : ''} variant='rounded' sx={{ width: 200, height: 200 , boxShadow: 10 }}/>
      <Typography variant='h5' sx={{textShadow: '1px 0 10px #000'}}>{song.title}</Typography>
      <Typography variant='subtitle1' sx={{textShadow: '1px 0 10px #000'}}>{song.artistName}</Typography>
    </Stack>
  )
}