import type { Priority } from "#lib/types";

export const priorityOrder: Record<Priority, number> = {
	Low: 0,
	Medium: 1,
	High: 2,
	Urgent: 3,
};

export function taskNumber(key: string) {
	const value = Number(key.split("-").pop());
	return Number.isFinite(value) ? value : 0;
}

export function comparePriorities(a: Priority, b: Priority) {
	return priorityOrder[a] - priorityOrder[b];
}
