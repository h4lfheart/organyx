import { Link, useMatches } from "@tanstack/react-router";
import { Fragment } from "react";

import {
	Breadcrumb,
	BreadcrumbItem,
	BreadcrumbLink,
	BreadcrumbList,
	BreadcrumbPage,
	BreadcrumbSeparator,
} from "#components/ui/breadcrumb";
import type { BreadcrumbCrumb } from "#lib/types/breadcrumb";

type ResolvedCrumb = {
	id: string;
	label: string;
	fullPath: string;
	params: Record<string, string>;
};

function resolveCrumb(
	crumb: BreadcrumbCrumb,
	match: {
		id: string;
		fullPath: string;
		params: Record<string, unknown>;
	},
): ResolvedCrumb {
	const params = Object.fromEntries(
		Object.entries(match.params).filter(
			(entry): entry is [string, string] => typeof entry[1] === "string",
		),
	);

	const label = typeof crumb === "string" ? crumb : crumb.label;

	return {
		id: match.id,
		label,
		fullPath: typeof crumb === "object" && crumb.to ? crumb.to : match.fullPath,
		params,
	};
}

export function RouteBreadcrumbs() {
	const matches = useMatches();

	const crumbs = matches.flatMap((match) => {
		const breadcrumb = match.staticData.breadcrumb;
		if (!breadcrumb) return [];

		const value =
			typeof breadcrumb === "function" ? breadcrumb(match) : breadcrumb;
		if (!value) return [];

		const items = Array.isArray(value) ? value : [value];
		return items.map((item) => resolveCrumb(item, match));
	});

	if (crumbs.length === 0) {
		return null;
	}

	return (
		<Breadcrumb>
			<BreadcrumbList>
				{crumbs.map((crumb, index) => {
					const isLast = index === crumbs.length - 1;

					return (
						<Fragment key={crumb.id}>
							{index > 0 ? <BreadcrumbSeparator /> : null}
							<BreadcrumbItem>
								{isLast ? (
									<BreadcrumbPage>{crumb.label}</BreadcrumbPage>
								) : (
									<BreadcrumbLink
										render={<Link to={crumb.fullPath} params={crumb.params} />}
									>
										{crumb.label}
									</BreadcrumbLink>
								)}
							</BreadcrumbItem>
						</Fragment>
					);
				})}
			</BreadcrumbList>
		</Breadcrumb>
	);
}
