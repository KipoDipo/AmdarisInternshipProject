import { useContext } from "react";
import { QueueStateContext } from "./QueueStateContext";

export const useQueue = () => useContext(QueueStateContext)