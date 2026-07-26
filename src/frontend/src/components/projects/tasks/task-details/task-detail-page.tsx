import { getRouteApi, notFound } from "@tanstack/react-router";

import { EntityRef } from "#components/shared/entity-ref";
import { ErrorState } from "#components/shared/error-state";
import { displayValue, EmptyValue } from "#components/ui/empty-value";
import { Skeleton } from "#components/ui/skeleton";
import { Text } from "#components/ui/text";
import { useTasks } from "#lib/hooks/tasks/use-tasks";

const taskRoute = getRouteApi("/_main/projects/$projectSlug/tasks/$taskKey");

export function TaskDetailPage() {
	const { projectSlug, taskKey } = taskRoute.useParams();
	const { data, isPending, isError } = useTasks(projectSlug);
	const task = data?.entries.find((entry) => entry.key === taskKey);

	if (isPending) {
		return (
			<main className="flex flex-1 flex-col gap-4 p-6">
				<div className="flex flex-col gap-2">
					<Skeleton className="h-8 w-64" />
					<Skeleton className="h-4 w-24" />
					<Skeleton className="mt-2 h-4 w-full max-w-md" />
				</div>
			</main>
		);
	}

	if (isError) {
		return (
			<main className="flex flex-1 flex-col gap-4 p-6">
				<ErrorState
					title="Could not load task"
					description="Something went wrong while fetching this task."
				/>
			</main>
		);
	}

	if (!task) {
		throw notFound();
	}

	const description = displayValue(task.description);

	return (
		<main className="flex flex-1 flex-col gap-4 p-6">
			<header className="flex flex-col gap-1">
				<Text as="h1" variant="title">
					{task.title}
				</Text>
				<EntityRef kind="task" entityKey={task.key} />
			</header>
			{description ? (
				<Text as="p" variant="body" tone="secondary">
					{description}
				</Text>
			) : (
				<EmptyValue />
			)}
		</main>
	);
}
