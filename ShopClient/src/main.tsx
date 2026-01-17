import './index.css';
import { createRoot } from 'react-dom/client';
import { queryClient } from './services/Settings/index.ts';
import { QueryClientProvider } from '@tanstack/react-query';
import { StrictMode } from 'react';
import App from './App.tsx';

createRoot(document.getElementById('root')!).render(
  // <StrictMode>
  <QueryClientProvider client={queryClient}>
    <App />
  </QueryClientProvider>
  // </StrictMode>
);
