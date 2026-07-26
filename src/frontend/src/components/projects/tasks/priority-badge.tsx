import { Badge } from "#components/ui/badge";
import type { Priority } from "#lib/types";

const priorityClassName: Record<Priority, string> = {
	Low: "bg-(--priority-low-bg) text-(--priority-low-fg)",
	Medium: "bg-(--priority-medium-bg) text-(--priority-medium-fg)",
	High: "bg-(--priority-high-bg) text-(--priority-high-fg)",
	Urgent: "bg-(--priority-urgent-bg) text-(--priority-urgent-fg)",
};

type PriorityBadgeProps = {
	priority: Priority;
};

export function PriorityBadge({ priority }: PriorityBadgeProps) {
	return (
		<Badge variant="plain" className={priorityClassName[priority]}>
			{priority}
		</Badge>
	);
}
