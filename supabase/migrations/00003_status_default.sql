alter table status
	add column is_default boolean not null default false;

-- Existing projects: mark the first status (by position) as default.
update status
set is_default = true
where id in (select distinct on (project_id) id
             from status
             order by project_id, position, id);

create unique index status_one_default_per_project
	on status (project_id)
	where is_default;

create or replace function seed_project_defaults()
	returns trigger
	language plpgsql
as
$$
begin
	insert into status (project_id, name, position, is_default)
	values (new.id, 'Todo', 0, true),
		   (new.id, 'In Progress', 1, false),
		   (new.id, 'Complete', 2, false);
	return new;
end;
$$;
