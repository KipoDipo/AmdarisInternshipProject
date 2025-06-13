import { fetcher } from "../Fetcher";
import { ListenerDetails } from "../Models/ListenerDetails";

export default function RefreshListener(setUser: (ld: ListenerDetails) => void) {
    fetcher.get('Listener/details')
        .then(response => {
            setUser(response.data)
        })
}