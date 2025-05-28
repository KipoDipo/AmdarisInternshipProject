import { baseURL } from "../Fetcher";

export default function FetchImage(imageId: string) {
    return `${baseURL}Image/${imageId}`
}