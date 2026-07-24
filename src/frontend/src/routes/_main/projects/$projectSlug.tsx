import { createFileRoute, notFound, Outlet } from "@tanstack/react-router";

import { projectsQueryOptions } from "#lib/queries/projects/list";

export const Route = createFileRoute("/_main/projects/$projectSlug")({
	loader: async ({ context, params }) => {
		const data = await context.queryClient.ensureQueryData(projectsQueryOptions);
		const project = data.entries.find(entry => entry.slug === params.projectSlug,);
		if (!project)
			throw notFound();

		return { project };
	},
	component: ProjectLayout,
});

function ProjectLayout() {
	return <Outlet />;
}
