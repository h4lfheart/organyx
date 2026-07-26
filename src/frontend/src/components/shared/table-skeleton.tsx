import { Skeleton } from "#components/ui/skeleton";
import {
	Table,
	TableBody,
	TableCell,
	TableHead,
	TableHeader,
	TableRow,
} from "#components/ui/table";
import { cn } from "#lib/utils";

type TableSkeletonProps = {
	columnCount: number;
	rowCount?: number;
};

const cellWidths = [
	"w-16",
	"w-32",
	"w-40",
	"w-20",
	"w-24",
	"w-28",
	"w-20",
	"w-20",
];

export function TableSkeleton({
	columnCount,
	rowCount = 5,
}: TableSkeletonProps) {
	const columns = Array.from({ length: columnCount }, (_, index) => index);
	const rows = Array.from({ length: rowCount }, (_, index) => index);

	return (
		<Table aria-busy="true" aria-label="Loading">
			<TableHeader>
				<TableRow>
					{columns.map((column) => (
						<TableHead key={column}>
							<Skeleton className="h-4 w-16" />
						</TableHead>
					))}
				</TableRow>
			</TableHeader>
			<TableBody>
				{rows.map((row) => (
					<TableRow key={row} className="hover:bg-transparent">
						{columns.map((column) => (
							<TableCell key={column}>
								<Skeleton
									className={cn("h-4", cellWidths[column % cellWidths.length])}
								/>
							</TableCell>
						))}
					</TableRow>
				))}
			</TableBody>
		</Table>
	);
}
