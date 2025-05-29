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
import { ReactElement, useEffect, useState } from "react";
import Login from "./Pages/Login";
import LogoAndName from "./Components/LogoAndName";
import AccountEdit from "./Pages/AccountEdit";
import Artist from "./Pages/Artist";
import Explore from "./Components/SearchBar";
import Playlists from "./Pages/Playlists";
import Playlist from "./Pages/Playlist";
import Album from "./Pages/Album";
import LikedSongs from "./Pages/LikedSongs";
import { QueueProvider } from "./Contexts/Queue/QueueProvider";
import Queue from "./Pages/Queue";
import { NotificationProvider } from "./Contexts/Snackbar/NotificationProvider";
import FollowingArtists from "./Pages/FollowingArtists";
import Badges from "./Pages/Badges";
// import AddPlaylistPage from "./Pages/AddPlaylist";

function App() {
    const token = localStorage.getItem('token')
    const [logged, setLogged] = useState(token !== null)
    const [role, setRole] = useState(localStorage.getItem('role') ?? "")
    const [component, setComponent] = useState<ReactElement>();

    function login(state: boolean) {
        setLogged(state);
        if (state) {
            setRole(localStorage.getItem('role') ?? "");
        }
    }

    useEffect(() => {
        if (!role)
            return;

        switch (role) {
            case 'Listener':
                setComponent(ListenerRoutes);
                break;

            case 'Artist':
                setComponent(ArtistRoutes);
                break;

            case 'Distributor':
                setComponent(DistributorRoutes);
                break;

            case 'Admnin':
                break;

        }
    }, [role])

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
                    <NotificationProvider>

                        <BrowserRouter>
                            {
                                !logged ?
                                    <Stack width='100vw' height='100vh' justifyContent='center' alignItems='center' gap={3}>
                                        <LogoAndName />
                                        <Routes>
                                            <Route path="/register" element={<Register />}></Route>
                                            <Route path="/login" element={<Login setLogged={login} />}></Route>
                                            <Route path="*" element={<Navigate to='/login' />}></Route>
                                        </Routes>
                                    </Stack>
                                    :
                                    <Box sx={{ flex: 1, display: 'flex', overflow: 'hidden' }}>
                                        <SideBar role={role} />
                                        {component}
                                    </Box>
                            }
                        </BrowserRouter>
                    </NotificationProvider>
                </Stack>
            </Box>
        </ThemeProvider>
    )
}

function DistributorRoutes() {
    return (
        <Routes>
            <Route index element={<Navigate to='/add-song' />} />
            <Route path="/add-song/" element={<AddSongPage />} />
            <Route path="/add-genre/" element={<AddGenrePage />} />
            <Route path="/add-album/" element={<AddAlbumPage />} />
        </Routes>
    )
}

function ArtistRoutes() {
    return (
        <Stack>

        </Stack>
    )
}

function ListenerRoutes() {
    return (
        <QueueProvider>
            <Box
                sx={{
                    flex: 1,
                    overflowY: 'auto',
                    paddingBottom: '100px', // space for the PlaybackBar height
                }}
            >
                <Routes>
                    <Route index element={<><Explore /><Home /></>} />
                    <Route path="/account" element={<><Explore /><Account /></>} />
                    <Route path="/account-edit" element={<AccountEdit />} />
                    <Route path="/artist/:id" element={<><Explore /><Artist /></>} />
                    <Route path="/playlist/:id" element={<><Explore /><Playlist /></>} />
                    <Route path="/album/:id" element={<><Explore /><Album /></>} />
                    <Route path="/liked" element={<><Explore /><LikedSongs /></>} />
                    <Route path="/playlists" element={<><Explore /><Playlists /></>} />
                    <Route path="/queue" element={<><Explore /><Queue /></>} />
                    <Route path="/following-artists" element={<><Explore /><FollowingArtists /></>} />
                    <Route path="/my-badges" element={<><Explore /><Badges /></>} />
                    {/* <Route path="/add-playlist/" element={<AddPlaylistPage />} /> */}
                </Routes>
            </Box>
            <PlaybackBar />
        </QueueProvider >

    )
}

export default App;