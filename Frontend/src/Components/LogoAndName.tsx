import { Avatar, Box, Typography } from "@mui/material";

export default function LogoAndName() {
    return (
        <>
            <Avatar src="/logo.png" sx={{ width: 150, height: 150, boxShadow: 5 }} />
            <Typography variant="h2">Hi-Five</Typography>
            <Box height={50} />
        </>
    );
}
