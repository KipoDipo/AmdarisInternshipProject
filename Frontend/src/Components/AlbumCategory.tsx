import { Typography, Box, Stack } from "@mui/material";
import { Album } from "../Models/Album";
import AlbumComponent from "./AlbumComponent";
import { theme } from "../Styling/Theme";

export default function AlbumCategory({name, albums}: {name: string, albums?: Album[]}) {
  return (
    <Stack alignItems='flex-start' padding={4} gap={2} sx={{background: theme.palette.secondary.dark, borderRadius: theme.shape.borderRadius }}>
      <Typography variant='h3'>{name}</Typography>
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
          {albums?.map((album) => (
            <AlbumComponent key={album.id} album={album} />
          ))}
        </Stack>
      </Box>
    </Stack>
  )
}
