import { ReactElement, ReactNode, useEffect, useState } from "react";

import { NavLink, useLocation } from "react-router-dom";

import { Tabs, Tab, Box, Typography, Divider, TabProps } from "@mui/material";

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

type AppTabProps = {
    label: string;
    icon: ReactElement;
    to: string;
    value?: string;
} & TabProps;

function AppTab({ label, icon, to, value, ...rest }: AppTabProps) {
    return <Tab label={label} icon={icon} iconPosition='start' component={NavLink} to={to} value={value ?? to} {...rest} sx={{
        minHeight: '54px',
        justifyContent: 'flex-start',
        borderRadius: '0 999px 999px 0px',
        marginRight: '24px',
        color: theme.palette.secondary.light,
    }} />
}

function TabGroup({ children }: { children: ReactNode }) {
    const location = useLocation();
    const tokens = location.pathname.split('/');

    return (
        <Tabs orientation='vertical' value={`/${tokens[1]}`}>
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
                    <AppTab label='Home' icon={<HomeRoundedIcon />} to='/' />
                    <AppTab label='Explore' icon={<SearchRoundedIcon />} to='/explore' />
                    <AppTab label='Settings' icon={<SettingsRoundedIcon />} to='/account-edit' />
                    {
                        (!listener || !listener.isSubscribed) &&
                        <AppTab label='Go Premium' icon={<ElectricBoltRoundedIcon />} to='/subscribe' />
                    }
                    <Divider sx={{ background: theme.palette.secondary.main, height: '1px', margin: '10% 0 0 0' }} />
                    <Typography variant='h4' sx={{ margin: '10px' }}>Library</Typography>
                    <AppTab label='Liked' icon={<ThumbUpRoundedIcon />} to='/liked' />
                    <AppTab label='Playlists' icon={<SubscriptionsRoundedIcon />} to='/playlists' />
                    <Divider sx={{ background: theme.palette.secondary.main, height: '1px', margin: '10% 0 0 0' }} />
                    <Typography variant='h4' sx={{ margin: '10px' }}>Social</Typography>
                    <AppTab label='Account' icon={<PersonRoundedIcon />} to='/account' />
                    <AppTab label='Following' icon={<PeopleAltRoundedIcon />} to='/TODO' />
                    <AppTab label='Artists' icon={<Diversity1RoundedIcon />} to='/following-artists' />
                </TabGroup>
            )
            break;
        case 'Distributor':
            component = (
                <TabGroup>
                    <AppTab label='Add Song' icon={<QueueMusicRoundedIcon />} to='/add-song' />
                    <AppTab label='Add Album' icon={<QueueMusicRoundedIcon />} to='/add-album' />
                    <AppTab label='Add Genre' icon={<QueueMusicRoundedIcon />} to='/add-genre' />
                    <AppTab label='Add Artist' icon={<QueueMusicRoundedIcon />} to='/add-artist' />
                </TabGroup>
            )
            break;

    }

    return (
        <Box sx={{
            height: 'calc(100% - 115px)',
            minWidth: '250px',
            width: '250px',
            paddingBottom: '115px',
            background: theme.palette.secondary.dark,
            borderRadius: '0 32px 0 0px',
            borderRight: `2px solid ${theme.palette.secondary.main}`,
        }}>
            <Box
                sx={{
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
