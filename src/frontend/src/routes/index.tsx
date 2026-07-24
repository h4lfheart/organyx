import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/')({ component: App })

function App() {
  return (
    <main className="flex min-h-screen flex-col items-center justify-center bg-[#141714] text-white">
      <div className="flex flex-col items-center gap-3">
        <img
          src="/logo/logo.png"
          alt=""
          className="size-14"
        />
        <h1 className="text-4xl font-bold tracking-tight">organyx</h1>
      </div>
      <p className="mt-3 text-sm tracking-wide text-white/55">coming soon</p>
    </main>
  )
}
