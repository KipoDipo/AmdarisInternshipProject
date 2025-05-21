import { Alert, AlertColor, Snackbar } from "@mui/material";
import { ReactNode, useCallback, useState } from "react";
import { NotificationContext, NotificationOptions } from "./NotificationContext";
import axios from "axios";

function extractMessage(input: unknown): string {
    if (typeof input === 'string') return input;

    console.error(input);
    if (axios.isAxiosError(input)) {
        if (input.response?.data) {
            return JSON.stringify(input.response.data, null, 2);
        }
        return input.message;
    }
    if (input instanceof Error) {
        return input.message;
    }
    try {
        return JSON.stringify(input);
    } catch {
        return 'An unknown error occurred';
    }
}

export const NotificationProvider = ({ children }: { children: ReactNode }) => {
    const [open, setOpen] = useState(false);
    const [message, setMessage] = useState("");
    const [severity, setSeverity] = useState<AlertColor>('info');
    const [duration, setDuration] = useState<number | null>();

    const notify = useCallback((options: NotificationOptions) => {
        const { message, severity = 'info', duration } = options;
        setMessage(extractMessage(message));
        setSeverity(severity);
        setDuration(duration);
        setOpen(true);
    }, []);

    const handleClose = (_event?: React.SyntheticEvent | Event, reason?: string) => {
        if (reason === 'clickaway')
            return;
        setOpen(false);
    };

    return (
        <NotificationContext.Provider value={{ notify }}>
            {children}
            <Snackbar
                open={open}
                autoHideDuration={severity == 'error' ? (duration ?? null) : duration ?? 3000}
                onClose={handleClose}
                anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
                sx={{ marginBottom: 15 }}
            >
                <Alert onClose={handleClose} severity={severity} variant='filled'  sx={{ width: '100%', whiteSpace: 'pre-line' }}>
                    {message}
                </Alert>
            </Snackbar>
        </NotificationContext.Provider>
    );
};

