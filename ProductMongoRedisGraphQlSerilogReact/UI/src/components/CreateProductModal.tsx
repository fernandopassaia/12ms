import { useState } from "react";

import {
  useMutation
} from "@apollo/client/react";

import { CREATE_PRODUCT }
  from "../graphql/mutations";

import { GET_PRODUCTS }
  from "../graphql/queries";

import "./CreateProductModal.css";

interface Props {
  isOpen: boolean;
  onClose: () => void;
}

export function CreateProductModal({
  isOpen,
  onClose
}: Props) {

  const [descricao, setDescricao]
    = useState("");

  const [valor, setValor]
    = useState(0);

  const [estoque, setEstoque]
    = useState(0);

  const [createProduct] =
    useMutation(CREATE_PRODUCT, {

      refetchQueries: [
        { query: GET_PRODUCTS }
      ]
    });

  async function handleSubmit() {

    await createProduct({
      variables: {
        descricao,
        valor,
        estoque
      }
    });

    onClose();
  }

  if (!isOpen)
    return null;

  return (
    <div className="modal-overlay">

      <div className="modal">

        <h2>Add Product</h2>

        <input
          placeholder="Description"
          value={descricao}
          onChange={(e) =>
            setDescricao(e.target.value)}
        />

        <input
          type="number"
          placeholder="Value"
          value={valor}
          onChange={(e) =>
            setValor(Number(e.target.value))}
        />

        <input
          type="number"
          placeholder="Stock"
          value={estoque}
          onChange={(e) =>
            setEstoque(Number(e.target.value))}
        />

        <div className="modal-buttons">

          <button
            onClick={handleSubmit}
            className="save-button"
          >
            Save
          </button>

          <button
            onClick={onClose}
            className="cancel-button"
          >
            Cancel
          </button>

        </div>

      </div>

    </div>
  );
}