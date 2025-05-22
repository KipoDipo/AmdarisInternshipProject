import { Button, Stack } from "@mui/material";
import { OptionalTextField } from "../Components/TextFields";
import { Dispatch, SetStateAction, useState } from "react";
import { RegisterListenerRequest } from "../Models/RegisterListener";

function RegisterPage({ setForm, onSubmit }: { setForm: Dispatch<SetStateAction<RegisterListenerRequest | undefined>>, onSubmit: () => void }) {
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [bio, setBio] = useState("");
    const [profilePicture, setProfilePicture] = useState<File>();

    const handleImageFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files?.[0];
        if (file) {
            setProfilePicture(file);
        }
    };

    return (
        <Stack gap={3}>
            <OptionalTextField
                label="First Name"
                value={firstName}
                onChange={(e) => setFirstName(e.target.value)}
            />
            <OptionalTextField
                label="Last Name"
                value={lastName}
                onChange={(e) => setLastName(e.target.value)}
            />
            <OptionalTextField
                label="Bio"
                multiline
                rows={5}
                value={bio}
                onChange={(e) => setBio(e.target.value)}
            />
            <OptionalTextField
                label="Profile Picture"
                type='file'
                slotProps={{
                    inputLabel: {
                        shrink: true,
                    }
                }}
                onChange={handleImageFileChange}
            />
            <Stack direction='row' gap={3}>
                <Button fullWidth onClick={() => {
                    setForm((last: any) => (
                        {
                            ...last,
                            firstName,
                            lastName,
                            bio,
                            profilePicture,
                        }
                    ))
                    onSubmit();
                }} variant="contained">Continue</Button>
                <Button fullWidth onClick={onSubmit}>Skip</Button>
            </Stack>
        </Stack>
    )
}

export default RegisterPage;