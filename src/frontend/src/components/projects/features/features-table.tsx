import type { ColumnDef } from "@tanstack/react-table";
import { DataTable } from "#components/shared/data-table";
import { EntityRef } from "#components/shared/entity-ref";
import { SortableHeader } from "#components/shared/sortable-table-header";
import { StatusBadge } from "#components/shared/status-badge";
import { displayValue, EmptyValue } from "#components/ui/empty-value";
import type { Feature } from "#lib/types";
import { compareTimestamps, formatTimestamp } from "#lib/utils";

type FeaturesTableProps = {
	projectSlug: string;
	features: Feature[];
};

const cellClassNames = {
	name: "max-w-64 truncate font-medium",
	description: "max-w-xs truncate text-muted-foreground",
	createdAt: "whitespace-nowrap text-muted-foreground tabular-nums",
	updatedAt: "whitespace-nowrap text-muted-foreground tabular-nums",
};

export function FeaturesTable({ projectSlug, features }: FeaturesTableProps) {
	const columns: ColumnDef<Feature>[] = [
		{
			accessorKey: "slug",
			header: ({ column }) => <SortableHeader column={column} title="ID" />,
			cell: ({ row }) => (
				<EntityRef kind="feature" entityKey={row.original.slug} />
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
			cell: ({ row }) => <StatusBadge status={row.original.status} />,
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
			data={features}
			cellClassNames={cellClassNames}
			getRowHref={(feature) =>
				`/projects/${projectSlug}/features/${feature.slug}`
			}
		/>
	);
}
