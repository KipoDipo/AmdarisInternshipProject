import { Avatar, Box, Button, FormControl, InputLabel, MenuItem, Select, Stack, Typography } from "@mui/material";
import { OptionalTextField } from "../Components/TextFields";
import { useEffect, useState } from "react";
import { fetcher } from "../Fetcher";
import { ListenerDetails } from "../Models/ListenerDetails";
import { textWidth, theme } from "../Styling/Theme";
import { useNotification } from "../Contexts/Snackbar/UseNotification";
import { Controller, useForm } from "react-hook-form";
import { Badge } from "../Models/Badge";
import { Title } from "../Models/Title";
import FetchImage from "../Utils/FetchImage";

type FormData = {
    displayName: string
    firstName: string
    lastName: string
    bio: string
    equippedBadgeId: string,
    equippedTitleId: string,
    profilePicture: FileList
}

export default function Form() {
    const { control, register, handleSubmit, reset } = useForm<FormData>();

    const [user, setUser] = useState<ListenerDetails>()
    const [badges, setBadges] = useState<Badge[]>();
    const [titles, setTitles] = useState<Title[]>();

    const notify = useNotification();

    useEffect(() => {
        fetcher.get("/Listener/details")
            .then((response) => setUser(response.data))
            .catch(error => notify({ message: error, severity: 'error' }))
    }, [notify])

    useEffect(() => {
        if (!user)
            return;

        const fetchTitles = async (): Promise<Title[]> => {
            const requests = user.titleIds.map(id => fetcher.get(`Trophy/get-title/${id}`));
            const responses = await Promise.all(requests);
            return responses.map(res => res.data).reverse();
        }

        const fetchBadges = async (): Promise<Badge[]> => {
            const requests = user.badgeIds.map(id => fetcher.get(`Trophy/get-badge/${id}`));
            const responses = await Promise.all(requests);
            return responses.map(res => res.data).reverse();
        }

        const fetchData = async () => {
            setTitles(await fetchTitles());
            setBadges(await fetchBadges());
        }

        fetchData();

        reset({
            displayName: user.displayName ?? "",
            firstName: user.firstName ?? "",
            lastName: user.lastName ?? "",
            bio: user.bio ?? "",
            profilePicture: undefined,
            equippedBadgeId: user.equippedBadgeId,
            equippedTitleId: user.equippedTitleId
        })
    }, [user, reset])

    const onSubmit = async (data: FormData) => {
        const form = new FormData();

        form.append('displayName', data.displayName);
        form.append('firstName', data.firstName);
        form.append('lastName', data.lastName);
        if (data.equippedBadgeId)
            form.append('equippedBadgeId', data.equippedBadgeId);
        if (data.equippedTitleId)
            form.append('equippedTitleId', data.equippedTitleId);
        form.append('bio', data.bio);
        if (data.profilePicture)
            form.append('profilePicture', data.profilePicture[0] as Blob);
        try {
            await fetcher.put('Listener/update', form);
            notify({ message: "Updated successfully", severity: 'success' });
        }
        catch (error) {
            notify({ message: error, severity: 'error' });
        }
        reset({ ...data, profilePicture: undefined })
    }

    return (
        <Box width='100%' display='flex' justifyContent='center'>
            {
                user &&
                <Stack gap={3} padding={5} margin={3} width={textWidth} bgcolor={theme.palette.secondary.dark} borderRadius={theme.shape.borderRadius}>
                    <FormControl fullWidth>
                        <InputLabel>Select Displayed Badge</InputLabel>
                        <Controller
                            control={control}
                            name='equippedBadgeId'
                            defaultValue={user.equippedBadgeId ?? ""}
                            render={({ field }) =>
                                <Select
                                    {...field}
                                    label="Select Dispalyed Badge"
                                    MenuProps={{ PaperProps: { style: { maxHeight: '70vh' } } }}
                                >
                                    {
                                        badges?.map((b) =>
                                            <MenuItem key={b.id} value={b.id}>
                                                <Stack direction='row' gap={3} alignItems='center'>
                                                    <Avatar src={FetchImage(b.imageId)} variant='rounded' />
                                                    <Typography>{b.name}</Typography>
                                                </Stack>
                                            </MenuItem>
                                        )
                                    }
                                </Select>

                            }
                        />
                    </FormControl>
                    <FormControl fullWidth>
                        <InputLabel>Select Equipped Title</InputLabel>
                        <Controller
                            control={control}
                            name='equippedTitleId'
                            render={({ field }) =>
                                <Select
                                    {...field}
                                    label="Select Equpped Title"
                                    defaultValue={user?.equippedTitleId ?? ""}
                                    MenuProps={{ PaperProps: { style: { maxHeight: '70vh' } } }}
                                >
                                    {
                                        titles?.map((t) =>
                                            <MenuItem key={t.id} value={t.id}>
                                                <Stack direction='row' gap={3} alignItems='center'>
                                                    <Typography>{t.name}</Typography>
                                                </Stack>
                                            </MenuItem>
                                        )
                                    }
                                </Select>

                            }
                        />
                    </FormControl>

                    <OptionalTextField
                        label="Display Name"
                        {...register('displayName')}
                        slotProps={{ inputLabel: { shrink: true } }}
                    />
                    <OptionalTextField
                        label="First Name"
                        {...register('firstName')}
                        slotProps={{ inputLabel: { shrink: true } }}
                    />
                    <OptionalTextField
                        label="Last Name"
                        {...register('lastName')}
                        slotProps={{ inputLabel: { shrink: true } }}
                    />
                    <OptionalTextField
                        label="Bio"
                        multiline
                        rows={5}
                        {...register('bio')}
                        slotProps={{ inputLabel: { shrink: true } }}
                    />
                    <OptionalTextField
                        label="Profile Picture"
                        type='file'
                        slotProps={{ inputLabel: { shrink: true } }}
                        {...register('profilePicture')}
                    />
                    <Stack direction='row' gap={3}>
                        <Button fullWidth onClick={handleSubmit(onSubmit)} variant="contained">Update</Button>
                    </Stack>
                </Stack>
            }
        </Box>
    )
}

