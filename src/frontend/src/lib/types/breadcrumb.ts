import type { AnyRouteMatch } from "@tanstack/react-router";

/** A single breadcrumb crumb: label string, or label + optional custom path. */
export type BreadcrumbCrumb = string | { label: string; to?: string };

/**
 * Route breadcrumb metadata. A function receives the matched route so labels
 * can come from loader data / params (e.g. project name).
 */
export type RouteBreadcrumb =
	| BreadcrumbCrumb
	| BreadcrumbCrumb[]
	| ((match: AnyRouteMatch) => BreadcrumbCrumb | BreadcrumbCrumb[] | undefined);

declare module "@tanstack/react-router" {
	interface StaticDataRouteOption {
		breadcrumb?: RouteBreadcrumb;
	}
}
