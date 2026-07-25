import { Text } from "#components/ui/text";
import { cn } from "#lib/utils";

type EmptyValueProps = {
	className?: string;
};

export function EmptyValue({ className }: EmptyValueProps) {
	return (
		<Text
			as="span"
			variant="body"
			tone="tertiary"
			className={cn("select-none", className)}
			aria-hidden
		>
			—
		</Text>
	);
}

export function displayValue(value: string | null | undefined) {
	const trimmed = value?.trim();
	return trimmed ? trimmed : null;
}
