import { Avatar, AvatarGroup, Box, Stack, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { ListenerDetails } from "../Models/ListenerDetails";
import { baseURL, fetcher } from "../Fetcher";
import { theme } from "../Styling/Theme";
import { Link } from "react-router-dom";
import { Artist } from "../Models/Artist";
import { useNotification } from "../Contexts/Snackbar/UseNotification";
import { Badge } from "../Models/Badge";
import { Title } from "../Models/Title";

export default function Account() {
    const [user, setUser] = useState<ListenerDetails>()
    const [artists, setArtists] = useState<Artist[]>([])

    const notify = useNotification();

    useEffect(() => {
        fetcher.get("/Listener")
            .then((response) => setUser(response.data))
            .catch(error => notify({ message: error, severity: 'error' }))

    }, [notify])

    useEffect(() => {
        if (!user)
            return;

        const fetchUsers = async () => {
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
                    <UserList title="Friends" ids={user?.followingListeners ?? []} />
                    <UserList title="Artists" ids={artists?.map(a => a.profilePictureId) ?? []} />
                    <TrophyList badgeIds={user?.badgeIds ?? []} titleIds={user?.titleIds ?? []} />
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

function ChunkArray<T>(arr: T[], size: number) {
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
                        <Avatar key={index} sx={{ width: 80, height: 80, opacity: id ? '100%' : '0%' }} src={id ? `${baseURL}Image/${id}` : ""}>Test</Avatar>
                    )
                }
            </AvatarGroup>
        </Stack>
    )
}

function TrophyList({ badgeIds, titleIds }: { badgeIds: string[], titleIds: string[] }) {
    const [titles, setTitles] = useState<Title[] | null>(null);
    const [badges, setBadges] = useState<Badge[] | null>(null);


    useEffect(() => {
        async function fetchTitles(): Promise<Title[]> {
            const requests = titleIds.map(id => fetcher.get(`Trophy/get-title/${id}`));
            const responses = await Promise.all(requests);
            return responses.map(res => res.data).reverse();
        }
        async function fetchBadges(): Promise<Badge[]> {
            const requests = badgeIds.map(id => fetcher.get(`Trophy/get-badge/${id}`));
            const responses = await Promise.all(requests);
            return responses.map(res => res.data).reverse();
        }

        async function fetchData() {
            setTitles(await fetchTitles());
            setBadges(await fetchBadges());
        }

        fetchData();
    }, [titleIds, badgeIds])

    return (
        <Stack direction='row' justifyContent='space-between' gap={3}>
            <Stack sx={{ background: theme.palette.secondary.dark }} gap={3} padding={3} borderRadius={10} flexGrow={1}>
                <Stack direction='row' justifyContent='space-between' alignItems='center'>
                    <Typography variant='h4'>Titles</Typography>
                    <Typography component={Link} to="/TODO">See all</Typography>
                </Stack>
                <Stack width='100%' alignItems='center' gap={3}>
                    {
                        titles?.map((title, index) => {
                            return <Typography key={index}>{title.name}</Typography>
                        })
                    }
                </Stack>
            </Stack>
            <Stack sx={{ background: theme.palette.secondary.dark }} gap={3} padding={3} borderRadius={10} flexGrow={1}>
                <Stack direction='row' justifyContent='space-between' alignItems='center'>
                    <Typography variant='h4'>Badges</Typography>
                    <Typography component={Link} to="/TODO">See all</Typography>
                </Stack>
                <Stack width='100%' alignItems='center' gap={3}>
                    {
                        ChunkArray(badges?.slice(0, 4) ?? [], 2).map((badgeGroup, index) => {
                            return (
                                <Stack gap={3} direction='row' key={index}>
                                    {
                                        badgeGroup?.map(badge =>
                                            <Avatar key={badge.id} src={`${baseURL}Image/${badge.imageId}`} sx={{ width: 64, height: 64 }} />)
                                    }
                                </Stack>
                            )
                        })
                    }
                </Stack>
            </Stack>
        </Stack>
    )
}

