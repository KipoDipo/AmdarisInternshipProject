import { Avatar, Box, Dialog, Stack, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { fetcher } from "../Fetcher";
import { useNotification } from "../Contexts/Snackbar/UseNotification";
import BadgeTile from "../Components/BadgeTile";
import { ListenerBadge } from "../Models/ListenerBadge";
import FetchImage from "../Utils/FetchImage";
import { DateTimeFormat } from "../Utils/TimeFormat";

export default function Badges() {
    const [badges, setBadges] = useState<ListenerBadge[] | null>(null);

    const [badgeInfo, setBadgeInfo] = useState<ListenerBadge | null>(null);

    function openDialogue(badge: ListenerBadge) {
        setBadgeInfo(badge);
    }

    const notify = useNotification();

    useEffect(() => {
        fetcher.get("Trophy/my-badges")
            .then((response) => {
                setBadges(response.data);
            })
            .catch(error => notify({ message: error, severity: 'error' }));
    })

    return (
        <>
            <Stack width='100%' display='flex' justifyContent='center'>
                <Typography variant='h2' alignSelf='flex-start' textAlign='center' margin={3}>Badges</Typography>
                <Box margin={3} gap={8} display='flex' flexWrap='wrap' justifyContent='center'>
                    {
                        badges?.map(badge => {
                            return (
                                <Box key={badge.id} onClick={() => openDialogue(badge)}>
                                    <BadgeTile badge={badge} />
                                </Box>
                            );
                        })
                    }
                </Box>
            </Stack>

            <Dialog open={badgeInfo !== null} onClose={() => setBadgeInfo(null)} fullWidth>
                {
                    badgeInfo &&
                    <Stack gap={3} margin={3} alignItems='center'>
                        <Avatar src={FetchImage(badgeInfo.imageId)} sx={{width: 200, height: 200, boxShadow: 10}}/>
                        <Typography variant='h3'>{badgeInfo.name}</Typography>
                        <Typography variant='h5'>{badgeInfo.description}</Typography>
                        <Typography variant='h6'>Awarded on: {DateTimeFormat(badgeInfo.awardedOn)}</Typography>
                    </Stack>
                }
            </Dialog>
        </>
    )
}