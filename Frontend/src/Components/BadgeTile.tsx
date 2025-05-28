import { Avatar, Stack, Typography } from "@mui/material";
import FetchImage from "../Utils/FetchImage";
import { ListenerBadge } from "../Models/ListenerBadge";

export default function Tile({ badge }: { badge: ListenerBadge }) {
    return (
        <Stack gap={2} alignItems='center' sx={{ textDecoration: 'none', color: 'inherit' }}>
            <Avatar sx={{ width: 150, height: 150, boxShadow: 10 }} src={FetchImage(badge.imageId)} />
            <Typography width={150} textAlign='center' variant='h6'>{badge.name}</Typography>
        </Stack>
    )
}