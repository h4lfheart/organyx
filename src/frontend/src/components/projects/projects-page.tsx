import { Link } from "@tanstack/react-router";

import { Text } from "#components/ui/text";
import { useProjects } from "#lib/hooks/projects/use-projects";

export function ProjectsPage() {
	const { data, isPending, isError } = useProjects();
	const projects = data?.entries ?? [];

	return (
		<main className="flex flex-1 flex-col gap-4 p-6">
			<Text as="h1" variant="title">
				Projects
			</Text>

			{isPending ? (
				<Text as="p" variant="caption" tone="secondary">
					Loading projects…
				</Text>
			) : isError ? (
				<Text as="p" variant="caption" tone="secondary">
					Could not load projects.
				</Text>
			) : projects.length === 0 ? (
				<Text as="p" variant="caption" tone="secondary">
					No projects yet.
				</Text>
			) : (
				<ul className="flex flex-col gap-1">
					{projects.map((project) => (
						<li key={project.id}>
							<Link
								to="/projects/$projectSlug"
								params={{ projectSlug: project.slug }}
								className="text-sm text-foreground underline-offset-4 hover:underline"
							>
								{project.name}
							</Link>
						</li>
					))}
				</ul>
			)}
		</main>
	);
}
