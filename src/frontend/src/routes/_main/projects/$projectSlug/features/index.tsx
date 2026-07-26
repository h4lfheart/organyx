import { createFileRoute } from "@tanstack/react-router";

import { FeaturesPage } from "#components/projects/features/features-page";
import { featuresQueryOptions } from "#lib/queries/features/list";

export const Route = createFileRoute("/_main/projects/$projectSlug/features/")({
	loader: async ({ context, params }) => {
		await context.queryClient.ensureQueryData(
			featuresQueryOptions(params.projectSlug),
		);
	},
	component: FeaturesPage,
});
