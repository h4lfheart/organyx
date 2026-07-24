import path from 'node:path'
import { fileURLToPath } from 'node:url'
import { defineConfig, loadEnv } from 'vite'
import { devtools } from '@tanstack/devtools-vite'
import { tanstackStart } from '@tanstack/react-start/plugin/vite'
import viteReact from '@vitejs/plugin-react'
import tailwindcss from '@tailwindcss/vite'

const repoRoot = path.resolve(path.dirname(fileURLToPath(import.meta.url)), '../..')
const srcDir = path.resolve(path.dirname(fileURLToPath(import.meta.url)), 'src')

export default defineConfig(({ mode }) => {
  const env = loadEnv(mode, repoRoot, '')
  const port = Number(env.FRONTEND_PORT)
  if (!Number.isFinite(port) || port <= 0) {
    throw new Error(
      'FRONTEND_PORT is required. Copy .env.example to .env.local at the repo root and adjust if needed.',
    )
  }

  return {
    envDir: repoRoot,
    resolve: {
      tsconfigPaths: true,
      alias: {
        '#lib': path.join(srcDir, 'lib'),
        '#components': path.join(srcDir, 'components'),
        '#hooks': path.join(srcDir, 'lib/hooks'),
      },
    },
    server: {
      host: '127.0.0.1',
      port,
      strictPort: true,
      watch: {
        usePolling: true,
        interval: 100,
      },
    },
    preview: { host: '127.0.0.1', port, strictPort: true },
    plugins: [devtools(), tailwindcss(), tanstackStart(), viteReact()],
  }
})
