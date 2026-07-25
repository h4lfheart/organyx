import { Badge } from "#components/ui/badge";
import type { Priority } from "#lib/types";
import { cn } from "#lib/utils";

const priorityClassName: Record<Priority, string> = {
	Low: "bg-yellow-400/20 text-yellow-700 dark:text-yellow-300",
	Medium: "bg-orange-500/15 text-orange-700 dark:text-orange-300",
	High: "bg-red-500/15 text-red-600 dark:text-red-400",
	Urgent: "bg-red-700/20 text-red-800 dark:bg-red-700/30 dark:text-red-300",
};

type PriorityBadgeProps = {
	priority: Priority;
};

export function PriorityBadge({ priority }: PriorityBadgeProps) {
	return <Badge className={cn(priorityClassName[priority])}>{priority}</Badge>;
}
