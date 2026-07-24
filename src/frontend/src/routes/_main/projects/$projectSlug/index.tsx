import { createFileRoute, getRouteApi } from "@tanstack/react-router";

import { Text } from "#components/ui/text";

const projectRoute = getRouteApi("/_main/projects/$projectSlug");

export const Route = createFileRoute("/_main/projects/$projectSlug/")({
	component: ProjectOverviewPage,
});

function ProjectOverviewPage() {
	const { project } = projectRoute.useLoaderData();

	return (
		<main className="flex flex-1 flex-col gap-4 p-6">
			<Text as="h1" variant="title">
				{project.name}
			</Text>
		</main>
	);
}
