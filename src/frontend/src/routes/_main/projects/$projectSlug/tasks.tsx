import { createFileRoute } from "@tanstack/react-router";

import { TasksPage } from "#components/projects/tasks/tasks-page";

export const Route = createFileRoute("/_main/projects/$projectSlug/tasks")({
	staticData: {
		breadcrumb: "Tasks",
	},
	component: TasksPage,
});
