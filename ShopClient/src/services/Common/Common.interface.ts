export const SortDirections = {
  Ascending: 'Asc',
  Descending: 'Desc'
} as const;

export type SortDirection = (typeof SortDirections)[keyof typeof SortDirections];

export interface PagedList<T> {
  items: T[];
  pagination: Pagination;
}

export interface Pagination {
  pageNumber: number;
  pageSize: number;
  totalItems: number;
  totalPages: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
}

export interface PaginationRequest {
  pageNumber: number;
  pageSize: number;
  sortDirection: SortDirection;
  sortField?: string;
}
