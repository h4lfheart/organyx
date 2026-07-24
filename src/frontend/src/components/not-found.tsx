import { Link } from "@tanstack/react-router";

import { Button } from "#components/ui/button";

export function NotFoundPage() {
	return (
		<main className="flex min-h-svh flex-col items-center justify-center gap-3 px-6">
			<h1 className="text-8xl font-extrabold tracking-tight text-foreground sm:text-9xl">
				404
			</h1>
			<p className="text-sm text-muted-foreground">page not found</p>
			<Button variant="ghost" size="xs" render={<Link to="/" />}>
				Return to Organyx
			</Button>
		</main>
	);
}
