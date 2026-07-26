import { useQuery } from "@tanstack/react-query";

import { featuresQueryOptions } from "#lib/queries/features/list";

export function useFeatures(projectSlug: string) {
	return useQuery(featuresQueryOptions(projectSlug));
}
