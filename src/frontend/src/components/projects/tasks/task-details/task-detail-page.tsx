import { getRouteApi } from "@tanstack/react-router";

import { EntityRef } from "#components/shared/entity-ref";
import { Text } from "#components/ui/text";

const taskRoute = getRouteApi("/_main/projects/$projectSlug/tasks/$taskKey");

export function TaskDetailPage() {
	const { task } = taskRoute.useLoaderData();

	return (
		<main className="flex flex-1 flex-col gap-4 p-6">
			<header className="flex flex-col gap-1">
				<Text as="h1" variant="title">
					{task.title}
				</Text>
				<EntityRef kind="task" entityKey={task.key} />
			</header>
		</main>
	);
}
