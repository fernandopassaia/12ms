import "./App.css";

import { ProductList }
  from "./components/ProductList";

function App() {

  return (
    <div className="container">

      <div className="header">
        <h1 className="title">
          Products
        </h1>

        <button className="add-button">
          Add Product
        </button>
      </div>

      <ProductList />

    </div>
  )
}

export default App