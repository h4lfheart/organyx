export function formatTimestamp(value: string) {
	return new Date(value).toLocaleString(undefined, {
		dateStyle: "medium",
		timeStyle: "short",
	});
}

export function compareTimestamps(a: string | null | undefined, b: string | null | undefined) {
	return Date.parse(a ?? "") - Date.parse(b ?? "");
}
