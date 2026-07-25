import { type ClassValue, clsx } from "clsx"
import { twMerge } from "tailwind-merge"

export function cn(...inputs: ClassValue[]) {
	return twMerge(clsx(inputs))
}

/** Soft rounded hover for inline clickable text (links, entity refs). */
export const interactiveRegionClassName =
	"-mx-1.5 rounded-md px-1.5 transition-colors hover:bg-muted focus-visible:bg-muted focus-visible:outline-none"
