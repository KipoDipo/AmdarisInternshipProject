import { createContext } from "react";
import { Queue } from "../../Models/Queue";

export const QueueStateContext = createContext<Queue | undefined>(undefined)