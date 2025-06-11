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
import TopBar from "./Components/SearchBar";
import Playlists from "./Pages/Playlists";
import Playlist from "./Pages/Playlist";
import Album from "./Pages/Album";
import LikedSongs from "./Pages/LikedSongs";
import { QueueProvider } from "./Contexts/Queue/QueueProvider";
import Queue from "./Pages/Queue";
import { NotificationProvider } from "./Contexts/Snackbar/NotificationProvider";
import FollowingArtists from "./Pages/FollowingArtists";
import Badges from "./Pages/Badges";
import AddArtistToDistributor from "./Pages/AddArtistToDistributor";
import Subscribe from "./Pages/Subscribe";
import SubscribeFail from "./Pages/SubscribeFail";
import SubscribeSuccess from "./Pages/SubscribeSuccess";
import { ListenerProvider } from "./Contexts/Listener/ListenerProvider"
import { ListenerDetails } from "./Models/ListenerDetails";
import { fetcher } from "./Fetcher";
import ManageArtistsPage from "./Pages/ManageArtistsPage";

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
                fetcher.get("/Listener/details")
                    .then((response) => setComponent(<ListenerRoutes listener={response.data}/>))
                break;

            case 'Artist':
                setComponent(<ArtistRoutes />);
                break;

            case 'Distributor':
                setComponent(<DistributorRoutes />);
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
                                    <Box display='flex' justifyContent='center' alignItems='center' width='100vw' minHeight='100vh' overflow='auto' >
                                        <Stack alignItems='center' padding={4} borderRadius={theme.shape.borderRadius} gap={2} bgcolor={theme.palette.secondary.dark}>
                                            <LogoAndName />
                                            <Routes>
                                                <Route path="/register" element={<Register />}></Route>
                                                <Route path="/login" element={<Login setLogged={login} />}></Route>
                                                <Route path="*" element={<Navigate to='/login' />}></Route>
                                            </Routes>
                                        </Stack>
                                    </Box>
                                    :
                                    <Box sx={{ flex: 1, display: 'flex', overflow: 'hidden', height: '100vh' }}>
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
            <Route path="/add-artist/" element={<AddArtistToDistributor />} />
            <Route path="/manage-artists/" element={<ManageArtistsPage />} />
        </Routes>
    )
}

function ArtistRoutes() {
    return (
        <Stack>

        </Stack>
    )
}

function ListenerRoutes({listener}: {listener: ListenerDetails}) {
    return (
        <ListenerProvider initialData={listener}>
            <QueueProvider>
                <Box
                    sx={{
                        flex: 1,
                        overflowY: 'auto',
                        paddingBottom: '100px', // space for the PlaybackBar height
                    }}
                >
                    <TopBar />
                    <Routes>
                        <Route index element={<Home />} />
                        <Route path="/account" element={<Account />} />
                        <Route path="/account-edit" element={<AccountEdit />} />
                        <Route path="/artist/:id" element={<Artist />} />
                        <Route path="/playlist/:id" element={<Playlist />} />
                        <Route path="/album/:id" element={<Album />} />
                        <Route path="/liked" element={<LikedSongs />} />
                        <Route path="/playlists" element={<Playlists />} />
                        <Route path="/queue" element={<Queue />} />
                        <Route path="/following-artists" element={<FollowingArtists />} />
                        <Route path="/my-badges" element={<Badges />} />
                        <Route path="/subscribe" element={<Subscribe />} />
                        <Route path="/subscribed-success" element={<SubscribeSuccess />} />
                        <Route path="/subscribed-fail" element={<SubscribeFail />} />
                    </Routes>
                </Box>
                <PlaybackBar />
            </QueueProvider >
        </ListenerProvider>

    )
}

export default App;