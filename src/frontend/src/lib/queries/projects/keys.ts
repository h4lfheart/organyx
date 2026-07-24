export const projectKeys = {
	all: ["projects"] as const,
	lists: () => [...projectKeys.all, "list"] as const,
	list: () => [...projectKeys.lists()] as const,
};
