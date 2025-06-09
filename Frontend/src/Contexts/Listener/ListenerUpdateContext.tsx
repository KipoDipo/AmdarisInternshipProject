import { createContext } from "react";
import { ListenerDetails } from "../../Models/ListenerDetails";

export const ListenerUpdateContext = createContext<React.Dispatch<React.SetStateAction<ListenerDetails | undefined>> | undefined>(undefined);