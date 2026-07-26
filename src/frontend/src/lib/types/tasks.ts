export type Priority = "Low" | "Medium" | "High" | "Urgent";

export type TaskStatusBadge = {
	id: string;
	name: string;
};

export type Task = {
	id: string;
	key: string;
	title: string;
	description: string | null;
	featureSlug: string | null;
	status: TaskStatusBadge;
	priority: Priority;
	createdAt: string | null;
	updatedAt: string | null;
};

export type TasksResponse = {
	entries: Task[];
};
