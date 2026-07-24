import { createFileRoute } from "@tanstack/react-router"

import { Text } from "#components/ui/text"

export const Route = createFileRoute("/_main/features")({
	component: FeaturesPage,
})

function FeaturesPage() {
	return (
		<main className="flex flex-1 flex-col gap-4 p-6">
			<Text as="h1" variant="title">
				Features
			</Text>
		</main>
	)
}
