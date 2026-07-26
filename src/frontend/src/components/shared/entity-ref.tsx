import { Link } from "@tanstack/react-router";
import { FolderKanban, Layers, SquareCheckBig } from "lucide-react";

import { cn, interactiveRegionClassName } from "#lib/utils";

const entityKinds = {
	task: {
		icon: SquareCheckBig,
		iconClassName: "text-info",
	},
	feature: {
		icon: Layers,
		iconClassName: "text-chart-5",
	},
	project: {
		icon: FolderKanban,
		iconClassName: "text-success",
	},
} as const;

export type EntityKind = keyof typeof entityKinds;

type EntityRefBaseProps = {
	kind: EntityKind;
	entityKey: string;
	className?: string;
};

type EntityRefLinkProps = EntityRefBaseProps & {
	kind: "task" | "feature" | "project";
	projectSlug: string;
};

type EntityRefStaticProps = EntityRefBaseProps & {
	projectSlug?: undefined;
};

export type EntityRefProps = EntityRefLinkProps | EntityRefStaticProps;

const chipClassName =
	"inline-flex h-5 items-center gap-1.5 text-sm leading-none font-medium text-foreground";

function entityLink(
	kind: "task" | "feature" | "project",
	projectSlug: string,
	entityKey: string,
) {
	switch (kind) {
		case "task":
			return {
				to: "/projects/$projectSlug/tasks/$taskKey" as const,
				params: { projectSlug, taskKey: entityKey },
			};
		case "feature":
			return {
				to: "/projects/$projectSlug/features/$featureSlug" as const,
				params: { projectSlug, featureSlug: entityKey },
			};
		case "project":
			return {
				to: "/projects/$projectSlug" as const,
				params: { projectSlug },
			};
	}
}

export function EntityRef({
	kind,
	entityKey,
	className,
	projectSlug,
}: EntityRefProps) {
	const { icon: Icon, iconClassName } = entityKinds[kind];

	const content = (
		<>
			<Icon className={cn("size-3.5 shrink-0", iconClassName)} aria-hidden />
			{entityKey}
		</>
	);

	if (projectSlug) {
		const link = entityLink(kind, projectSlug, entityKey);
		return (
			<Link
				data-slot="entity-ref"
				to={link.to}
				params={link.params}
				className={cn(chipClassName, interactiveRegionClassName, className)}
			>
				{content}
			</Link>
		);
	}

	return (
		<span data-slot="entity-ref" className={cn(chipClassName, className)}>
			{content}
		</span>
	);
}
