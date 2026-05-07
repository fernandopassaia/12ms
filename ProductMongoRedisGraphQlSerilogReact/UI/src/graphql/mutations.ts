import { gql } from "@apollo/client";

export const CREATE_PRODUCT = gql`
  mutation CreateProduct(
    $descricao: String!,
    $valor: Decimal!,
    $estoque: Int!
  ) {
    createProduct(input: {
      descricao: $descricao,
      valor: $valor,
      estoque: $estoque
    })
  }
`;