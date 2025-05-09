import { Box, Stack, ThemeProvider } from "@mui/material";
import PlaybackBar from "./Components/PlaybackBar";
import SideBar from "./Components/SideBar"
import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import { theme } from "./Styling/Theme";

import Home from "./Pages/Home";
import Account from "./Pages/Account";
import AddSongPage from "./Pages/AddSong";
import AddGenrePage from "./Pages/AddGenre";
import AddAlbumPage from "./Pages/AddAlbum";
import Register from "./Pages/Register";
import { useState } from "react";
import Login from "./Pages/Login";
import { SongProvider } from "./Contexts/SongProvider";
import LogoAndName from "./Components/LogoAndName";
// import AddPlaylistPage from "./Pages/AddPlaylist";

function App() {
    const token = localStorage.getItem('token')
    const [logged, setLogged] = useState(token !== null)

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
            <Box sx={{ position: 'relative', zIndex: 1 }} width="100vw" minHeight="100vh">
                <Stack direction="row">
                    <BrowserRouter>
                        {
                            !logged ?
                                <Stack width='100vw' height='100vh' justifyContent='center' alignItems='center' gap={3}>
                                    <LogoAndName />
                                    <Routes>
                                        <Route path="/register" element={<Register />}></Route>
                                        <Route path="/login" element={<Login setLogged={setLogged} />}></Route>
                                        <Route path="*" element={<Navigate to='/login' />}></Route>
                                    </Routes>
                                </Stack>
                                :
                                <SongProvider>
                                    <SideBar />
                                    <Routes>
                                        <Route index element={<Home />} />
                                        <Route path="/account" element={<Account />} />
                                        <Route path="/add-song/" element={<AddSongPage />} />
                                        <Route path="/add-genre/" element={<AddGenrePage />} />
                                        <Route path="/add-album/" element={<AddAlbumPage />} />
                                        {/* <Route path="/add-playlist/" element={<AddPlaylistPage />} /> */}
                                    </Routes>
                                    <PlaybackBar />
                                </SongProvider>
                        }
                    </BrowserRouter>
                </Stack>
            </Box>
        </ThemeProvider>
    )
}

export default App