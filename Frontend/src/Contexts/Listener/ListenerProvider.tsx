import { ReactNode, useState } from "react";
import { ListenerStateContext } from "./ListenerStateContext";
import { ListenerUpdateContext } from "./ListenerUpdateContext";
import { ListenerDetails } from "../../Models/ListenerDetails";

export const ListenerProvider = ({ initialData, children }: { initialData?: ListenerDetails, children: ReactNode }) => {
    const [listener, setListener] = useState<ListenerDetails | undefined>(initialData);

    return (
        <ListenerStateContext.Provider value={listener}>
            <ListenerUpdateContext.Provider value={setListener}>
                {children}
            </ListenerUpdateContext.Provider>
        </ListenerStateContext.Provider>
    )
}