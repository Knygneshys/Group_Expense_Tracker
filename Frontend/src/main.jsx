import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import Groups from './Pages/Groups.jsx'

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <Groups />
  </StrictMode>,
)
