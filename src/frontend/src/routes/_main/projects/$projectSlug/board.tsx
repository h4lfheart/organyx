import { createFileRoute } from "@tanstack/react-router";

import { BoardPage } from "#components/projects/board/board-page";

export const Route = createFileRoute("/_main/projects/$projectSlug/board")({
	staticData: {
		breadcrumb: "Board",
	},
	component: BoardPage,
});
