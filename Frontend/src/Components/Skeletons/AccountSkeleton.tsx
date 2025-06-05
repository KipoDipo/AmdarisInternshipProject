import { Stack, Skeleton } from '@mui/material';

export default function UserProfileSkeleton() {
    return (
        <Stack margin={3}>
            <Stack direction="row" justifyContent="space-between" width="80vw" gap={3}>
                <Stack gap={3}>
                    <Stack direction="row" alignItems="center" gap={3}>
                        <Skeleton variant="circular" width={300} height={300} />

                        <Stack>
                            <Skeleton variant="text" width={300} height={60} />
                            <Skeleton variant="text" width={200} height={40} />
                        </Stack>
                    </Stack>

                    <Stack>
                        <Skeleton variant="text" width={100} height={40} />
                        <Skeleton variant="text" width="60%" />
                        <Skeleton variant="text" width="80%" />
                        <Skeleton variant="text" width="40%" />
                    </Stack>
                </Stack>

                <Stack gap={3}>
                    <Skeleton variant="rectangular" width={300} height={200} />
                    <Skeleton variant="rectangular" width={300} height={200} />
                    <Skeleton variant="rectangular" width={300} height={200} />
                </Stack>
            </Stack>
        </Stack>
    );
}