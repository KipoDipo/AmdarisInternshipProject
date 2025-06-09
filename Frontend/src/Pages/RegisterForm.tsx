import { Button, Stack } from "@mui/material";
import { RequiredTextField } from "../Components/TextFields";
import { Dispatch, SetStateAction, useState } from "react";
import { RegisterUserRequest } from "../Models/RegisterUser";
import { useForm } from "react-hook-form";

type FormFields = {
    email: string
    username: string
    displayName: string
    password: string
    repeat: string
}

function RegisterPage({ setForm, onSubmit }: { setForm: Dispatch<SetStateAction<RegisterUserRequest | undefined>>, onSubmit: () => void }) {
    const { register, handleSubmit, watch, formState: { isValid } } = useForm<FormFields>({ mode: 'onChange' })

    const password = watch('password')

    const onClick = (data: FormFields) => {
        setForm(
            {
                email: data.email,
                username: data.username,
                displayName: data.displayName,
                password: data.password,
            }
        )
        onSubmit();
    }

    return (
        <Stack gap={3} alignItems='center'>
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