import { Badge } from "#components/ui/badge";
import { EmptyValue } from "#components/ui/empty-value";

type StatusBadgeProps = {
	status: { name: string; isComplete: boolean } | null | undefined;
};

export function StatusBadge({ status }: StatusBadgeProps) {
	if (!status) {
		return <EmptyValue />;
	}

	return (
		<Badge variant={status.isComplete ? "success" : "outline"}>
			{status.name}
		</Badge>
	);
}
