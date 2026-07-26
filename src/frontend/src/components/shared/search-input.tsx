import { Search, XIcon } from "lucide-react";
import type * as React from "react";

import { Button } from "#components/ui/button";
import { Input } from "#components/ui/input";
import { cn } from "#lib/utils";

type SearchInputProps = Omit<
	React.ComponentProps<"input">,
	"type" | "value" | "onChange"
> & {
	value: string;
	onValueChange: (value: string) => void;
};

export function SearchInput({
	className,
	value,
	onValueChange,
	placeholder = "Search…",
	...props
}: SearchInputProps) {
	const hasValue = value.length > 0;

	return (
		<div className={cn("relative w-full max-w-sm", className)}>
			<Search
				aria-hidden
				className="pointer-events-none absolute top-1/2 left-3 size-4 -translate-y-1/2 text-muted-foreground"
			/>
			<Input
				type="text"
				role="searchbox"
				value={value}
				onChange={(event) => onValueChange(event.target.value)}
				placeholder={placeholder}
				className={cn("pl-9", hasValue && "pr-9")}
				{...props}
			/>
			{hasValue ? (
				<Button
					type="button"
					variant="ghost"
					size="icon-xs"
					className="absolute top-1/2 right-1.5 -translate-y-1/2 text-muted-foreground"
					aria-label="Clear search"
					onClick={() => onValueChange("")}
				>
					<XIcon />
				</Button>
			) : null}
		</div>
	);
}
