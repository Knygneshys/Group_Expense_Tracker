const apiUrl = "https://localhost:7204";
let data;
fetchGroups();
// fetchGroupDebts();

function GroupList(){
    const list = data.map(group => <tr key={group.name}>
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

async function fetchGroupDebts(id)
{
    try{
        const response = await fetch(`${apiUrl}/api/Groups/GroupDebt/${id}`);
        if(!response.ok)
        {
          throw new Error(`Failed to receive group nr: ${id} debts`);
        }

        debt = await response.json();
        console.log(debt);

    }
    catch(error){
      console.log(error);
    }
}

export default GroupList