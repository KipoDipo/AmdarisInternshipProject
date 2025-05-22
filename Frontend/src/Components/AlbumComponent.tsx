import { Typography } from "@mui/material";
import Stack from "@mui/material/Stack";
import { baseURL } from "../Fetcher";
import ClickableAvatar from "./ClickableAvatar";
import { Album } from "../Models/Album";
import { Link } from "react-router-dom";

export default function AlbumComponent({album}: {album: Album}) {
  return (
    <Stack alignItems='center' sx={{ cursor: 'pointer', maxWidth: 200, textAlign: 'center', whiteSpace: 'normal'}}>
      <ClickableAvatar 
        src={album.coverImageId ? `${baseURL}Image/${album.coverImageId}` : ''}
        variant='rounded'
        component={Link}
        to={`/album/${album.id}`}
        sx={{ width: 200, height: 200 , boxShadow: 10 }}/>
      <Typography variant='h5' sx={{textShadow: '1px 0 10px #000'}}>{album.title}</Typography>
    </Stack>
  )
}