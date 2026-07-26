import { getRouteApi, notFound } from "@tanstack/react-router";

import { EntityRef } from "#components/shared/entity-ref";
import { ErrorState } from "#components/shared/error-state";
import { displayValue, EmptyValue } from "#components/ui/empty-value";
import { Skeleton } from "#components/ui/skeleton";
import { Text } from "#components/ui/text";
import { useFeatures } from "#lib/hooks/features/use-features";

const featureRoute = getRouteApi(
	"/_main/projects/$projectSlug/features/$featureSlug",
);

export function FeatureDetailPage() {
	const { projectSlug, featureSlug } = featureRoute.useParams();
	const { data, isPending, isError } = useFeatures(projectSlug);
	const feature = data?.entries.find((entry) => entry.slug === featureSlug);

	if (isPending) {
		return (
			<main className="flex flex-1 flex-col gap-4 p-6">
				<div className="flex flex-col gap-2">
					<Skeleton className="h-8 w-64" />
					<Skeleton className="h-4 w-24" />
					<Skeleton className="mt-2 h-4 w-full max-w-md" />
				</div>
			</main>
		);
	}

	if (isError) {
		return (
			<main className="flex flex-1 flex-col gap-4 p-6">
				<ErrorState
					title="Could not load feature"
					description="Something went wrong while fetching this feature."
				/>
			</main>
		);
	}

	if (!feature) {
		throw notFound();
	}

	const description = displayValue(feature.description);

	return (
		<main className="flex flex-1 flex-col gap-4 p-6">
			<header className="flex flex-col gap-1">
				<Text as="h1" variant="title">
					{feature.name}
				</Text>
				<EntityRef kind="feature" entityKey={feature.slug} />
			</header>
			{description ? (
				<Text as="p" variant="body" tone="secondary">
					{description}
				</Text>
			) : (
				<EmptyValue />
			)}
		</main>
	);
}
