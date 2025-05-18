import React, {useState, useEffect} from "react";
import { useNavigate } from "react-router-dom";
import NewGroupAddition from "../Components/NewGroupAddition";

const apiUrl = "https://localhost:7204";

function GroupList(){
    const [groups, setGroups] = useState("empty");
    const [groupDebts, setGroupDebts] = useState("empty")
    useEffect(()=>{
      async function getData() {
        const data = await fetchGroups();
        setGroups(data);

        if(data && Array.isArray(data))
        {
          const debtArray = {};
          for(let i = 0; i < data.length; i++)
          {
            const groupId = data[i].id;
            const debt = await fetchGroupDebts(groupId);
            debtArray[groupId] = debt;
            console.log(debt);
          }
          setGroupDebts(debtArray);
        }
      }
      getData();
    },[]);

    const navigate = useNavigate();

    const goToGroup = (groupId) =>{
      navigate(`/Group/${groupId}`)
    }

    if(groups != "empty" && groupDebts != "empty")
    {
      const list = groups.map(group => <tr key={group.name}>
                                    <td>{group.name}</td>
                                    <td>{groupDebts[group.id].toFixed(2)}</td>
                                    <td><button onClick={()=>goToGroup(group.id)}>Open</button></td>
                                    </tr>);
    return( 
      <div>
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
        <NewGroupAddition />
      </div>   
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
        return debt;
    }
    catch(error){
      console.log(error);
    }
}

export default GroupList