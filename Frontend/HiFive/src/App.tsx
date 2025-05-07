import { Box, Stack, ThemeProvider } from "@mui/material";
import PlaybackBar from "./Components/PlaybackBar";
import SideBar from "./Components/SideBar"
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { SongProvider } from "./Contexts/SongContext";
import { theme } from "./Styling/Theme";

import Home from "./Pages/Home";
import Account from "./Pages/Account";
import AddSongPage from "./Pages/AddSong";
import AddGenrePage from "./Pages/AddGenre";
import AddAlbumPage from "./Pages/AddAlbum";
import AddPlaylistPage from "./Pages/AddPlaylist";

function App() {
  return (
  <ThemeProvider theme={theme}>
    <Box
        sx={{
          position: 'fixed',
          top: 0,
          left: 0,
          width: '100vw',
          height: '100vh',
          background: `linear-gradient(${theme.palette.secondary.main}, ${theme.palette.secondary.dark})`,
          zIndex: -1,
        }}
        />
      <Box sx={{ position: 'relative', zIndex: 1 }} width="100vw" minHeight="120vh">
        <Stack direction="row">
          <SongProvider>
            <BrowserRouter>
              <SideBar />
              <Routes>
                <Route index element={<Home />} />
                <Route path="/account" element={<Account />} />
                <Route path="/add-song/" element={<AddSongPage />} />
                <Route path="/add-genre/" element={<AddGenrePage />} />
                <Route path="/add-album/" element={<AddAlbumPage />} />
                <Route path="/add-playlist/" element={<AddPlaylistPage />} />
              </Routes>
            </BrowserRouter>
            <PlaybackBar/>
          </SongProvider>
        </Stack>
      </Box>
  </ThemeProvider>
  )
}

export default App