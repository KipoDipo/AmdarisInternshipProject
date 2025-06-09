import { Button, Divider, Stack, Typography } from "@mui/material";
import RegisterForm from "./RegisterForm";
import { RegisterUserRequest } from "../Models/RegisterUser";
import { Link, useNavigate } from "react-router-dom";
import axios from "axios";
import { textWidth, theme } from "../Styling/Theme";
import { useNotification } from "../Contexts/Snackbar/UseNotification";

type AccountType = "Listener" | "Artist" | "Distributor";

function Page() {
    const navigate = useNavigate();

    const notify = useNotification();

    async function createAccount(type: AccountType, form: RegisterUserRequest) {
        const formData = new FormData();

        let key: keyof RegisterUserRequest;
        for (key in form) {
            const value = form[key];
            if (value !== undefined)
                formData.append(key, value);
        }

        try {

            switch (type) {
                case "Listener":
                    await axios.post('https://localhost:7214/User/listener', formData);
                    break;
                case "Artist":
                    await axios.post('https://localhost:7214/User/artist', formData);
                    break;
                case "Distributor":
                    await axios.post('https://localhost:7214/User/distributor', formData);
                    break;
            }
            notify({ message: "Account created successfully!", severity: 'success' })
            navigate('/login')
        }
        catch (error) {
            notify({ message: error, severity: 'error', duration: 5000 })
        }
    }

    return (
        <Stack gap={7} >
            <Stack alignItems='center' gap={3}>
                <Typography variant='h4'>Sign up</Typography>
                <RegisterForm onSubmit={createAccount} />
            </Stack>
            <Divider sx={{ background: theme.palette.secondary.main }} />
            <Stack alignItems='center' gap={3}>
                <Typography variant='h4'>Already have an account?</Typography>
                <Button component={Link} to="/login" variant='contained' sx={{ width: textWidth }}>Login</Button>
            </Stack>
        </Stack>
    )
}

export default Page;