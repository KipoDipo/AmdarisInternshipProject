import { useContext } from "react";
import { ListenerStateContext } from "./ListenerStateContext";

export const useListener = () => useContext(ListenerStateContext)