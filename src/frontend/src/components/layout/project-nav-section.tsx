import { Link, useParams } from "@tanstack/react-router";
import {
	ChevronRight,
	Layers,
	LayoutDashboard,
	ListTodo,
	PanelTop,
} from "lucide-react";
import { useEffect, useState } from "react";

import {
	Collapsible,
	CollapsibleContent,
	CollapsibleTrigger,
} from "#components/ui/collapsible";
import {
	SidebarGroup,
	SidebarGroupContent,
	SidebarGroupLabel,
	SidebarMenu,
	SidebarMenuButton,
	SidebarMenuItem,
} from "#components/ui/sidebar";
import type { Project } from "#lib/types";

const navActiveOptions = { exact: true, includeSearch: false } as const;
const navActiveProps = { "data-active": "" } as const;

type ProjectNavSectionProps = {
	project: Project;
};

export function ProjectNavSection({ project }: ProjectNavSectionProps) {
	const { projectSlug } = useParams({ strict: false });
	const isActive = projectSlug === project.slug;
	const [open, setOpen] = useState(isActive);

	useEffect(() => {
		if (isActive) setOpen(true);
	}, [isActive]);

	return (
		<Collapsible
			open={open}
			onOpenChange={setOpen}
			className="group/collapsible"
		>
			<SidebarGroup className="gap-0.5 py-0.5">
				<SidebarGroupLabel
					render={<CollapsibleTrigger />}
					className="w-full cursor-pointer hover:bg-sidebar-accent hover:text-sidebar-accent-foreground"
				>
					{project.name}
					<ChevronRight className="ml-auto transition-transform group-data-open/collapsible:rotate-90" />
				</SidebarGroupLabel>
				<CollapsibleContent>
					<SidebarGroupContent>
						<SidebarMenu className="gap-1">
							<SidebarMenuItem>
								<SidebarMenuButton
									render={
										<Link
											to="/projects/$projectSlug"
											params={{ projectSlug: project.slug }}
											activeOptions={navActiveOptions}
											activeProps={navActiveProps}
										/>
									}
								>
									<PanelTop />
									<span>Overview</span>
								</SidebarMenuButton>
							</SidebarMenuItem>
							<SidebarMenuItem>
								<SidebarMenuButton
									render={
										<Link
											to="/projects/$projectSlug/board"
											params={{ projectSlug: project.slug }}
											activeOptions={navActiveOptions}
											activeProps={navActiveProps}
										/>
									}
								>
									<LayoutDashboard />
									<span>Board</span>
								</SidebarMenuButton>
							</SidebarMenuItem>
							<SidebarMenuItem>
								<SidebarMenuButton
									render={
										<Link
											to="/projects/$projectSlug/tasks"
											params={{ projectSlug: project.slug }}
											activeOptions={{ exact: false, includeSearch: false }}
											activeProps={navActiveProps}
										/>
									}
								>
									<ListTodo />
									<span>Tasks</span>
								</SidebarMenuButton>
							</SidebarMenuItem>
							<SidebarMenuItem>
								<SidebarMenuButton
									render={
										<Link
											to="/projects/$projectSlug/features"
											params={{ projectSlug: project.slug }}
											activeOptions={{ exact: false, includeSearch: false }}
											activeProps={navActiveProps}
										/>
									}
								>
									<Layers />
									<span>Features</span>
								</SidebarMenuButton>
							</SidebarMenuItem>
						</SidebarMenu>
					</SidebarGroupContent>
				</CollapsibleContent>
			</SidebarGroup>
		</Collapsible>
	);
}
