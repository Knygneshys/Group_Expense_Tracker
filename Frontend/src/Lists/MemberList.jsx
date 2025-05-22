const apiUrl = import.meta.env.VITE_API_URL;
const EURO = import.meta.env.VITE_EURO;

const MemberList = ({ members, setRefresh, goToTransaction }) => {
  if (members !== null && members.length > 0) {
    const list = members.map((mem) => (
      <tr key={mem.id}>
        <td>{mem.name}</td>
        <td>{mem.surname}</td>
        <td>
          {mem.debt.toFixed(2)}
          {EURO}
        </td>
        <td>{fetchSettleButton(mem, setRefresh, goToTransaction)}</td>
      </tr>
    ));

    return (
      <table className="table table-striped table-hover table-bordered">
        <thead>
          <tr>
            <th>Name</th>
            <th>Surname</th>
            <th>Debt</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>{list}</tbody>
      </table>
    );
  }

  return <h2 style={{ padding: "50px" }}>Group is empty</h2>;
};

function fetchSettleButton(member, setRefresh, goToTransaction) {
  if (member.debt === null || member.debt != 0) {
    return (
      <button
        onClick={() => settleDebt(member, goToTransaction)}
        className="btn btn-light"
      >
        Settle
      </button>
    );
  }

  return (
    <button
      className="btn btn-warning"
      onClick={async () => {
        await removeMember(member.id);
        setRefresh((x) => x + 1);
      }}
    >
      Remove
    </button>
  );
}

async function settleDebt(member, goToTransaction) {
  goToTransaction(member.groupId, member.id);
}

async function removeMember(id) {
  try {
    const response = await fetch(`${apiUrl}/api/Members/${id}`, {
      method: `DELETE`,
      body: { id },
    });

    if (!response.ok) {
      throw new Error(`Failed to delete member of id: ${id}`);
    }
    console.log(`Succesfully deleted member of id: ${id}`);
  } catch (error) {
    console.log(error);
  }
}

export default MemberList;
