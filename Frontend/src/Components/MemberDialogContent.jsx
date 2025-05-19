import { forwardRef } from "react";

const apiUrl = import.meta.env.VITE_API_URL;
let closeDialog;
let groupId;
let setMem;

const MemberDialogContent = forwardRef(
  ({ toggleDialog, gId, setMembers }, ref) => {
    closeDialog = toggleDialog;
    groupId = gId;
    setMem = setMembers;
    return (
      <>
        <div>
          <h3>Create a new member</h3>
        </div>
        <form action={addMember}>
          <label>Name: </label>
          <input
            name="memberName"
            required
            type="text"
            placeholder="New member's name"
          />
          <br />
          <label>Surname: </label>
          <input
            name="memberSurname"
            required
            type="text"
            placeholder="New member's surname"
          />
          <br />
          <label>Debt: </label>
          <input
            name="memberDebt"
            required
            type="number"
            placeholder="New member's debt"
          />
          <br />
          <button type="submit">Add</button>
        </form>
        <button onClick={toggleDialog}>Close</button>
      </>
    );
  }
);

async function addMember(formData) {
  const memberName = formData.get("memberName");
  const memberSurname = formData.get("memberSurname");
  const memberDebt = parseInt(formData.get("memberDebt"));
  const member = {
    name: memberName,
    surname: memberSurname,
    debt: memberDebt,
    groupId: groupId,
  };
  console.log(member);

  try {
    const response = await fetch(`${apiUrl}/api/Members`, {
      method: `POST`,
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(member),
    });
    if (!response.ok) {
      throw new Error("Failed to POST new member.");
    }
    console.log(`Added new member \"${memberName} ${memberSurname}\"`);
  } catch (error) {
    console.log(error);
  }

  setMem((m) => [...m, member]);
  closeDialog();
}

export default MemberDialogContent;
