


function NewGroupAddition()
{
    function createNewGroup(formData)
    {
        const name = formData.get("groupName");
        console.log(name);
    }

    return(
    <form action={createNewGroup}>
        <label>
            New group's name: <input name="groupName" type="text" placeholder="Group name"/>
        </label>
        <button type="submit">Create new group</button>
    </form>)
}



export default NewGroupAddition