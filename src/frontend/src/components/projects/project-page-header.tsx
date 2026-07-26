import { getRouteApi } from "@tanstack/react-router";

import { EntityRef } from "#components/shared/entity-ref";
import { Text } from "#components/ui/text";
import { useProject } from "#lib/hooks/projects/use-project";

const projectRoute = getRouteApi("/_main/projects/$projectSlug");

type ProjectPageHeaderProps = {
	page: string;
};

export function ProjectPageHeader({ page }: ProjectPageHeaderProps) {
	const { projectSlug } = projectRoute.useParams();
	const { project } = useProject(projectSlug);

	if (!project) {
		return null;
	}

	return (
		<header className="flex flex-col gap-0.5">
			<Text as="h1" variant="title">
				{project.name}
			</Text>
			{page === "Overview" ? (
				<EntityRef kind="project" entityKey={project.slug} />
			) : (
				<Text as="p" variant="body" tone="secondary">
					{page}
				</Text>
			)}
		</header>
	);
}
