import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import Stocks from './Stocks.tsx'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <Stocks />
  </StrictMode>,
)
