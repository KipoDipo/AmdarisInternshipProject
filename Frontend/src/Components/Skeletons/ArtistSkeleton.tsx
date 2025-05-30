import {
    Stack,
    Skeleton,
    Typography,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Paper
} from '@mui/material';

export default function ArtistProfileSkeleton() {
    return (
        <Stack margin={3}>
            <Stack direction='row' justifyContent='space-between' width='80vw' gap={3}>
                <Stack gap={3}>

                    <Stack direction='row' alignItems='center' gap={3}>
                        <Skeleton variant="circular" width={400} height={400} />

                        <Stack gap={3}>
                            <Skeleton variant="text" width={300} height={60} />
                            <Skeleton variant="rectangular" width={150} height={45} />
                        </Stack>
                    </Stack>

                    <Stack>
                        <Skeleton variant="text" width="60%" />
                        <Skeleton variant="text" width="80%" />
                        <Skeleton variant="text" width="40%" />
                    </Stack>

                    <Stack gap={3}>
                        <Skeleton variant="text" width={200} height={40} />
                        <Skeleton variant="rectangular" width="100%" height={150} />
                    </Stack>

                    <Stack gap={3}>
                        <Typography variant='h3'>Discography</Typography>
                        <TableContainer component={Paper}>
                            <Table sx={{ width: '80vw' }}>
                                <TableHead>
                                    <TableRow>
                                        <TableCell>Title</TableCell>
                                        <TableCell>Album</TableCell>
                                        <TableCell>Length</TableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {Array.from({ length: 5 }).map((_, i) => (
                                        <TableRow key={i}>
                                            <TableCell>
                                                <Stack direction='row' alignItems='center' gap={2}>
                                                    <Skeleton variant="rounded" width={40} height={40} />
                                                    <Skeleton variant="text" width={150} />
                                                </Stack>
                                            </TableCell>
                                            <TableCell>
                                                <Skeleton variant="text" width={100} />
                                            </TableCell>
                                            <TableCell>
                                                <Skeleton variant="text" width={50} />
                                            </TableCell>
                                        </TableRow>
                                    ))}
                                </TableBody>
                            </Table>
                        </TableContainer>
                    </Stack>

                </Stack>
            </Stack>
        </Stack>
    );
}