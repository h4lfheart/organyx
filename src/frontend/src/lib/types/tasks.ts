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
	status: TaskStatusBadge;
	priority: Priority;
};

export type TasksResponse = {
	entries: Task[];
};
