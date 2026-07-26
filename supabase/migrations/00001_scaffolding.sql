create type priority as enum ('low', 'medium', 'high', 'urgent');

create table projects
(
	id uuid primary key default gen_random_uuid(),
	key text not null unique,
	slug text not null unique,
	name text not null,
	description text,
	task_seq integer not null default 0,
	created_at timestamptz not null default now(),
	updated_at timestamptz not null default now(),
	constraint projects_slug_format check (slug ~ '^[a-z0-9]+(?:-[a-z0-9]+)*$')
);

create table status
(
	id uuid primary key default gen_random_uuid(),
	project_id uuid not null references projects (id) on delete cascade,
	name text not null,
	position integer not null default 0,
	is_default boolean not null default false
);

create unique index status_one_default_per_project
	on status (project_id)
	where is_default;

create table features
(
	id uuid primary key default gen_random_uuid(),
	project_id uuid not null references projects (id) on delete cascade,
	status_id uuid references status (id) on delete set null,
	slug text not null,
	name text not null,
	description text,
	created_at timestamptz not null default now(),
	updated_at timestamptz not null default now(),
	unique (project_id, slug),
	constraint features_slug_format check (slug ~ '^[a-z0-9]+(?:-[a-z0-9]+)*$')
);

create table tasks
(
	id uuid primary key default gen_random_uuid(),
	project_id uuid not null references projects (id) on delete cascade,
	feature_id uuid references features (id) on delete set null,
	status_id uuid not null references status (id) on delete restrict,
	number integer not null,
	title text not null,
	description text,
	priority priority not null default 'medium',
	created_at timestamptz not null default now(),
	updated_at timestamptz not null default now(),
	unique (project_id, number)
);

create or replace function set_updated_at()
returns trigger
language plpgsql
as $$
	begin
		new.updated_at = now();
		return new;
	end;
$$;

create trigger projects_set_updated_at
	before update
	on projects
	for each row execute function set_updated_at();

create trigger features_set_updated_at
	before update
	on features
	for each row execute function set_updated_at();

create trigger tasks_set_updated_at
	before update
	on tasks
	for each row execute function set_updated_at();

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
		insert into status (project_id, name, position, is_default)
		values (new.id, 'Todo', 0, true),
			(new.id, 'In Progress', 1, false),
			(new.id, 'Complete', 2, false);
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
create index on features (status_id);
create index on status (project_id);

grant usage on type priority to service_role;

grant select, insert, update, delete on table projects to service_role;
grant select, insert, update, delete on table status to service_role;
grant select, insert, update, delete on table features to service_role;
grant select, insert, update, delete on table tasks to service_role;
