import { useState } from "react";

import "./App.css";

import { ProductList }
  from "./components/ProductList";

import {
  CreateProductModal
} from "./components/CreateProductModal";

function App() {

  const [isModalOpen,
    setIsModalOpen]
      = useState(false);

  return (
    <div className="container">

      <div className="header">

        <h1 className="title">
          Products
        </h1>

        <button
          className="add-button"

          onClick={() =>
            setIsModalOpen(true)}
        >
          Add Product
        </button>

      </div>

      <ProductList />

      <CreateProductModal
        isOpen={isModalOpen}

        onClose={() =>
          setIsModalOpen(false)}
      />

    </div>
  )
}

export default App