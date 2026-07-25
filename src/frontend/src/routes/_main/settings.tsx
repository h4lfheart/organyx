import { createFileRoute } from "@tanstack/react-router";

import { SettingsPage } from "#components/settings/settings-page";

export const Route = createFileRoute("/_main/settings")({
	staticData: {
		breadcrumb: "Settings",
	},
	component: SettingsPage,
});
