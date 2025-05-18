import React, {useState, useEffect} from "react";

const apiUrl = "https://localhost:7204";

// fetchGroupDebts(1);

function GroupList(){
    const [groups, setGroups] = useState("empty");

    useEffect(()=>{
      async function getData() {
        const data = await fetchGroups();
        setGroups(data);
      }
      getData();
    },[]);
    if(groups != "empty")
    {
      const list = groups.map(group => <tr key={group.name}>
                                    <td>{group.name}</td>
                                    <td>0.00</td>
                                    <td><button>Open</button></td>
                                    </tr>);
    return(   
      <table>
        <thead>
          <tr>
            <th>Name</th>
            <th>Debt</th>
          </tr>  
        </thead>
        <tbody>
          {list}
        </tbody>
      </table>   
      );
    }

    return (<div>Loading...</div>);

}

async function fetchGroups()
{
  try{
    const response = await fetch(`${apiUrl}/api/Groups`);
    
    if(!response.ok){
      throw new Error("Could not fetch groups");
    }
    
    const data = await response.json();
    // setGroups(data);
    console.log(data);
    data.forEach(value => console.log(value));

    return data;
  } 
  catch(error){
    console.log(error)
  }
}

async function fetchGroupDebts(id)
{
    try{
        const response = await fetch(`${apiUrl}/api/Groups/GroupDebt/${id}`);
        if(!response.ok)
        {
          throw new Error(`Failed to receive group nr: ${id} debts`);
        }

        const debt = await response.json();
        console.log(debt);

    }
    catch(error){
      console.log(error);
    }
}

export default GroupList