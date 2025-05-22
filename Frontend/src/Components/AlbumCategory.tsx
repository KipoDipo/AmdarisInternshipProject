import { Typography, Box, Stack } from "@mui/material";
import { Album } from "../Models/Album";
import AlbumComponent from "./AlbumComponent";

export default function AlbumCategory({name, albums}: {name: string, albums?: Album[]}) {
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
          {albums?.map((album) => (
            <AlbumComponent key={album.id} album={album} />
          ))}
        </Stack>
      </Box>
    </>
  )
}
