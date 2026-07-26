import { getRouteApi } from "@tanstack/react-router";
import { useMemo, useState } from "react";

import { ProjectPageHeader } from "#components/projects/project-page-header";
import { TasksTable } from "#components/projects/tasks/tasks-table";
import { EmptyState } from "#components/shared/empty-state";
import { ErrorState } from "#components/shared/error-state";
import { QueryState } from "#components/shared/query-state";
import { SearchInput } from "#components/shared/search-input";
import { TableSkeleton } from "#components/shared/table-skeleton";
import { useTasks } from "#lib/hooks/tasks/use-tasks";
import { matchesTextSearch } from "#lib/utils";

const projectRoute = getRouteApi("/_main/projects/$projectSlug");

export function TasksPage() {
	const { projectSlug } = projectRoute.useParams();
	const { data, isPending, isError } = useTasks(projectSlug);
	const tasks = data?.entries ?? [];
	const [query, setQuery] = useState("");

	const filteredTasks = useMemo(
		() => tasks.filter((task) => matchesTextSearch(query, task.title)),
		[tasks, query],
	);

	return (
		<main className="flex flex-1 flex-col gap-4 p-6">
			<ProjectPageHeader page="Tasks" />

			<QueryState
				isPending={isPending}
				isError={isError}
				isEmpty={tasks.length === 0}
				pending={<TableSkeleton />}
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
				<div className="flex flex-col gap-4">
					<SearchInput
						value={query}
						onValueChange={setQuery}
						placeholder="Search tasks…"
						aria-label="Search tasks"
					/>
					{filteredTasks.length === 0 ? (
						<EmptyState
							title="No matching tasks"
							description="Try a different search term."
						/>
					) : (
						<TasksTable projectSlug={projectSlug} tasks={filteredTasks} />
					)}
				</div>
			</QueryState>
		</main>
	);
}
