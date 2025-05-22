import { Typography, Box, Stack } from "@mui/material";
import { Song } from "../Models/Song";
import SongComponent from "./SongComponent";

export default function Category({name, songs}: {name: string, songs?: Song[]}) {
  return (
    <>
      <Typography variant='h3'>{name}</Typography>
      <Box
        sx={{
          overflowX: 'auto',
          whiteSpace: 'nowrap',
          width: '80vw',
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
    </>
  )
}
