import { type ErrorComponentProps, isNotFound } from "@tanstack/react-router";

import { ErrorState } from "#components/shared/error-state";

export function RouteError({ error }: ErrorComponentProps) {
	if (isNotFound(error)) {
		throw error;
	}

	const description =
		error instanceof Error && error.message
			? error.message
			: "An unexpected error occurred.";

	return (
		<main className="flex flex-1 flex-col gap-4 p-6">
			<ErrorState title="Something went wrong" description={description} />
		</main>
	);
}
