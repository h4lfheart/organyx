import { createFileRoute, Outlet } from "@tanstack/react-router";

export const Route = createFileRoute("/_main/projects")({
	staticData: {
		breadcrumb: "Projects",
	},
	component: ProjectsLayout,
});

function ProjectsLayout() {
	return <Outlet />;
}
