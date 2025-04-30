import { Avatar, Box, Stack, Typography } from "@mui/material";
import { useSetSong, } from "../SongContext";

function Song() {
  
  const songs = [
    {image: 'https://upload.wikimedia.org/wikipedia/en/b/b7/Reoldigitalandcdcover.png', artist: 'Reol', title: 'Digital'},
    {image: 'https://i.scdn.co/image/ab67616d00001e02ed5713bd79cd3aabd648d736', artist: 'Reol', title: 'ミラージュ'},
    {image: 'https://upload.wikimedia.org/wikipedia/en/6/6a/Sabaton_-_The_Last_Stand_cover.jpg', artist: 'Sabaton', title: 'The Last Stand'},
    {image: 'https://upload.wikimedia.org/wikipedia/en/f/f9/Sabaton_Alblem_cover.jpg', artist: 'Sabaton', title: 'Carolus Rex'},
    {image: 'https://www.angrymetalguy.com/wp-content/uploads/2022/03/Sabaton-The-War-to-End-All-Wars-01-scaled.jpg', artist: 'Sabaton', title: 'The War to End All Wars'},
    {image: 'https://i.scdn.co/image/ab67616d00001e021b0339f042d798da979c898f', artist: 'Dorohedoro OST', title: 'Welcome to En'},
    {image: 'https://cdn-images.dzcdn.net/images/cover/19007d946ad530f750267db192db11de/0x1900-000000-80-0-0.jpg', artist: 'Reol', title: 'Kinjirareta Asobi'},
    {image: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTezrrkeokvrtJFBQR_OJySMcxNQAca5UQTPA&s', artist: 'Sabaton', title: 'The Red Baron'},
    {image: 'https://m.media-amazon.com/images/I/61kv5OIQUTL._SX354_SY354_BL0_QL100__UXNaN_FMjpg_QL85_.jpg', artist: 'Sabaton', title: 'Primo Victoria'},
    {image: 'https://i.scdn.co/image/ab67616d0000b273d8ff4bc4a6a3f1beefbeb26c', artist: 'Persona 3 OST', title: 'Burn My Dread'},
    {image: 'https://m.media-amazon.com/images/I/91VDetfVuOL.jpg', artist: 'Persona 5 OST', title: 'Last Surprise'},
  ]
  
  const song = songs[Math.floor(Math.random() * songs.length)]
  
  const setSong = useSetSong();

  const handleClick = () => {
    setSong({title: song.title, artist: song.artist, image: song.image, duration: 0});
  }

  return (
    <Stack alignItems='center' sx={{ cursor: 'pointer', maxWidth: 200, textAlign: 'center'}} onClick={handleClick}>
      <Avatar src={song.image} variant='rounded' sx={{ width: 200, height: 200 , boxShadow: 10}}/>
      <Typography variant='h5' sx={{textShadow: '1px 0 10px #000'}}>{song.title}</Typography>
      <Typography variant='subtitle1' sx={{textShadow: '1px 0 10px #000'}}>{song.artist}</Typography>
    </Stack>
  )
}

function Category({name}: {name: string}) {
  return (
    <>
      <Typography variant='h3'>{name}</Typography>
      <Stack direction='row' gap={2} marginTop={1}>
        <Song/>
        <Song/>
        <Song/>
        <Song/>
        <Song/>
      </Stack>
    </>
  )
}


function Home() {
  return (
    <Box margin={3}>
      <Stack>
        <Category name="Recently played"/>
        <Category name="Your friends listened to"/>
        <Category name="Recommended for you"/>
      </Stack>
    </Box>
  );
}

export default Home;