import { defineConfig } from "vite";
import react from "@vitejs/plugin-react-swc";

const buildTimestamp = new Date().getTime();

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    port: 3000,
  },

  build: {
    rollupOptions: {
      output: {
        entryFileNames: `assets/[name].[hash].${buildTimestamp}.js`,
        chunkFileNames: `assets/[name].[hash].${buildTimestamp}.js`,
        assetFileNames: `assets/[name].[hash].${buildTimestamp}.[ext]`,
      },
    },
  },

  define: {
    "process.env.BUILD_TIMESTAMP": JSON.stringify(buildTimestamp),
  },
});
