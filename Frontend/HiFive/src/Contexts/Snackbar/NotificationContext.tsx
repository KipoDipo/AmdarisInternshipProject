import { AlertColor } from "@mui/material";
import { createContext } from "react";

export type NotificationOptions = {
    message: string | Error | unknown;
    severity?: AlertColor;
    duration?: number;
};

export type NotificationContextType = {
    notify: (options: NotificationOptions) => void;
}

export const NotificationContext = createContext<NotificationContextType | undefined>(undefined);
