import { Button, Stack } from "@mui/material";
import { OptionalTextField } from "../Components/TextFields";
import { useEffect, useState } from "react";
import { fetcher } from "../Fetcher";
import { ListenerDetails } from "../Models/ListenerDetails";
import { textWidth } from "../Styling/Theme";
import { useNotification } from "../Contexts/Snackbar/UseNotification";

export default function Form() {
    const [displayName, setDisplayName] = useState("");
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [bio, setBio] = useState("");
    const [profilePicture, setProfilePicture] = useState<File>();

    const [user, setUser] = useState<ListenerDetails>()
    
    const notify = useNotification();

    useEffect(() => {
        fetcher.get("/Listener")
            .then((response) => setUser(response.data))
            .catch(error => notify({ message: error, severity: 'error' }))
    }, [])

    useEffect(() => {
        if (user === undefined)
            return;

        setDisplayName(user.displayName ?? "")
        setFirstName(user.firstName ?? "")
        setLastName(user.lastName ?? "")
        setBio(user.bio ?? "")
    }, [user])


    const handleImageFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files?.[0];
        if (file) {
            setProfilePicture(file);
        }
    };


    return (
        <Stack gap={3} margin={3} width={textWidth}>
            <OptionalTextField
                label="Display Name"
                value={displayName}
                onChange={(e) => setDisplayName(e.target.value)}
            />
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
                <Button fullWidth onClick={async () => {
                    const form = new FormData();

                    form.append('displayName', displayName);
                    form.append('firstName', firstName);
                    form.append('lastName', lastName);
                    form.append('bio', bio);
                    form.append('profilePicture', profilePicture as Blob);

                    try {
                        await fetcher.put('https://localhost:7214/Listener/update', form);
                        notify({message: "Updated successfully", severity: 'success'});
                    }
                    catch (error) {
                        notify({message: error, severity: 'error'});
                    }

                    setProfilePicture(undefined);
                }} variant="contained">Update</Button>
            </Stack>
        </Stack>
    )
}

