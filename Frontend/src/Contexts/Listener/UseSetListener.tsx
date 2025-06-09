import { useContext } from "react";
import { ListenerUpdateContext } from "./ListenerUpdateContext";

export const useSetListener = () => useContext(ListenerUpdateContext)