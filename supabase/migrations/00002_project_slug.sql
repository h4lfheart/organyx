alter table projects
	add column slug text;

update projects
set slug = lower(key)
where slug is null;

alter table projects
	alter column slug set not null;

alter table projects
	add constraint projects_slug_unique unique (slug);

alter table projects
	add constraint projects_slug_format
	check (slug ~ '^[a-z0-9]+(?:-[a-z0-9]+)*$');
