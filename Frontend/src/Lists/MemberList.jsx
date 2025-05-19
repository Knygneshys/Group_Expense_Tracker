function MemberList({ members }) {
  console.log(members);
  if (members !== null && members.length > 0) {
    const list = members.map((mem) => (
      <tr key={mem.id}>
        <td>{mem.name}</td>
        <td>{mem.surname}</td>
        <td>{mem.debt.toFixed(2)}</td>
        <td>{fetchSettleButton(mem)}</td>
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
}

function fetchSettleButton(member) {
  if (member.debt === null || member.debt != 0) {
    return <button onClick={() => settleDebt(member.id)}>Settle</button>;
  }

  return <button onClick={() => removeMember(member.id)}>Remove</button>;
}

async function settleDebt(id) {
  console.log(id);
}

async function removeMember(id) {
  console.log(`Remove ${id}`);
}

export default MemberList;
