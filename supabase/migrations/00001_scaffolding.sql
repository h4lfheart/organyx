create type priority as enum ('low', 'medium', 'high', 'urgent');

create table projects
(
	id uuid primary key default gen_random_uuid(),
	key text not null unique,
	name text not null,
	description text,
	task_seq integer not null default 0
);

create table status
(
	id uuid primary key default gen_random_uuid(),
	project_id uuid not null references projects (id) on delete cascade,
	name text not null,
	position integer not null default 0
);

create table features
(
	id uuid primary key default gen_random_uuid(),
	project_id uuid not null references projects (id) on delete cascade,
	name text not null,
	description text
);

create table tasks
(
	id uuid primary key default gen_random_uuid(),
	project_id uuid not null references projects (id) on delete cascade,
	feature_id uuid references features (id) on delete set null,
	status_id uuid references status (id) on delete set null,
	number integer not null,
	title text not null,
	description text,
	priority priority not null default 'medium',
	unique (project_id, number)
);

create or replace function set_task_number()
returns trigger
language plpgsql
as $$
	begin
		update projects
		set task_seq = task_seq + 1
		where id = new.project_id
		returning task_seq into new.number;
		return new;
	end;
$$;

create trigger on_task_insert
	before insert
	on tasks
	for each row execute function set_task_number();

create or replace function seed_project_defaults()
returns trigger
language plpgsql
as $$
	begin
		insert into status (project_id, name, position)
		values (new.id, 'Todo', 0),
			(new.id, 'In Progress', 1),
			(new.id, 'Complete', 2);
		return new;
	end;
$$;

create trigger on_project_created
after insert
on projects
for each row execute function seed_project_defaults();

create index on tasks (project_id);
create index on tasks (feature_id);
create index on tasks (status_id);
create index on features (project_id);
create index on status (project_id);

grant usage on type priority to service_role;

grant select, insert, update, delete on table projects to service_role;
grant select, insert, update, delete on table status to service_role;
grant select, insert, update, delete on table features to service_role;
grant select, insert, update, delete on table tasks to service_role;
