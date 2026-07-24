import { cva, type VariantProps } from "class-variance-authority";
import type { ComponentProps } from "react";

import { cn } from "#lib/utils";

const textVariants = cva("font-sans text-balance", {
	variants: {
		variant: {
			caption: "text-xs font-normal leading-normal",
			body: "text-sm font-normal leading-normal",
			bodyStrong: "text-sm font-semibold leading-normal",
			subtitle: "text-xl font-bold leading-snug tracking-tight",
			title: "text-[1.75rem] font-extrabold leading-tight tracking-tight",
			titleLarge: "text-4xl font-extrabold leading-none tracking-tight",
		},
		tone: {
			primary: "text-foreground",
			secondary: "text-muted-foreground",
			tertiary: "text-tertiary-foreground",
		},
	},
	defaultVariants: {
		variant: "body",
		tone: "primary",
	},
});

type TextElement = "p" | "span" | "div" | "h1" | "h2" | "h3" | "h4" | "label";

type TextProps = Omit<ComponentProps<"p">, "color"> &
	VariantProps<typeof textVariants> & {
		as?: TextElement;
	};

function Text({
	className,
	variant,
	tone,
	as: Comp = "p",
	...props
}: TextProps) {
	return (
		<Comp
			data-slot="text"
			className={cn(textVariants({ variant, tone }), className)}
			{...props}
		/>
	);
}

export { Text, textVariants };
