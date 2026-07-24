export type Project = {
	id: string;
	key: string;
	slug: string;
	name: string;
	description: string | null;
};

export type ProjectsResponse = {
	entries: Project[];
};
