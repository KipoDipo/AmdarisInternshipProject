import { useContext } from 'react';
import { SongUpdateContext } from './SongUpdateContext';

export const useSetSong = () => {
  const setSong = useContext(SongUpdateContext);
  if (setSong === undefined) {
    throw new Error("useSetSong must be used within a SongProvider");
  }
  return setSong;
};