import { Avatar, Box, ButtonBase, Stack, Typography } from "@mui/material";
import { useSetSong, } from "../Contexts/UseSetSong";

import axios from "axios";
import { useEffect, useState } from "react";

import { Song } from "../Models/Song";

function SongComponent(song: Song) {
  const setSong = useSetSong();

  const handleClick = () => {
    setSong(song);
  }
  
  return (
    <Stack alignItems='center' sx={{ cursor: 'pointer', maxWidth: 200, textAlign: 'center', whiteSpace: 'normal'}} onClick={handleClick}>
      <ButtonBase 
      sx={{
        transition: 'filter 0.3s ease, transform 0.2s',
        '&:hover': {
          filter: 'brightness(1.3)',
          transform: 'scale(0.95)'
        },
        '&:active': {
          filter: 'brightness(1.5)',
          transform: 'scale(0.9)'
        }
      }}>
        <Avatar src={`https://localhost:7214/Image/${song.coverImageId}`} variant='rounded' sx={{ width: 200, height: 200 , boxShadow: 10 }}/>
      </ButtonBase>
      <Typography variant='h5' sx={{textShadow: '1px 0 10px #000'}}>{song.title}</Typography>
      <Typography variant='subtitle1' sx={{textShadow: '1px 0 10px #000'}}>{song.artistName}</Typography>
    </Stack>
  )
}

function Category({name, songs}: {name: string, songs?: Song[]}) {
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
            <SongComponent key={song.id} {...song} />
          ))}
        </Stack>
      </Box>
    </>
  )
}


function Home() {
  const [songs, setSongs] = useState<Song[]>([]);
  const token = localStorage.getItem("token");

  useEffect(() => {
    axios.get('https://localhost:7214/Song', {
      headers: {
        Authorization: `Bearer ${token}`
      }
    })
      .then((response) => {
        setSongs(response.data);
      })
      .catch((error) => {
        console.error('Error fetching data:', error);
      });
    }, [token])

  return (
    <Stack margin={3}>
      <Category name="Songs in the DB" songs={songs} />
    </Stack>
  );
}

export default Home;