import type { Column } from "@tanstack/react-table";
import { ArrowDown, ArrowUp, ArrowUpDown } from "lucide-react";

import { Button } from "#components/ui/button";

type SortableHeaderProps<TData> = {
	column: Column<TData, unknown>;
	title: string;
};

export function SortableHeader<TData>({
	column,
	title,
}: SortableHeaderProps<TData>) {
	const sorted = column.getIsSorted();

	let SortIcon = ArrowUpDown;
	let iconClassName = "size-3.5 opacity-40";
	if (sorted === "asc") {
		SortIcon = ArrowUp;
		iconClassName = "size-3.5";
	} else if (sorted === "desc") {
		SortIcon = ArrowDown;
		iconClassName = "size-3.5";
	}

	return (
		<Button
			variant="ghost"
			size="sm"
			className="-ml-2 h-8 gap-1.5 px-2 has-data-[icon=inline-end]:pr-1.5"
			onClick={column.getToggleSortingHandler()}
		>
			{title}
			<SortIcon data-icon="inline-end" className={iconClassName} />
		</Button>
	);
}
