import { Avatar, Stack, Typography } from "@mui/material";
import { Artist } from "../Models/Artist";
import { Link } from "react-router-dom";
import FetchImage from "../Utils/FetchImage";

export default function ArtistComponent({ artist }: { artist: Artist }) {
    return (
        <Stack gap={2} alignItems='center' component={Link} to={`/artist/${artist.id}`}
            sx={{
                textDecoration: 'none',
                color: 'inherit',
                cursor: 'pointer',
                transition: 'transform 0.2s ease-in-out',
                ':hover': {
                    transform: 'scale(1.05)',
                }
            }}>
            <Avatar sx={{ width: 200, height: 200, boxShadow: 10 }} src={FetchImage(artist.profilePictureId)} />
            <Typography width={200} textAlign='center' variant='h4'>{artist.displayName}</Typography>
        </Stack>
    )
}