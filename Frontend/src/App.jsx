import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'

// (parameters) => some code

const apiUrl = "https://localhost:7204";
let data;
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
    const response = await fetch(`${apiUrl}/api/Groups`);
    
    if(!response.ok){
      throw new Error("Could not fetch groups");
    }
    
    data = await response.json();
    console.log(data);
    data.forEach(value => console.log(value));
  } 
  catch(error){
    console.log(error)
  }
}

export default App
