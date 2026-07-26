export function matchesTextSearch(
	query: string,
	...values: Array<string | null | undefined>
) {
	const normalized = query.trim().toLowerCase();
	if (!normalized) {
		return true;
	}

	return values.some((value) => value?.toLowerCase().includes(normalized));
}
