const apiUrl = import.meta.env.VITE_API_URL;

if (typeof apiUrl !== "string" || apiUrl.length === 0) {
	throw new Error(
		"VITE_API_URL is required. Copy .env.example to .env.local at the repo root and adjust if needed.",
	);
}

export const env = {
	apiUrl,
} as const;
