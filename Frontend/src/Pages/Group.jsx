import { useParams } from "react-router-dom";
const apiUrl = import.meta.env.VITE_API_URL;

function Group()
{


    const renderMessage = () => {
        const {id} = useParams();
        const groupId = parseInt(id, 10);
        if(Number.isInteger(groupId)) {
            fetchGroup(groupId);
            return <p>{id}</p>;
        } 
        else {
            return <p>Oops... An error has occured!</p>;
        }
    };
    return(
        <>
            {renderMessage()}
        </>
    )
}

async function fetchGroup(id)
{
    try{
        console.log(id)
        const response = await fetch(`${apiUrl}/api/Groups/${id}`);
        if(!response.ok) {
            throw new Error(`Failed to fetch group by id: ${id}`);
        }

        const data = await response.json();
        console.log(data);
    }
    catch(error){
        console.log(error);
    }
}

export default Group;