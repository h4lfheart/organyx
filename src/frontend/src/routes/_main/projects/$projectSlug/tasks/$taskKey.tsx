import { createFileRoute, notFound } from "@tanstack/react-router";
import { TaskDetailPage } from "#components/projects/tasks/task-details/task-detail-page";
import { EntityRef } from "#components/shared/entity-ref";
import { tasksQueryOptions } from "#lib/queries/tasks/list";
import type { Task } from "#lib/types";

export const Route = createFileRoute(
	"/_main/projects/$projectSlug/tasks/$taskKey",
)({
	loader: async ({ context, params }) => {
		const data = await context.queryClient.ensureQueryData(
			tasksQueryOptions(params.projectSlug),
		);
		const task = data.entries.find((entry) => entry.key === params.taskKey);
		if (!task) throw notFound();

		return { task };
	},
	staticData: {
		breadcrumb: (match) => {
			const task = (match.loaderData as { task: Task } | undefined)?.task;
			const key = task?.key ?? String(match.params.taskKey ?? "");

			return {
				label: <EntityRef kind="task" entityKey={key} />,
			};
		},
	},
	component: TaskDetailPage,
});
