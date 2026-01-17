export interface Product {
  name: string;
  description: string;
  price: number;
  pictureUri: string;
  type: string;
  brand: string;
  quantityInStock: number;
}

export interface ProductResponse extends Product {
  id: string;
  changeTimestamp: string;
  dateAdded: string;
}

export interface CreateProductRequest extends Product {}

export interface UpdateProductRequest extends Product {
  id: string;
}

export interface GetProductFilters {
  ids?: string[];
  brands?: string[];
  types?: string[];
  inStockOnly?: boolean;
  searchTerm?: string;
}
