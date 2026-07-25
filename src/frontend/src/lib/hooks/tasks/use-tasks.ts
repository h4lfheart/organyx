import { useQuery } from "@tanstack/react-query";

import { tasksQueryOptions } from "#lib/queries/tasks/list";

export function useTasks(projectSlug: string) {
	return useQuery(tasksQueryOptions(projectSlug));
}
