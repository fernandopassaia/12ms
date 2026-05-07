import React from 'react'
import ReactDOM from 'react-dom/client'

import {
  ApolloProvider
} from "@apollo/client/react";

import App from './App.tsx'

import { apolloClient }
  from './services/apollo.ts'

ReactDOM.createRoot(
  document.getElementById('root')!
).render(
  <React.StrictMode>
    <ApolloProvider client={apolloClient}>
      <App />
    </ApolloProvider>
  </React.StrictMode>,
)