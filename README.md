<div align="center">

# <img src=".github/images/logo.png" width="48" height="48" style="margin-bottom: 6px; margin-right: 4px;" align="center" /> Organyx

Lightweight, self-hosted project management for developers.

</div>

> [!WARNING]
> **Work in progress.** Organyx is early and incomplete. APIs, UI, and schema may change without notice, including **breaking changes**.

## Stack

- **Frontend:** React + TypeScript (Vite, TanStack Start/Router/Query, Tailwind)
- **Backend:** ASP.NET Web API
- **Database / Auth:** PostgreSQL via Supabase

## Local setup

**Prerequisites:** [Supabase CLI](https://supabase.com/docs/guides/cli), [.NET 10 SDK](https://dotnet.microsoft.com/download), [Node.js](https://nodejs.org/en)

```bash
# create local env copy
cp .env.example .env.local

# database
supabase start
supabase db reset # only required for initial migrations + seeding

# api
dotnet run --project src/backend/Organyx.Api

# frontend
cd src/frontend
npm install
npm run dev
```

| Service | URL |
|---------|-----|
| Frontend | http://127.0.0.1:24000 |
| API  | http://127.0.0.1:24001 |
| Supabase Studio | http://127.0.0.1:24002 |