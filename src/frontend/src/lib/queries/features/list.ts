import { queryOptions } from "@tanstack/react-query";

import { apiClient } from "#lib/config/api-client";
import type { FeaturesResponse } from "#lib/types/features";

import { featureKeys } from "./keys";

export async function fetchFeatures(
	projectSlug: string,
): Promise<FeaturesResponse> {
	const { data } = await apiClient.get<FeaturesResponse>(
		`/projects/${projectSlug}/features`,
	);
	return data;
}

export function featuresQueryOptions(projectSlug: string) {
	return queryOptions({
		queryKey: featureKeys.list(projectSlug),
		queryFn: () => fetchFeatures(projectSlug),
	});
}
