import { createContext, useContext, useState, ReactNode } from 'react';
import { Song } from '../Models/Song';

const SongStateContext = createContext<Song>(null);
const SongUpdateContext = createContext<((song: Song) => void) | undefined>(undefined);

export const SongProvider = ({ children }: { children: ReactNode }) => {
  const [song, setSong] = useState<Song>(null);

  return (
    <SongStateContext.Provider value={song}>
      <SongUpdateContext.Provider value={setSong}>
        {children}
      </SongUpdateContext.Provider>
    </SongStateContext.Provider>
  );
};

export const useSong = () => useContext(SongStateContext);
export const useSetSong = () => {
  const setSong = useContext(SongUpdateContext);
  if (setSong === undefined) {
    throw new Error("useSetSong must be used within a SongProvider");
  }
  return setSong;
};