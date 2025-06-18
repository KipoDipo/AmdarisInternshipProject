import { Avatar, Stack, Typography } from "@mui/material";
import { Link } from "react-router-dom";
import FetchImage from "../Utils/FetchImage";
import { Listener } from "../Models/Listener";
import AppBadge from "./AppBadge";

export default function ArtistComponent({ listener }: { listener: Listener }) {
    return (
        <Stack gap={2} alignItems='center' component={Link} to={`/account/${listener.id}`}
            sx={{
                textDecoration: 'none',
                color: 'inherit',
                cursor: 'pointer',
                transition: 'transform 0.2s ease-in-out',
                ':hover': {
                    transform: 'scale(1.05)',
                }
            }}>
            <AppBadge badgeId={listener.badgeId}>
                <Avatar sx={{ width: 200, height: 200, boxShadow: 10 }} src={FetchImage(listener.profilePictureId)} />
            </AppBadge>
            <Typography width={200} textAlign='center' variant='h4'>{listener.displayName}</Typography>
        </Stack>
    )
}