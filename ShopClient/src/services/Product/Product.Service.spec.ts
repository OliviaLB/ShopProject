import { describe, it, expect, vi, beforeEach, type MockedFunction } from 'vitest';
import { client } from '../Settings';
import type { GetProductFilters, ProductResponse } from './Product.interface';
import { getPaginatedProducts, getProduct } from './Product.service';
import type { PagedList, PaginationRequest } from '../Common/Common.interface';

vi.mock('../Settings', () => ({
  client: vi.fn()
}));

describe('getProduct', () => {
  const mockGet = vi.fn();

  beforeEach(() => {
    vi.clearAllMocks();

    (client as MockedFunction<typeof client>).mockReturnValue({
      get: mockGet
    } as any);
  });

  it('calls API with correct route and returns product', async () => {
    const productId = '850b40bd-134e-4762-ad76-6240554f04c7';

    const mockProduct: ProductResponse = {
      id: productId,
      name: 'Test Product'
    } as ProductResponse;

    mockGet.mockResolvedValueOnce({ data: mockProduct });

    const result = await getProduct(productId);

    expect(client).toHaveBeenCalledOnce();
    expect(mockGet).toHaveBeenCalledWith(`/products/${productId}`, { signal: undefined });
    expect(result).toEqual(mockProduct);
  });
});

describe('getPaginatedProducts', () => {
  const mockGet = vi.fn();

  beforeEach(() => {
    vi.clearAllMocks();

    (client as MockedFunction<typeof client>).mockReturnValue({
      get: mockGet
    } as any);
  });

  it('builds query string correctly and calls API', async () => {
    const paginationRequest: PaginationRequest = {
      pageNumber: 1,
      pageSize: 20,
      sortDirection: 'Desc',
      sortField: 'Price'
    } as PaginationRequest;

    const productFilters: GetProductFilters = {
      brands: ['Nike', 'Adidas'],
      ids: ['id-1', 'id-2'],
      types: ['Hats', 'Boards'],
      inStockOnly: true,
      searchTerm: 'beanie'
    } as GetProductFilters;

    const apiResponse: PagedList<ProductResponse> = {
      items: [{ id: 'p1', name: 'Test Product' } as ProductResponse],
      pageNumber: 1,
      pageSize: 20,
      totalCount: 1,
      totalPages: 1
    } as unknown as PagedList<ProductResponse>;

    mockGet.mockResolvedValueOnce({ data: apiResponse });

    const result = await getPaginatedProducts(paginationRequest, productFilters);

    expect(client).toHaveBeenCalledOnce();
    expect(mockGet).toHaveBeenCalledOnce();

    // Assert URL (including repeated keys for arrays)
    const [calledUrl, calledConfig] = mockGet.mock.calls[0];

    expect(calledUrl).toContain('/products?');
    expect(calledUrl).toContain('PageNumber=1');
    expect(calledUrl).toContain('PageSize=20');
    expect(calledUrl).toContain('SortDirection=Desc');
    expect(calledUrl).toContain('SortField=Price');

    // Arrays should be repeated keys (NOT Types[]=)
    expect(calledUrl).toContain('Brands=Nike');
    expect(calledUrl).toContain('Brands=Adidas');
    expect(calledUrl).toContain('Ids=id-1');
    expect(calledUrl).toContain('Ids=id-2');
    expect(calledUrl).toContain('Types=Hats');
    expect(calledUrl).toContain('Types=Boards');

    // Boolean + search
    expect(calledUrl).toContain('InStockOnly=true');
    expect(calledUrl).toContain('SearchTerm=beanie');

    // Config
    expect(calledConfig).toEqual({ signal: undefined });

    // Return value
    expect(result).toEqual(apiResponse);
  });

  it('omits optional filters when they are null/undefined', async () => {
    const paginationRequest: PaginationRequest = {
      pageNumber: 2,
      pageSize: 11,
      sortDirection: 'Asc',
      sortField: undefined
    } as PaginationRequest;

    const apiResponse: PagedList<ProductResponse> = {
      items: [],
      pageNumber: 2,
      pageSize: 11,
      totalCount: 0,
      totalPages: 0
    } as unknown as PagedList<ProductResponse>;

    mockGet.mockResolvedValueOnce({ data: apiResponse });

    await getPaginatedProducts(paginationRequest, null);

    const [calledUrl] = mockGet.mock.calls[0];

    expect(calledUrl).toContain('PageNumber=2');
    expect(calledUrl).toContain('PageSize=11');
    expect(calledUrl).toContain('SortDirection=Asc');

    // SortField should not appear
    expect(calledUrl).not.toContain('SortField=');

    // Filters should not appear
    expect(calledUrl).not.toContain('Brands=');
    expect(calledUrl).not.toContain('Ids=');
    expect(calledUrl).not.toContain('Types=');
    expect(calledUrl).not.toContain('InStockOnly=');
    expect(calledUrl).not.toContain('SearchTerm=');
  });
});
