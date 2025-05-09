import { ReactNode, useState } from 'react';
import { Song } from '../Models/Song';
import { SongStateContext } from './SongStateContext';
import { SongUpdateContext } from './SongUpdateContext';

export const SongProvider = ({ children }: { children: ReactNode }) => {
  const [song, setSong] = useState<Song | undefined>(undefined);

  return (
    <SongStateContext.Provider value={song}>
      <SongUpdateContext.Provider value={setSong}>
        {children}
      </SongUpdateContext.Provider>
    </SongStateContext.Provider>
  );
};