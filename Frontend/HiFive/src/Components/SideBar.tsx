import { useState } from "react";

import { Link } from "react-router-dom";

import { Tabs, Tab, Box, Typography, Divider, useTheme } from "@mui/material";

import HomeRoundedIcon from '@mui/icons-material/HomeRounded';
import PersonRoundedIcon from '@mui/icons-material/PersonRounded';
import SearchRoundedIcon from '@mui/icons-material/SearchRounded';
import SettingsRoundedIcon from '@mui/icons-material/SettingsRounded';
import ElectricBoltRoundedIcon from '@mui/icons-material/ElectricBoltRounded';

import ThumbUpRoundedIcon from '@mui/icons-material/ThumbUpRounded';
import QueueMusicRoundedIcon from '@mui/icons-material/QueueMusicRounded';
import SubscriptionsRoundedIcon from '@mui/icons-material/SubscriptionsRounded';

function SideBar() {
    const [value, setValue] = useState(0)
    const theme = useTheme();

    const handleChange = (_event: React.SyntheticEvent, newValue: number) => {
        setValue(newValue);
    }; 

    return (
        <Box sx={{height: '100vh', width: '15%',}}>
            <Box
                sx={{
                    background: theme.palette.secondary.dark,
                    borderRadius: '0 32px 0 0px',
                    borderRight: `2px solid ${theme.palette.secondary.main}`,
                    height: 'inherit',
                    width: 'inherit',    
                    position: 'fixed',
                }}
            >
                <Tabs 
                    orientation='vertical'
                    value={value}
                    onChange={handleChange}
                    sx={{
                        '& .MuiTab-root': {
                            justifyContent: 'flex-start',
                        },
                        '& .Mui-selected': {
                            borderRadius: '0 999px 999px 0px',
                        },
                        '& .MuiTouchRipple-root': {
                            borderRadius: '0 999px 999px 0px',
                        },
                        color: theme.palette.secondary.light,
                        padding: '16px 16px 0 0',
                    }}
                >
                    <Tab label='Home' icon={<HomeRoundedIcon/>} iconPosition='start' component={Link} to='/'/>
                    <Tab label='Account' icon={<PersonRoundedIcon/>} iconPosition='start' component={Link} to='/account'/>
                    <Tab label='Explore'icon={<SearchRoundedIcon/>} iconPosition='start'/>
                    <Tab label='Settings'icon={<SettingsRoundedIcon/>} iconPosition='start'/>
                    <Tab label='Go Premium'icon={<ElectricBoltRoundedIcon/>} iconPosition='start'/>
                    <Divider sx={{background: theme.palette.secondary.main, height: '1px', margin: '10% 0 0 0'}}/>
                    <Typography variant='h4' sx={{margin: '10px'}}>Library</Typography>
                    <Tab label='Liked'icon={<ThumbUpRoundedIcon/>} iconPosition='start'/>
                    <Tab label='Queue'icon={<QueueMusicRoundedIcon/>} iconPosition='start'/>
                    <Tab label='Playlists'icon={<SubscriptionsRoundedIcon/>} iconPosition='start'/>
                </Tabs>
            </Box>
        </Box>
    )
}

export default SideBar;
