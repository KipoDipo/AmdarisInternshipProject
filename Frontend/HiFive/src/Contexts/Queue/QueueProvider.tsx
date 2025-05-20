import { ReactNode, useState } from "react";
import { QueueStateContext } from "./QueueStateContext";
import { QueueUpdateContext } from "./QueueUpdateContext";
import { Queue } from "../../Models/Queue";
import { CreateQueue } from "../../Utils/QueueUtils";

export const QueueProvider = ({ children }: { children: ReactNode }) => {
    const [queue, setQueue] = useState<Queue>(CreateQueue([]));

    return (
        <QueueStateContext.Provider value={queue}>
            <QueueUpdateContext.Provider value={setQueue}>
                {children}
            </QueueUpdateContext.Provider>
        </QueueStateContext.Provider>
    )
}