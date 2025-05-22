export function TimeFormat(seconds: number) {
    seconds = Math.round(seconds);
    const min = Math.floor(seconds / 60);
    const sec = seconds % 60;

    return `${min}:${String(sec).padStart(2, '0')}`
}