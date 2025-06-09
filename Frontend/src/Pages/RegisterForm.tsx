import { Button, Stack, Tab, Tabs, Typography } from "@mui/material";
import { RequiredTextField } from "../Components/TextFields";
import { useState } from "react";
import { RegisterUserRequest } from "../Models/RegisterUser";
import { useForm } from "react-hook-form";
import { theme } from "../Styling/Theme";

type FormFields = {
    email: string
    username: string
    displayName: string
    password: string
    repeat: string
}

type AccountType = "Listener" | "Artist" | "Distributor";

function RegisterPage({ onSubmit }: {onSubmit: (type: AccountType, form: RegisterUserRequest) => void }) {
    const { register, handleSubmit, watch, formState: { isValid } } = useForm<FormFields>({ mode: 'onChange' })

    const [type, setType] = useState(0)

    const handleChange = (_event: React.SyntheticEvent, newType: number) => {
        setType(newType);
    };

    const password = watch('password')

    const onClick = (data: FormFields) => {
        const form = 
            {
                email: data.email,
                username: data.username,
                displayName: data.displayName,
                password: data.password,
            }
        switch (type) {
            case 0:
                onSubmit("Listener", form);
                break;
            case 1:
                onSubmit("Artist", form);
                break;
            case 2:
                onSubmit("Distributor", form);
                break;
        }
    }

    return (
        <Stack gap={3} alignItems='center'>
            <Stack alignItems='center' gap={1}>
                <Typography>Type of account</Typography>
                <Tabs value={type} onChange={handleChange} sx={{ border: 1, borderColor: '#777', borderRadius: theme.shape.borderRadius }}>
                    <Tab label='Listener' />
                    <Tab label='Artist' />
                    <Tab label='Distributor' />
                </Tabs>
            </Stack>
            <RequiredTextField
                label="Email"
                {...register('email', { required: true })}
            />
            <RequiredTextField
                label="Username"
                {...register('username', { required: true })}
            />
            <RequiredTextField
                label="Display Name"
                {...register('displayName', { required: true })}
            />

            <RequiredTextField
                label="Password"
                type="password"
                {...register('password', { required: true, minLength: 8 })}
            />
            <RequiredTextField
                label="Repeat password"
                type="password"
                {...register('repeat', { required: true, minLength: 8, validate: value => value === password })}
            />
            <Button
                variant="contained"
                fullWidth
                type='submit'
                disabled={!isValid}
                onClick={handleSubmit(onClick)}
            >
                Sign up
            </Button>
        </Stack>
    )
}

export default RegisterPage;