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
import type { Priority, Task } from "#lib/types";

const priorityOrder: Record<Priority, number> = {
	Low: 0,
	Medium: 1,
	High: 2,
	Urgent: 3,
};

function taskNumber(key: string) {
	const value = Number(key.split("-").pop());
	return Number.isFinite(value) ? value : 0;
}

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
				priorityOrder[rowA.original.priority] -
				priorityOrder[rowB.original.priority],
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
