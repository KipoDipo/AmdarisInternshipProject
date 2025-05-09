import { Stack, TextField, TextFieldProps } from "@mui/material";
import { textWidth } from "../Styling/Theme";

export function OptionalTextField(props: TextFieldProps) {
    return (
        <TextField {...props} sx={{ width: textWidth }} />
    )
}

export function RequiredTextField(props: TextFieldProps) {
    return (
        <Stack direction='row' alignItems='center' gap={3}>
            <TextField {...props} sx={{ width: textWidth }} required/>
            {/* <Typography>*Required</Typography> */}
        </Stack>
    )
}