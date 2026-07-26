import { createFileRoute, notFound, Outlet } from "@tanstack/react-router";
import { EntityRef } from "#components/shared/entity-ref";
import { ErrorState } from "#components/shared/error-state";
import { Skeleton } from "#components/ui/skeleton";
import { useProject } from "#lib/hooks/projects/use-project";
import { projectsQueryOptions } from "#lib/queries/projects/list";

export const Route = createFileRoute("/_main/projects/$projectSlug")({
	loader: async ({ context }) => {
		await context.queryClient.prefetchQuery(projectsQueryOptions);
	},
	staticData: {
		breadcrumb: (match) => ({
			label: (
				<EntityRef
					kind="project"
					entityKey={String(match.params.projectSlug ?? "")}
				/>
			),
		}),
	},
	component: ProjectLayout,
});

function ProjectLayout() {
	const { projectSlug } = Route.useParams();
	const { project, isPending, isError } = useProject(projectSlug);

	if (isPending) {
		return (
			<main className="flex flex-1 flex-col gap-4 p-6">
				<div className="flex flex-col gap-2">
					<Skeleton className="h-8 w-48" />
					<Skeleton className="h-4 w-24" />
				</div>
			</main>
		);
	}

	if (isError) {
		return (
			<main className="flex flex-1 flex-col gap-4 p-6">
				<ErrorState
					title="Could not load project"
					description="Something went wrong while fetching this project."
				/>
			</main>
		);
	}

	if (!project) {
		throw notFound();
	}

	return <Outlet />;
}
