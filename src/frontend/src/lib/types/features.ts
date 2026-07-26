export type FeatureStatusBadge = {
	id: string;
	name: string;
	isComplete: boolean;
};

export type Feature = {
	id: string;
	slug: string;
	name: string;
	description: string | null;
	status: FeatureStatusBadge | null;
	createdAt: string | null;
	updatedAt: string | null;
};

export type FeaturesResponse = {
	entries: Feature[];
};
