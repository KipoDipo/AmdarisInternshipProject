import { createContext } from "react";
import { Queue } from "../../Models/Queue";

export const QueueUpdateContext = createContext<React.Dispatch<React.SetStateAction<Queue>> | undefined>(undefined);