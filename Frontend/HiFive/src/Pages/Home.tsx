import { Stack} from "@mui/material";

import { useEffect, useState } from "react";

import { Song } from "../Models/Song";
import { fetcher } from "../Fetcher";
import SongCategory from "../Components/SongCategory";

function Home() {
  const [songs, setSongs] = useState<Song[]>([]);

  useEffect(() => {
    fetcher.get('Song')
      .then((response) => {
        setSongs(response.data);
      })
      .catch((error) => {
        console.error('Error fetching data:', error);
      });
    }, [])

  return (
    <Stack margin={3}>
      <SongCategory name="Songs in the DB" songs={songs} />
    </Stack>
  );
}

export default Home;