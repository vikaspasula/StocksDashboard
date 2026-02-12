import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import StocksGrid from "./components/stocksgrid";

createRoot(document.getElementById('root')!).render(
  <StrictMode>
        <StocksGrid />
  </StrictMode>,
)
