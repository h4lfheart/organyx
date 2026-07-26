import { createFileRoute } from "@tanstack/react-router";

import { TaskDetailPage } from "#components/projects/tasks/task-details/task-detail-page";
import { EntityRef } from "#components/shared/entity-ref";
import { tasksQueryOptions } from "#lib/queries/tasks/list";

export const Route = createFileRoute(
	"/_main/projects/$projectSlug/tasks/$taskKey",
)({
	loader: async ({ context, params }) => {
		await context.queryClient.prefetchQuery(
			tasksQueryOptions(params.projectSlug),
		);
	},
	staticData: {
		breadcrumb: (match) => ({
			label: (
				<EntityRef kind="task" entityKey={String(match.params.taskKey ?? "")} />
			),
		}),
	},
	component: TaskDetailPage,
});
