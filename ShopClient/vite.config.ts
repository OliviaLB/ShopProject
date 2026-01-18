import { defineConfig, type UserConfig } from 'vite';
import react from '@vitejs/plugin-react-swc';
import tailwindcss from '@tailwindcss/vite';
import mkcert from 'vite-plugin-mkcert';
import { tanstackRouter } from '@tanstack/router-plugin/vite';

const buildTimestamp = new Date().getTime();

export default defineConfig({
  plugins: [
    tanstackRouter({
      target: 'react',
      autoCodeSplitting: true
    }),
    react(),
    tailwindcss(),
    mkcert()
  ],
  server: {
    port: 3000
  },
  test: {
    environment: 'jsdom',
    globals: true,
    setupFiles: './tests/setup.ts'
  },
  build: {
    rollupOptions: {
      output: {
        entryFileNames: `assets/[name].[hash].${buildTimestamp}.js`,
        chunkFileNames: `assets/[name].[hash].${buildTimestamp}.js`,
        assetFileNames: `assets/[name].[hash].${buildTimestamp}.[ext]`
      }
    }
  },

  define: {
    'process.env.BUILD_TIMESTAMP': JSON.stringify(buildTimestamp)
  }
} as UserConfig);
