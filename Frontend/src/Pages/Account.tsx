import { Avatar, AvatarGroup, Box, Skeleton, Stack, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { ListenerDetails } from "../Models/ListenerDetails";
import { fetcher } from "../Fetcher";
import { theme } from "../Styling/Theme";
import { Link } from "react-router-dom";
import { Artist } from "../Models/Artist";
import { useNotification } from "../Contexts/Snackbar/UseNotification";
import { Badge } from "../Models/Badge";
import { Title } from "../Models/Title";
import AppBadge from "../Components/AppBadge";
import FetchImage from "../Utils/FetchImage";
import { Listener } from "../Models/Listener";
import AccountSkeleton from "../Components/Skeletons/AccountSkeleton";

export default function Account() {
    const [user, setUser] = useState<ListenerDetails>()
    const [artists, setArtists] = useState<Artist[] | undefined>(undefined)
    const [friends, setFriends] = useState<Listener[] | undefined>(undefined)

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
            setArtists(responses.map(res => res.data) ?? []);
        }

        fetchUsers();
    }, [user])

    return (
        user && artists ?
            <Stack margin={3}>
                <Stack direction='row' justifyContent='space-between' gap={3}>
                    <Stack gap={3}>
                        <Stack direction='row' alignItems='center' gap={3}>
                            {
                                user ?
                                    <AppBadge badgeId={user.equippedBadgeId}>
                                        <Avatar src={FetchImage(user.profilePictureId)} sx={{ width: '400px', height: `400px` }}></Avatar>
                                    </AppBadge>
                                    :
                                    <Box sx={{ width: '400px', height: `400px` }}></Box>
                            }
                            <Stack>
                                <Typography variant='h2'>{user?.displayName}</Typography>
                                <Typography variant='h5'>{user?.firstName} {user?.lastName}</Typography>
                            </Stack>
                        </Stack>
                        <Stack width={'40vw'} gap={3} padding={3} bgcolor={theme.palette.secondary.dark} borderRadius={theme.shape.borderRadius}>
                            <Typography variant='h3'>Bio</Typography>
                            <Typography variant='body1'>{user?.bio}</Typography>
                        </Stack>
                    </Stack>

                    <Stack gap={3}>
                        {
                            friends && friends.length > 0 &&
                            <ListenerList title="Friends" listeners={friends ?? []} to='/TODO' />
                        }
                        {
                            artists && artists.length > 0 &&
                            <ArtistList title="Artists" to='/following-artists' artists={artists} />
                        }
                        <TrophyList badgeIds={user?.badgeIds ?? []} titleIds={user?.titleIds ?? []} />
                    </Stack>
                </Stack>
            </Stack>
            :
            <AccountSkeleton />
    );
}

function PaddingArray<T>(number: number, elements: T[]) {
    const stack: (T | null)[] = [];
    for (let i = 0; i < number; i++) {
        stack.push(elements[i] !== undefined ? elements[i] : null)
    }
    return stack;
}

function ChunkArray<T>(arr: T[], size: number) {
    const result = [];
    for (let i = 0; i < arr.length; i += size) {
        result.push(arr.slice(i, i + size));
    }
    return result;
}

function ArtistList({ title, artists, to }: { title: string, artists: Artist[], to: string }) {
    return (
        <Stack sx={{ background: theme.palette.secondary.dark }} gap={3} padding={3} borderRadius={theme.shape.borderRadius} alignItems='flex-start'>
            <Stack direction='row' justifyContent='space-between' alignItems='center' width='100%'>
                <Typography variant='h4'>{title}</Typography>
                <Typography component={Link} to={to}>See all</Typography>
            </Stack>
            <AvatarGroup max={6}>
                {
                    PaddingArray(6, artists).map((artist, index) => {
                        return (
                            <Avatar
                                key={index}
                                component={artist ? Link : 'div'}
                                to={artist ? `/artist/${artist.id}` : ''}
                                sx={{ width: 80, height: 80, opacity: artist ? '100%' : '0%' }}
                                src={artist ? FetchImage(artist.profilePictureId) : ''}
                            />
                        )
                    })
                }
            </AvatarGroup>
        </Stack>
    )
}

function ListenerList({ title, listeners, to }: { title: string, listeners: Listener[], to: string }) {
    return (
        <Stack sx={{ background: theme.palette.secondary.dark }} gap={3} padding={3} borderRadius={theme.shape.borderRadius} alignItems='flex-start'>
            <Stack direction='row' justifyContent='space-between' alignItems='center' width='100%'>
                <Typography variant='h4'>{title}</Typography>
                <Typography component={Link} to={to}>See all</Typography>
            </Stack>
            <AvatarGroup max={6}>
                {
                    PaddingArray(6, listeners).map((listener, index) => {
                        return (
                            <Avatar
                                key={index}
                                component={listener ? Link : 'div'}
                                to={listener ? `/listener/${listener.id}` : ''}
                                sx={{ width: 80, height: 80, opacity: listener ? '100%' : '0%' }}
                                src={(listener && listener.profilePictureId) ? FetchImage(listener.profilePictureId) : ''}
                            />
                        )
                    })
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
        <Stack direction='row' justifyContent='space-between' gap={3} height={270}>
            <Stack sx={{ background: theme.palette.secondary.dark }} gap={3} padding={3} borderRadius={theme.shape.borderRadius} flexGrow={1}>
                <Stack direction='row' justifyContent='space-between' alignItems='center'>
                    <Typography variant='h4'>Titles</Typography>
                    <Typography component={Link} to="/TODO">See all</Typography>
                </Stack>
                <Stack width='100%' alignItems='center' gap={3}>
                    {
                        titles ?
                            titles.map((title, index) => {
                                return <Typography key={index}>{title.name}</Typography>
                            })
                            :
                            new Array(3).fill(0).map((_, index) => <Skeleton key={index} variant='text' width={200} />)
                    }
                </Stack>
            </Stack>
            <Stack sx={{ background: theme.palette.secondary.dark }} gap={3} padding={3} borderRadius={theme.shape.borderRadius} flexGrow={1}>
                <Stack direction='row' justifyContent='space-between' alignItems='center'>
                    <Typography variant='h4'>Badges</Typography>
                    <Typography component={Link} to="/my-badges">See all</Typography>
                </Stack>
                <Stack width='100%' alignItems='center' gap={3}>
                    {
                        badges?
                            ChunkArray(badges?.slice(0, 4) ?? [], 2).map((badgeGroup, index) => {
                                return (
                                    <Stack gap={3} direction='row' key={index}>
                                        {
                                            badgeGroup.map(badge =>
                                                <Avatar key={badge.id} src={FetchImage(badge.imageId)} sx={{ width: 64, height: 64 }} />)
                                        }
                                    </Stack>
                                )
                            })
                            :
                            new Array(2).fill(0).map((_, index) => {
                                return (
                                    <Stack gap={3} direction='row' key={index}>
                                        <Skeleton variant='circular' width={64} height={64} />
                                        <Skeleton variant='circular' width={64} height={64} />
                                    </Stack>
                                )
                            })
                    }
                </Stack>
            </Stack>
        </Stack>
    )
}

