import { createFileRoute } from "@tanstack/react-router";

import { FeatureDetailPage } from "#components/projects/features/feature-details/feature-detail-page";
import { EntityRef } from "#components/shared/entity-ref";
import { featuresQueryOptions } from "#lib/queries/features/list";

export const Route = createFileRoute(
	"/_main/projects/$projectSlug/features/$featureSlug",
)({
	loader: async ({ context, params }) => {
		await context.queryClient.prefetchQuery(
			featuresQueryOptions(params.projectSlug),
		);
	},
	staticData: {
		breadcrumb: (match) => ({
			label: (
				<EntityRef
					kind="feature"
					entityKey={String(match.params.featureSlug ?? "")}
				/>
			),
		}),
	},
	component: FeatureDetailPage,
});
