import { useNavigate } from "@tanstack/react-router";
import {
	type ColumnDef,
	flexRender,
	getCoreRowModel,
	getSortedRowModel,
	type Row,
	type SortingState,
	useReactTable,
} from "@tanstack/react-table";
import { type MouseEvent, useState } from "react";

import {
	Table,
	TableBody,
	TableCell,
	TableHead,
	TableHeader,
	TableRow,
} from "#components/ui/table";
import { cn } from "#lib/utils";

type DataTableProps<TData> = {
	columns: ColumnDef<TData, unknown>[];
	data: TData[];
	cellClassNames?: Record<string, string>;
	getRowHref?: (row: TData) => string;
};

function isInteractiveTarget(target: EventTarget | null) {
	if (!(target instanceof Element)) {
		return false;
	}

	return Boolean(target.closest("a, button, [role='button']"));
}

export function DataTable<TData>({
	columns,
	data,
	cellClassNames,
	getRowHref,
}: DataTableProps<TData>) {
	const navigate = useNavigate();
	const [sorting, setSorting] = useState<SortingState>([]);

	const table = useReactTable({
		data,
		columns,
		state: { sorting },
		onSortingChange: setSorting,
		getCoreRowModel: getCoreRowModel(),
		getSortedRowModel: getSortedRowModel(),
	});

	function handleRowClick(
		row: Row<TData>,
		event: MouseEvent<HTMLTableRowElement>,
	) {
		if (!getRowHref || isInteractiveTarget(event.target)) {
			return;
		}

		void navigate({ to: getRowHref(row.original) });
	}

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
					<TableRow
						key={row.id}
						className={cn(getRowHref && "cursor-pointer")}
						onClick={(event) => handleRowClick(row, event)}
					>
						{row.getVisibleCells().map((cell) => (
							<TableCell
								key={cell.id}
								className={cellClassNames?.[cell.column.id]}
							>
								{flexRender(cell.column.columnDef.cell, cell.getContext())}
							</TableCell>
						))}
					</TableRow>
				))}
			</TableBody>
		</Table>
	);
}
