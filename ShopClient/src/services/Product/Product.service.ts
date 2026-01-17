import type { GetProductFilters, ProductResponse } from './Product.interface';
import { client } from '../Settings';
import { type PagedList, type PaginationRequest } from '../Common/Common.interface';
import { buildQueryParams } from '../Helpers';

const route = '/products';

export const getProduct = async (id: string, signal?: AbortSignal): Promise<ProductResponse> => {
  const api = client();
  const response = await api.get<ProductResponse>(`${route}/${id}`, { signal });
  return response.data;
};

export const getPaginatedProducts = async (
  paginationRequest: PaginationRequest,
  productFilters: GetProductFilters | null,
  signal?: AbortSignal
): Promise<PagedList<ProductResponse>> => {
  const api = client();

  const query = buildQueryParams({
    PageNumber: paginationRequest.pageNumber,
    PageSize: paginationRequest.pageSize,
    SortDirection: paginationRequest.sortDirection,
    SortField: paginationRequest.sortField,
    Brands: productFilters?.brands,
    Ids: productFilters?.ids,
    Types: productFilters?.types,
    InStockOnly: productFilters?.inStockOnly,
    SearchTerm: productFilters?.searchTerm
  });

  const response = await api.get<PagedList<ProductResponse>>(`${route}?${query.toString()}`, {
    signal
  });

  return response.data;
};
