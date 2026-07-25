import { queryOptions } from "@tanstack/react-query";

import { apiClient } from "#lib/config/api-client";
import type { TasksResponse } from "#lib/types/tasks";

import { taskKeys } from "./keys";

export async function fetchTasks(projectSlug: string): Promise<TasksResponse> {
	const { data } = await apiClient.get<TasksResponse>(
		`/projects/${projectSlug}/tasks`,
	);
	return data;
}

export function tasksQueryOptions(projectSlug: string) {
	return queryOptions({
		queryKey: taskKeys.list(projectSlug),
		queryFn: () => fetchTasks(projectSlug),
	});
}
