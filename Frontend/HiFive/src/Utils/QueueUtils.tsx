import { Queue } from "../Models/Queue";
import { Song } from "../Models/Song";

export function CreateQueue(songs: Song[]): Queue {
    return {
        songs: songs,
        current: 0
    }
}

export function AddSongToQueue(queue: Queue, song: Song): Queue {
    return {
        ...queue,
        songs: [...queue.songs, song],
    }
}