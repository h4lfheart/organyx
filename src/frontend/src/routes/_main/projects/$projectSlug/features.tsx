import { createFileRoute } from "@tanstack/react-router";

import { FeaturesPage } from "#components/projects/features/features-page";

export const Route = createFileRoute("/_main/projects/$projectSlug/features")({
	staticData: {
		breadcrumb: "Features",
	},
	component: FeaturesPage,
});
