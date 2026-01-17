import { useQuery } from '@tanstack/react-query';
import { AxiosError } from 'axios';
import { getProduct, type ProductResponse } from '../../services/Product';
import type { ProblemDetails } from '../../services/Common';

export const useFetchProductById = (productId?: string) => {
  const query = useQuery<ProductResponse, AxiosError<ProblemDetails>>({
    queryKey: ['productById', productId],
    enabled: Boolean(productId),

    queryFn: ({ signal }) => {
      if (!productId) throw new Error('Product ID is required.');
      return getProduct(productId, signal);
    },

    retry: (failureCount, error) => {
      const status = error.response?.status;

      if (status === 404) {
        return false;
      }

      if (status && status >= 400 && status < 500) {
        return false;
      }

      return failureCount < 2;
    },

    refetchOnWindowFocus: false,
    staleTime: 30_000
  });

  return {
    ...query,
    isNotFound: query.error?.response?.status === 404,
    errorMessage: query.error?.response?.data?.detail
  };
};
