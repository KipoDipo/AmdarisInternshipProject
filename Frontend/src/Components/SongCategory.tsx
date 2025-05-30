import { Typography, Box, Stack, Avatar } from "@mui/material";
import { Song } from "../Models/Song";
import SongComponent from "./SongComponent";
import { Link } from "react-router-dom";
import { theme } from "../Styling/Theme";

export default function Category({name, songs, imageUrl, to}: {name: string, songs?: Song[], imageUrl?: string, to?: string}) {
  return (
    <Stack alignItems='flex-start' padding={4} gap={2} sx={{background: theme.palette.secondary.dark, borderRadius: theme.shape.borderRadius }}>
      <Stack direction='row' alignItems='center' gap={2} component={to ? Link : 'div'} to={to} sx={{textDecoration: 'none', color: 'inherit'}}>
        {
          imageUrl &&
          <Avatar src={imageUrl} sx={{height:'4em', width: '4em'}}/>
        }
        <Typography variant='h3'>{name}</Typography>
      </Stack>
      <Box
        sx={{
          overflowX: 'auto',
          whiteSpace: 'nowrap',
          width: '100%',
        }}>
        <Stack 
          direction='row' 
          gap={2} 
          marginTop={1} 
          marginBottom={2}
          flexWrap='nowrap'
        >
          {songs?.map((song) => (
            <SongComponent key={song.id} song={song} />
          ))}
        </Stack>
      </Box>
    </Stack>
  )
}
