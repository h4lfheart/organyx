import type { ColumnDef } from "@tanstack/react-table";
import { PriorityBadge } from "#components/projects/tasks/priority-badge";
import { DataTable } from "#components/shared/data-table";
import { EntityRef } from "#components/shared/entity-ref";
import { SortableHeader } from "#components/shared/sortable-table-header";
import { StatusBadge } from "#components/shared/status-badge";
import { displayValue, EmptyValue } from "#components/ui/empty-value";
import type { Task } from "#lib/types";
import {
	comparePriorities,
	compareTimestamps,
	formatTimestamp,
	taskNumber,
} from "#lib/utils";

type TasksTableProps = {
	projectSlug: string;
	tasks: Task[];
};

const cellClassNames = {
	title: "max-w-64 truncate font-medium",
	description: "max-w-xs truncate text-muted-foreground",
	createdAt: "whitespace-nowrap text-muted-foreground tabular-nums",
	updatedAt: "whitespace-nowrap text-muted-foreground tabular-nums",
};

export function TasksTable({ projectSlug, tasks }: TasksTableProps) {
	const columns: ColumnDef<Task>[] = [
		{
			accessorKey: "key",
			header: ({ column }) => <SortableHeader column={column} title="ID" />,
			cell: ({ row }) => <EntityRef kind="task" entityKey={row.original.key} />,
			sortingFn: (rowA, rowB) =>
				taskNumber(rowA.original.key) - taskNumber(rowB.original.key),
		},
		{
			accessorKey: "title",
			header: ({ column }) => <SortableHeader column={column} title="Title" />,
			cell: ({ row }) => (
				<span
					className={
						row.original.status.isComplete
							? "line-through text-muted-foreground"
							: undefined
					}
				>
					{row.original.title}
				</span>
			),
		},
		{
			accessorKey: "description",
			header: ({ column }) => (
				<SortableHeader column={column} title="Description" />
			),
			cell: ({ row }) => {
				const description = displayValue(row.original.description);
				return description ?? <EmptyValue />;
			},
			sortingFn: (rowA, rowB) => {
				const a = rowA.original.description?.trim() ?? "";
				const b = rowB.original.description?.trim() ?? "";
				return a.localeCompare(b);
			},
		},
		{
			accessorKey: "featureSlug",
			header: ({ column }) => (
				<SortableHeader column={column} title="Feature" />
			),
			cell: ({ row }) => {
				const featureSlug = displayValue(row.original.featureSlug);
				if (!featureSlug) {
					return <EmptyValue />;
				}

				return (
					<EntityRef
						kind="feature"
						entityKey={featureSlug}
						projectSlug={projectSlug}
					/>
				);
			},
			sortingFn: (rowA, rowB) => {
				const a = rowA.original.featureSlug?.trim() ?? "";
				const b = rowB.original.featureSlug?.trim() ?? "";
				return a.localeCompare(b);
			},
		},
		{
			id: "status",
			accessorFn: (task) => task.status.name,
			header: ({ column }) => <SortableHeader column={column} title="Status" />,
			cell: ({ row }) => <StatusBadge status={row.original.status} />,
		},
		{
			accessorKey: "priority",
			header: ({ column }) => (
				<SortableHeader column={column} title="Priority" />
			),
			cell: ({ row }) => <PriorityBadge priority={row.original.priority} />,
			sortingFn: (rowA, rowB) =>
				comparePriorities(rowA.original.priority, rowB.original.priority),
		},
		{
			accessorKey: "createdAt",
			header: ({ column }) => (
				<SortableHeader column={column} title="Created" />
			),
			cell: ({ row }) => {
				const createdAt = displayValue(row.original.createdAt);
				return createdAt ? formatTimestamp(createdAt) : <EmptyValue />;
			},
			sortingFn: (rowA, rowB) =>
				compareTimestamps(rowA.original.createdAt, rowB.original.createdAt),
		},
		{
			accessorKey: "updatedAt",
			header: ({ column }) => (
				<SortableHeader column={column} title="Updated" />
			),
			cell: ({ row }) => {
				const updatedAt = displayValue(row.original.updatedAt);
				return updatedAt ? formatTimestamp(updatedAt) : <EmptyValue />;
			},
			sortingFn: (rowA, rowB) =>
				compareTimestamps(rowA.original.updatedAt, rowB.original.updatedAt),
		},
	];

	return (
		<DataTable
			columns={columns}
			data={tasks}
			cellClassNames={cellClassNames}
			getRowHref={(task) => `/projects/${projectSlug}/tasks/${task.key}`}
		/>
	);
}
