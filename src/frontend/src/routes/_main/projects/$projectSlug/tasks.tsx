import { createFileRoute, Outlet } from "@tanstack/react-router";

export const Route = createFileRoute("/_main/projects/$projectSlug/tasks")({
	staticData: {
		breadcrumb: "Tasks",
	},
	component: TasksLayout,
});

function TasksLayout() {
	return <Outlet />;
}
