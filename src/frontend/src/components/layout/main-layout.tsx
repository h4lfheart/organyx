import { Link, Outlet } from "@tanstack/react-router";
import { FolderKanban, Home, Settings } from "lucide-react";

import { ProjectNavSection } from "#components/layout/project-nav-section";
import { RouteBreadcrumbs } from "#components/layout/route-breadcrumbs";
import {
	Sidebar,
	SidebarContent,
	SidebarFooter,
	SidebarGroup,
	SidebarGroupContent,
	SidebarGroupLabel,
	SidebarHeader,
	SidebarInset,
	SidebarMenu,
	SidebarMenuButton,
	SidebarMenuItem,
	SidebarProvider,
	SidebarSeparator,
} from "#components/ui/sidebar";
import { Skeleton } from "#components/ui/skeleton";
import { Text } from "#components/ui/text";
import { TooltipProvider } from "#components/ui/tooltip";
import { useProjects } from "#lib/hooks/projects/use-projects";

const workspaceItems = [
	{ title: "Home", icon: Home, to: "/" },
	{ title: "Projects", icon: FolderKanban, to: "/projects" },
] as const;

/** Exact path match; drive SidebarMenuButton's data-active via Link. */
const navActiveOptions = { exact: true, includeSearch: false } as const;
const navActiveProps = { "data-active": "" } as const;

export function MainLayout() {
	const { data, isPending, isError } = useProjects();
	const projects = data?.entries ?? [];

	return (
		<TooltipProvider>
			<SidebarProvider>
				<Sidebar collapsible="none" className="h-svh border-r-0 shadow-soft">
					<SidebarHeader className="gap-3 px-3 py-4">
						<div className="flex items-center gap-2.5 px-1">
							<img
								src="/logo/logo.png"
								alt=""
								className="size-9 shrink-0 object-contain"
							/>
							<div className="flex min-w-0 flex-col leading-none">
								<Text as="span" variant="subtitle">
									organyx
								</Text>
								<Text as="span" variant="caption" tone="secondary">
									v1.0.0
								</Text>
							</div>
						</div>
					</SidebarHeader>

					<SidebarContent>
						<SidebarGroup>
							<SidebarGroupLabel>Workspace</SidebarGroupLabel>
							<SidebarGroupContent>
								<SidebarMenu className="gap-1">
									{workspaceItems.map((item) => (
										<SidebarMenuItem key={item.title}>
											<SidebarMenuButton
												render={
													<Link
														to={item.to}
														activeOptions={navActiveOptions}
														activeProps={navActiveProps}
													/>
												}
											>
												<item.icon />
												<span>{item.title}</span>
											</SidebarMenuButton>
										</SidebarMenuItem>
									))}
								</SidebarMenu>
							</SidebarGroupContent>
						</SidebarGroup>

						<SidebarSeparator className="mb-2" />

						{isPending ? (
							<SidebarGroup>
								<SidebarGroupLabel>Projects</SidebarGroupLabel>
								<SidebarGroupContent className="space-y-2 px-2">
									<Skeleton className="h-4 w-24" />
									<Skeleton className="h-4 w-20" />
								</SidebarGroupContent>
							</SidebarGroup>
						) : isError ? (
							<SidebarGroup>
								<SidebarGroupLabel>Projects</SidebarGroupLabel>
								<SidebarGroupContent className="px-3">
									<Text as="p" variant="caption" tone="secondary">
										Could not load projects.
									</Text>
								</SidebarGroupContent>
							</SidebarGroup>
						) : projects.length === 0 ? (
							<SidebarGroup>
								<SidebarGroupLabel>Projects</SidebarGroupLabel>
								<SidebarGroupContent className="px-3">
									<Text as="p" variant="caption" tone="secondary">
										No projects yet.
									</Text>
								</SidebarGroupContent>
							</SidebarGroup>
						) : (
							projects.map((project) => (
								<ProjectNavSection key={project.id} project={project} />
							))
						)}
					</SidebarContent>

					<SidebarFooter>
						<SidebarMenu className="gap-1">
							<SidebarMenuItem>
								<SidebarMenuButton
									render={
										<Link
											to="/settings"
											activeOptions={navActiveOptions}
											activeProps={navActiveProps}
										/>
									}
								>
									<Settings />
									<span>Settings</span>
								</SidebarMenuButton>
							</SidebarMenuItem>
						</SidebarMenu>
					</SidebarFooter>
				</Sidebar>

				<SidebarInset className="border-l-2 border-border">
					<header className="flex h-12 shrink-0 items-center border-b border-border px-6">
						<RouteBreadcrumbs />
					</header>
					<Outlet />
				</SidebarInset>
			</SidebarProvider>
		</TooltipProvider>
	);
}
