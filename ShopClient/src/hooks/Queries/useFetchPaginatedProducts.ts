import { getPaginatedProducts, type GetProductFilters, type ProductResponse } from '../../services/Product';
import { useQuery } from '@tanstack/react-query';
import type { AxiosError } from 'axios';
import type { PagedList, PaginationRequest } from '../../services/Common/Common.interface';
import type { ProblemDetails } from '../../services/Common';

export const useFetchPaginatedProducts = (
  paginationRequest: PaginationRequest,
  productFilters: GetProductFilters | null
) => {
  const query = useQuery<PagedList<ProductResponse>, AxiosError<ProblemDetails>>({
    queryKey: ['paginatedProducts', paginationRequest, productFilters],

    queryFn: ({ signal }) => {
      if (!paginationRequest) {
        throw new Error('Pagination request is required.');
      }

      return getPaginatedProducts(paginationRequest, productFilters, signal);
    },

    retry: (failureCount, error) => {
      const status = error.response?.status;

      if (status && status >= 400 && status < 500) return false;

      // Retry up to 2 times for network errors / 5xx / unknown
      return failureCount < 2;
    },

    refetchOnWindowFocus: false,
    staleTime: 30_000
  });

  return {
    ...query,
    errorMessage: query.error?.response?.data?.detail
  };
};
