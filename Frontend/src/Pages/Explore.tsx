import { useEffect, useState } from "react"
import { fetcher, fetchPaged } from "../Fetcher"
import { Stack, Typography } from "@mui/material";
import { Song } from "../Models/Song";
import SongCategory from "../Components/SongCategory";
import { Listener } from "../Models/Listener";
import { useListener } from "../Contexts/Listener/UseListener";
import { Shuffled } from "../Utils/Shuffle";
import FetchImage from "../Utils/FetchImage";

type GenreWithSongs = {
    name: string,
    songs: Song[]
}

type FolloweeWithSongs = Listener & {
    songs: Song[]
}

export default function Page() {
    const [categories, setCategories] = useState<GenreWithSongs[]>([])
    const [followees, setFollowees] = useState<FolloweeWithSongs[]>([])
    const [anyListens, setAnyListens] = useState(false)

    const user = useListener();

    useEffect(() => {
        setFollowees([])
        setCategories([])
        fetchPaged('Song/my-recommended', 1, 10, { "countPerGenre": 10 })
            .then(result => setCategories(result));

        fetcher.get(`Listener/following-listeners/${user?.id}`)
            .then(result => {
                const followees: Listener[] = result.data;
                const pageFollowees = Shuffled(followees).slice(0, 10);

                pageFollowees.forEach(followee => {
                    fetcher.get(`Song/history/${followee.id}`)
                        .then(result => {
                            const history: Song[] = result.data;
                            if (history.length > 0)
                                setAnyListens(true);
                            setFollowees(last => ([...last, { ...followee, songs: history }]));
                        });
                });
            })
    }, [user])

    return (
        <Stack gap={3} margin={3}>
            {
                categories.length > 0 &&
                <>
                    <Typography variant='h2' alignSelf='center'>Genres you listen to</Typography>
                    {
                        categories?.slice(0, 3).map(cat => {
                            return <SongCategory name={cat.name[0].toUpperCase() + cat.name.slice(1)} songs={cat.songs} />
                        })
                    }
                </>
            }
            {
                anyListens &&
                <>
                    <Typography variant='h2' alignSelf='center'>Users you follow listen to</Typography>
                    {
                        followees?.map(followee => {
                            return followee.songs.length > 0 ?
                                <SongCategory name={followee.displayName} imageUrl={FetchImage(followee.profilePictureId)} songs={followee.songs} to={`/account/${followee.id}`} />
                                :
                                <></>
                        })
                    }
                </>
            }

        </Stack>
    )
}