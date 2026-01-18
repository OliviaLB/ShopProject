import { useParams } from '@tanstack/react-router';

const ProductDetail = () => {
  const productId = useParams({
    from: '/Emporium/$productId',
    select: (params) => params.productId
  });

  return <div>ProductId ID: {productId}</div>;
};

export default ProductDetail;
