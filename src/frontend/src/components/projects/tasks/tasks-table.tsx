import { PriorityBadge } from "#components/projects/tasks/priority-badge";
import { EntityRef } from "#components/shared/entity-ref";
import { Badge } from "#components/ui/badge";
import { displayValue, EmptyValue } from "#components/ui/empty-value";
import {
	Table,
	TableBody,
	TableCell,
	TableHead,
	TableHeader,
	TableRow,
} from "#components/ui/table";
import type { Task } from "#lib/types";

type TasksTableProps = {
	projectSlug: string;
	tasks: Task[];
};

export function TasksTable({ projectSlug, tasks }: TasksTableProps) {
	return (
		<Table>
			<TableHeader>
				<TableRow>
					<TableHead>ID</TableHead>
					<TableHead>Title</TableHead>
					<TableHead>Description</TableHead>
					<TableHead>Status</TableHead>
					<TableHead>Priority</TableHead>
				</TableRow>
			</TableHeader>
			<TableBody>
				{tasks.map((task) => {
					const description = displayValue(task.description);

					return (
						<TableRow key={task.id}>
							<TableCell>
								<EntityRef
									kind="task"
									entityKey={task.key}
									projectSlug={projectSlug}
								/>
							</TableCell>
							<TableCell className="max-w-64 truncate font-medium">
								{task.title}
							</TableCell>
							<TableCell className="max-w-xs truncate text-muted-foreground">
								{description ?? <EmptyValue />}
							</TableCell>
							<TableCell>
								<Badge variant="outline">{task.status.name}</Badge>
							</TableCell>
							<TableCell>
								<PriorityBadge priority={task.priority} />
							</TableCell>
						</TableRow>
					);
				})}
			</TableBody>
		</Table>
	);
}
