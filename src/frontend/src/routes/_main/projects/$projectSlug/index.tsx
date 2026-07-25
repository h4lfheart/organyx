import { createFileRoute } from "@tanstack/react-router";

import { ProjectOverviewPage } from "#components/projects/overview/overview-page";

export const Route = createFileRoute("/_main/projects/$projectSlug/")({
	component: ProjectOverviewPage,
});
