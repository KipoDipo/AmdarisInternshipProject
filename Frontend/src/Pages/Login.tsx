import { Button, Stack, Typography } from "@mui/material";
import { RequiredTextField } from "../Components/TextFields";
import { useState } from "react";
import axios from "axios";
import { Link, useNavigate } from "react-router-dom";
import { textWidth } from "../Styling/Theme";
import { useNotification } from "../Contexts/Snackbar/UseNotification";

export default function Page({ setLogged }: ({ setLogged: (logged: boolean) => void })) {
    return (
        <Stack direction='row' gap={10} >
            <Stack alignItems='center' gap={3}>
                <Typography variant='h4'>Log in</Typography>
                <Form setLogged={setLogged} />
            </Stack>
            <Stack alignItems='center' gap={3}>
                <Typography variant='h4'>Don't have an account?</Typography>
                <Button component={Link} to="/register" variant='contained' sx={{ width: textWidth }}>Sign up</Button>
            </Stack>
        </Stack>
    )
};

function Form({ setLogged }: ({ setLogged: (logged: boolean) => void })) {
    const [email, setEmail] = useState("")
    const [password, setPassword] = useState("")

    const navigate = useNavigate();

    const notify = useNotification();

    async function handleClick() {
        try {

            const response = await axios.post("https://localhost:7214/Listener/login", {
                email,
                password
            });
            if (response.status == 200 && response.data.token) {
                localStorage.setItem("token", response.data.token);
                setLogged(true);
                navigate('/');
            }
        }
        catch (error) {
            notify({message: error, severity: 'error', duration: 2000})
        }
    }

    return (
        <Stack>
            <Stack gap={3} alignItems='center'>
                <RequiredTextField
                    label="Email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                />
                <RequiredTextField
                    label="Password"
                    type='password'
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                />
                <Button
                    variant='contained'
                    onClick={handleClick}
                >
                    Log in
                </Button>
            </Stack>
        </Stack>
    )
}

