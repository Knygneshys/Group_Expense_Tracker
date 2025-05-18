import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import AllGroups from './Pages/AllGroups.jsx'
import Group from './Pages/Group.jsx'
import {BrowserRouter, Routes, Route} from "react-router-dom"

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <BrowserRouter>
      <Routes>
        <Route index element={<AllGroups/>} />
        <Route path="/Groups" element={<AllGroups/>} />
        <Route path="/Group/:id" element={<Group/>} />
      </Routes>
    </BrowserRouter>
  </StrictMode>,
)