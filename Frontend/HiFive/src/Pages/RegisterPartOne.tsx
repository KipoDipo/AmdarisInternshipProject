import { Button, Stack } from "@mui/material";
import { RequiredTextField } from "../Components/TextFields";
import { Dispatch, SetStateAction, useState } from "react";
import { RegisterListenerRequest } from "../Models/RegisterListener";

function RegisterPage({ setForm, onSubmit }: { setForm: Dispatch<SetStateAction<RegisterListenerRequest | undefined>>, onSubmit: () => void }) {

    const [emailError, setEmailError] = useState(false);
    const [usernameError, setUsernameError] = useState(false);
    const [displayNameError, setDisplayNameError] = useState(false);
    const [passwordError, setPasswordError] = useState(false);
    const [repeatError, setRepeatError] = useState(false);

    const [email, setEmail] = useState("")
    const [username, setUsername] = useState("")
    const [displayName, setDisplayName] = useState("")
    const [password, setPassword] = useState("")
    const [repeat, setRepeat] = useState("")

    return (
        <Stack gap={3} alignItems='center'>
            <RequiredTextField
                label="Email"
                error={emailError}
                value={email}
                onChange={(e) => { setEmail(e.target.value); setEmailError(false) }}
                onBlur={(e) => setEmailError(e.target.value == "")}

            />
            <RequiredTextField
                label="Username"
                error={usernameError}
                value={username}
                onChange={(e) => { setUsername(e.target.value); setUsernameError(false) }}
                onBlur={(e) => setUsernameError(e.target.value == "")}
            />
            <RequiredTextField
                label="Display Name"
                error={displayNameError}
                value={displayName}
                onChange={(e) => { setDisplayName(e.target.value); setDisplayNameError(false) }}
                onBlur={(e) => setDisplayNameError(e.target.value == "")}
            />

            <RequiredTextField
                label="Password"
                type="password"
                error={passwordError}
                value={password}
                onChange={(e) => { setPassword(e.target.value); setPasswordError(false) }}
                onBlur={(e) => setPasswordError(e.target.value == "")}
            />
            <RequiredTextField
                label="Repeat password"
                type="password"
                error={repeatError}
                value={repeat}
                onChange={(e) => { setRepeat(e.target.value); setRepeatError(false) }}
                onBlur={(e) => {
                    setRepeatError(e.target.value == "")
                    if (e.target.value != password)
                        setRepeatError(true)
                }}
            />
            <Button
                variant="contained"
                fullWidth
                type='submit'
                disabled={
                    emailError || usernameError || passwordError || repeatError ||
                    email.length == 0 || username.length == 0 || password.length == 0 || repeat.length == 0
                }
                onClick={() => {
                    setForm(
                        {
                            email,
                            username,
                            displayName,
                            password,
                        }
                    )
                    onSubmit();
                }}
            >
                Sign up
            </Button>
        </Stack>
    )
}

export default RegisterPage;