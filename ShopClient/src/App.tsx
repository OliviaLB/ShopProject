import { useMemo } from 'react';
import { useFetchPaginatedProducts } from './hooks/Queries';
import { SortDirections, type PaginationRequest } from './services/Common/Common.interface';
import type { GetProductFilters } from './services/Product';

const App = () => {
  const pagination = useMemo<PaginationRequest>(
    () => ({
      sortDirection: SortDirections.Descending,
      pageNumber: 1,
      pageSize: 20,
      sortField: 'Price'
    }),
    []
  );

  const filters = useMemo<GetProductFilters>(
    () => ({
      types: ['Hats', 'Boots']
    }),
    []
  );

  const { data, isLoading } = useFetchPaginatedProducts(pagination, filters);

  console.log(data);

  return (
    <div>
      {!isLoading && (
        <ul>
          {data?.items.map((x) => (
            <li key={x.id}>
              {x.name} - {x.price}
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};

export default App;
