const apiUrl = import.meta.env.VITE_API_URL;

const NewGroupAddition = ({ refreshDad }) => {
  async function createNewGroup(formData) {
    const groupName = formData.get("groupName");
    const group = {
      name: groupName,
    };

    try {
      const response = await fetch(`${apiUrl}/api/Groups`, {
        method: `POST`,
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(group),
      });
      if (!response.ok) {
        throw new Error("Failed to add new group");
      }
      console.log(`Added new group: \"${groupName}\" succesfully!`);
      alert("Sucessfully added new group!");
      refreshDad((x) => x + 1);
    } catch (error) {
      console.log(error);
      alert("Failed to add new group...");
    }
  }

  return (
    <form action={createNewGroup}>
      <label className="form-label" style={{ fontSize: "13pt" }}>
        New group's name:
        <input
          name="groupName"
          required
          type="text"
          placeholder="Group name"
          className="form-control"
        />
      </label>
      <button
        type="submit"
        className="btn btn-primary"
        style={{ margin: "10px" }}
      >
        Create new group
      </button>
    </form>
  );
};

export default NewGroupAddition;
