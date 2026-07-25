import { Link } from "@tanstack/react-router";
import { Layers, SquareCheckBig } from "lucide-react";

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
} as const;

export type EntityKind = keyof typeof entityKinds;

type EntityRefBaseProps = {
	kind: EntityKind;
	entityKey: string;
	className?: string;
};

type EntityRefLinkProps = EntityRefBaseProps & {
	kind: "task";
	projectSlug: string;
};

type EntityRefStaticProps = EntityRefBaseProps & {
	projectSlug?: undefined;
};

export type EntityRefProps = EntityRefLinkProps | EntityRefStaticProps;

const chipClassName =
	"inline-flex h-5 items-center gap-1.5 text-sm leading-none font-medium text-foreground";

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
		return (
			<Link
				to="/projects/$projectSlug/tasks/$taskKey"
				params={{ projectSlug, taskKey: entityKey }}
				className={cn(chipClassName, interactiveRegionClassName, className)}
			>
				{content}
			</Link>
		);
	}

	return <span className={cn(chipClassName, className)}>{content}</span>;
}
