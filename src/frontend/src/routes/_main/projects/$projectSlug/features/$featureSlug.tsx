import { createFileRoute, notFound } from "@tanstack/react-router";
import { FeatureDetailPage } from "#components/projects/features/feature-details/feature-detail-page";
import { EntityRef } from "#components/shared/entity-ref";
import { featuresQueryOptions } from "#lib/queries/features/list";
import type { Feature } from "#lib/types";

export const Route = createFileRoute(
	"/_main/projects/$projectSlug/features/$featureSlug",
)({
	loader: async ({ context, params }) => {
		const data = await context.queryClient.ensureQueryData(
			featuresQueryOptions(params.projectSlug),
		);
		const feature = data.entries.find(
			(entry) => entry.slug === params.featureSlug,
		);
		if (!feature) throw notFound();

		return { feature };
	},
	staticData: {
		breadcrumb: (match) => {
			const feature = (match.loaderData as { feature: Feature } | undefined)
				?.feature;
			const slug = feature?.slug ?? String(match.params.featureSlug ?? "");

			return {
				label: <EntityRef kind="feature" entityKey={slug} />,
			};
		},
	},
	component: FeatureDetailPage,
});
