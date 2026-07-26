import {
	type Column,
	type ColumnDef,
	flexRender,
	getCoreRowModel,
	getSortedRowModel,
	type SortingState,
	useReactTable,
} from "@tanstack/react-table";
import { ArrowDown, ArrowUp, ArrowUpDown } from "lucide-react";
import { useState } from "react";
import { PriorityBadge } from "#components/projects/tasks/priority-badge";
import { EntityRef } from "#components/shared/entity-ref";
import { Badge } from "#components/ui/badge";
import { Button } from "#components/ui/button";
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
import {
	comparePriorities,
	compareTimestamps,
	formatTimestamp,
	taskNumber,
} from "#lib/utils";

function SortableHeader({
	column,
	title,
}: {
	column: Column<Task, unknown>;
	title: string;
}) {
	const sorted = column.getIsSorted();

	return (
		<Button
			variant="ghost"
			size="sm"
			className="-ml-2 h-8 gap-1.5 px-2 has-data-[icon=inline-end]:pr-1.5"
			onClick={column.getToggleSortingHandler()}
		>
			{title}
			{sorted === "asc" ? (
				<ArrowUp data-icon="inline-end" className="size-3.5" />
			) : sorted === "desc" ? (
				<ArrowDown data-icon="inline-end" className="size-3.5" />
			) : (
				<ArrowUpDown data-icon="inline-end" className="size-3.5 opacity-40" />
			)}
		</Button>
	);
}

type TasksTableProps = {
	projectSlug: string;
	tasks: Task[];
};

export function TasksTable({ projectSlug, tasks }: TasksTableProps) {
	const [sorting, setSorting] = useState<SortingState>([]);

	const columns: ColumnDef<Task>[] = [
		{
			accessorKey: "key",
			header: ({ column }) => <SortableHeader column={column} title="ID" />,
			cell: ({ row }) => (
				<EntityRef
					kind="task"
					entityKey={row.original.key}
					projectSlug={projectSlug}
				/>
			),
			sortingFn: (rowA, rowB) =>
				taskNumber(rowA.original.key) - taskNumber(rowB.original.key),
		},
		{
			accessorKey: "title",
			header: ({ column }) => <SortableHeader column={column} title="Title" />,
			cell: ({ row }) => row.original.title,
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
			id: "status",
			accessorFn: (task) => task.status.name,
			header: ({ column }) => <SortableHeader column={column} title="Status" />,
			cell: ({ row }) => (
				<Badge variant="outline">{row.original.status.name}</Badge>
			),
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

	const table = useReactTable({
		data: tasks,
		columns,
		state: { sorting },
		onSortingChange: setSorting,
		getCoreRowModel: getCoreRowModel(),
		getSortedRowModel: getSortedRowModel(),
	});

	return (
		<Table>
			<TableHeader>
				{table.getHeaderGroups().map((headerGroup) => (
					<TableRow key={headerGroup.id}>
						{headerGroup.headers.map((header) => (
							<TableHead key={header.id}>
								{header.isPlaceholder
									? null
									: flexRender(
											header.column.columnDef.header,
											header.getContext(),
										)}
							</TableHead>
						))}
					</TableRow>
				))}
			</TableHeader>
			<TableBody>
				{table.getRowModel().rows.map((row) => (
					<TableRow key={row.id}>
						{row.getVisibleCells().map((cell) => {
							const className =
								cell.column.id === "title"
									? "max-w-64 truncate font-medium"
									: cell.column.id === "description"
										? "max-w-xs truncate text-muted-foreground"
										: cell.column.id === "createdAt" ||
												cell.column.id === "updatedAt"
											? "whitespace-nowrap text-muted-foreground tabular-nums"
											: undefined;

							return (
								<TableCell key={cell.id} className={className}>
									{flexRender(cell.column.columnDef.cell, cell.getContext())}
								</TableCell>
							);
						})}
					</TableRow>
				))}
			</TableBody>
		</Table>
	);
}
