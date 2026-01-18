import { createFileRoute } from '@tanstack/react-router';
import About from './About';

export const Route = createFileRoute('/About/')({
  component: About
});
