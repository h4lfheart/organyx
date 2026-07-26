import type { ReactNode } from "react";

import { Text } from "#components/ui/text";

type QueryStateProps = {
	isPending: boolean;
	isError: boolean;
	isEmpty: boolean;
	pending: ReactNode;
	error: ReactNode;
	empty: ReactNode;
	children: ReactNode;
};

function queryMessage(content: ReactNode) {
	if (typeof content === "string") {
		return (
			<Text as="p" variant="caption" tone="secondary">
				{content}
			</Text>
		);
	}

	return content;
}

export function QueryState({
	isPending,
	isError,
	isEmpty,
	pending,
	error,
	empty,
	children,
}: QueryStateProps) {
	if (isPending) {
		return queryMessage(pending);
	}

	if (isError) {
		return queryMessage(error);
	}

	if (isEmpty) {
		return queryMessage(empty);
	}

	return children;
}
