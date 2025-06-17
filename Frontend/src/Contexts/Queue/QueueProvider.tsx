import { ReactNode, useState } from "react";
import { QueueStateContext } from "./QueueStateContext";
import { QueueUpdateContext } from "./QueueUpdateContext";
import { Queue } from "../../Models/Queue";
import { CreateQueue } from "../../Utils/QueueUtils";

export const QueueProvider = ({ children }: { children: ReactNode }) => {
    const [queue, setQueue] = useState<Queue>((localStorage.getItem('queue') && localStorage.getItem('queue') !== 'undefined') ? JSON.parse(localStorage.getItem('queue')!) : CreateQueue([]));

    function setQueueProxy(newQueue: React.SetStateAction<Queue>) {
        setQueue(newQueue);
        // check if this is a function or a value
        if (typeof newQueue === 'function') {
            newQueue = newQueue(queue);
        }
        localStorage.setItem('queue', JSON.stringify(newQueue))
    }

    return (
        <QueueStateContext.Provider value={queue}>
            <QueueUpdateContext.Provider value={setQueueProxy}>
                {children}
            </QueueUpdateContext.Provider>
        </QueueStateContext.Provider>
    )
}