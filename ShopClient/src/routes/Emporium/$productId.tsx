import { createFileRoute } from '@tanstack/react-router';
import { ProductDetail } from '../../Pages';

export const Route = createFileRoute('/Emporium/$productId')({
  component: ProductDetail
});
