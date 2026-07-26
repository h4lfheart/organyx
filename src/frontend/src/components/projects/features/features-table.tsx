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
import type { Feature } from "#lib/types";
import { compareTimestamps, formatTimestamp } from "#lib/utils";

function SortableHeader({
	column,
	title,
}: {
	column: Column<Feature, unknown>;
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

type FeaturesTableProps = {
	projectSlug: string;
	features: Feature[];
};

export function FeaturesTable({ projectSlug, features }: FeaturesTableProps) {
	const [sorting, setSorting] = useState<SortingState>([]);

	const columns: ColumnDef<Feature>[] = [
		{
			accessorKey: "slug",
			header: ({ column }) => <SortableHeader column={column} title="ID" />,
			cell: ({ row }) => (
				<EntityRef
					kind="feature"
					entityKey={row.original.slug}
					projectSlug={projectSlug}
				/>
			),
		},
		{
			accessorKey: "name",
			header: ({ column }) => <SortableHeader column={column} title="Name" />,
			cell: ({ row }) => (
				<span
					className={
						row.original.status?.isComplete
							? "line-through text-muted-foreground"
							: undefined
					}
				>
					{row.original.name}
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
			id: "status",
			accessorFn: (feature) => feature.status?.name ?? "",
			header: ({ column }) => <SortableHeader column={column} title="Status" />,
			cell: ({ row }) => {
				const status = row.original.status;
				return status ? (
					<Badge variant={status.isComplete ? "success" : "outline"}>
						{status.name}
					</Badge>
				) : (
					<EmptyValue />
				);
			},
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
		data: features,
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
								cell.column.id === "name"
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
