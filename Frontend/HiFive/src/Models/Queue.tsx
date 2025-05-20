import { Song } from "./Song"

export type Queue = {
    songs: Song[]
    current: number
}