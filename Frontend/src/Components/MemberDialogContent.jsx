function MemberDialogContent() {
  return (
    <>
      <div>
        <h3>Create a new member</h3>
      </div>
      <div>
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
      </div>
    </>
  );
}

export default MemberDialogContent;
