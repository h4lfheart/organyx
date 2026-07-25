import type { AnyRouteMatch } from "@tanstack/react-router";
import type { ReactNode } from "react";

export type BreadcrumbCrumb = string | { label: ReactNode; to?: string };

export type RouteBreadcrumb =
	| BreadcrumbCrumb
	| BreadcrumbCrumb[]
	| ((match: AnyRouteMatch) => BreadcrumbCrumb | BreadcrumbCrumb[] | undefined);

declare module "@tanstack/react-router" {
	interface StaticDataRouteOption {
		breadcrumb?: RouteBreadcrumb;
	}
}
