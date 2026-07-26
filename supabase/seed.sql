insert into projects (id, key, slug, name, description) values
  ('00000000-0000-0000-0000-000000000001', 'ORG', 'organyx', 'Organyx', 'The project roadmap for Organyx');

insert into features (project_id, slug, name, description) values
  ('00000000-0000-0000-0000-000000000001', 'authentication', 'Authentication', 'The user system.'),
  ('00000000-0000-0000-0000-000000000001', 'projects', 'Projects', 'Project management with support for feature and task config,'),
  ('00000000-0000-0000-0000-000000000001', 'tasks', 'Tasks', 'The tasks system.');

with
	project as (
		select '00000000-0000-0000-0000-000000000001'::uuid as id
	),
	statuses as (
		select
			s.id,
			s.name
		from status s
		join project p on s.project_id = p.id
	),
	feats as (
		select
			f.id,
			f.slug
		from features f
		join project p on f.project_id = p.id
	)
insert into tasks (project_id, feature_id, status_id, title, description, priority)
select
	p.id,
	t.feature_id,
	t.status_id,
	t.title,
	t.description,
	t.priority::priority
from project p
cross join lateral (
	values
		(
			(select id from feats where slug = 'authentication'),
			(select id from statuses where name = 'Todo'),
			'GitHub Auth',
			null,
			'high'
		),
		(
			(select id from feats where slug = 'authentication'),
			(select id from statuses where name = 'In Progress'),
			'Discord Auth',
			'Allow users to be able to log into the platform with their discord account.',
			'high'
		),
		(
			(select id from feats where slug = 'authentication'),
			(select id from statuses where name = 'Todo'),
			'Google Auth',
			'Sign in with Google OAuth.',
			'medium'
		),
		(
			(select id from feats where slug = 'authentication'),
			(select id from statuses where name = 'Complete'),
			'Email/password Auth',
			'Basic email and password registration and login.',
			'urgent'
		),
		(
			(select id from feats where slug = 'projects'),
			(select id from statuses where name = 'Todo'),
			'Project Settings Page',
			'Edit project name, key, and description.',
			'medium'
		),
		(
			(select id from feats where slug = 'projects'),
			(select id from statuses where name = 'In Progress'),
			'Sidebar Project List',
			null,
			'low'
		),
		(
			(select id from feats where slug = 'projects'),
			(select id from statuses where name = 'Complete'),
			'Create Project API',
			'POST endpoint for creating projects with unique key and slug.',
			'high'
		),
		(
			(select id from feats where slug = 'tasks'),
			(select id from statuses where name = 'Todo'),
			'Status Change API',
			'Allow users to be able to change the status of a task',
			'medium'
		),
		(
			(select id from feats where slug = 'tasks'),
			(select id from statuses where name = 'In Progress'),
			'Kanban Board',
			'Drag-and-drop board grouped by status columns.',
			'urgent'
		),
		(
			(select id from feats where slug = 'tasks'),
			(select id from statuses where name = 'Complete'),
			'Task List Table',
			'Sortable table of tasks for a project.',
			'high'
		),
		(
			(select id from feats where slug = 'tasks'),
			(select id from statuses where name = 'Todo'),
			'Task Search',
			'Filter tasks by title, key, or description.',
			'low'
		),
		(
			null,
			(select id from statuses where name = 'Todo'),
			'Public Project Sharing',
			'Anonymous read-only access to shared projects.',
			'medium'
		),
		(
			null,
			(select id from statuses where name = 'In Progress'),
			'Invite Links',
			'One-time contributor invite links from admins.',
			'high'
		),
		(
			null,
			(select id from statuses where name = 'Complete'),
			'Seed Default Statuses',
			'Auto-create Todo, In Progress, and Complete on project creation.',
			'low'
		),
		(
			null,
			(select id from statuses where name = 'Todo'),
			'Comments on Tasks',
			null,
			'urgent'
		)
) as t(feature_id, status_id, title, description, priority);
