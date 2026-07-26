import { EmptyState } from "#components/shared/empty-state";
import { EntityRef } from "#components/shared/entity-ref";
import { ErrorState } from "#components/shared/error-state";
import { QueryState } from "#components/shared/query-state";
import { Skeleton } from "#components/ui/skeleton";
import { Text } from "#components/ui/text";
import { useProjects } from "#lib/hooks/projects/use-projects";

export function ProjectsPage() {
	const { data, isPending, isError } = useProjects();
	const projects = data?.entries ?? [];

	return (
		<main className="flex flex-1 flex-col gap-4 p-6">
			<Text as="h1" variant="title">
				Projects
			</Text>

			<QueryState
				isPending={isPending}
				isError={isError}
				isEmpty={projects.length === 0}
				pending={
					<ul
						className="flex flex-col gap-2"
						aria-busy="true"
						aria-label="Loading"
					>
						<li>
							<Skeleton className="h-5 w-24" />
						</li>
						<li>
							<Skeleton className="h-5 w-20" />
						</li>
						<li>
							<Skeleton className="h-5 w-28" />
						</li>
					</ul>
				}
				error={
					<ErrorState
						title="Could not load projects"
						description="Something went wrong while fetching your projects."
					/>
				}
				empty={
					<EmptyState
						title="No projects yet"
						description="Create a project to start organizing tasks and features."
					/>
				}
			>
				<ul className="flex flex-col gap-2">
					{projects.map((project) => (
						<li key={project.id}>
							<EntityRef
								kind="project"
								entityKey={project.slug}
								projectSlug={project.slug}
							/>
						</li>
					))}
				</ul>
			</QueryState>
		</main>
	);
}
