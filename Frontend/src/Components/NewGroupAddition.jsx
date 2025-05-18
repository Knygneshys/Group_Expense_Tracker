
const apiUrl = "https://localhost:7204";

function NewGroupAddition()
{
    async function createNewGroup(formData)
    {
        const groupName = formData.get("groupName");
        const group = {
            name: groupName
        }

        try{
            const response = await fetch('https://localhost:7204/api/Groups',{
                method: `POST`,
                headers: {"Content-Type": "application/json"},
                body: JSON.stringify(group)
            });
            if(!response.ok){
                throw new Error("Failed to add new group");
            }
            console.log(`Added new group: \"${groupName}\" succesfully!`);
        }
        catch(error) {
            console.log(error);
        }
    }

    return(
    <form action={createNewGroup}>
        <label>
            New group's name: <input name="groupName" required type="text" placeholder="Group name"/>
        </label>
        <button type="submit">Create new group</button>
    </form>)
}



export default NewGroupAddition