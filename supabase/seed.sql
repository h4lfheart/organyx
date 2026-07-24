insert into projects (id, key, slug, name, description) values
  ('00000000-0000-0000-0000-000000000001', 'ORG', 'organyx', 'Organyx', 'The project roadmap for Organyx');

insert into features (project_id, name, description) values
  ('00000000-0000-0000-0000-000000000001', 'Authentication', 'The user system.'),
  ('00000000-0000-0000-0000-000000000001', 'Projects', 'Project management with support for feature and task config,'),
  ('00000000-0000-0000-0000-000000000001', 'Tasks', 'The tasks system.');

insert into tasks (project_id, feature_id, status_id, title, description, priority) values
  ('00000000-0000-0000-0000-000000000001', (select id from features where name = 'Authentication'), (select id from status where project_id = '00000000-0000-0000-0000-000000000001' and is_default), 'GitHub Auth', null, 'high'),
  ('00000000-0000-0000-0000-000000000001', (select id from features where name = 'Authentication'), (select id from status where project_id = '00000000-0000-0000-0000-000000000001' and is_default), 'Discord Auth', 'Allow users to be able to log into the platform with their discord account.', 'high'),
  ('00000000-0000-0000-0000-000000000001', (select id from features where name = 'Tasks'), (select id from status where project_id = '00000000-0000-0000-0000-000000000001' and is_default), 'Status Change API', 'Allow users to be able to change the status of a task', 'medium');
