import { createFileRoute, notFound, Outlet } from "@tanstack/react-router";

import { EntityRef } from "#components/shared/entity-ref";
import { projectsQueryOptions } from "#lib/queries/projects/list";
import type { Project } from "#lib/types";

export const Route = createFileRoute("/_main/projects/$projectSlug")({
	loader: async ({ context, params }) => {
		const data =
			await context.queryClient.ensureQueryData(projectsQueryOptions);
		const project = data.entries.find(
			(entry) => entry.slug === params.projectSlug,
		);
		if (!project) throw notFound();

		return { project };
	},
	staticData: {
		breadcrumb: (match) => {
			const project = (match.loaderData as { project: Project } | undefined)
				?.project;
			const slug = project?.slug ?? String(match.params.projectSlug ?? "");

			return {
				label: <EntityRef kind="project" entityKey={slug} />,
			};
		},
	},
	component: ProjectLayout,
});

function ProjectLayout() {
	return <Outlet />;
}
