import { useParams } from "react-router-dom";


function Group()
{
    const renderMessage = () => {
        const {id} = useParams();
        const string = JSON.stringify(id);
        if(string != "{}") {
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

export default Group;