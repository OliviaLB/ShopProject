import { createFileRoute } from '@tanstack/react-router';
import { About } from '../../Pages';

export const Route = createFileRoute('/About/')({
  component: About
});
