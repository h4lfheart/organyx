import { Text } from "#components/ui/text";
import { cn } from "#lib/utils";

type EmptyStateProps = {
	title: string;
	description: string;
	className?: string;
};

export function EmptyState({ title, description, className }: EmptyStateProps) {
	return (
		<div
			data-slot="empty-state"
			className={cn("flex flex-col gap-1 py-8", className)}
		>
			<Text as="h2" variant="bodyStrong">
				{title}
			</Text>
			<Text as="p" variant="body" tone="secondary">
				{description}
			</Text>
		</div>
	);
}
