import { QueryClient } from "@tanstack/react-query";
import { isAxiosError } from "axios";

function shouldRetry(failureCount: number, error: Error) {
	if (isAxiosError(error) && error.response && error.response.status < 500) {
		return false;
	}

	return failureCount < 1;
}

export function createQueryClient() {
	return new QueryClient({
		defaultOptions: {
			queries: {
				staleTime: 60_000,
				retry: shouldRetry,
			},
		},
	});
}
