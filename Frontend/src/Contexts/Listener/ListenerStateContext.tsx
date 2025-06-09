import { createContext } from "react";
import { ListenerDetails } from "../../Models/ListenerDetails";

export const ListenerStateContext = createContext<ListenerDetails | undefined>(undefined)