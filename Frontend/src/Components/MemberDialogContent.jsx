import { forwardRef } from "react";

let closeDialog;

const MemberDialogContent = forwardRef(({ toggleDialog }, ref) => {
  closeDialog = toggleDialog;
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
});

async function addMember(formData) {
  const memberName = formData.get("memberName");
  const memberSurname = formData.get("memberSurname");
  const memberDebt = formData.get("memberDebt");
  console.log(`${memberName} ${memberSurname} ${memberDebt}`);
  closeDialog();
}

export default MemberDialogContent;
