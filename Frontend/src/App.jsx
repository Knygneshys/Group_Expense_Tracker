import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import GroupList from './Lists/GroupList.jsx'
// (parameters) => some code



function App() {
  document.title = `Group Finance Tracker`;
  return (
    <>
      <h1>Groups</h1>
      <GroupList/>
    </>
  )
}


export default App
