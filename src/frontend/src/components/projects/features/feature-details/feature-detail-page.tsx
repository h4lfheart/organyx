import { getRouteApi } from "@tanstack/react-router";

import { EntityRef } from "#components/shared/entity-ref";
import { displayValue, EmptyValue } from "#components/ui/empty-value";
import { Text } from "#components/ui/text";

const featureRoute = getRouteApi(
	"/_main/projects/$projectSlug/features/$featureSlug",
);

export function FeatureDetailPage() {
	const { feature } = featureRoute.useLoaderData();
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
