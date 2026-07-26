import { CircleAlert } from "lucide-react";

import { Text } from "#components/ui/text";
import { cn } from "#lib/utils";

type ErrorStateProps = {
	title: string;
	description: string;
	className?: string;
};

export function ErrorState({ title, description, className }: ErrorStateProps) {
	return (
		<div
			data-slot="error-state"
			role="alert"
			className={cn("flex flex-col gap-1 py-8", className)}
		>
			<div className="flex items-center gap-2">
				<CircleAlert className="size-4 shrink-0 text-destructive" aria-hidden />
				<Text as="h2" variant="bodyStrong">
					{title}
				</Text>
			</div>
			<Text as="p" variant="body" tone="secondary">
				{description}
			</Text>
		</div>
	);
}
