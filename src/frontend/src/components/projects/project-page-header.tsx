import { getRouteApi } from "@tanstack/react-router";

import { Text } from "#components/ui/text";

const projectRoute = getRouteApi("/_main/projects/$projectSlug");

type ProjectPageHeaderProps = {
	page: string;
};

export function ProjectPageHeader({ page }: ProjectPageHeaderProps) {
	const { project } = projectRoute.useLoaderData();

	return (
		<header className="flex flex-col gap-0.5">
			<Text as="h1" variant="title">
				{project.name}
			</Text>
			<Text as="p" variant="body" tone="secondary">
				{page}
			</Text>
		</header>
	);
}
