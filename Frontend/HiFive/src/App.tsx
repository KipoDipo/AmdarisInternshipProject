import { Box, Stack, ThemeProvider } from "@mui/material";
import PlaybackBar from "./PlaybackBar";
import SideBar from "./SideBar"
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Home from "./Pages/Home";
import Account from "./Pages/Account";
import { SongProvider } from "./SongContext";

import { theme } from "./Styling/Theme";

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
      <Box sx={{ position: 'relative', zIndex: 1 }}>
        <Box width="100vw" minHeight="120vh">
          <Box justifyContent="flex-start" alignItems="flex-start">
            <Stack direction="row">
              <SongProvider>
                <BrowserRouter>
                  <SideBar />
                  <Routes>
                    <Route index element={<Home />} />
                    <Route path="/Account" element={<Account />} />
                  </Routes>
                </BrowserRouter>
                <PlaybackBar/>
              </SongProvider>
            </Stack>
          </Box>
        </Box>
      </Box>
  </ThemeProvider>
  )
}

export default App