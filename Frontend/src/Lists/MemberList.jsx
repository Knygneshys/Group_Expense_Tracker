function MemberList({ members }) {
  console.log(members);
  if (members !== null) {
    const list = members.map((mem) => (
      <tr key={mem.id}>
        <td>{mem.name}</td>
        <td>{mem.surname}</td>
        <td>{mem.debt}</td>
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

  return <h2>Loading...</h2>;
}

export default MemberList;
