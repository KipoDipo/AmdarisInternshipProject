export function TimeFormat(seconds: number) {
    seconds = Math.round(seconds);
    const min = Math.floor(seconds / 60);
    const sec = seconds % 60;

    return `${min}:${String(sec).padStart(2, '0')}`
}

export function DateTimeFormat(date: string) {
    const dateObj = new Date(date);

    const pad = (n: number) => n.toString().padStart(2, '0');

    const day = pad(dateObj.getDate());
    const month = pad(dateObj.getMonth() + 1);
    const year = dateObj.getFullYear();

    const hours = pad(dateObj.getHours());
    const minutes = pad(dateObj.getMinutes());
    const seconds = pad(dateObj.getSeconds());

    return `${day}/${month}/${year} ${hours}:${minutes}:${seconds}`;
}