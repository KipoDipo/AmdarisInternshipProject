import { baseURL } from "../Fetcher";

export default function FetchImage(imageId?: string) {
    if (!imageId) {
        return ``;
    }
    return `${baseURL}Image/${imageId}`
}