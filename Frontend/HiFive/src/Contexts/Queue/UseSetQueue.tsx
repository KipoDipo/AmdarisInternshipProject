import { useContext } from "react"
import { QueueUpdateContext } from "./QueueUpdateContext"

export const useSetQueue = () => {
    const setQueue = useContext(QueueUpdateContext)
    if (!setQueue) {
        throw new Error("useSetQueue must be used within a QueueProvider")
    }
    return setQueue;
}