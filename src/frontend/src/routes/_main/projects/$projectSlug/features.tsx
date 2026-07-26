import { createFileRoute, Outlet } from "@tanstack/react-router";

export const Route = createFileRoute("/_main/projects/$projectSlug/features")({
	staticData: {
		breadcrumb: "Features",
	},
	component: FeaturesLayout,
});

function FeaturesLayout() {
	return <Outlet />;
}
