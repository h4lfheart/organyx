import { Skeleton } from "#components/ui/skeleton";

export function TableSkeleton() {
	return (
		<Skeleton
			aria-busy="true"
			aria-label="Loading"
			className="h-64 w-full rounded-2xl"
		/>
	);
}
