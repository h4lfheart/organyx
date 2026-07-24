import { queryOptions } from "@tanstack/react-query";

import { apiClient } from "#lib/config/api-client";
import type { ProjectsResponse } from "#lib/types/projects";

import { projectKeys } from "./keys";

export async function fetchProjects(): Promise<ProjectsResponse> {
	const { data } = await apiClient.get<ProjectsResponse>("/projects");
	return data;
}

export const projectsQueryOptions = queryOptions({
	queryKey: projectKeys.list(),
	queryFn: fetchProjects,
});
