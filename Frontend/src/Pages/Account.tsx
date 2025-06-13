import { Avatar, AvatarGroup, Button, Skeleton, Stack, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { fetcher } from "../Fetcher";
import { textWidth, theme } from "../Styling/Theme";
import { Link, redirect, useParams } from "react-router-dom";
import { Artist } from "../Models/Artist";
import { Badge } from "../Models/Badge";
import { Title } from "../Models/Title";
import AppBadge from "../Components/AppBadge";
import FetchImage from "../Utils/FetchImage";
import { Listener } from "../Models/Listener";
import { useListener } from "../Contexts/Listener/UseListener";
import { ListenerDetails } from "../Models/ListenerDetails";
import { useSetListener } from "../Contexts/Listener/UseSetListener";
import RefreshListener from "../Utils/RefreshListener";

export default function Account() {
    const { id } = useParams();

    const [artists, setArtists] = useState<Artist[] | undefined>(undefined)
    const [following, setFollowing] = useState<Listener[] | undefined>(undefined)

    const [isFollowing, setIsFollowing] = useState(false)

    const user = useListener()!;
    const setUser = useSetListener()!;

    const [accountHolder, setAccountHolder] = useState<ListenerDetails>();

    useEffect(() => {
        if (!setUser)
            return;
        
        RefreshListener(setUser);
    }, [setUser])

    useEffect(() => {
        if (!id || !user)
            return;

        const fetchIdHolder = async () => {
            try {
                const response = await fetcher.get(`Listener/details/${id}`);
                const accHolder: ListenerDetails = response.data;
                setAccountHolder(accHolder);
                
                const follows = await fetcher.get(`Listener/following-listeners/${accHolder.id}`)
                setFollowing(follows.data)
                setIsFollowing(user.followingListeners.includes(accHolder.id))
            }
            catch (error) {
                console.log(error);
                redirect('/')
            }
        }

        fetchIdHolder();
    }, [id, user])


    async function handleFollow() {
        if (isFollowing)
            unfollow();
        else
            follow();
    }

    async function follow() {
        if (!accountHolder)
            return;

        fetcher.post(`/Listener/follow-listener/${accountHolder.id}`);
        setIsFollowing(true);
    }

    async function unfollow() {
        if (!accountHolder)
            return;

        fetcher.post(`/Listener/unfollow-listener/${accountHolder.id}`);
        setIsFollowing(false);
    }


    useEffect(() => {
        if (!accountHolder)
            return;

        const fetchUsers = async () => {
            const responses = await Promise.all(
                accountHolder.followingArtists?.map((a) => fetcher.get(`/Artist/id/${a}`))
            )
            setArtists(responses.map(res => res.data) ?? []);
        }

        fetchUsers();
    }, [accountHolder])

    return (
        accountHolder && user &&
        <Stack margin={3}>
            <Stack direction='column' justifyContent='space-between' gap={3}>
                <Stack gap={3} padding={5} margin={-3} style={{
                    background: `linear-gradient(transparent, ${accountHolder.isSubscribed ? theme.palette.primary.dark : theme.palette.secondary.main} 30%, transparent)`
                }}>
                    <Stack direction='row' alignItems='center' gap={3}>
                        <AppBadge badgeId={accountHolder.equippedBadgeId}>
                            <Avatar src={FetchImage(accountHolder.profilePictureId)} sx={{ width: '300px', height: `300px` }}></Avatar>
                        </AppBadge>
                        <Stack alignItems='flex-start'>
                            <Typography variant='h2'>{accountHolder.displayName}</Typography>
                            <Typography variant='h5'>{accountHolder.firstName} {accountHolder.lastName}</Typography>
                            <Stack direction='row' gap={1} marginTop={3}>
                                <Typography component={Link} to={`/following-users/${accountHolder.id}`}>{accountHolder.followingListeners?.length} Following</Typography>
                                <Typography>{`•`}</Typography>
                                <Typography>{accountHolder.followedByCount} Followers</Typography>
                            </Stack>
                            {
                                accountHolder.id !== user.id &&
                                <Button sx={{ marginTop: 3, width: textWidth }} variant='contained' onClick={handleFollow}>{!isFollowing ? "Follow" : "Unfollow"}</Button>
                            }
                        </Stack>
                    </Stack>
                    <Stack gap={1}>
                        <Typography variant='h4'>Bio</Typography>
                        <Typography variant='body1' sx={{ whiteSpace: 'pre-line' }}>{accountHolder.bio}</Typography>
                    </Stack>
                </Stack>

                <Stack gap={3}>
                    {
                        following && following.length > 0 &&
                        <ListenerList title="Following" listeners={following ?? []} to={`/following-users/${accountHolder.id}`} />
                    }
                    {
                        artists && artists.length > 0 &&
                        <ArtistList title="Artists" to={`/following-artists/${accountHolder.id}`} artists={artists} />
                    }
                    <TrophyList badgeIds={accountHolder.badgeIds ?? []} titleIds={accountHolder.titleIds ?? []} />
                </Stack>
            </Stack>
        </Stack >
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
        <Stack sx={{ background: theme.palette.secondary.dark }} gap={3} padding={5} borderRadius={theme.shape.borderRadius} alignItems='flex-start'>
            <Stack direction='row' justifyContent='space-between' alignItems='center' width='100%'>
                <Typography variant='h4'>{title}</Typography>
                <Typography component={Link} to={to}>See all</Typography>
            </Stack>
            <AvatarGroup max={20}>
                {
                    PaddingArray(20, artists).map((artist, index) => {
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
        <Stack sx={{ background: theme.palette.secondary.dark }} gap={3} padding={5} borderRadius={theme.shape.borderRadius} alignItems='flex-start'>
            <Stack direction='row' justifyContent='space-between' alignItems='center' width='100%'>
                <Typography variant='h4'>{title}</Typography>
                <Typography component={Link} to={to}>See all</Typography>
            </Stack>
            <AvatarGroup max={20}>
                {
                    PaddingArray(20, listeners).map((listener, index) => {
                        return (
                            <Avatar
                                key={index}
                                component={listener ? Link : 'div'}
                                to={listener ? `/account/${listener.id}` : ''}
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
            <Stack sx={{ background: theme.palette.secondary.dark }} width='50%' gap={3} padding={5} borderRadius={theme.shape.borderRadius}>
                <Stack direction='row' justifyContent='space-between' alignItems='center'>
                    <Typography variant='h4'>Titles</Typography>
                    <Typography component={Link} to="/TODO">See all</Typography>
                </Stack>
                <Stack width='100%' alignItems='center' gap={3}>
                    {
                        titles ?
                            titles.slice(0, 3).map((title, index) => {
                                return <Typography key={index}>{title.name}</Typography>
                            })
                            :
                            new Array(3).fill(0).map((_, index) => <Skeleton key={index} variant='text' width={200} />)
                    }
                </Stack>
            </Stack>
            <Stack sx={{ background: theme.palette.secondary.dark }} width='50%' gap={3} padding={5} borderRadius={theme.shape.borderRadius}>
                <Stack direction='row' justifyContent='space-between' alignItems='center'>
                    <Typography variant='h4'>Badges</Typography>
                    <Typography component={Link} to="/my-badges">See all</Typography>
                </Stack>
                <Stack width='100%' alignItems='center' gap={3}>
                    {
                        badges ?
                            ChunkArray(badges?.slice(0, 4) ?? [], 9).map((badgeGroup, index) => {
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
                            ChunkArray(Array(14).fill(null), 7).map((_, index) => {
                                return (
                                    <Stack gap={3} direction='row' key={index}>
                                        {
                                            Array(7).fill(null).map((_, index) => {
                                                return <Skeleton key={index} variant='circular' width={64} height={64} />

                                            })
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

