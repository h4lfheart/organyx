import { getRouteApi } from "@tanstack/react-router";
import { useMemo, useState } from "react";

import { FeaturesTable } from "#components/projects/features/features-table";
import { ProjectPageHeader } from "#components/projects/project-page-header";
import { EmptyState } from "#components/shared/empty-state";
import { ErrorState } from "#components/shared/error-state";
import { QueryState } from "#components/shared/query-state";
import { SearchInput } from "#components/shared/search-input";
import { TableSkeleton } from "#components/shared/table-skeleton";
import { useFeatures } from "#lib/hooks/features/use-features";
import { matchesTextSearch } from "#lib/utils";

const projectRoute = getRouteApi("/_main/projects/$projectSlug");

export function FeaturesPage() {
	const { projectSlug } = projectRoute.useParams();
	const { data, isPending, isError } = useFeatures(projectSlug);
	const features = data?.entries ?? [];
	const [query, setQuery] = useState("");

	const filteredFeatures = useMemo(
		() => features.filter((feature) => matchesTextSearch(query, feature.name)),
		[features, query],
	);

	return (
		<main className="flex flex-1 flex-col gap-4 p-6">
			<ProjectPageHeader page="Features" />

			<QueryState
				isPending={isPending}
				isError={isError}
				isEmpty={features.length === 0}
				pending={<TableSkeleton />}
				error={
					<ErrorState
						title="Could not load features"
						description="Something went wrong while fetching features for this project."
					/>
				}
				empty={
					<EmptyState
						title="No features yet"
						description="Create a feature to group related tasks in this project."
					/>
				}
			>
				<div className="flex flex-col gap-4">
					<SearchInput
						value={query}
						onValueChange={setQuery}
						placeholder="Search features…"
						aria-label="Search features"
					/>
					{filteredFeatures.length === 0 ? (
						<EmptyState
							title="No matching features"
							description="Try a different search term."
						/>
					) : (
						<FeaturesTable
							projectSlug={projectSlug}
							features={filteredFeatures}
						/>
					)}
				</div>
			</QueryState>
		</main>
	);
}
