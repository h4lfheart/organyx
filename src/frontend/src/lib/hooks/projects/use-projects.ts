import { useQuery } from "@tanstack/react-query";

import { projectsQueryOptions } from "#lib/queries/projects/list";

export function useProjects() {
	return useQuery(projectsQueryOptions);
}
