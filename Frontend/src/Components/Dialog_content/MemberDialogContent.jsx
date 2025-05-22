import { forwardRef } from "react";

const apiUrl = import.meta.env.VITE_API_URL;
let closeDialog;
let groupId;
let refreshDad;

const MemberDialogContent = forwardRef(
  ({ toggleDialog, gId, setRefresh }, ref) => {
    closeDialog = toggleDialog;
    groupId = gId;
    refreshDad = setRefresh;
    return (
      <>
        <div className="mb-3">
          <h3>Create a new member</h3>
        </div>
        <form action={addMember}>
          <div className="mb-3">
            <label className="form-label">Name:</label>
            <input
              className="form-control"
              name="memberName"
              required
              type="text"
              placeholder="New member's name"
            />
          </div>

          <div className="mb-3">
            <label className="form-label">Surname:</label>
            <input
              className="form-control"
              name="memberSurname"
              required
              type="text"
              placeholder="New member's surname"
            />
          </div>

          <div className="mb-3">
            <label className="form-label">Debt:</label>
            <input
              className="form-control"
              name="memberDebt"
              step={0.01}
              required
              type="number"
              placeholder="New member's debt"
            />
          </div>

          <div className="mb-3">
            <button type="submit" className="btn btn-primary me-2">
              Add
            </button>
            <button
              type="button"
              className="btn btn-secondary"
              onClick={toggleDialog}
            >
              Close
            </button>
          </div>
        </form>
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

  try {
    const response = await fetch(`${apiUrl}/api/Members`, {
      method: `POST`,
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(member),
    });
    if (!response.ok) {
      throw new Error("Failed to POST new member.");
    }
    console.log(`Added new member "${memberName} ${memberSurname}"`);
    alert(`Successfully added new member "${memberName} ${memberSurname}"!`);
  } catch (error) {
    console.log(error);
    alert("Failed to add member...");
  }

  refreshDad((x) => ++x);
  closeDialog();
}

export default MemberDialogContent;
