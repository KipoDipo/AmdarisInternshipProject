import { useContext } from "react";
import { SongStateContext } from "./SongStateContext";

export const useSong = () => useContext(SongStateContext);
