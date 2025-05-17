const apiUrl = "https://localhost:7204";
let data;
fetchGroups();

function GroupList(){
    const list = data.map(group => <tr>
                                    <td>{group.name}</td>
                                    <td>0.00</td>
                                    </tr>);
    
    return(list);
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

export default GroupList