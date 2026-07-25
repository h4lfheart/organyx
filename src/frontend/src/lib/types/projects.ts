export type Project = {
	id: string;
	key: string;
	slug: string;
	name: string;
};

export type ProjectsResponse = {
	entries: Project[];
};
