import { ButtonBase } from "@mui/material";
import { ReactNode } from "react";

export default function ClickableBase({children}: {children: ReactNode}) {
    return (
        <ButtonBase
            sx={{
                transition: 'filter 0.3s ease, transform 0.2s',
                '&:hover': {
                    filter: 'brightness(1.3)',
                    transform: 'scale(0.95)'
                },
                '&:active': {
                    filter: 'brightness(1.5)',
                    transform: 'scale(0.9)'
                }
            }}>
            {children}
        </ButtonBase>
    )
}