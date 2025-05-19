import { Avatar, AvatarGroup, Box, Stack, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { ListenerDetails } from "../Models/ListenerDetails";
import { baseURL, fetcher } from "../Fetcher";
import { theme } from "../Styling/Theme";
import { Link } from "react-router-dom";
import { Artist } from "../Models/Artist";

export default function Account() {
    const [user, setUser] = useState<ListenerDetails>()
    const [artists, setArtists] = useState<Artist[]>([])

    useEffect(() => {
        fetcher.get("/Listener")
            .then((response) => setUser(response.data))
            .catch(error => console.error(error))
    }, [])

    useEffect(() => {
        if (!user)
            return;

        const fetchUsers = async () =>
        {
            const responses = await Promise.all(
                user.followingArtists.map((a) => fetcher.get(`/Artist/id/${a}`))
            )
            setArtists(responses.map(res => res.data));
        }

        fetchUsers();
    }, [user])

    return (
        <Stack margin={3}>
            <Stack direction='row' justifyContent='space-between' width='80vw' gap={3}>
                <Stack gap={3}>
                    <Stack direction='row' alignItems='center' gap={3}>
                        {
                            user ?
                                <Avatar src={`${baseURL}Image/${user?.profilePictureId}`} sx={{ width: '400px', height: `400px` }}></Avatar>
                                :
                                <Box sx={{ width: '400px', height: `400px` }}></Box>
                        }
                        <Stack>
                            <Typography variant='h2'>{user?.displayName}</Typography>
                            <Typography variant='h5'>{user?.firstName} {user?.lastName}</Typography>
                        </Stack>
                    </Stack>
                    <Stack>
                        <Typography variant='h3'>Bio</Typography>
                        <Typography variant='body1'>{user?.bio}</Typography>
                    </Stack>
                </Stack>

                <Stack gap={3}>
                    <UserList title="Friends" ids={user?.followingListeners ?? []}/>
                    <UserList title="Artists" ids={artists?.map(a => a.profilePictureId) ?? []}/>
                    <TrophyList />
                </Stack>
            </Stack>
        </Stack>
    );
}

function Ids(number: number, ids: string[]) {
    const stacks: string[] = [];
    for (let i = 0; i < number; i++) {
        stacks.push(ids[i] ?? "")
    }
    return stacks;
}

function ChunkArray(arr: string[], size: number) {
    const result = [];
    for (let i = 0; i < arr.length; i += size) {
        result.push(arr.slice(i, i + size));
    }
    return result;
}

function UserList({ title, ids }: { title: string, ids: string[] }) {
    return (
        <Stack sx={{ background: theme.palette.secondary.dark }} gap={3} padding={3} borderRadius={10}>
            <Stack direction='row' justifyContent='space-between' alignItems='center'>
                <Typography variant='h4'>{title}</Typography>
                <Typography component={Link} to="/TODO">See all</Typography>
            </Stack>
            <AvatarGroup max={6}>
                {
                    Ids(ids.length > 6 ? ids.length : 6, ids).map((id, index) =>
                        <Avatar key={index} sx={{ width: 80, height: 80, opacity:id? '100%' : '0%'}} src={id ? `${baseURL}Image/${id}` : ""}>Test</Avatar>
                    )
                }
            </AvatarGroup>
        </Stack>
    )
}

function TrophyList() {
    return (
        <Stack sx={{ background: theme.palette.secondary.dark }} gap={3} padding={3} borderRadius={10} direction='row' justifyContent='space-between'>
            <Stack alignItems='center' flexGrow={1} gap={3}>
                <Typography variant='h4'>Titles</Typography>
                <Stack gap={3}>
                    {
                        Ids(4, [/*every title ID*/]).map((_, id) => {
                            return <Typography key={id}>Test</Typography>
                        })
                    }
                </Stack>
            </Stack>
            <Stack alignItems='center' flexGrow={1} gap={3}>
                <Typography variant='h4'>Badges</Typography>
                <Stack gap={3}>
                    {
                        ChunkArray(Ids(4, [/*every badge ID*/]), 2).map((group, rowIndex) =>
                            <Box key={rowIndex} display='flex' gap={3}>
                                {
                                    group.map((id: string) => {
                                        return <Avatar key={id} sx={{ width: 80, height: 80 }}>Test</Avatar>
                                    })
                                }
                            </Box>
                        )
                    }
                </Stack>
            </Stack>
        </Stack>
    )
}

