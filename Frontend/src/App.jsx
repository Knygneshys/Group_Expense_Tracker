import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'

// (parameters) => some code

fetchGroups();

function App() {

  return (
    <>

    </>
  )
}

async function fetchGroups()
{
  try{
    const response = await fetch("https://localhost:7204/api/Groups");
    
    if(!response.ok){
      throw new Error("Could not fetch groups");
    }
    
    const data = await response.json();
    console.log(data);
  } 
  catch(error){
    console.log(error)
  }
}

export default App
