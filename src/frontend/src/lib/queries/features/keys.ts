export const featureKeys = {
	all: ["features"] as const,
	lists: () => [...featureKeys.all, "list"] as const,
	list: (projectSlug: string) =>
		[...featureKeys.lists(), projectSlug] as const,
};
