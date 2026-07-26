import { getRouteApi } from "@tanstack/react-router";

import { ProjectPageHeader } from "#components/projects/project-page-header";
import { TasksTable } from "#components/projects/tasks/tasks-table";
import { EmptyState } from "#components/shared/empty-state";
import { ErrorState } from "#components/shared/error-state";
import { QueryState } from "#components/shared/query-state";
import { TableSkeleton } from "#components/shared/table-skeleton";
import { useTasks } from "#lib/hooks/tasks/use-tasks";

const projectRoute = getRouteApi("/_main/projects/$projectSlug");

export function TasksPage() {
	const { projectSlug } = projectRoute.useParams();
	const { data, isPending, isError } = useTasks(projectSlug);
	const tasks = data?.entries ?? [];

	return (
		<main className="flex flex-1 flex-col gap-4 p-6">
			<ProjectPageHeader page="Tasks" />

			<QueryState
				isPending={isPending}
				isError={isError}
				isEmpty={tasks.length === 0}
				pending={<TableSkeleton columnCount={8} />}
				error={
					<ErrorState
						title="Could not load tasks"
						description="Something went wrong while fetching tasks for this project."
					/>
				}
				empty={
					<EmptyState
						title="No tasks yet"
						description="Create a task to start tracking work in this project."
					/>
				}
			>
				<TasksTable projectSlug={projectSlug} tasks={tasks} />
			</QueryState>
		</main>
	);
}
