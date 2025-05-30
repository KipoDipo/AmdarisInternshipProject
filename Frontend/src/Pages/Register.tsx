import { Button, Divider, Stack, Typography } from "@mui/material";
import RegisterPartOne from "./RegisterPartOne";
// import RegisterPartTwo from "./RegisterPartTwo";
import { useEffect, useState } from "react";
import { RegisterListenerRequest } from "../Models/RegisterListener";
import { Link, useNavigate } from "react-router-dom";
import axios from "axios";
import { textWidth, theme } from "../Styling/Theme";
import { useNotification } from "../Contexts/Snackbar/UseNotification";

function Page() {
    const [hasFilledRequiredInfo, setHasFilledRequiredInfo] = useState(false);
    // const [hasFilledOptionalInfo, setHasFilledOptionalInfo] = useState(false);

    const [form, setForm] = useState<RegisterListenerRequest>()

    const navigate = useNavigate();

    const notify = useNotification();

    useEffect(() => {
        // if (hasFilledRequiredInfo && hasFilledOptionalInfo) {
        if (hasFilledRequiredInfo) {
            if (form == undefined)
                return;

            const formData = new FormData();

            let key: keyof RegisterListenerRequest;
            for (key in form) {
                const value = form[key];
                if (value !== undefined)
                    formData.append(key, value);
            }
            axios.post('https://localhost:7214/User/listener', formData)
                .then(() => {
                    notify({ message: "Account created successfully!", severity: 'success' })
                    navigate('/login')
                })
                .catch((error) => { notify({ message: error, severity: 'error', duration: 5000 }); setHasFilledRequiredInfo(false); })
        }
    }, [hasFilledRequiredInfo, form, navigate, notify])

    return (
        <Stack gap={7} >
            <Stack alignItems='center' gap={3}>
                <Typography variant='h4'>Sign up</Typography>
                <RegisterPartOne setForm={setForm} onSubmit={() => setHasFilledRequiredInfo(true)} />
            </Stack>
            <Divider sx={{background: theme.palette.secondary.main}}/>
            <Stack alignItems='center' gap={3}>
                <Typography variant='h4'>Already have an account?</Typography>
                <Button component={Link} to="/login" variant='contained' sx={{ width: textWidth }}>Login</Button>
            </Stack>
        </Stack>
    )
}

export default Page;