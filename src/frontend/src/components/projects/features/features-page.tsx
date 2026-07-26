import { getRouteApi } from "@tanstack/react-router";

import { FeaturesTable } from "#components/projects/features/features-table";
import { ProjectPageHeader } from "#components/projects/project-page-header";
import { Text } from "#components/ui/text";
import { useFeatures } from "#lib/hooks/features/use-features";

const projectRoute = getRouteApi("/_main/projects/$projectSlug");

export function FeaturesPage() {
	const { project } = projectRoute.useLoaderData();
	const { data, isPending, isError } = useFeatures(project.slug);
	const features = data?.entries ?? [];

	return (
		<main className="flex flex-1 flex-col gap-4 p-6">
			<ProjectPageHeader page="Features" />

			{isPending ? (
				<Text as="p" variant="caption" tone="secondary">
					Loading features…
				</Text>
			) : isError ? (
				<Text as="p" variant="caption" tone="secondary">
					Could not load features.
				</Text>
			) : features.length === 0 ? (
				<Text as="p" variant="caption" tone="secondary">
					No features yet.
				</Text>
			) : (
				<FeaturesTable projectSlug={project.slug} features={features} />
			)}
		</main>
	);
}
