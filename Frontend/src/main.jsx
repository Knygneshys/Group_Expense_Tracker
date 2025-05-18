import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import AllGroups from './Pages/AllGroups.jsx'

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <AllGroups />
  </StrictMode>,
)
