import { createContext } from "react";
import { Song } from "../Models/Song";

export const SongUpdateContext = createContext<((song: Song) => void) | undefined>(undefined);
