import { useParams } from "react-router-dom";
import React, {useState, useEffect} from "react";
const apiUrl = import.meta.env.VITE_API_URL;

function Group()
{
    const {id} = useParams();
    const [groupId, setGroupId] = useState();
    const [members, setMembers] = useState([]);

    useEffect(()=> {
        const parsedId = parseInt(id, 10);
        setGroupId(parsedId);
    },[])

    useEffect( () => {
        async function getData() {
            if(Number.isInteger(groupId)) {
                const group = await fetchGroup(groupId);

                if(JSON.stringify(members) !== JSON.stringify(group.members))
                {
                    setMembers(group.members);
                }
                
                console.log(members);

                return <p>{id}</p>;
            } 
            else {
                return <p>Oops... An error has occured!</p>;
            }
        }
        getData();
    },[groupId, members]);


    return(
        <>
           <p>Hello, group nr. {groupId}</p>
        </>
    )
}

async function fetchGroup(id)
{
    try{
        const response = await fetch(`${apiUrl}/api/Groups/${id}`);
        if(!response.ok) {
            throw new Error(`Failed to fetch group by id: ${id}`);
        }

        const data = await response.json();
        console.log(data);
        return data;
    }
    catch(error){
        console.log(error);
    }
}

export default Group;