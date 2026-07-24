import { useProjects } from "./use-projects";

export function useProject(slug: string) {
	const query = useProjects();
	const project = query.data?.entries.find((entry) => entry.slug === slug);

	return {
		...query,
		project,
	};
}
