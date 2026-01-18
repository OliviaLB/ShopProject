import { createFileRoute } from '@tanstack/react-router';
import { Home } from '../Pages';

export const Route = createFileRoute('/')({
  component: Home
});
