import { Skeleton, Stack } from "@mui/material";

export default function Component() {
    return (
        <Stack alignItems='center' sx={{ maxWidth: 200, textAlign: 'center', whiteSpace: 'normal' }}>
            <Skeleton variant='rounded' width={200} height={200} />
            <Skeleton variant='text' width={100} height={40} />
            <Skeleton variant='text' width={70} height={30} />
        </Stack>
    )
}