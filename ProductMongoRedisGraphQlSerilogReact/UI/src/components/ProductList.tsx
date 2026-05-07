import {
  useQuery
} from "@apollo/client/react";
import "./ProductList.css";

import { GET_PRODUCTS }
  from "../graphql/queries";

import type { Product }
  from "../types/Product";

interface ProductsQuery {
  products: Product[];
}

export function ProductList() {

  const {
    loading,
    error,
    data
  } = useQuery<ProductsQuery>(
    GET_PRODUCTS
  );

  if (loading)
    return <p>Loading...</p>;

  if (error)
    return <p>Error...</p>;

  return (
    <table className="product-table">
      <thead>
        <tr>
          <th>Description</th>
          <th>Value</th>
          <th>Stock</th>
        </tr>
      </thead>

      <tbody>
        {data?.products.map(
          (product: Product) => (
            <tr key={product.id}>
              <td>{product.descricao}</td>
              <td>{product.valor}</td>
              <td>{product.estoque}</td>
            </tr>
        ))}
      </tbody>
    </table>
  );
}