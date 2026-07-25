import { getRouteApi } from "@tanstack/react-router";

import { ProjectPageHeader } from "#components/projects/project-page-header";
import { TasksTable } from "#components/projects/tasks/tasks-table";
import { Text } from "#components/ui/text";
import { useTasks } from "#lib/hooks/tasks/use-tasks";

const projectRoute = getRouteApi("/_main/projects/$projectSlug");

export function TasksPage() {
	const { project } = projectRoute.useLoaderData();
	const { data, isPending, isError } = useTasks(project.slug);
	const tasks = data?.entries ?? [];

	return (
		<main className="flex flex-1 flex-col gap-4 p-6">
			<ProjectPageHeader page="Tasks" />

			{isPending ? (
				<Text as="p" variant="caption" tone="secondary">
					Loading tasks…
				</Text>
			) : isError ? (
				<Text as="p" variant="caption" tone="secondary">
					Could not load tasks.
				</Text>
			) : tasks.length === 0 ? (
				<Text as="p" variant="caption" tone="secondary">
					No tasks yet.
				</Text>
			) : (
				<TasksTable projectSlug={project.slug} tasks={tasks} />
			)}
		</main>
	);
}
