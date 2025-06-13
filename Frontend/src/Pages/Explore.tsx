import { useEffect, useState } from "react"
import {  fetchPaged } from "../Fetcher"
import { Stack, Typography } from "@mui/material";
import { Song } from "../Models/Song";
import SongCategory from "../Components/SongCategory";

type GenreWithSongs = {
    name: string,
    songs: Song[]
}

export default function Page() {
    const [categories, setCategories] = useState<GenreWithSongs[]>()

    useEffect(() => {
        fetchPaged('Song/my-recommended', 1, 10, {"countPerGenre": 10})
        .then(result => setCategories(result));
    }, [])

    return (
        <Stack gap={3} margin={3}>
            <Typography variant='h2' alignSelf='center'>Genres you listen to</Typography>
            {
                categories?.slice(0, 3).map(cat => {
                    return <SongCategory name={cat.name[0].toUpperCase() + cat.name.slice(1)} songs={cat.songs}/>
                })
            }
        </Stack>
    )
}