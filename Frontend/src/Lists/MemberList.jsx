import { useNavigate } from "react-router-dom";

const apiUrl = import.meta.env.VITE_API_URL;

const MemberList = ({ members, setRefresh }) => {
  const navigate = useNavigate();
  const goToTransaction = (groupId, memberId) => {
    navigate(`/Transaction/${groupId}/${memberId}`);
  };

  if (members !== null && members.length > 0) {
    const list = members.map((mem) => (
      <tr key={mem.id}>
        <td>{mem.name}</td>
        <td>{mem.surname}</td>
        <td>{mem.debt.toFixed(2)}</td>
        <td>{fetchSettleButton(mem, setRefresh, goToTransaction)}</td>
      </tr>
    ));

    return (
      <table>
        <thead>
          <tr>
            <th>Name</th>
            <th>Surname</th>
            <th>Debt</th>
          </tr>
        </thead>
        <tbody>{list}</tbody>
      </table>
    );
  }

  return <h2>Group is empty</h2>;
};

function fetchSettleButton(member, setRefresh, goToTransaction) {
  if (member.debt === null || member.debt != 0) {
    return (
      <button onClick={() => settleDebt(member, goToTransaction)}>
        Settle
      </button>
    );
  }

  return (
    <button
      onClick={() => {
        removeMember(member.id);
        setRefresh((x) => ++x);
      }}
    >
      Remove
    </button>
  );
}

async function settleDebt(member, goToTransaction) {
  console.log(member);
  goToTransaction(member.id, member.groupId);
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
