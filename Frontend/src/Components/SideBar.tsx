import { ReactNode, useEffect, useState } from "react";

import { Link } from "react-router-dom";

import { Tabs, Tab, Box, Typography, Divider } from "@mui/material";

import HomeRoundedIcon from '@mui/icons-material/HomeRounded';
import PersonRoundedIcon from '@mui/icons-material/PersonRounded';
import SearchRoundedIcon from '@mui/icons-material/SearchRounded';
import SettingsRoundedIcon from '@mui/icons-material/SettingsRounded';
import ElectricBoltRoundedIcon from '@mui/icons-material/ElectricBoltRounded';
import PeopleAltRoundedIcon from '@mui/icons-material/PeopleAltRounded';
import Diversity1RoundedIcon from '@mui/icons-material/Diversity1Rounded';

import ThumbUpRoundedIcon from '@mui/icons-material/ThumbUpRounded';
import QueueMusicRoundedIcon from '@mui/icons-material/QueueMusicRounded';
import SubscriptionsRoundedIcon from '@mui/icons-material/SubscriptionsRounded';
import { theme } from "../Styling/Theme";
import { Listener } from "../Models/Listener";
import { fetcher } from "../Fetcher";

function TabGroup({ children }: { children: ReactNode }) {
    const [value, setValue] = useState(0)

    const handleChange = (_event: React.SyntheticEvent, newValue: number) => {
        setValue(newValue);
    };

    return (
        <Tabs
            orientation='vertical'
            value={value}
            onChange={handleChange}
            sx={{
                '& .MuiTab-root': {
                    minHeight: '54px',
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
            {children}
        </Tabs>
    )
}

function SideBar({ role }: { role: string }) {
    const [listener, setListener] = useState<Listener | undefined>()

    useEffect(() => {
        if (role === 'Listener' && !listener)
            fetcher.get('Listener')
                .then(response => setListener(response.data))
    }, [role, listener]);

    let component;
    switch (role) {
        case 'Listener':
            component = (
                <TabGroup>
                    <Typography variant='h4' sx={{ margin: '10px' }}>Hub</Typography>
                    <Tab label='Home' icon={<HomeRoundedIcon />} iconPosition='start' component={Link} to='/' />
                    <Tab label='Explore' icon={<SearchRoundedIcon />} iconPosition='start' component={Link} to='/explore' />
                    <Tab label='Settings' icon={<SettingsRoundedIcon />} iconPosition='start' component={Link} to='/account-edit' />
                    {
                        (!listener || !listener.isSubscribed) &&
                        <Tab label='Go Premium' icon={<ElectricBoltRoundedIcon />} iconPosition='start' component={Link} to='/subscribe' />
                    }
                    <Divider sx={{ background: theme.palette.secondary.main, height: '1px', margin: '10% 0 0 0' }} />
                    <Typography variant='h4' sx={{ margin: '10px' }}>Library</Typography>
                    <Tab label='Liked' icon={<ThumbUpRoundedIcon />} iconPosition='start' component={Link} to='/liked' />
                    <Tab label='Queue' icon={<QueueMusicRoundedIcon />} iconPosition='start' component={Link} to='/queue' />
                    <Tab label='Playlists' icon={<SubscriptionsRoundedIcon />} component={Link} to='/playlists' iconPosition='start' />
                    <Divider sx={{ background: theme.palette.secondary.main, height: '1px', margin: '10% 0 0 0' }} />
                    <Typography variant='h4' sx={{ margin: '10px' }}>Social</Typography>
                    <Tab label='Account' icon={<PersonRoundedIcon />} iconPosition='start' component={Link} to='/account' />
                    <Tab label='Following' icon={<PeopleAltRoundedIcon />} iconPosition='start' component={Link} to='/TODO' />
                    <Tab label='Artists' icon={<Diversity1RoundedIcon />} iconPosition='start' component={Link} to='/following-artists' />
                </TabGroup>
            )
            break;
        case 'Distributor':
            component = (
                <TabGroup>
                    <Tab label='Add Song' icon={<QueueMusicRoundedIcon />} iconPosition='start' component={Link} to='/add-song' />
                    <Tab label='Add Album' icon={<QueueMusicRoundedIcon />} iconPosition='start' component={Link} to='/add-album' />
                    <Tab label='Add Genre' icon={<QueueMusicRoundedIcon />} iconPosition='start' component={Link} to='/add-genre' />
                    <Tab label='Add Artist' icon={<QueueMusicRoundedIcon />} iconPosition='start' component={Link} to='/add-artist' />
                </TabGroup>
            )
            break;

    }

    return (
        <Box sx={{ height: 'calc(100% - 115px)', width: '250px', paddingBottom: '100px' }}>
            <Box
                sx={{
                    background: theme.palette.secondary.dark,
                    borderRadius: '0 32px 0 0px',
                    borderRight: `2px solid ${theme.palette.secondary.main}`,
                    height: 'inherit',
                    width: 'inherit',
                    position: 'fixed',
                    overflow: 'auto',
                }}
            >
                {component}
            </Box>
        </Box >
    )
}

export default SideBar;
