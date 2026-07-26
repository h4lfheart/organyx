import { createFileRoute } from "@tanstack/react-router";

import { TasksPage } from "#components/projects/tasks/tasks-page";
import { tasksQueryOptions } from "#lib/queries/tasks/list";

export const Route = createFileRoute("/_main/projects/$projectSlug/tasks/")({
	loader: async ({ context, params }) => {
		await context.queryClient.prefetchQuery(
			tasksQueryOptions(params.projectSlug),
		);
	},
	component: TasksPage,
});
