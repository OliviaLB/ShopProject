import { createFileRoute } from '@tanstack/react-router';
import ProductDetail from './Emporium.$productId';

export const Route = createFileRoute('/Emporium/$productId/')({
  component: ProductDetail
});
