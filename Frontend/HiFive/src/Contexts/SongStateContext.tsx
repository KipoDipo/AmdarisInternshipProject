import { createContext } from "react";
import { Song } from "../Models/Song";

export const SongStateContext = createContext<Song | undefined>(undefined);
