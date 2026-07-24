import {
	Link,
	Outlet,
	createFileRoute,
	useRouterState,
} from "@tanstack/react-router"
import { ChevronRight, Home, Layers, LayoutDashboard, Settings } from "lucide-react"

import {
	Collapsible,
	CollapsibleContent,
	CollapsibleTrigger,
} from "#components/ui/collapsible"
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
} from "#components/ui/sidebar"
import { Text } from "#components/ui/text"
import { TooltipProvider } from "#components/ui/tooltip"

export const Route = createFileRoute("/_main")({
	component: MainLayout,
})

const workspaceItems = [{ title: "Home", icon: Home, to: "/" }] as const

const projectItems = [
	{ title: "Board", icon: LayoutDashboard, to: "/board" },
	{ title: "Features", icon: Layers, to: "/features" },
] as const

const projects = [{ id: "org", name: "Organyx", defaultOpen: true }] as const

function MainLayout() {
	const pathname = useRouterState({ select: (s) => s.location.pathname })

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
												render={<Link to={item.to} />}
												isActive={pathname === item.to}
											>
												<item.icon />
												<span>{item.title}</span>
											</SidebarMenuButton>
										</SidebarMenuItem>
									))}
								</SidebarMenu>
							</SidebarGroupContent>
						</SidebarGroup>

						<SidebarSeparator />

						{projects.map((project) => (
							<Collapsible
								key={project.id}
								defaultOpen={project.defaultOpen}
								className="group/collapsible"
							>
								<SidebarGroup>
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
												{projectItems.map((item) => (
													<SidebarMenuItem key={item.title}>
														<SidebarMenuButton
															render={<Link to={item.to} />}
															isActive={pathname === item.to}
														>
															<item.icon />
															<span>{item.title}</span>
														</SidebarMenuButton>
													</SidebarMenuItem>
												))}
											</SidebarMenu>
										</SidebarGroupContent>
									</CollapsibleContent>
								</SidebarGroup>
							</Collapsible>
						))}
					</SidebarContent>

					<SidebarFooter>
						<SidebarMenu className="gap-1">
							<SidebarMenuItem>
								<SidebarMenuButton
									render={<Link to="/settings" />}
									isActive={pathname === "/settings"}
								>
									<Settings />
									<span>Settings</span>
								</SidebarMenuButton>
							</SidebarMenuItem>
						</SidebarMenu>
					</SidebarFooter>
				</Sidebar>

				<SidebarInset>
					<Outlet />
				</SidebarInset>
			</SidebarProvider>
		</TooltipProvider>
	)
}
