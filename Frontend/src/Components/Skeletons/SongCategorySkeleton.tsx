import { Box, Skeleton, Stack } from "@mui/material";
import { theme } from "../../Styling/Theme";
import SongComponentSkeleton from "./SongComponentSkeleton";

export default function Category() {
    return (
        <Stack alignItems='flex-start' padding={4} gap={2} sx={{ background: theme.palette.secondary.dark, borderRadius: 10 }}>
            <Stack direction='row' alignItems='center' gap={2}>
                <Skeleton variant='text' width={200} height={50} />
            </Stack>
            <Box
                sx={{
                    overflowX: 'auto',
                    whiteSpace: 'nowrap',
                    width: '100%',
                }}>
                <Stack
                    direction='row'
                    gap={2}
                    marginTop={1}
                    marginBottom={2}
                    flexWrap='nowrap'
                >
                    {
                        new Array(6).fill(null).map((_, index) => {
                            return <SongComponentSkeleton key={index} />
                        })
                    }
                </Stack>
            </Box>
        </Stack>
    )
}
